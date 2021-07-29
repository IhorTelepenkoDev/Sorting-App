
namespace WinFormsApp
{
    partial class FormSortApp
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
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.labelForPathField = new System.Windows.Forms.Label();
            this.buttonArrReadingByPath = new System.Windows.Forms.Button();
            this.buttonRandomArrayAssigning = new System.Windows.Forms.Button();
            this.comboBoxDataTypeOfArr = new System.Windows.Forms.ComboBox();
            this.labelForChoosingDataType = new System.Windows.Forms.Label();
            this.numUpDownRowsInArr = new System.Windows.Forms.NumericUpDown();
            this.numUpDownColumnsInArr = new System.Windows.Forms.NumericUpDown();
            this.labelBtwSizesOfArr = new System.Windows.Forms.Label();
            this.labelSizesOfArr = new System.Windows.Forms.Label();
            this.textBoxBasicArrOutput = new System.Windows.Forms.TextBox();
            this.textBoxResArrOutput = new System.Windows.Forms.TextBox();
            this.buttonDoSort = new System.Windows.Forms.Button();
            this.comboBoxSortingMethod = new System.Windows.Forms.ComboBox();
            this.labelChoosingTypeOfSort = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRowsInArr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownColumnsInArr)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(12, 44);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(549, 20);
            this.textBoxFilePath.TabIndex = 0;
            // 
            // labelForPathField
            // 
            this.labelForPathField.AutoSize = true;
            this.labelForPathField.Location = new System.Drawing.Point(12, 28);
            this.labelForPathField.Name = "labelForPathField";
            this.labelForPathField.Size = new System.Drawing.Size(108, 13);
            this.labelForPathField.TabIndex = 1;
            this.labelForPathField.Text = "Path to the Array File:";
            // 
            // buttonArrReadingByPath
            // 
            this.buttonArrReadingByPath.Location = new System.Drawing.Point(567, 41);
            this.buttonArrReadingByPath.Name = "buttonArrReadingByPath";
            this.buttonArrReadingByPath.Size = new System.Drawing.Size(75, 23);
            this.buttonArrReadingByPath.TabIndex = 2;
            this.buttonArrReadingByPath.Text = "Read Array";
            this.buttonArrReadingByPath.UseVisualStyleBackColor = true;
            this.buttonArrReadingByPath.Click += new System.EventHandler(this.buttonArrReadingByPath_Click);
            // 
            // buttonRandomArrayAssigning
            // 
            this.buttonRandomArrayAssigning.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRandomArrayAssigning.Location = new System.Drawing.Point(6, 25);
            this.buttonRandomArrayAssigning.Name = "buttonRandomArrayAssigning";
            this.buttonRandomArrayAssigning.Size = new System.Drawing.Size(105, 41);
            this.buttonRandomArrayAssigning.TabIndex = 3;
            this.buttonRandomArrayAssigning.Text = "Assign the Array Randomly\r\n";
            this.buttonRandomArrayAssigning.UseVisualStyleBackColor = true;
            this.buttonRandomArrayAssigning.Click += new System.EventHandler(this.buttonRandomArrayAssign_Click);
            // 
            // comboBoxDataTypeOfArr
            // 
            this.comboBoxDataTypeOfArr.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.comboBoxDataTypeOfArr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataTypeOfArr.FormattingEnabled = true;
            this.comboBoxDataTypeOfArr.Items.AddRange(new object[] {
            "",
            "Integer",
            "Float",
            "Text"});
            this.comboBoxDataTypeOfArr.Location = new System.Drawing.Point(146, 115);
            this.comboBoxDataTypeOfArr.Name = "comboBoxDataTypeOfArr";
            this.comboBoxDataTypeOfArr.Size = new System.Drawing.Size(62, 21);
            this.comboBoxDataTypeOfArr.TabIndex = 4;
            this.comboBoxDataTypeOfArr.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataTypeOfArr_SelectedIndexChanged);
            // 
            // labelForChoosingDataType
            // 
            this.labelForChoosingDataType.Location = new System.Drawing.Point(143, 77);
            this.labelForChoosingDataType.Name = "labelForChoosingDataType";
            this.labelForChoosingDataType.Size = new System.Drawing.Size(82, 35);
            this.labelForChoosingDataType.TabIndex = 5;
            this.labelForChoosingDataType.Text = "Type of the Array Elements:";
            // 
            // numUpDownRowsInArr
            // 
            this.numUpDownRowsInArr.Location = new System.Drawing.Point(253, 116);
            this.numUpDownRowsInArr.Name = "numUpDownRowsInArr";
            this.numUpDownRowsInArr.Size = new System.Drawing.Size(53, 20);
            this.numUpDownRowsInArr.TabIndex = 6;
            // 
            // numUpDownColumnsInArr
            // 
            this.numUpDownColumnsInArr.Location = new System.Drawing.Point(330, 116);
            this.numUpDownColumnsInArr.Name = "numUpDownColumnsInArr";
            this.numUpDownColumnsInArr.Size = new System.Drawing.Size(50, 20);
            this.numUpDownColumnsInArr.TabIndex = 7;
            // 
            // labelBtwSizesOfArr
            // 
            this.labelBtwSizesOfArr.AutoSize = true;
            this.labelBtwSizesOfArr.Location = new System.Drawing.Point(312, 118);
            this.labelBtwSizesOfArr.Name = "labelBtwSizesOfArr";
            this.labelBtwSizesOfArr.Size = new System.Drawing.Size(12, 13);
            this.labelBtwSizesOfArr.TabIndex = 8;
            this.labelBtwSizesOfArr.Text = "x";
            // 
            // labelSizesOfArr
            // 
            this.labelSizesOfArr.Location = new System.Drawing.Point(260, 87);
            this.labelSizesOfArr.Name = "labelSizesOfArr";
            this.labelSizesOfArr.Size = new System.Drawing.Size(110, 25);
            this.labelSizesOfArr.TabIndex = 9;
            this.labelSizesOfArr.Text = "Size of the 2D Array:";
            // 
            // textBoxBasicArrOutput
            // 
            this.textBoxBasicArrOutput.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxBasicArrOutput.Location = new System.Drawing.Point(15, 161);
            this.textBoxBasicArrOutput.Multiline = true;
            this.textBoxBasicArrOutput.Name = "textBoxBasicArrOutput";
            this.textBoxBasicArrOutput.ReadOnly = true;
            this.textBoxBasicArrOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxBasicArrOutput.Size = new System.Drawing.Size(300, 286);
            this.textBoxBasicArrOutput.TabIndex = 10;
            this.textBoxBasicArrOutput.WordWrap = false;
            // 
            // textBoxResArrOutput
            // 
            this.textBoxResArrOutput.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxResArrOutput.Location = new System.Drawing.Point(367, 161);
            this.textBoxResArrOutput.Multiline = true;
            this.textBoxResArrOutput.Name = "textBoxResArrOutput";
            this.textBoxResArrOutput.ReadOnly = true;
            this.textBoxResArrOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxResArrOutput.Size = new System.Drawing.Size(300, 286);
            this.textBoxResArrOutput.TabIndex = 11;
            this.textBoxResArrOutput.WordWrap = false;
            // 
            // buttonDoSort
            // 
            this.buttonDoSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDoSort.Location = new System.Drawing.Point(567, 87);
            this.buttonDoSort.Name = "buttonDoSort";
            this.buttonDoSort.Size = new System.Drawing.Size(75, 56);
            this.buttonDoSort.TabIndex = 12;
            this.buttonDoSort.Text = "SORT!";
            this.buttonDoSort.UseVisualStyleBackColor = true;
            // 
            // comboBoxSortingMethod
            // 
            this.comboBoxSortingMethod.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.comboBoxSortingMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSortingMethod.FormattingEnabled = true;
            this.comboBoxSortingMethod.Items.AddRange(new object[] {
            "Select another source..."});
            this.comboBoxSortingMethod.Location = new System.Drawing.Point(411, 115);
            this.comboBoxSortingMethod.Name = "comboBoxSortingMethod";
            this.comboBoxSortingMethod.Size = new System.Drawing.Size(130, 21);
            this.comboBoxSortingMethod.TabIndex = 13;
            // 
            // labelChoosingTypeOfSort
            // 
            this.labelChoosingTypeOfSort.Location = new System.Drawing.Point(441, 87);
            this.labelChoosingTypeOfSort.Name = "labelChoosingTypeOfSort";
            this.labelChoosingTypeOfSort.Size = new System.Drawing.Size(78, 25);
            this.labelChoosingTypeOfSort.TabIndex = 14;
            this.labelChoosingTypeOfSort.Text = "Sorting Type:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRandomArrayAssigning);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 78);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Random Aray";
            // 
            // FormSortApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 459);
            this.Controls.Add(this.labelChoosingTypeOfSort);
            this.Controls.Add(this.comboBoxSortingMethod);
            this.Controls.Add(this.buttonDoSort);
            this.Controls.Add(this.textBoxResArrOutput);
            this.Controls.Add(this.textBoxBasicArrOutput);
            this.Controls.Add(this.labelSizesOfArr);
            this.Controls.Add(this.labelBtwSizesOfArr);
            this.Controls.Add(this.numUpDownColumnsInArr);
            this.Controls.Add(this.numUpDownRowsInArr);
            this.Controls.Add(this.labelForChoosingDataType);
            this.Controls.Add(this.comboBoxDataTypeOfArr);
            this.Controls.Add(this.buttonArrReadingByPath);
            this.Controls.Add(this.labelForPathField);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormSortApp";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRowsInArr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownColumnsInArr)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Label labelForPathField;
        private System.Windows.Forms.Button buttonArrReadingByPath;
        private System.Windows.Forms.Button buttonRandomArrayAssigning;
        private System.Windows.Forms.ComboBox comboBoxDataTypeOfArr;
        private System.Windows.Forms.Label labelForChoosingDataType;
        private System.Windows.Forms.NumericUpDown numUpDownRowsInArr;
        private System.Windows.Forms.NumericUpDown numUpDownColumnsInArr;
        private System.Windows.Forms.Label labelBtwSizesOfArr;
        private System.Windows.Forms.Label labelSizesOfArr;
        private System.Windows.Forms.TextBox textBoxBasicArrOutput;
        private System.Windows.Forms.TextBox textBoxResArrOutput;
        private System.Windows.Forms.Button buttonDoSort;
        private System.Windows.Forms.ComboBox comboBoxSortingMethod;
        private System.Windows.Forms.Label labelChoosingTypeOfSort;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

