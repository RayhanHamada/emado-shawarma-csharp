using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using ClosedXML.Excel;

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

            DataGridViewCellStyle styleHapus = new DataGridViewCellStyle
            {
                ForeColor = Color.Red
            };
            dg_karyawan.Columns["col_hapus"].DefaultCellStyle = styleHapus;

            DataGridViewCellStyle styleUpdate = new DataGridViewCellStyle
            {
                ForeColor = Color.Green
            };

            using (var colGaji = dg_karyawan.Columns["gaji"])
            {
                colGaji.DefaultCellStyle.Format = "C";
                colGaji.DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("id-ID");
            }

            using (var colTunjangan = dg_karyawan.Columns["tunjangan"])
            {
                colTunjangan.DefaultCellStyle.Format = "C";
                colTunjangan.DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("id-ID");
            }

            mi_exportExcel.Click += Mi_exportExcel_Click;
            FormClosed += TableForm_FormClosed;
        }

        private void TableForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose(true);
            Environment.Exit(0);
        }

        private void Mi_exportExcel_Click(object sender, EventArgs e)
        {
            var workbook = new XLWorkbook();
            workbook.AddWorksheet(Koneksi.Table, "data_karyawan");
            workbook.Worksheet("data_karyawan").ColumnWidth = 25.0;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = $"export_data_karyawan_{ DateTime.Now.ToString("dd_MM_yyyy")}.xlsx";
            sfd.Filter = "Excel |*.xlsx";
            sfd.ShowDialog();

            workbook.SaveAs(sfd.FileName);
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
                    var id = (long)senderGrid.Rows[rowIndex].Cells["id"].Value;
                    var success = Koneksi.DeleteKaryawan(id);

                    if (success)
                    {
                        MessageBox.Show($"karyawan bernama {name} berhasil terhapus !");
                        ResetTextCari();
                        return;
                    }

                    MessageBox.Show($"karyawan bernama {name} tidak berhasil terhapus !");
                }
            } 
            //buat update karyawan
            else if (senderGrid.Columns[colIndex] is DataGridViewButtonColumn
                && senderGrid.Columns[colIndex].Name == "col_update")
            {
                var id = (long)senderGrid.Rows[rowIndex].Cells["id"].Value;

                var formUpdate = new TambahUpdateForm(id);
                formUpdate.Show(this);
                ResetTextCari();
            }
        }

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            var formTambah = new TambahUpdateForm();
            formTambah.Show(this);
            ResetTextCari();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            if (txt_cari.Text != "")
            {
                Koneksi.SearchRefresh(txt_cari.Text);
                return;
            }

            Koneksi.RefreshTable();
        }

        private async void txt_cari_TextChanged(object sender, EventArgs e)
        {
            // this inner method checks if user is still typing
            async Task<bool> UserKeepsTyping()
            {
                string txt = txt_cari.Text;   // remember text
                await Task.Delay(500);        // wait some
                return txt != txt_cari.Text;  // return that text chaged or not
            }
            if (await UserKeepsTyping()) return;
            // user is done typing, do your stuff
            Koneksi.SearchRefresh(txt_cari.Text);
        }

        private void ResetTextCari()
        {
            if (txt_cari.Text != "") txt_cari.Text = "";
        }
    }
}
