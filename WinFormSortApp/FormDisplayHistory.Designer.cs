
namespace WinFormSortApp
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
            this.dataGridViewSortHistory.Location = new System.Drawing.Point(18, 71);
            this.dataGridViewSortHistory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewSortHistory.Name = "dataGridViewSortHistory";
            this.dataGridViewSortHistory.ReadOnly = true;
            this.dataGridViewSortHistory.RowHeadersVisible = false;
            this.dataGridViewSortHistory.RowHeadersWidth = 62;
            this.dataGridViewSortHistory.ShowEditingIcon = false;
            this.dataGridViewSortHistory.Size = new System.Drawing.Size(850, 532);
            this.dataGridViewSortHistory.TabIndex = 0;
            // 
            // labelSortHistory
            // 
            this.labelSortHistory.AutoSize = true;
            this.labelSortHistory.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSortHistory.Location = new System.Drawing.Point(23, 25);
            this.labelSortHistory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSortHistory.Name = "labelSortHistory";
            this.labelSortHistory.Size = new System.Drawing.Size(206, 30);
            this.labelSortHistory.TabIndex = 1;
            this.labelSortHistory.Text = "History of Sortings:";
            this.labelSortHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonUpdateContent
            // 
            this.buttonUpdateContent.Image = ((System.Drawing.Image)(resources.GetObject("buttonUpdateContent.Image")));
            this.buttonUpdateContent.Location = new System.Drawing.Point(893, 88);
            this.buttonUpdateContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonUpdateContent.Name = "buttonUpdateContent";
            this.buttonUpdateContent.Size = new System.Drawing.Size(63, 63);
            this.buttonUpdateContent.TabIndex = 2;
            this.toolTipInfo.SetToolTip(this.buttonUpdateContent, "Update data");
            this.buttonUpdateContent.UseVisualStyleBackColor = true;
            this.buttonUpdateContent.Click += new System.EventHandler(this.buttonUpdateContent_Click);
            // 
            // FormDisplayHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 622);
            this.Controls.Add(this.buttonUpdateContent);
            this.Controls.Add(this.labelSortHistory);
            this.Controls.Add(this.dataGridViewSortHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private System.Windows.Forms.ToolTip toolTipInfo;
    }
}