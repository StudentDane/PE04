using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Karakter.Lib.Entities;
using Utilities.Lib;

namespace Karakter.Lib.Services
{
    public class KarakterService
    {
        public TextFileFunctions textFileFunctions = new TextFileFunctions();
        string tekstBestandLocatie = TextFileFunctions.rootPad + "Karakters.csv";
        public List<Speler> Spelers { get; set; }

        public KarakterService()
        {
            Spelers = new List<Speler>();
        }

        public void MaakSpelersAan()
        {
            Speler speler = new Speler("Danor Nightblade", Rassen.Elf, "Man", 10, 6, 5, 6);
            VoegSpelerToe(speler);
            Speler speler1 = new Speler("Kyllion Crymaster", Rassen.Ork, "Man", 10, 7, 5, 5);
            VoegSpelerToe(speler1);
        }

        public void VoegSpelerToe(Speler speler)
        {
            Spelers.Add(speler);
        }

        public void VoegSpelerToe(string naam, Rassen ras, string geslacht, int levenspunten, int kracht, int intelligentie, int snelheid, ImageSource avatar)
        {
            Speler speler = new Speler(naam, ras, geslacht, levenspunten, kracht, intelligentie, snelheid);
            Spelers.Add(speler);
        }
        #region Serialisatie
        public void SlaTekstBestandOp()
        {
            List<string[]> opTeSlaan = ZetSpelersOm();
            textFileFunctions.SchrijfListVanArrays(opTeSlaan, tekstBestandLocatie, ";");
        }

        List<string[]> ZetSpelersOm()
        {
            List<string[]> SpelerInfo = new List<string[]>();
            foreach (Speler speler in Spelers)
            {
                string[] spelerArr = new string[7];
                spelerArr[0] = speler.Naam;
                spelerArr[1] = speler.Ras.ToString();
                spelerArr[2] = speler.Geslacht;
                spelerArr[3] = speler.Levenspunten.ToString();
                spelerArr[4] = speler.Kracht.ToString();
                spelerArr[5] = speler.Intelligentie.ToString();
                spelerArr[6] = speler.Snelheid.ToString();

                SpelerInfo.Add(spelerArr);
            }
            return SpelerInfo;
        }
        #endregion
    }
}
