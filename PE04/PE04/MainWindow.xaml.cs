using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Utilities.Lib;
using Karakter.Lib.Entities;
using Karakter.Lib.Services;

namespace KarakterCreatie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int totaalTeBestedenPunten = 20;
        bool negeerEvent = true;
        KarakterService beheerKarakters;
        Karakter.Lib.Entities.Karakter huidigKarakter;
        //public List<Karakter> = new List<Karakter>();

        #region Startsituatie
        public MainWindow()
        {
            InitializeComponent();
            beheerKarakters = new KarakterService();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartSituatie();
        }
        private void StartSituatie()
        {
            cmbGeslacht.Items.Add("Man");
            cmbGeslacht.Items.Add("Vrouw");

            foreach (string ras in Enum.GetNames(typeof(Rassen)))
            {
                cmbRas.Items.Add(ras);
            }
            cmbRas.IsEnabled = false;
            grdBasisGegevens.Visibility = Visibility.Hidden;
            grdAttributen.Visibility = Visibility.Hidden;
            grdAchtergrond.Visibility = Visibility.Hidden;
            imgAvatar.Visibility = Visibility.Hidden;
            btnBevestig.IsEnabled = false;
        }
        #endregion
        #region Attributen
        void AttributenToevoegen(TextBox ingevuldAttribuut)
        {
            if (negeerEvent)
            {
                int besteeddePunten;
                besteeddePunten = Convert.ToInt16(ingevuldAttribuut.Text);
                if (totaalTeBestedenPunten - besteeddePunten >= 0)
                {
                    totaalTeBestedenPunten -= besteeddePunten;
                    ingevuldAttribuut.IsEnabled = false;
                    lblBeschikbarePunten.Content = totaalTeBestedenPunten + " beschikbare punten!";
                    txtFeedback.Text = "Je hebt nog beschikbare punten om te besteden";
                }
                else if (totaalTeBestedenPunten == 0)
                {
                    txtFeedback.Text = "Je hebt al je punten besteed, druk op bevestigen";
                }
                else
                {
                    txtFeedback.Text = "Je hebt niet genoeg punten om te besteden";
                    btnBevestigAttributen.IsEnabled = false;
                }
            }
        }

        void BonusAttributen(Rassen gekozenRas)
        {
            int bonusLevenspunten = 3;
            int bonusKracht = 0;
            int bonusIntelligentie = 0;
            int bonusSnelheid = 0;

            switch (gekozenRas)
            {
                case Rassen.Mens:
                    bonusSnelheid++;
                    break;
                case Rassen.Elf:
                    bonusIntelligentie++;
                    break;
                case Rassen.Dwerg:
                    bonusLevenspunten++;
                    break;
                case Rassen.Ork:
                    bonusKracht++;
                    break;
            }
            if ((string)cmbGeslacht.SelectedValue == "Man")
            {
                bonusKracht++;
            }
            else
            {
                bonusSnelheid++;
            }
            txtBonusLevensPunten.Text = "+ " + bonusLevenspunten;
            txtBonusKracht.Text = "+ " + bonusKracht;
            txtBonusIntelligentie.Text = "+ " + bonusIntelligentie;
            txtBonusSnelheid.Text = "+ " + bonusSnelheid;
        }
        #endregion
        #region Images
        private void SelecteerAvatar()
        {
            Image avatar = new Image();
            string gekozenGeslacht, gekozenRas;
            gekozenRas = cmbRas.SelectedValue.ToString();
            gekozenGeslacht = cmbGeslacht.SelectedValue.ToString();

            avatar.Source = new BitmapImage(new Uri(@"\Images\"+ gekozenRas + gekozenGeslacht + ".jpg", UriKind.Relative));

            imgAvatar.Source = avatar.Source;
            imgAvatar.Visibility = Visibility.Visible;
        }
        #endregion
        #region Welkoms Menu
        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            WelkomsMenu.Visibility = Visibility.Hidden;
            grdBasisGegevens.Visibility = Visibility.Visible;
            txtVoornaam.Focus();
        }

        private void BtnBestaand_Click(object sender, RoutedEventArgs e)
        {
            lstKiesKarakter.Visibility = Visibility.Visible;
        }
        #endregion

        #region Events
        private void TxtLevensPunten_TextChanged(object sender, RoutedEventArgs e)
        {
            AttributenToevoegen(txtLevensPunten);
        }
        private void TxtKracht_TextChanged(object sender, RoutedEventArgs e)
        {
            AttributenToevoegen(txtKracht);
        }
        private void TxtSnelheid_TextChanged(object sender, RoutedEventArgs e)
        {
            AttributenToevoegen(txtSnelheid);
        }
        private void TxtIntelligentie_TextChanged(object sender, RoutedEventArgs e)
        {
            AttributenToevoegen(txtIntelligentie);
        }

        private void BtnBevestigAttributen_Click(object sender, RoutedEventArgs e)
        {
            if (totaalTeBestedenPunten == 0)
            {
                txtFeedback.Text = "Je hebt alle punten besteed";
                grdAttributen.IsEnabled = false;
                grdAttributen.Background = new SolidColorBrush(Colors.LightGreen);
                grdAchtergrond.Visibility = Visibility.Visible;
                txtVerhaal.Focus();
            }
            else if (totaalTeBestedenPunten > 0)
            {
                txtFeedback.Text = "Je had nog " + totaalTeBestedenPunten + " punten om te besteden, probeer opnieuw";
                btnOpnieuwAttributen.Focus();
            }
        }

        private void BtnBevestig_Click(object sender, RoutedEventArgs e)
        {
            if (txtVoornaam.Text != "")
            {
                grdBasisGegevens.IsEnabled = false;
                grdBasisGegevens.Background = new SolidColorBrush(Colors.LightGreen);
                grdAttributen.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Vul een Voornaam in");
            }
        }

        private void BtnOpnieuw_Click(object sender, RoutedEventArgs e)
        {
            negeerEvent = false;
            GuiFunctions.ClearPanel(grdClearable);
            cmbGeslacht.IsEnabled = true;
            btnBevestig.IsEnabled = false;
            negeerEvent = true;
        }

        private void BtnOpnieuwAttributen_Click(object sender, RoutedEventArgs e)
        {
            negeerEvent = false;
            GuiFunctions.ClearTextBoxes(grdClearableAttributen, true);
            totaalTeBestedenPunten = 20;
            lblBeschikbarePunten.Content = totaalTeBestedenPunten + " beschikbare punten!";
            negeerEvent = true;
        }

        private void CmbGeslacht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (negeerEvent)
            {
                cmbGeslacht.IsEnabled = false;
                cmbRas.IsEnabled = true;
            }
        }

        private void BtnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            string naam;
            Rassen ras;
            string geslacht;
            int levenspunten;
            int kracht;
            int intelligentie;
            int snelheid;

            naam = txtVoornaam.Text + " " + txtAchterNaam.Text;
            ras = (Rassen) cmbRas.SelectedValue;
            geslacht = cmbGeslacht.SelectedValue.ToString();
            levenspunten = int.Parse(txtLevensPunten.Text + txtBonusLevensPunten.Text);
            kracht = int.Parse(txtKracht.Text + txtBonusKracht.Text);
            intelligentie = int.Parse(txtIntelligentie.Text + txtBonusIntelligentie.Text);
            snelheid = int.Parse(txtSnelheid.Text + txtBonusSnelheid.Text);


            /*Karakter(string naam, Rassen ras, string geslacht, int levenspunten, int kracht, int intelligentie, int snelheid)
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
            }*/


        }

        private void CmbRas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (negeerEvent)
            {
                Rassen gekozenRas = (Rassen)Enum.Parse(typeof(Rassen), cmbRas.SelectedItem.ToString());
                BonusAttributen(gekozenRas);
                SelecteerAvatar();
                cmbRas.IsEnabled = false;
                btnBevestig.IsEnabled = true;
                btnBevestig.Focus();
            }
        }

        private void TxtVerhaal_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtVoorbeeld.Visibility = Visibility.Visible;
            if (txtVerhaal.Text.Length > 0)
            {
                txtVoorbeeld.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Control User Input

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}

