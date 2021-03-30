using System;
using System.Collections.Generic;
using System.Text;

namespace emado_swarma_csharp
{
    class Karyawan
    {
        public int Id { get; set; }

        public string Nama { get; set; }
        public string Golongan { get; set; }
        public string Jabatan { get; set; }
        public string Departemen { get; set; }
        public DateTime TglLahir { get; set; }
        public string JenisKelamin { get; set; }
        public string Alamat { get; set; }
        public string Norek { get; set; }
        public string NPWP { get; set; }
        public string BPJS { get; set; }
        public string Lokasi { get; set; }
        public UInt32 Gaji { get; set; }
        public UInt32 Tunjangan { get; set; }
        public string UrlFoto { get; set; }

        //untuk tambah data
        public Karyawan() { }

        //untuk update data
        public Karyawan(int id)
        {
            this.Id = id;
        }
    }
}
