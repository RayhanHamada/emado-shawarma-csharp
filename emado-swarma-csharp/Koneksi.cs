using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace emado_swarma_csharp
{
    class Koneksi
    {
        public static MySqlConnection conn;
        public Koneksi()
        {
            try
            {
                conn = new MySqlConnection("host=localhost; port=3306; user=root; database=emado_shawarma; convert zero datetime=True");
                conn.StateChange += Conn_StateChange;
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

        public static void FillDataGrid(DataTable table)
        {
            if (!IsConnected())
            {
                return;
            }

            var query = "SELECT id, nama, golongan, jabatan," +
                "departemen, gaji, tunjangan, DATE_FORMAT(tgl_lahir, '%d-%m-%Y') as tgl_lahir," +
                "jenis_kelamin, alamat, no_rek, no_npwp, no_bpjs, lokasi FROM tbl_karyawan";

            MySqlDataAdapter selectAllKaryawan = new MySqlDataAdapter(query, conn);
            selectAllKaryawan.FillAsync(table);
        }

        public static void RefreshTableWithNewCommand(DataTable table, string cmd)
        {
            if (!IsConnected())
            {
                // make handler on not connecting
                return;
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd, conn);
            table.Clear();
            adapter.FillAsync(table);
        }

        public static void RefreshTable(DataTable table)
        {
            if (!IsConnected())
            {
                // make handler on not connecting
                return;
            }

            var query = "SELECT id, nama, golongan, jabatan," +
                "departemen, gaji, tunjangan, DATE_FORMAT(tgl_lahir, '%d-%m-%Y') as tgl_lahir," +
                "jenis_kelamin, alamat, no_rek, no_npwp, no_bpjs, lokasi FROM tbl_karyawan";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            table.Clear();
            adapter.FillAsync(table);
        }

        public static bool DeleteKaryawan(int id)
        {
            if (!IsConnected())
            {
                MessageBox.Show("Hapus data gagal, aplikasi tidak terkoneksi ke database !");
                return false;
            }

            var cmd = new MySqlCommand($"DELETE FROM tbl_karyawan WHERE id = {id}", conn);
            var affected = cmd.ExecuteNonQuery();

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
                $"tgl_lahir = '{k.TglLahir.Date.Year}-{k.TglLahir.Date.Month}-{k.TglLahir.Date.Month}'," +
                $"jenis_kelamin = '{k.JenisKelamin}'," +
                $"alamat = '{k.Alamat}'," +
                $"no_rek = '{k.Norek}'," +
                $"no_npwp = '{k.NPWP}'," +
                $"no_bpjs = '{k.BPJS}'," +
                $"lokasi = '{k.Lokasi}'" +
                $"WHERE id = {k.Id}";

            Console.WriteLine(query);

            var cmd = new MySqlCommand(query, conn);
            var affected = cmd.ExecuteNonQuery();

            return affected > 0;
        }

        public static Karyawan GetKaryawan(int id)
        {
            if (!IsConnected())
            {
                MessageBox.Show("Ambil data gagal, aplikasi tidak terkoneksi ke database !");
                return null;
            }

            var cmd = new MySqlCommand($"SELECT * FROM tbl_karyawan WHERE id = '{id}'", conn);
            var result = cmd.ExecuteReader();
            var k = new Karyawan();

            result.Read();

            k.Id = int.Parse(result.GetString("id"));
            k.Nama = result.GetString("nama");
            k.Golongan = result.GetString("golongan");
            k.Jabatan = result.GetString("jabatan");
            k.Departemen = result.GetString("departemen");
            k.Gaji = result.GetUInt32("gaji");
            k.Tunjangan = result.GetUInt32("tunjangan");
            k.TglLahir = result.GetDateTime("tgl_lahir").Date;
            k.JenisKelamin = result.GetString("jenis_kelamin");
            k.Alamat = result.GetString("alamat");
            k.Norek = result.GetString("no_rek");
            k.NPWP = result.GetString("no_npwp");
            k.BPJS = result.GetString("no_bpjs");
            k.Lokasi = result.GetString("no_bpjs");
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
            var cmdCari = new MySqlCommand($"SELECT COUNT(nama) as banyak FROM tbl_karyawan WHERE nama = '{k.Nama}'", conn);
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
                $"'{k.TglLahir.Date.Year}-{k.TglLahir.Date.Month}-{k.TglLahir.Date.Month}'," +
                $"'{k.JenisKelamin}'," +
                $"'{k.Alamat}'," +
                $"'{k.Norek}'," +
                $"'{k.NPWP}'," +
                $"'{k.BPJS}'," +
                $"'{k.Lokasi}'," +
                $"'{k.UrlFoto}')";

            var cmd = new MySqlCommand(query, conn);
            var result = cmd.ExecuteNonQuery();
            return result > 0;
        }

        private static void Conn_StateChange(object sender, StateChangeEventArgs e)
        {

        }
    }
}
