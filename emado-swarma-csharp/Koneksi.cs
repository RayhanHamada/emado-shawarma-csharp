using System;
using System.Data;
using System.Collections.Generic;
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
                conn = new MySqlConnection("host=localhost; port=3306; user=root; database=emado_shawarma");
                conn.StateChange += Conn_StateChange;
            } catch 
            {
                
            }
            
        }

        public static void Connect()
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
                return;
            }
        }

        public static bool IsConnected()
        {
            return conn.State == System.Data.ConnectionState.Open;
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

        public static bool DeleteKaryawan(int id)
        {
            if (!IsConnected())
            {
                //make handler on not connecting
                return false;
            }

            var cmd = new MySqlCommand($"DELETE FROM tbl_karyawan WHERE id = {id}", conn);
            var affected = cmd.ExecuteNonQuery();

            return affected > 0;
        }

        private static void Conn_StateChange(object sender, StateChangeEventArgs e)
        {
            
            
        }
    }
}
