namespace WinFormsApp1;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panel1 = new Panel();
        customPanel = new Panel();
        panel2 = new Panel();
        startTimePicker = new DateTimePicker();
        label10 = new Label();
        endTimePicker = new DateTimePicker();
        label11 = new Label();
        comboBox = new ComboBox();
        filterLabel = new Label();
        commonPanel = new Panel();
        label1 = new Label();
        panel3 = new Panel();
        radioButton2 = new RadioButton();
        radioButton1 = new RadioButton();
        saveButton = new Button();
        downloadButton = new Button();
        tablesList = new ListBox();
        summaryList = new ListBox();
        table = new DataGridView();
        panel1.SuspendLayout();
        customPanel.SuspendLayout();
        panel2.SuspendLayout();
        commonPanel.SuspendLayout();
        panel3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)table).BeginInit();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panel1.Controls.Add(customPanel);
        panel1.Controls.Add(commonPanel);
        panel1.Controls.Add(saveButton);
        panel1.Controls.Add(downloadButton);
        panel1.Controls.Add(tablesList);
        panel1.Controls.Add(summaryList);
        panel1.Controls.Add(table);
        panel1.Location = new Point(15, 12);
        panel1.Name = "panel1";
        panel1.Size = new Size(908, 527);
        panel1.TabIndex = 1;
        // 
        // customPanel
        // 
        customPanel.Controls.Add(panel2);
        customPanel.Controls.Add(comboBox);
        customPanel.Controls.Add(filterLabel);
        customPanel.Location = new Point(292, 193);
        customPanel.Name = "customPanel";
        customPanel.Size = new Size(613, 84);
        customPanel.TabIndex = 6;
        customPanel.Visible = false;
        // 
        // panel2
        // 
        panel2.Controls.Add(startTimePicker);
        panel2.Controls.Add(label10);
        panel2.Controls.Add(endTimePicker);
        panel2.Controls.Add(label11);
        panel2.Location = new Point(377, 3);
        panel2.Name = "panel2";
        panel2.Size = new Size(233, 81);
        panel2.TabIndex = 8;
        // 
        // startTimePicker
        // 
        startTimePicker.Location = new Point(60, 14);
        startTimePicker.Name = "startTimePicker";
        startTimePicker.Size = new Size(159, 27);
        startTimePicker.TabIndex = 4;
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new Point(11, 16);
        label10.Name = "label10";
        label10.Size = new Size(43, 20);
        label10.TabIndex = 7;
        label10.Text = "Start:";
        // 
        // endTimePicker
        // 
        endTimePicker.Location = new Point(60, 47);
        endTimePicker.Name = "endTimePicker";
        endTimePicker.Size = new Size(159, 27);
        endTimePicker.TabIndex = 5;
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(11, 47);
        label11.Name = "label11";
        label11.Size = new Size(37, 20);
        label11.TabIndex = 6;
        label11.Text = "End:";
        // 
        // comboBox
        // 
        comboBox.FormattingEnabled = true;
        comboBox.Location = new Point(101, 29);
        comboBox.Name = "comboBox";
        comboBox.Size = new Size(187, 28);
        comboBox.TabIndex = 1;
        // 
        // filterLabel
        // 
        filterLabel.AutoSize = true;
        filterLabel.Location = new Point(3, 32);
        filterLabel.Name = "filterLabel";
        filterLabel.Size = new Size(92, 20);
        filterLabel.TabIndex = 0;
        filterLabel.Text = "Department:";
        // 
        // commonPanel
        // 
        commonPanel.Controls.Add(label1);
        commonPanel.Controls.Add(panel3);
        commonPanel.Location = new Point(55, 193);
        commonPanel.Name = "commonPanel";
        commonPanel.Size = new Size(207, 84);
        commonPanel.TabIndex = 3;
        commonPanel.Visible = false;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 32);
        label1.Name = "label1";
        label1.Size = new Size(58, 20);
        label1.TabIndex = 3;
        label1.Text = "Period: ";
        // 
        // panel3
        // 
        panel3.Controls.Add(radioButton2);
        panel3.Controls.Add(radioButton1);
        panel3.Location = new Point(76, 9);
        panel3.Name = "panel3";
        panel3.Size = new Size(122, 72);
        panel3.TabIndex = 2;
        panel3.Paint += panel3_Paint;
        // 
        // radioButton2
        // 
        radioButton2.AutoSize = true;
        radioButton2.Location = new Point(0, 48);
        radioButton2.Name = "radioButton2";
        radioButton2.Size = new Size(58, 24);
        radioButton2.TabIndex = 1;
        radioButton2.TabStop = true;
        radioButton2.Text = "Year";
        radioButton2.UseVisualStyleBackColor = true;
        // 
        // radioButton1
        // 
        radioButton1.AutoSize = true;
        radioButton1.Location = new Point(0, 3);
        radioButton1.Name = "radioButton1";
        radioButton1.Size = new Size(73, 24);
        radioButton1.TabIndex = 0;
        radioButton1.TabStop = true;
        radioButton1.Text = "Month";
        radioButton1.UseVisualStyleBackColor = true;
        radioButton1.CheckedChanged += radioButton1_CheckedChanged;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(0, 138);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(449, 41);
        saveButton.TabIndex = 2;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += button3_Click;
        // 
        // downloadButton
        // 
        downloadButton.Location = new Point(455, 138);
        downloadButton.Name = "downloadButton";
        downloadButton.Size = new Size(453, 41);
        downloadButton.TabIndex = 1;
        downloadButton.Text = "Download";
        downloadButton.UseVisualStyleBackColor = true;
        // 
        // tablesList
        // 
        tablesList.Anchor = AnchorStyles.None;
        tablesList.FormattingEnabled = true;
        tablesList.Location = new Point(0, 35);
        tablesList.Name = "tablesList";
        tablesList.Size = new Size(449, 104);
        tablesList.TabIndex = 1;
        // 
        // summaryList
        // 
        summaryList.FormattingEnabled = true;
        summaryList.Items.AddRange(new object[] { "Common summary (month/year).", "Department summary.", "Goods summary.", "Employee summary.", "Goods amount document." });
        summaryList.Location = new Point(455, 35);
        summaryList.Name = "summaryList";
        summaryList.Size = new Size(450, 104);
        summaryList.TabIndex = 0;
        // 
        // table
        // 
        table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        table.Location = new Point(3, 283);
        table.Name = "table";
        table.RowHeadersWidth = 51;
        table.Size = new Size(902, 241);
        table.TabIndex = 0;
        table.CellContentClick += table_CellContentClick;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(935, 551);
        Controls.Add(panel1);
        Name = "Form1";
        Text = "Leshat";
        Load += Form1_Load;
        panel1.ResumeLayout(false);
        customPanel.ResumeLayout(false);
        customPanel.PerformLayout();
        panel2.ResumeLayout(false);
        panel2.PerformLayout();
        commonPanel.ResumeLayout(false);
        commonPanel.PerformLayout();
        panel3.ResumeLayout(false);
        panel3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)table).EndInit();
        ResumeLayout(false);
    }

    #endregion
    private Panel panel1;
    private DataGridView table;
    private ListBox tablesList;
    private Button saveButton;
    private Panel commonPanel;
    private ListBox summaryList;
    private Button downloadButton;
    private Label label1;
    private Panel panel3;
    private RadioButton radioButton2;
    private RadioButton radioButton1;
    private Panel customPanel;
    private ComboBox comboBox;
    private Label filterLabel;
    private Label label10;
    private Label label11;
    private DateTimePicker endTimePicker;
    private DateTimePicker startTimePicker;
    private Panel panel2;
}