CREATE TABLE IF NOT EXISTS tbl_karyawan (
    id INTEGER PRIMARY KEY,
    nama TEXT NOT NULL,
    golongan TEXT NOT NUll,
    jabatan TEXT NOT NULL,
    departemen TEXT NOT NULL,
    gaji INTEGER,
    tunjangan INTEGER,
    tgl_lahir TEXT,
    jenis_kelamin TEXT ,
    alamat TEXT,
    no_rek TEXT,
    no_npwp TEXT,
    no_bpjs TEXT,
    lokasi TEXT,
    url_foto TEXT
);
