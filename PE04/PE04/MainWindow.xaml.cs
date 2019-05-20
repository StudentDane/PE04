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

namespace PE04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int totaalTeBestedenPunten = 20;
        /*int levenspunten, kracht, intelligentie, snelheid;*/
        int bonusLevenspunten, bonusKracht, bonusIntelligentie, bonusSnelheid;
        bool negeerEvent;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbGeslacht.Items.Add("Man");
            cmbGeslacht.Items.Add("Vrouw");


            foreach (string ras in Enum.GetNames(typeof(Rassen)))
            {
                cmbRas.Items.Add(ras);
            }
            txtVoornaam.Focus();
            btnBevestig.IsEnabled = false;
        }
        #region Attributen
        void AttributenToevoegen(TextBox ingevuldAttribuut)
        {
            if (!negeerEvent)
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
                else
                {
                    txtFeedback.Text = "Je hebt niet genoeg punten om te besteden";
                    btnBevestigAttributen.IsEnabled = false;
                }
            }
        }

        void BonusAttributen(Rassen gekozenRas)
        {
            bonusLevenspunten = 3;

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

        #region Welkoms Menu
        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            WelkomsMenu.Visibility = Visibility.Hidden;
            grdBasisGegevens.Visibility = Visibility.Visible;
        }

        private void BtnBestaand_Click(object sender, RoutedEventArgs e)
        {
            lstKiesKarakter.Visibility = Visibility.Visible;
        }
        #endregion

        #region Karakter Aanmaken Events
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
            grdBasisGegevens.IsEnabled = false;
            grdBasisGegevens.Background = new SolidColorBrush(Colors.LightGreen);
            grdAttributen.Visibility = Visibility.Visible;
        }

        private void BtnOpnieuw_Click(object sender, RoutedEventArgs e)
        {
            negeerEvent = true;
            GuiFunctions.ClearPanel(grdClearable);
            cmbRas.IsEnabled = true;
            cmbGeslacht.IsEnabled = true;
            btnBevestig.IsEnabled = false;
            negeerEvent = false;
        }

        private void BtnOpnieuwAttributen_Click(object sender, RoutedEventArgs e)
        {
            negeerEvent = true;
            GuiFunctions.ClearTextBoxes(grdClearableAttributen);
            totaalTeBestedenPunten = 20;
            lblBeschikbarePunten.Content = totaalTeBestedenPunten + " beschikbare punten!";
            /*grdClearableAttributen.IsEnabled = true;*/
            txtLevensPunten.IsEnabled = true;
            txtKracht.IsEnabled = true;
            txtIntelligentie.IsEnabled = true;
            txtSnelheid.IsEnabled = true;
            negeerEvent = false;
        }

        private void CmbGeslacht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbGeslacht.IsEnabled = false;
        }

        private void CmbRas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!negeerEvent)
            {
                Rassen gekozenRas = (Rassen)Enum.Parse(typeof(Rassen), cmbRas.SelectedItem.ToString());
                BonusAttributen(gekozenRas);
                //Als Ras en Geslacht beide gekozen zijn kan een afbeelding van dat ras en geslacht getoont worden bv mannelijke ork
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

