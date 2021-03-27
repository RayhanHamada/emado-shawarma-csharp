using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emado_swarma_csharp
{
    public partial class LoginForm : Form
    {
        private string username, password;

        private void btn_login_Click(object sender, EventArgs e)
        {
            username = txt_username.Text;
            password = txt_password.Text;

            //username dan password harus admin atau tidak kosong
            if (username == "" || password == "")
            {
                MessageBox.Show("Username atau password tidak boleh kosong");
                return;
            }

            if (username != "admin" || password != "admin")
            {
                MessageBox.Show("Username atau password salah");
                return;
            }

            //Koneksi ke database
            Koneksi.Connect();
        }

        public LoginForm()
        {
            InitializeComponent();
            username = password = "";
        }
    }
}
