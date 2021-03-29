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
        private TableForm tableForm;

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
            if (Koneksi.IsConnected())
            {
                tableForm = new TableForm();
                tableForm.Show();
                this.Visible = false;
                return;
            }

            MessageBox.Show("Aplikasi tidak terkoneksi ke database !");
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        public LoginForm()
        {
            InitializeComponent();
            username = password = "";
        }
    }
}
