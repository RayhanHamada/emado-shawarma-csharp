using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SQLite;

namespace emado_swarma_csharp
{
    class Koneksi
    {
        public static SQLiteConnection conn;
        public static DataTable Table { get; set; }
        public Koneksi()
        {
            try
            {
                conn = new SQLiteConnection("host=localhost; port=3306; user=root; database=emado_shawarma; convert zero datetime=True");
                conn.StateChange += Conn_StateChange;
                Table = new DataTable();
            } catch 
            {
                
            }
        }

        public static void Connect()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                return;
            }
        }

        public static bool IsConnected()
        {
            return conn.State == ConnectionState.Open;
        }

        public static void FillTable()
        {
            if (!IsConnected())
            {
                return;
            }

            var query = "SELECT id, nama, golongan, jabatan," +
                "departemen, gaji, tunjangan, DATE_FORMAT(tgl_lahir, '%d-%m-%Y') as tgl_lahir," +
                "jenis_kelamin, alamat, no_rek, no_npwp, no_bpjs, lokasi FROM tbl_karyawan";

            var selectAllKaryawan = new SQLiteDataAdapter(query, conn);
            selectAllKaryawan.Fill(Table);
        }

        public static void RefreshTableWithNewCommand(string cmd)
        {
            if (!IsConnected())
            {
                // make handler on not connecting
                return;
            }

            var adapter = new SQLiteDataAdapter(cmd, conn);
            Table.Clear();
            adapter.Fill(Table);
        }

        public static void SearchRefresh(string name)
        {
            if (!IsConnected())
            {
                // make handler on not connecting
                return;
            }

            var query = "SELECT id, nama, golongan, jabatan," +
                "departemen, gaji, tunjangan, DATE_FORMAT(tgl_lahir, '%d-%m-%Y') as tgl_lahir," +
                $"jenis_kelamin, alamat, no_rek, no_npwp, no_bpjs, lokasi FROM tbl_karyawan WHERE nama LIKE '%{name}%'";

            var adapter = new SQLiteDataAdapter(query, conn);
            Table.Clear();
            adapter.Fill(Table);
        }

        public static void RefreshTable()
        {
            if (!IsConnected())
            {
                // make handler on not connecting
                return;
            }

            var query = "SELECT id, nama, golongan, jabatan," +
                "departemen, gaji, tunjangan, DATE_FORMAT(tgl_lahir, '%d-%m-%Y') as tgl_lahir," +
                "jenis_kelamin, alamat, no_rek, no_npwp, no_bpjs, lokasi FROM tbl_karyawan";

            var adapter = new SQLiteDataAdapter(query, conn);
            Table.Clear();
            adapter.Fill(Table);
        }

        public static bool DeleteKaryawan(int id)
        {
            if (!IsConnected())
            {
                MessageBox.Show("Hapus data gagal, aplikasi tidak terkoneksi ke database !");
                return false;
            }

            var cmd = new SQLiteCommand($"DELETE FROM tbl_karyawan WHERE id = {id}", conn);
            var affected = cmd.ExecuteNonQuery();
            RefreshTable();

            return affected > 0;
        }

        public static bool UpdateKaryawan(Karyawan k)
        {
            if (!IsConnected())
            {
                MessageBox.Show("Update data gagal, aplikasi tidak terkoneksi ke database !");
                return false;
            }

            var query = $"UPDATE tbl_karyawan SET " +
                $"nama = '{k.Nama}'," +
                $"golongan = '{k.Golongan}'," +
                $"jabatan = '{k.Jabatan}'," +
                $"departemen = '{k.Departemen}'," +
                $"gaji = {k.Gaji}," +
                $"tunjangan = {k.Tunjangan}," +
                $"tgl_lahir = '{k.TglLahir.ToString("yyyy-MM-dd")}'," +
                $"jenis_kelamin = '{k.JenisKelamin}'," +
                $"alamat = '{k.Alamat}'," +
                $"no_rek = '{k.Norek}'," +
                $"no_npwp = '{k.NPWP}'," +
                $"no_bpjs = '{k.BPJS}'," +
                $"lokasi = '{k.Lokasi}'," +
                $"url_foto = '{k.UrlFoto.Replace("\\", "\\\\")}'" +
                $"WHERE id = {k.Id}";

            Console.WriteLine(query);

            var cmd = new SQLiteCommand(query, conn);
            var affected = cmd.ExecuteNonQuery();
            RefreshTable();

            return affected > 0;
        }

        public static Karyawan GetKaryawan(int id)
        {
            if (!IsConnected())
            {
                MessageBox.Show("Ambil data gagal, aplikasi tidak terkoneksi ke database !");
                return null;
            }

            var cmd = new SQLiteCommand($"SELECT * FROM tbl_karyawan WHERE id = '{id}'", conn);
            var result = cmd.ExecuteReader();
            var k = new Karyawan();

            result.Read();

            k.Id = int.Parse(result.GetString("id"));
            k.Nama = result.GetString("nama");
            k.Golongan = result.GetString("golongan");
            k.Jabatan = result.GetString("jabatan");
            k.Departemen = result.GetString("departemen");
            k.Gaji = (uint)result.GetInt32("gaji");
            k.Tunjangan = (uint)result.GetInt32("tunjangan");
            k.TglLahir = result.GetDateTime("tgl_lahir").Date;
            k.JenisKelamin = result.GetString("jenis_kelamin");
            k.Alamat = result.GetString("alamat");
            k.Norek = result.GetString("no_rek");
            k.NPWP = result.GetString("no_npwp");
            k.BPJS = result.GetString("no_bpjs");
            k.Lokasi = result.GetString("lokasi");
            k.UrlFoto = result.GetString("url_foto");
            result.Close();

            return k;
        }

        public static bool AddKaryawan(Karyawan k)
        {
            if (!IsConnected())
            {
                MessageBox.Show("Tambah data gagal, aplikasi tidak terkoneksi ke database !");
                return false;
            }

            // cari karyawan dengan nama sama
            var cmdCari = new SQLiteCommand($"SELECT COUNT(nama) as banyak FROM tbl_karyawan WHERE nama = '{k.Nama}'", conn);
            var banyak = (long)cmdCari.ExecuteScalar();

            if (banyak > 0)
            {
                MessageBox.Show($"Tambah data gagal, karyawan dengan nama {k.Nama} sudah ada !");
                return false;
            }

            var query = $"INSERT INTO tbl_karyawan VALUES (" +
                $"NULL," +
                $"'{k.Nama}'," +
                $"'{k.Golongan}'," +
                $"'{k.Jabatan}'," +
                $"'{k.Departemen}'," +
                $"{k.Gaji}," +
                $"{k.Tunjangan}," +
                $"'{k.TglLahir.ToString("yyyy-MM-dd")}'," +
                $"'{k.JenisKelamin}'," +
                $"'{k.Alamat}'," +
                $"'{k.Norek}'," +
                $"'{k.NPWP}'," +
                $"'{k.BPJS}'," +
                $"'{k.Lokasi}'," +
                $"'{k.UrlFoto.Replace("\\", "\\\\")}')";

            var cmd = new SQLiteCommand(query, conn);
            var result = cmd.ExecuteNonQuery();
            RefreshTable();

            return result > 0;
        }

        private static void Conn_StateChange(object sender, StateChangeEventArgs e)
        {

        }
    }
}
