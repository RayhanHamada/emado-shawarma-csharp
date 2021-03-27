using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace emado_swarma_csharp
{
    class Koneksi
    {
        public static MySqlConnection conn;
        public Koneksi()
        {
            conn = new MySqlConnection("host=localhost; port=3306; user=root; database=emado_shawarma");
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
    }
}
