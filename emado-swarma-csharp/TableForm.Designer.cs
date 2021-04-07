
namespace emado_swarma_csharp
{
    partial class TableForm
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
            this.btn_tambah = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.dg_karyawan = new System.Windows.Forms.DataGridView();
            this.col_hapus = new System.Windows.Forms.DataGridViewButtonColumn();
            this.col_update = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txt_cari = new System.Windows.Forms.TextBox();
            this.ms_shawarma = new System.Windows.Forms.MenuStrip();
            this.file = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_exportExcel = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dg_karyawan)).BeginInit();
            this.ms_shawarma.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_tambah
            // 
            this.btn_tambah.Location = new System.Drawing.Point(23, 512);
            this.btn_tambah.Name = "btn_tambah";
            this.btn_tambah.Size = new System.Drawing.Size(241, 79);
            this.btn_tambah.TabIndex = 0;
            this.btn_tambah.Text = "Tambah Karyawan";
            this.btn_tambah.UseVisualStyleBackColor = true;
            this.btn_tambah.Click += new System.EventHandler(this.btn_tambah_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_refresh.Location = new System.Drawing.Point(1016, 58);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(94, 29);
            this.btn_refresh.TabIndex = 1;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // dg_karyawan
            // 
            this.dg_karyawan.AllowUserToAddRows = false;
            this.dg_karyawan.AllowUserToDeleteRows = false;
            this.dg_karyawan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_karyawan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_karyawan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_hapus,
            this.col_update});
            this.dg_karyawan.Location = new System.Drawing.Point(23, 93);
            this.dg_karyawan.Name = "dg_karyawan";
            this.dg_karyawan.RowHeadersWidth = 51;
            this.dg_karyawan.RowTemplate.Height = 29;
            this.dg_karyawan.Size = new System.Drawing.Size(1087, 413);
            this.dg_karyawan.TabIndex = 2;
            this.dg_karyawan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_karyawan_CellContentClick);
            // 
            // col_hapus
            // 
            this.col_hapus.HeaderText = "Hapus";
            this.col_hapus.MinimumWidth = 6;
            this.col_hapus.Name = "col_hapus";
            this.col_hapus.ReadOnly = true;
            this.col_hapus.Text = "Hapus";
            this.col_hapus.UseColumnTextForButtonValue = true;
            this.col_hapus.Width = 75;
            // 
            // col_update
            // 
            this.col_update.HeaderText = "Update";
            this.col_update.MinimumWidth = 6;
            this.col_update.Name = "col_update";
            this.col_update.ReadOnly = true;
            this.col_update.Text = "Update";
            this.col_update.UseColumnTextForButtonValue = true;
            this.col_update.Width = 75;
            // 
            // txt_cari
            // 
            this.txt_cari.Location = new System.Drawing.Point(23, 60);
            this.txt_cari.Name = "txt_cari";
            this.txt_cari.PlaceholderText = "Cari Karyawan";
            this.txt_cari.Size = new System.Drawing.Size(232, 27);
            this.txt_cari.TabIndex = 3;
            this.txt_cari.TextChanged += new System.EventHandler(this.txt_cari_TextChanged);
            // 
            // ms_shawarma
            // 
            this.ms_shawarma.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ms_shawarma.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file});
            this.ms_shawarma.Location = new System.Drawing.Point(0, 0);
            this.ms_shawarma.Name = "ms_shawarma";
            this.ms_shawarma.Size = new System.Drawing.Size(1131, 28);
            this.ms_shawarma.TabIndex = 4;
            this.ms_shawarma.Text = "menuStrip1";
            // 
            // file
            // 
            this.file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_exportExcel});
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(46, 24);
            this.file.Text = "File";
            // 
            // mi_exportExcel
            // 
            this.mi_exportExcel.Name = "mi_exportExcel";
            this.mi_exportExcel.Size = new System.Drawing.Size(260, 26);
            this.mi_exportExcel.Text = "Export Database To Excel";
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1131, 673);
            this.Controls.Add(this.txt_cari);
            this.Controls.Add(this.dg_karyawan);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.btn_tambah);
            this.Controls.Add(this.ms_shawarma);
            this.Name = "TableForm";
            this.Text = "Tabel";
            this.Load += new System.EventHandler(this.TableForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_karyawan)).EndInit();
            this.ms_shawarma.ResumeLayout(false);
            this.ms_shawarma.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_tambah;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.DataGridView dg_karyawan;
        private System.Windows.Forms.TextBox txt_cari;
        private System.Windows.Forms.DataGridViewButtonColumn col_hapus;
        private System.Windows.Forms.DataGridViewButtonColumn col_update;
        private System.Windows.Forms.MenuStrip ms_shawarma;
        private System.Windows.Forms.ToolStripMenuItem file;
        private System.Windows.Forms.ToolStripMenuItem mi_exportExcel;
    }
}