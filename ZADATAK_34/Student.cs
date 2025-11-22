using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vezba10 {
    public class Student {

        private string ime;
        private string prezime;
        private string brojIndeksa;
        private DateTime datumRodjenja;
        private string smer;

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojIndeksa { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Smer { get; set; }

        public override string ToString() {
            return $"Ime: {Ime}, Prezime: {Prezime}, brIndeksa: {BrojIndeksa}, Datum Rodjenja: {DatumRodjenja}, Smer: {Smer}";
        }
    }
}
