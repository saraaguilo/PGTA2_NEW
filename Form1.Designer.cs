namespace PGTA2_DEF
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ExportCSV = new System.Windows.Forms.Button();
            this.Viewmap = new System.Windows.Forms.Button();
            this.selectfile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(34, 18);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1158, 714);
            this.dataGridView1.TabIndex = 0;
            // 
            // ExportCSV
            // 
            this.ExportCSV.Location = new System.Drawing.Point(1248, 98);
            this.ExportCSV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExportCSV.Name = "ExportCSV";
            this.ExportCSV.Size = new System.Drawing.Size(123, 52);
            this.ExportCSV.TabIndex = 1;
            this.ExportCSV.Text = "Export CSV";
            this.ExportCSV.UseVisualStyleBackColor = true;
            this.ExportCSV.Click += new System.EventHandler(this.ExportCSV_Click);
            // 
            // Viewmap
            // 
            this.Viewmap.Location = new System.Drawing.Point(1248, 160);
            this.Viewmap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Viewmap.Name = "Viewmap";
            this.Viewmap.Size = new System.Drawing.Size(123, 52);
            this.Viewmap.TabIndex = 2;
            this.Viewmap.Text = "View Map";
            this.Viewmap.UseVisualStyleBackColor = true;
            this.Viewmap.Click += new System.EventHandler(this.Viewmap_Click);
            // 
            // selectfile
            // 
            this.selectfile.Location = new System.Drawing.Point(1252, 222);
            this.selectfile.Name = "selectfile";
            this.selectfile.Size = new System.Drawing.Size(118, 45);
            this.selectfile.TabIndex = 3;
            this.selectfile.Text = "Select File";
            this.selectfile.UseVisualStyleBackColor = true;
            this.selectfile.Click += new System.EventHandler(this.selectfile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 780);
            this.Controls.Add(this.selectfile);
            this.Controls.Add(this.Viewmap);
            this.Controls.Add(this.ExportCSV);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ExportCSV;
        private System.Windows.Forms.Button Viewmap;
        private System.Windows.Forms.Button selectfile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}