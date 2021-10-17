﻿
namespace WinFormSortApp
{
    partial class SortApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SortApp));
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.labelForPathTextBox = new System.Windows.Forms.Label();
            this.buttonReadArrByPath = new System.Windows.Forms.Button();
            this.buttonRandomArrayAssign = new System.Windows.Forms.Button();
            this.comboBoxArrDataType = new System.Windows.Forms.ComboBox();
            this.labelForSelectDataType = new System.Windows.Forms.Label();
            this.numUpDownRowsInArr = new System.Windows.Forms.NumericUpDown();
            this.numUpDownColumnsInArr = new System.Windows.Forms.NumericUpDown();
            this.labelSignBetweenArrSizes = new System.Windows.Forms.Label();
            this.labelArrSizes = new System.Windows.Forms.Label();
            this.buttonStartSorting = new System.Windows.Forms.Button();
            this.comboBoxSelectedSorter = new System.Windows.Forms.ComboBox();
            this.labelSelectSorter = new System.Windows.Forms.Label();
            this.groupBoxRandomArrInit = new System.Windows.Forms.GroupBox();
            this.trackBarSortSlower = new System.Windows.Forms.TrackBar();
            this.labelSortSlower = new System.Windows.Forms.Label();
            this.dataGridViewUnsortedArr = new System.Windows.Forms.DataGridView();
            this.tabControlSortedArrResult = new System.Windows.Forms.TabControl();
            this.tabPageDefaultSortedArr = new System.Windows.Forms.TabPage();
            this.dataGridViewSortedArr0 = new System.Windows.Forms.DataGridView();
            this.buttonSortingHistory = new System.Windows.Forms.Button();
            this.toolTipValue = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSelectArrFileLocation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRowsInArr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownColumnsInArr)).BeginInit();
            this.groupBoxRandomArrInit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSortSlower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUnsortedArr)).BeginInit();
            this.tabControlSortedArrResult.SuspendLayout();
            this.tabPageDefaultSortedArr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSortedArr0)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(12, 34);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(543, 20);
            this.textBoxFilePath.TabIndex = 0;
            this.textBoxFilePath.TextChanged += new System.EventHandler(this.textBoxFilePath_TextChanged);
            // 
            // labelForPathTextBox
            // 
            this.labelForPathTextBox.AutoSize = true;
            this.labelForPathTextBox.Location = new System.Drawing.Point(15, 18);
            this.labelForPathTextBox.Name = "labelForPathTextBox";
            this.labelForPathTextBox.Size = new System.Drawing.Size(108, 13);
            this.labelForPathTextBox.TabIndex = 1;
            this.labelForPathTextBox.Text = "Path to the Array File:";
            // 
            // buttonReadArrByPath
            // 
            this.buttonReadArrByPath.Enabled = false;
            this.buttonReadArrByPath.Location = new System.Drawing.Point(624, 31);
            this.buttonReadArrByPath.Name = "buttonReadArrByPath";
            this.buttonReadArrByPath.Size = new System.Drawing.Size(81, 23);
            this.buttonReadArrByPath.TabIndex = 2;
            this.buttonReadArrByPath.Text = "Read Array";
            this.buttonReadArrByPath.UseVisualStyleBackColor = true;
            this.buttonReadArrByPath.Click += new System.EventHandler(this.buttonReadArrByPath_Click);
            // 
            // buttonRandomArrayAssign
            // 
            this.buttonRandomArrayAssign.Enabled = false;
            this.buttonRandomArrayAssign.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRandomArrayAssign.Location = new System.Drawing.Point(6, 25);
            this.buttonRandomArrayAssign.Name = "buttonRandomArrayAssign";
            this.buttonRandomArrayAssign.Size = new System.Drawing.Size(105, 41);
            this.buttonRandomArrayAssign.TabIndex = 3;
            this.buttonRandomArrayAssign.Text = "Assign the Array Randomly\r\n";
            this.buttonRandomArrayAssign.UseVisualStyleBackColor = true;
            this.buttonRandomArrayAssign.Click += new System.EventHandler(this.buttonRandomArrayAssign_Click);
            // 
            // comboBoxArrDataType
            // 
            this.comboBoxArrDataType.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.comboBoxArrDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArrDataType.FormattingEnabled = true;
            this.comboBoxArrDataType.Location = new System.Drawing.Point(146, 115);
            this.comboBoxArrDataType.Name = "comboBoxArrDataType";
            this.comboBoxArrDataType.Size = new System.Drawing.Size(62, 21);
            this.comboBoxArrDataType.TabIndex = 4;
            this.comboBoxArrDataType.SelectedIndexChanged += new System.EventHandler(this.comboBoxArrDataType_SelectedIndexChanged);
            // 
            // labelForSelectDataType
            // 
            this.labelForSelectDataType.Location = new System.Drawing.Point(143, 77);
            this.labelForSelectDataType.Name = "labelForSelectDataType";
            this.labelForSelectDataType.Size = new System.Drawing.Size(82, 35);
            this.labelForSelectDataType.TabIndex = 5;
            this.labelForSelectDataType.Text = "Type of the Array Elements:";
            // 
            // numUpDownRowsInArr
            // 
            this.numUpDownRowsInArr.Location = new System.Drawing.Point(253, 116);
            this.numUpDownRowsInArr.Name = "numUpDownRowsInArr";
            this.numUpDownRowsInArr.Size = new System.Drawing.Size(53, 20);
            this.numUpDownRowsInArr.TabIndex = 6;
            this.numUpDownRowsInArr.ValueChanged += new System.EventHandler(this.numUpDownRowsInArr_ValueChanged);
            // 
            // numUpDownColumnsInArr
            // 
            this.numUpDownColumnsInArr.Location = new System.Drawing.Point(330, 116);
            this.numUpDownColumnsInArr.Name = "numUpDownColumnsInArr";
            this.numUpDownColumnsInArr.Size = new System.Drawing.Size(50, 20);
            this.numUpDownColumnsInArr.TabIndex = 7;
            this.numUpDownColumnsInArr.ValueChanged += new System.EventHandler(this.numUpDownColumnsInArr_ValueChanged);
            // 
            // labelSignBetweenArrSizes
            // 
            this.labelSignBetweenArrSizes.AutoSize = true;
            this.labelSignBetweenArrSizes.Location = new System.Drawing.Point(312, 118);
            this.labelSignBetweenArrSizes.Name = "labelSignBetweenArrSizes";
            this.labelSignBetweenArrSizes.Size = new System.Drawing.Size(12, 13);
            this.labelSignBetweenArrSizes.TabIndex = 8;
            this.labelSignBetweenArrSizes.Text = "x";
            // 
            // labelArrSizes
            // 
            this.labelArrSizes.Location = new System.Drawing.Point(260, 87);
            this.labelArrSizes.Name = "labelArrSizes";
            this.labelArrSizes.Size = new System.Drawing.Size(110, 25);
            this.labelArrSizes.TabIndex = 9;
            this.labelArrSizes.Text = "Size of the 2D Array:";
            // 
            // buttonStartSorting
            // 
            this.buttonStartSorting.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonStartSorting.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonStartSorting.Enabled = false;
            this.buttonStartSorting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartSorting.Location = new System.Drawing.Point(642, 87);
            this.buttonStartSorting.Name = "buttonStartSorting";
            this.buttonStartSorting.Size = new System.Drawing.Size(63, 56);
            this.buttonStartSorting.TabIndex = 12;
            this.buttonStartSorting.Text = "SORT!";
            this.buttonStartSorting.UseVisualStyleBackColor = false;
            this.buttonStartSorting.Click += new System.EventHandler(this.buttonStartSorting_Click);
            // 
            // comboBoxSelectedSorter
            // 
            this.comboBoxSelectedSorter.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.comboBoxSelectedSorter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectedSorter.FormattingEnabled = true;
            this.comboBoxSelectedSorter.Location = new System.Drawing.Point(411, 115);
            this.comboBoxSelectedSorter.Name = "comboBoxSelectedSorter";
            this.comboBoxSelectedSorter.Size = new System.Drawing.Size(130, 21);
            this.comboBoxSelectedSorter.TabIndex = 13;
            this.comboBoxSelectedSorter.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectedSorter_SelectedIndexChanged);
            // 
            // labelSelectSorter
            // 
            this.labelSelectSorter.Location = new System.Drawing.Point(435, 87);
            this.labelSelectSorter.Name = "labelSelectSorter";
            this.labelSelectSorter.Size = new System.Drawing.Size(84, 25);
            this.labelSelectSorter.TabIndex = 14;
            this.labelSelectSorter.Text = "Sorting Method:";
            // 
            // groupBoxRandomArrInit
            // 
            this.groupBoxRandomArrInit.Controls.Add(this.buttonRandomArrayAssign);
            this.groupBoxRandomArrInit.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBoxRandomArrInit.Location = new System.Drawing.Point(12, 70);
            this.groupBoxRandomArrInit.Name = "groupBoxRandomArrInit";
            this.groupBoxRandomArrInit.Size = new System.Drawing.Size(374, 78);
            this.groupBoxRandomArrInit.TabIndex = 15;
            this.groupBoxRandomArrInit.TabStop = false;
            this.groupBoxRandomArrInit.Text = "Random Array";
            // 
            // trackBarSortSlower
            // 
            this.trackBarSortSlower.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarSortSlower.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBarSortSlower.Location = new System.Drawing.Point(547, 110);
            this.trackBarSortSlower.Maximum = 800;
            this.trackBarSortSlower.Name = "trackBarSortSlower";
            this.trackBarSortSlower.Size = new System.Drawing.Size(89, 45);
            this.trackBarSortSlower.TabIndex = 16;
            this.trackBarSortSlower.TickFrequency = 80;
            this.trackBarSortSlower.Scroll += new System.EventHandler(this.trackBarSortSlower_Scroll);
            // 
            // labelSortSlower
            // 
            this.labelSortSlower.AutoSize = true;
            this.labelSortSlower.Location = new System.Drawing.Point(550, 87);
            this.labelSortSlower.Name = "labelSortSlower";
            this.labelSortSlower.Size = new System.Drawing.Size(78, 13);
            this.labelSortSlower.TabIndex = 17;
            this.labelSortSlower.Text = "Sorting Slower:";
            // 
            // dataGridViewUnsortedArr
            // 
            this.dataGridViewUnsortedArr.AllowUserToAddRows = false;
            this.dataGridViewUnsortedArr.AllowUserToDeleteRows = false;
            this.dataGridViewUnsortedArr.AllowUserToResizeColumns = false;
            this.dataGridViewUnsortedArr.AllowUserToResizeRows = false;
            this.dataGridViewUnsortedArr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewUnsortedArr.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewUnsortedArr.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewUnsortedArr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUnsortedArr.ColumnHeadersVisible = false;
            this.dataGridViewUnsortedArr.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridViewUnsortedArr.Location = new System.Drawing.Point(18, 174);
            this.dataGridViewUnsortedArr.Name = "dataGridViewUnsortedArr";
            this.dataGridViewUnsortedArr.ReadOnly = true;
            this.dataGridViewUnsortedArr.RowHeadersVisible = false;
            this.dataGridViewUnsortedArr.RowHeadersWidth = 62;
            this.dataGridViewUnsortedArr.ShowEditingIcon = false;
            this.dataGridViewUnsortedArr.Size = new System.Drawing.Size(306, 275);
            this.dataGridViewUnsortedArr.TabIndex = 18;
            // 
            // tabControlSortedArrResult
            // 
            this.tabControlSortedArrResult.Controls.Add(this.tabPageDefaultSortedArr);
            this.tabControlSortedArrResult.Location = new System.Drawing.Point(357, 149);
            this.tabControlSortedArrResult.Name = "tabControlSortedArrResult";
            this.tabControlSortedArrResult.SelectedIndex = 0;
            this.tabControlSortedArrResult.Size = new System.Drawing.Size(319, 306);
            this.tabControlSortedArrResult.TabIndex = 20;
            // 
            // tabPageDefaultSortedArr
            // 
            this.tabPageDefaultSortedArr.Controls.Add(this.dataGridViewSortedArr0);
            this.tabPageDefaultSortedArr.Location = new System.Drawing.Point(4, 22);
            this.tabPageDefaultSortedArr.Name = "tabPageDefaultSortedArr";
            this.tabPageDefaultSortedArr.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDefaultSortedArr.Size = new System.Drawing.Size(311, 280);
            this.tabPageDefaultSortedArr.TabIndex = 0;
            this.tabPageDefaultSortedArr.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSortedArr0
            // 
            this.dataGridViewSortedArr0.AllowUserToAddRows = false;
            this.dataGridViewSortedArr0.AllowUserToDeleteRows = false;
            this.dataGridViewSortedArr0.AllowUserToResizeColumns = false;
            this.dataGridViewSortedArr0.AllowUserToResizeRows = false;
            this.dataGridViewSortedArr0.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewSortedArr0.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewSortedArr0.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewSortedArr0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSortedArr0.ColumnHeadersVisible = false;
            this.dataGridViewSortedArr0.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewSortedArr0.Name = "dataGridViewSortedArr0";
            this.dataGridViewSortedArr0.ReadOnly = true;
            this.dataGridViewSortedArr0.RowHeadersVisible = false;
            this.dataGridViewSortedArr0.RowHeadersWidth = 62;
            this.dataGridViewSortedArr0.ShowEditingIcon = false;
            this.dataGridViewSortedArr0.Size = new System.Drawing.Size(305, 275);
            this.dataGridViewSortedArr0.TabIndex = 19;
            // 
            // buttonSortingHistory
            // 
            this.buttonSortingHistory.Image = ((System.Drawing.Image)(resources.GetObject("buttonSortingHistory.Image")));
            this.buttonSortingHistory.Location = new System.Drawing.Point(678, 171);
            this.buttonSortingHistory.Name = "buttonSortingHistory";
            this.buttonSortingHistory.Size = new System.Drawing.Size(32, 30);
            this.buttonSortingHistory.TabIndex = 21;
            this.toolTipInfo.SetToolTip(this.buttonSortingHistory, "Open sorting history");
            this.buttonSortingHistory.UseVisualStyleBackColor = true;
            this.buttonSortingHistory.Click += new System.EventHandler(this.buttonSortingHistory_Click);
            // 
            // buttonSelectArrFileLocation
            // 
            this.buttonSelectArrFileLocation.BackColor = System.Drawing.SystemColors.HighlightText;
            this.buttonSelectArrFileLocation.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSelectArrFileLocation.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonSelectArrFileLocation.Location = new System.Drawing.Point(558, 32);
            this.buttonSelectArrFileLocation.Name = "buttonSelectArrFileLocation";
            this.buttonSelectArrFileLocation.Size = new System.Drawing.Size(51, 23);
            this.buttonSelectArrFileLocation.TabIndex = 22;
            this.buttonSelectArrFileLocation.Text = ". . .";
            this.buttonSelectArrFileLocation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSelectArrFileLocation.UseVisualStyleBackColor = false;
            this.buttonSelectArrFileLocation.Click += new System.EventHandler(this.buttonSelectArrFileLocation_Click);
            // 
            // SortApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 459);
            this.Controls.Add(this.buttonSelectArrFileLocation);
            this.Controls.Add(this.buttonSortingHistory);
            this.Controls.Add(this.tabControlSortedArrResult);
            this.Controls.Add(this.dataGridViewUnsortedArr);
            this.Controls.Add(this.labelSortSlower);
            this.Controls.Add(this.trackBarSortSlower);
            this.Controls.Add(this.labelSelectSorter);
            this.Controls.Add(this.comboBoxSelectedSorter);
            this.Controls.Add(this.buttonStartSorting);
            this.Controls.Add(this.labelArrSizes);
            this.Controls.Add(this.labelSignBetweenArrSizes);
            this.Controls.Add(this.numUpDownColumnsInArr);
            this.Controls.Add(this.numUpDownRowsInArr);
            this.Controls.Add(this.labelForSelectDataType);
            this.Controls.Add(this.comboBoxArrDataType);
            this.Controls.Add(this.buttonReadArrByPath);
            this.Controls.Add(this.labelForPathTextBox);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.groupBoxRandomArrInit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SortApp";
            this.Text = "Sorting Application";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRowsInArr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownColumnsInArr)).EndInit();
            this.groupBoxRandomArrInit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSortSlower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUnsortedArr)).EndInit();
            this.tabControlSortedArrResult.ResumeLayout(false);
            this.tabPageDefaultSortedArr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSortedArr0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Label labelForPathTextBox;
        private System.Windows.Forms.Button buttonReadArrByPath;
        private System.Windows.Forms.Button buttonRandomArrayAssign;
        private System.Windows.Forms.ComboBox comboBoxArrDataType;
        private System.Windows.Forms.Label labelForSelectDataType;
        private System.Windows.Forms.NumericUpDown numUpDownRowsInArr;
        private System.Windows.Forms.NumericUpDown numUpDownColumnsInArr;
        private System.Windows.Forms.Label labelSignBetweenArrSizes;
        private System.Windows.Forms.Label labelArrSizes;
        private System.Windows.Forms.Button buttonStartSorting;
        private System.Windows.Forms.ComboBox comboBoxSelectedSorter;
        private System.Windows.Forms.Label labelSelectSorter;
        private System.Windows.Forms.GroupBox groupBoxRandomArrInit;
        private System.Windows.Forms.TrackBar trackBarSortSlower;
        private System.Windows.Forms.Label labelSortSlower;
        private System.Windows.Forms.DataGridView dataGridViewUnsortedArr;
        private System.Windows.Forms.TabControl tabControlSortedArrResult;
        private System.Windows.Forms.TabPage tabPageDefaultSortedArr;
        private System.Windows.Forms.DataGridView dataGridViewSortedArr0;
        private System.Windows.Forms.Button buttonSortingHistory;
        private System.Windows.Forms.ToolTip toolTipValue;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Button buttonSelectArrFileLocation;
    }
}
