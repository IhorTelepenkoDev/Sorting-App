
namespace WinFormsApp
{
    partial class FormDisplayHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDisplayHistory));
            this.dataGridViewSortHistory = new System.Windows.Forms.DataGridView();
            this.labelSortHistory = new System.Windows.Forms.Label();
            this.buttonUpdateContent = new System.Windows.Forms.Button();
            this.buttonCleanContent = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSortHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSortHistory
            // 
            this.dataGridViewSortHistory.AllowUserToAddRows = false;
            this.dataGridViewSortHistory.AllowUserToDeleteRows = false;
            this.dataGridViewSortHistory.AllowUserToResizeRows = false;
            this.dataGridViewSortHistory.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewSortHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewSortHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSortHistory.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridViewSortHistory.Location = new System.Drawing.Point(12, 46);
            this.dataGridViewSortHistory.Name = "dataGridViewSortHistory";
            this.dataGridViewSortHistory.ReadOnly = true;
            this.dataGridViewSortHistory.RowHeadersVisible = false;
            this.dataGridViewSortHistory.ShowEditingIcon = false;
            this.dataGridViewSortHistory.Size = new System.Drawing.Size(567, 346);
            this.dataGridViewSortHistory.TabIndex = 0;
            // 
            // labelSortHistory
            // 
            this.labelSortHistory.AutoSize = true;
            this.labelSortHistory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSortHistory.Location = new System.Drawing.Point(12, 13);
            this.labelSortHistory.Name = "labelSortHistory";
            this.labelSortHistory.Size = new System.Drawing.Size(157, 21);
            this.labelSortHistory.TabIndex = 1;
            this.labelSortHistory.Text = "History of Sortings:";
            this.labelSortHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonUpdateContent
            // 
            this.buttonUpdateContent.Image = ((System.Drawing.Image)(resources.GetObject("buttonUpdateContent.Image")));
            this.buttonUpdateContent.Location = new System.Drawing.Point(598, 58);
            this.buttonUpdateContent.Name = "buttonUpdateContent";
            this.buttonUpdateContent.Size = new System.Drawing.Size(47, 46);
            this.buttonUpdateContent.TabIndex = 2;
            this.toolTipInfo.SetToolTip(this.buttonUpdateContent, "Update data");
            this.buttonUpdateContent.UseVisualStyleBackColor = true;
            this.buttonUpdateContent.Click += new System.EventHandler(this.buttonUpdateContent_Click);
            // 
            // buttonCleanContent
            // 
            this.buttonCleanContent.Image = ((System.Drawing.Image)(resources.GetObject("buttonCleanContent.Image")));
            this.buttonCleanContent.Location = new System.Drawing.Point(598, 121);
            this.buttonCleanContent.Name = "buttonCleanContent";
            this.buttonCleanContent.Size = new System.Drawing.Size(47, 46);
            this.buttonCleanContent.TabIndex = 3;
            this.toolTipInfo.SetToolTip(this.buttonCleanContent, "Clean history");
            this.buttonCleanContent.UseVisualStyleBackColor = true;
            this.buttonCleanContent.Click += new System.EventHandler(this.buttonCleanContent_Click);
            // 
            // FormDisplayHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 404);
            this.Controls.Add(this.buttonCleanContent);
            this.Controls.Add(this.buttonUpdateContent);
            this.Controls.Add(this.labelSortHistory);
            this.Controls.Add(this.dataGridViewSortHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDisplayHistory";
            this.Text = "History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDisplayDB_FormClosing);
            this.Load += new System.EventHandler(this.FormDisplayDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSortHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSortHistory;
        private System.Windows.Forms.Label labelSortHistory;
        private System.Windows.Forms.Button buttonUpdateContent;
        private System.Windows.Forms.Button buttonCleanContent;
        private System.Windows.Forms.ToolTip toolTipInfo;
    }
}