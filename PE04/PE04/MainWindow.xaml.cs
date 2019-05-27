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

        #region Startsituatie
        public MainWindow()
        {
            InitializeComponent();
            beheerKarakters = new KarakterService();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartSituatie();
            beheerKarakters.MaakSpelersAan();
        }

        private void StartSituatie()
        {
            cmbGeslacht.Items.Add("Man");
            cmbGeslacht.Items.Add("Vrouw");
            cmbRas.ItemsSource = Enum.GetValues(typeof(Rassen));

            grdBasisGegevens.Visibility = Visibility.Hidden;
            grdAttributen.Visibility = Visibility.Hidden;
            grdAchtergrond.Visibility = Visibility.Hidden;
            imgAvatar.Visibility = Visibility.Hidden;
            btnBevestig.IsEnabled = false;
            btnStart.IsEnabled = false;
            cmbRas.IsEnabled = false;
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
                ingevuldAttribuut.IsEnabled = false;
            }
        }

        void BonusAttributen(Rassen gekozenRas)
        {
            int bonusLevenspunten = 5;
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
            txtBonusLevensPunten.Text = bonusLevenspunten.ToString();
            txtBonusKracht.Text = bonusKracht.ToString();
            txtBonusIntelligentie.Text = bonusIntelligentie.ToString();
            txtBonusSnelheid.Text = bonusSnelheid.ToString();
        }
        #endregion

        #region Speler
        void SpelerAanmaken()
        {
            string naam = txtVoornaam.Text + " " + txtAchterNaam.Text;
            Rassen ras = (Rassen)cmbRas.SelectedItem;
            string geslacht = cmbGeslacht.SelectedValue.ToString();
            int levenspunten = int.Parse(txtLevensPunten.Text + txtBonusLevensPunten.Text);
            int kracht = int.Parse(txtKracht.Text + txtBonusKracht.Text);
            int intelligentie = int.Parse(txtIntelligentie.Text + txtBonusIntelligentie.Text);
            int snelheid = int.Parse(txtSnelheid.Text + txtBonusSnelheid.Text);
            ImageSource avatar = SelecteerAvatar(geslacht, ras.ToString());

            Speler speler = new Speler(naam, ras, geslacht, levenspunten, kracht, intelligentie, snelheid);
            beheerKarakters.VoegSpelerToe(speler);

            StartSpel(speler);
        }

        public void StartSpel(Speler speler)
        {
            /*Spel spel = new Spel(speler);
            spel.Show();*/
            Console.WriteLine("Pas dit aan wanneer je de references hebt toegevoegd kyle");
        }

        public ImageSource SelecteerAvatar(string geslacht, string ras)
        {
            ImageSource avatar = null;
            ras = cmbRas.SelectedValue.ToString();
            geslacht = cmbGeslacht.SelectedValue.ToString();
            avatar = new BitmapImage(new Uri(@"\Images\" + ras + geslacht + ".jpg", UriKind.Relative));
            return avatar;
        }

        void PasAvatarAan()
        {
            string ras, geslacht;

            ras = cmbRas.SelectedValue.ToString();
            geslacht = cmbGeslacht.SelectedValue.ToString();

            imgAvatar.Source = SelecteerAvatar(ras, geslacht);
            imgAvatar.Visibility = Visibility.Visible;

        }

        void SpelerWegschrijven()
        {
            beheerKarakters.SlaTekstBestandOp();
        }
        #endregion

        #region Welkoms Menu Events
        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            WelkomsMenu.Visibility = Visibility.Hidden;
            grdBasisGegevens.Visibility = Visibility.Visible;
            txtVoornaam.Focus();
        }

        private void BtnBestaand_Click(object sender, RoutedEventArgs e)
        {
            foreach (Speler speler in beheerKarakters.Spelers)
            {
                lstKiesKarakter.Items.Add(speler);
            }
            lstKiesKarakter.Visibility = Visibility.Visible;

        }

        private void LstKiesKarakter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnStart.IsEnabled = true;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            StartSpel((Speler) lstKiesKarakter.SelectedItem);
        }
        #endregion

        #region Karakter Creatie Events
        private void TxtLevensPunten_TextChanged(object sender, RoutedEventArgs e)
        {
            AttributenToevoegen(txtLevensPunten);
            txtKracht.Focus();
        }
        private void TxtKracht_TextChanged(object sender, RoutedEventArgs e)
        {
            AttributenToevoegen(txtKracht);
            txtIntelligentie.Focus();
        }
        private void TxtIntelligentie_TextChanged(object sender, RoutedEventArgs e)
        {
            AttributenToevoegen(txtIntelligentie);
            txtSnelheid.Focus();
        }
        private void TxtSnelheid_TextChanged(object sender, RoutedEventArgs e)
        {
            AttributenToevoegen(txtSnelheid);
            btnBevestigAttributen.Focus();
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
                txtLevensPunten.Focus();
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
            imgAvatar.Source = null;
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
            SpelerAanmaken();
            SpelerWegschrijven();
        }

        private void CmbRas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (negeerEvent)
            {
                Rassen gekozenRas = (Rassen)Enum.Parse(typeof(Rassen), cmbRas.SelectedItem.ToString());
                BonusAttributen(gekozenRas);

                PasAvatarAan();
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

