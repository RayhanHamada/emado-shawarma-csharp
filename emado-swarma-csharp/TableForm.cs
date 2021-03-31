using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace emado_swarma_csharp
{
    public partial class TableForm : Form
    {
        public TableForm()
        {
            InitializeComponent();
        }

        private void TableForm_Load(object sender, EventArgs e)
        {
            Koneksi.FillTable();
            dg_karyawan.DataSource = Koneksi.Table;
            dg_karyawan.Columns["id"].Visible = false;

            DataGridViewCellStyle styleHapus = new DataGridViewCellStyle();
            styleHapus.ForeColor = Color.Red;
            dg_karyawan.Columns["col_hapus"].DefaultCellStyle = styleHapus;

            DataGridViewCellStyle styleUpdate = new DataGridViewCellStyle();
            styleUpdate.ForeColor = Color.Green;
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{dg_karyawan.Rows[0].Cells["tgl_lahir"].Value}");
        }

        private void dg_karyawan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var rowIndex = e.RowIndex;
            var colIndex = e.ColumnIndex;

            // buat delete karyawan
            if (senderGrid.Columns[colIndex] is DataGridViewButtonColumn 
                && senderGrid.Columns[colIndex].Name == "col_hapus")
            {
                var name = (string)senderGrid.Rows[rowIndex].Cells["nama"].Value;

                // konfirmasi penghapusan
                var dialogResult = MessageBox.Show($"Apa anda yakin untuk menghapus {name} ?", "Konfirmasi Hapus", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    var id = (int)senderGrid.Rows[rowIndex].Cells["id"].Value;
                    var success = Koneksi.DeleteKaryawan(id);

                    if (success)
                    {
                        MessageBox.Show($"karyawan bernama {name} berhasil terhapus !");
                        return;
                    }

                    MessageBox.Show($"karyawan bernama {name} tidak berhasil terhapus !");
                }
            } 
            //buat update karyawan
            else if (senderGrid.Columns[colIndex] is DataGridViewButtonColumn
                && senderGrid.Columns[colIndex].Name == "col_update")
            {
                var id = (int)senderGrid.Rows[rowIndex].Cells["id"].Value;

                var formUpdate = new TambahUpdateForm(id);
                formUpdate.Show();
            }
        }

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            var formTambah = new TambahUpdateForm();
            formTambah.Show();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            Koneksi.RefreshTable();
        }
    }
}
