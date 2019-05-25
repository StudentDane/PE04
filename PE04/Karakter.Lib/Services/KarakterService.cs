using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karakter.Lib.Entities;
using Utilities.Lib;

namespace Karakter.Lib.Services
{
    public class KarakterService
    {
        private TextFileFunctions textFileFunctions;
        string tekstBestandLocatie = TextFileFunctions.rootPad + "Karakters.csv";
        /*public List<Karakter> Karakters { get; set; }

        public KarakterService()
        {
            Karakters = new List<Karakter>();
            MaakKaraktersAan();
        }
        private void MaakKaraktersAan()
        {
            //VoegKarakterToe();
        }
            void VoegKarakterToe(Karakter karakter)
        {
            Karakters.Add(karakter);
        }*/

        /*public void SlaTekstBestandOp()
        {
            List<string[]> opTeSlaan = ZetKaraktersOm();
            textFileFunctions.SchrijfListVanArrays(opTeSlaan, tekstBestandLocatie, ";");
        }*/

        /*List<string[]> ZetKaraktersOm()
        {
            List<string[]> personeelsLedenArr = new List<string[]>();
            foreach (Karakter karakter in KarakterService)
            {
                string[] personeelsLidArr = new string[5];
                personeelsLidArr[ID] = personeelsLid.Id.ToString();
                personeelsLidArr[FAMILIE_NAAM] = personeelsLid.FamilieNaam;
                personeelsLidArr[VOOR_NAAM] = personeelsLid.Voornaam;
                personeelsLidArr[GESLACHT] = ((int)personeelsLid.Geslacht).ToString();
                personeelsLidArr[FUNCTIE_KLASSE] = ((int)personeelsLid.FunctieKlasse).ToString();
                personeelsLedenArr.Add(personeelsLidArr);
            }
            return personeelsLedenArr;
        }*/

        /*void VoegToe(Karakter toeTeVoegen)
        {
            KarakterService.Add(toeTeVoegen);
        }*/
    }
}
