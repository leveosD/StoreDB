using Npgsql;
using NpgsqlTypes;
using System.Data;
using OfficeOpenXml;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    string pattern = $"Server=localhost;User Id=postgres;Password=123454321;Database=shop";

    List<bool> saveStates;
    List<DataSet> dataSets;
    //List<NpgsqlDataAdapter> adapters;
    NpgsqlConnection longConnection;
    NpgsqlDataAdapter adapter;

    string downloadFunction;

    public Form1()
    {
        InitializeComponent();

        ExcelPackage.License.SetNonCommercialPersonal("Logunkov");

        tablesList.SelectedIndexChanged += OnTableListItemClick;
        summaryList.SelectedIndexChanged += OnSummaryListItemClick;

        saveButton.Click += OnSaveButtonClick;
        downloadButton.Click += OnDownloadButtonCLick;

        table.CellValueChanged += OnTableChanged;
        table.UserDeletedRow += OnTableChanged;
        table.CellEnter += table_CellEnter;

        saveStates = new List<bool>();
        dataSets = new List<DataSet>();
        //adapters = new List<NpgsqlDataAdapter>();
        FillListbox();
    }

    ~Form1()
    {
        foreach (DataSet ds in dataSets)
            ds.Dispose();
        /*foreach (NpgsqlDataAdapter a in adapters)
            a.Dispose();*/
        adapter.Dispose();
        longConnection.Dispose();
    }

    private void FillListbox()
    {
        // Соединение с выбранной базой данных
        using (var connection = new NpgsqlConnection(pattern))
        {
            connection.Open();
            string query = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';";

            using (var cmd = new NpgsqlCommand(query, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tableName = reader.GetString(0);
                        tablesList.Items.Add(tableName);
                        saveStates.Add(false);
                        dataSets.Add(null);
                        //adapters.Add(null);
                    }
                }
            }
        }
    }

    private void OnTableListItemClick(object sender, EventArgs e)
    {
        string name = tablesList.SelectedItem.ToString();
        int index = tablesList.SelectedIndex;
        if (saveStates[index])
            saveButton.BackColor = Color.LightBlue;
        else
            saveButton.BackColor = Color.White;

        longConnection = new NpgsqlConnection(pattern);
        {
            longConnection.Open();

            if (adapter != null)
                adapter.Dispose();
            adapter = new NpgsqlDataAdapter($"SELECT * FROM {name};", longConnection);

            if (dataSets[index] == null)
            {
                dataSets[index] = new DataSet();
                adapter.Fill(dataSets[index], name);
            }
            var dataSet = dataSets[index];

            string commandUpdate = $"UPDATE {name} SET ";

            var columns = dataSet.Tables[name].Columns;
            for (int i = 1; i < columns.Count; i++)
            {
                commandUpdate += columns[i].ColumnName + " = @" + columns[i].ColumnName + ", ";
            }
            commandUpdate = commandUpdate.Remove(commandUpdate.Length - 2) + $" WHERE {columns[0].ColumnName} = @{columns[0].ColumnName};";

            adapter.UpdateCommand = new NpgsqlCommand(commandUpdate, longConnection);
            foreach (DataColumn column in dataSet.Tables[name].Columns)
            {
                var columnType = ConvertToNpgsqlType(column.DataType);
                int len = 0;
                if (columnType == NpgsqlDbType.Text)
                    len = 50;
                adapter.UpdateCommand.Parameters.Add("@" + column.ColumnName, columnType, 50, column.ColumnName);
            }

            string commandInsert = $"INSERT INTO {name} (";

            for (int i = 0; i < columns.Count; i++)
            {
                commandInsert += columns[i].ColumnName + ", ";
            }
            commandInsert = commandInsert.Remove(commandInsert.Length - 2) + ") VALUES (";

            for (int i = 0; i < columns.Count; i++)
            {
                commandInsert += "@" + columns[i].ColumnName + ", ";
            }

            commandInsert = commandInsert.Remove(commandInsert.Length - 2) + $");";

            adapter.InsertCommand = new NpgsqlCommand(commandInsert, longConnection);
            foreach (DataColumn column in dataSet.Tables[name].Columns)
            {
                var columnType = ConvertToNpgsqlType(column.DataType);
                int len = 0;
                if (columnType == NpgsqlDbType.Text)
                    len = 50;
                adapter.InsertCommand.Parameters.Add("@" + column.ColumnName, columnType, 50, column.ColumnName);
            }

            adapter.DeleteCommand = new NpgsqlCommand($"DELETE FROM {name} WHERE id = @id", longConnection);
            adapter.DeleteCommand.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer, 0, "id");

            table.DataSource = dataSet.Tables[name];
        }
    }

    private void OnSummaryListItemClick(object sender, EventArgs e)
    {
        comboBox.Items.Clear();
        int index = summaryList.SelectedIndex;
        if (index == 0)
        {
            commonPanel.Visible = true;
            customPanel.Visible = false;
        }
        else
        {
            commonPanel.Visible = false;
            customPanel.Visible = true;
        }

        string tableName;

        using (var connection = new NpgsqlConnection(pattern))
        {
            DataSet dataSet = new DataSet();
            switch (index)
            {
                case 0:
                    downloadFunction = "SELECT * FROM get_sales_by_period(";
                    break;
                case 1:
                    filterLabel.Text = "Department:";
                    downloadFunction = "SELECT * FROM get_sales_by_department(";
                    FillComboBox("departments");
                    panel2.Enabled = true;
                    break;

                case 2:
                    filterLabel.Text = "Goods:";
                    downloadFunction = "SELECT * FROM get_sales_by_product(";
                    FillComboBox("goods");
                    panel2.Enabled = true;
                    break;

                case 3:
                    filterLabel.Text = "Employee:";
                    downloadFunction = "SELECT * FROM get_sales_by_seller(";
                    FillComboBox("employees");
                    panel2.Enabled = true;
                    break;

                case 4:
                    downloadFunction = "SELECT * FROM get_current_product_quantity(";
                    filterLabel.Text = "Goods:";
                    FillComboBox("goods");
                    panel2.Enabled = false;
                    break;
            }
        }
    }

    private void FillComboBox(string table)
    {
        string name = "name";
        if (table == "employees")
            name = "last_name";
        using (var connection = new NpgsqlConnection(pattern))
        {
            connection.Open();
            using (var command = new NpgsqlCommand($"SELECT {name} FROM {table};", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox.Items.Add(reader.GetString(0));
                    }
                }
            }
        }
    }

    private void OnSaveButtonClick(object sender, EventArgs e)
    {
        var selectedTable = tablesList.SelectedItem;
        int index = tablesList.SelectedIndex;
        if (selectedTable != null)
        {
            try
            {
                adapter.Update(dataSets[index], selectedTable.ToString());
                saveButton.BackColor = Color.LightGreen;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message);
                saveButton.BackColor = Color.LightPink;
            }
            saveStates[tablesList.SelectedIndex] = false;
        }
    }

    private void OnDownloadButtonCLick(object sender, EventArgs e)
    {
        //bool state = timeChecker.Checked;

        using (var connection = new NpgsqlConnection(pattern))
        {
            connection.Open();
            string query = downloadFunction;
            if (commonPanel.Visible)
            {
                if (radioButton1.Checked)
                    query += "TRUE, \'" + DateTime.Today.ToString("yyyy-MM-dd") + "\');";
                else if (radioButton2.Checked)
                    query += "FALSE, \'" + DateTime.Today.ToString("yyyy-MM-dd") + "\');";
                else
                {
                    MessageBox.Show("Choose period.");
                    return;
                }
            }
            else if (!panel2.Enabled)
            {
                query += (comboBox.SelectedIndex + 1).ToString() + ")";
            }
            else
                query += (comboBox.SelectedIndex + 1).ToString() + ", \'" +
                startTimePicker.Value.ToString("yyyy-MM-dd") + "\', \'" + endTimePicker.Value.ToString("yyyy-MM-dd") + "\');";
            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            workSheet.Cells[1, i + 1].Value = reader.GetName(i);
                        }

                        int row = 2;
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                workSheet.Cells[row, i + 1].Value = reader[i];
                            }
                            row++;
                        }

                        var saveFileDialog = new SaveFileDialog
                        {
                            Filter = "Excel Files|*.xlsx",
                            Title = "Save an Excel File"
                        };
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            System.IO.FileInfo fi = new System.IO.FileInfo(saveFileDialog.FileName);
                            excel.SaveAs(fi);
                        }
                    }
                }
            }
        }
    }

    private NpgsqlDbType ConvertToNpgsqlType(Type type)
    {
        if (type == typeof(int))
            return NpgsqlDbType.Integer;
        if (type == typeof(string))
            return NpgsqlDbType.Text;
        if (type == typeof(bool))
            return NpgsqlDbType.Boolean;
        if (type == typeof(DateTime))
            return NpgsqlDbType.Timestamp;
        if (type == typeof(decimal))
            return NpgsqlDbType.Numeric;
        else
            return NpgsqlDbType.Text;
    }

    private void OnTableChanged(object sender, EventArgs e)
    {
        saveButton.BackColor = Color.LightBlue;
        saveStates[tablesList.SelectedIndex] = true;
    }

    private void table_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        // Проверим: это ли столбец id?
        if (table.Columns[e.ColumnIndex].Name == "id")
        {
            var row = table.Rows[e.RowIndex];
            if (row.IsNewRow)
            {
                table.Columns[e.ColumnIndex].ReadOnly = false;
            }
            else
            {
                table.Columns[e.ColumnIndex].ReadOnly = true;
            }
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void deletePanel_Paint(object sender, PaintEventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
    {

    }

    private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
    {

    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void tableLayoutPanel3_Paint_1(object sender, PaintEventArgs e)
    {

    }

    private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }

    private void button3_Click(object sender, EventArgs e)
    {

    }

    private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void panel3_Paint(object sender, PaintEventArgs e)
    {

    }
}