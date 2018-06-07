using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookBetter
{
    public class Address
    {
        //property
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }
        public string Kota { get; set; }
        public DateTime TglLahir { get; set; }
        public string Email { get; set; }

        //public string Nama; -> backing field (variabel yang menampung data property, harus pakai getter setter lg)
        public Address() {
        }

        public Address(string nama, string alamat, string notelp, string kota, DateTime tgllahir, string email) {
            Nama = nama;
            Alamat = alamat;
            NoTelp = notelp;
            Kota = kota;
            TglLahir = tgllahir;
            Email = email;
        }
    }
}
