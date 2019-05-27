using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karakter.Lib.Entities
{
    public class Karakter
    {
        public string Naam { get; set; }
        public Rassen Ras { get; set; }
        public string Geslacht { get; set; }
        public int Levenspunten { get; set; }
        public int Kracht{ get; set; }
        public int Intelligentie{ get; set; }
        public int Snelheid{ get; set; }

        public int Level { get; set; }
        public int Ervaring { get; set; }
        public decimal Goud { get; set; }
        //public Locatie huidigeLocatie { get; set; }

        //public List<Voorwerp> uitrusting { get; set; }

        public Karakter(string naam, Rassen ras, string geslacht, int levenspunten, int kracht, int intelligentie, int snelheid)
        {
            Naam = naam;
            Ras = ras;
            Geslacht = geslacht;
            Levenspunten = levenspunten;
            Kracht = kracht;
            Intelligentie = intelligentie;
            Snelheid = snelheid;
            Level = 1;
            Ervaring = 0;
            Goud = 0;
        }
        //Locatie en uitrusting moet er ook nog bij
        public Karakter(string naam, Rassen ras, string geslacht, int level, int ervaring, decimal goud, int levenspunten, int kracht, int intelligentie, int snelheid)
        {
            Naam = naam;
            Ras = ras;
            Geslacht = geslacht;
            Levenspunten = levenspunten;
            Kracht = kracht;
            Intelligentie = intelligentie;
            Snelheid = snelheid;
            Level = level;
            Ervaring = ervaring;
            Goud = goud;
        }

        /*bool IdBestaatReeds(int id)
        {
            bool bestaat = true;
            reedsToegekendeIds.Contains(id);
            return bestaat;
        }

        int GeefNieuwId()
        {
            int nieuwId = 0;
            nieuwId = ++maxId;
            return nieuwId;
        }*/
    }
}
