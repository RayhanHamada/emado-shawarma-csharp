﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace emado_swarma_csharp
{
    public partial class TambahUpdateForm : Form
    {
        private readonly Karyawan k;
        private readonly bool tambah;

        // nama karyawan sebelum di update
        private readonly string namaSebelum;

        //untuk tambah karyawan
        public TambahUpdateForm()
        {
            InitializeComponent();
            k = new Karyawan();
            tambah = true;
            InitializeCustom();
            btn_update_tambah.Text = "Tambah Karyawan";
        }

        //untuk update karyawan
        public TambahUpdateForm(int id)
        {
            InitializeComponent();
            k = Koneksi.GetKaryawan(id);
            if (k.Equals(null))
            {
                this.Close();
            }
            namaSebelum = k.Nama;
            tambah = false;
            InitializeCustom();
            btn_update_tambah.Text = "Update Karyawan";
            PopulateForm(k);
        }

        private void InitializeCustom()
        {
            dt_tgl_lahir.Format = DateTimePickerFormat.Custom;
            dt_tgl_lahir.CustomFormat = "dd-MM-YYYY";
        }

        private void btn_update_tambah_Click(object sender, EventArgs e)
        {
            if (txt_nama.Text == "")
            {
                MessageBox.Show("Kolom Nama tidak boleh kosong", "Kesalahan Input");
            }
            else
            {
                k.Nama = txt_nama.Text;
            }

            k.Golongan = cb_golongan.Text;
            k.Jabatan = cb_jabatan.Text;
            k.Departemen = cb_departemen.Text;
            try
            {
                k.Gaji = uint.Parse(txt_gaji.Text);
            } catch (Exception e1)
            {
                if (e1 is FormatException)
                {
                    MessageBox.Show("Penulisan Gaji tidak benar", "Kesalahan Input");
                }
                MessageBox.Show("Kolom Gaji tidak boleh kosong");
            }

            try
            {
                k.Tunjangan = uint.Parse(txt_tunjangan.Text);
            }
            catch (Exception e1)
            {
                if (e1 is FormatException)
                {
                    MessageBox.Show("Penulisan Tunjangan tidak benar", "Kesalahan Input");
                }
                MessageBox.Show("Kolom Tunjangan tidak boleh kosong");
            }

            k.TglLahir = dt_tgl_lahir.Value;
            k.JenisKelamin = cb_jk.Text;
            k.Alamat = rtb_alamat.Text;

            if (txt_norek.Text == "")
            {
                MessageBox.Show("Kolom Norek tidak boleh kosong", "Kesalahan Input");
            } else
            {
                k.Norek = txt_norek.Text;
            }
            
            k.NPWP = txt_npwp.Text;
            k.BPJS = txt_bpjs.Text;
            k.Lokasi = txt_lokasi.Text;

            bool success;

            if (tambah)
            {
                //panggil Koneksi.AddKaryawan
                success = Koneksi.AddKaryawan(k);
            } else
            {
                //panggil Koneksi.UpdateKaryawan
                success = Koneksi.UpdateKaryawan(k);
            }

            if (success)
            {
                MessageBox.Show($"{(tambah ? "Tambah" : "Update")} karyawan {namaSebelum} berhasil !");
                return;
            }

            MessageBox.Show($"{(tambah ? "Tambah" : "Update")} karyawan {namaSebelum} tidak berhasil");
            this.Close();
        }

        private void PopulateForm(Karyawan k)
        {
            txt_nama.Text = k.Nama;
            cb_golongan.Text = k.Golongan;
            cb_jabatan.Text = k.Jabatan;
            cb_departemen.Text = k.Departemen;
            txt_gaji.Text = k.Gaji.ToString();
            txt_tunjangan.Text = k.Tunjangan.ToString();
            dt_tgl_lahir.Value = k.TglLahir;
            cb_jk.Text = k.JenisKelamin;
            rtb_alamat.Text = k.Alamat;
            txt_norek.Text = k.Norek;
            txt_npwp.Text = k.NPWP;
            txt_bpjs.Text = k.BPJS;
            txt_lokasi.Text = k.Lokasi;
            pb_foto.ImageLocation = k.UrlFoto;
        }
    }
}
