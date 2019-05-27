using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Karakter.Lib.Entities
{
    public class Speler
    {
        public string Naam { get; set; }
        public Rassen Ras { get; set; }
        public string Geslacht { get; set; }
        public int Levenspunten { get; set; }
        public int Kracht{ get; set; }
        public int Intelligentie{ get; set; }
        public int Snelheid{ get; set; }
        public ImageSource Avatar { get; set; }

        public int Level { get; set; }
        public int Ervaring { get; set; }
        public decimal Goud { get; set; }

        public Speler(string naam, Rassen ras, string geslacht, int levenspunten, int kracht, int intelligentie, int snelheid)
        {
            Naam = naam;
            Ras = ras;
            Geslacht = geslacht;
            Levenspunten = levenspunten;
            Kracht = kracht;
            Intelligentie = intelligentie;
            Snelheid = snelheid;
            Avatar = new BitmapImage(new Uri(@"\Images\" + ras + geslacht + ".jpg", UriKind.Relative));
            Goud = 0;
        }

        public Speler(string naam, Rassen ras, string geslacht, int goud, int levenspunten, int kracht, int intelligentie, int snelheid, int huidigeLevenspunten, int locatie)
        {
            Naam = naam;
            Ras = ras;
            Geslacht = geslacht;
            Levenspunten = levenspunten;
            Kracht = kracht;
            Intelligentie = intelligentie;
            Snelheid = snelheid;
            Goud = goud;
        }

        public override string ToString()
        {
            string info;
            info = $"{Naam}\t{Ras}\t{Geslacht}\t{Levenspunten}\t{Kracht}\t{Intelligentie}\t{Snelheid}\"";
            return info;
        }
    }
}
