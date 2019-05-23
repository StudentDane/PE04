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
        int bonusLevenspunten, bonusKracht, bonusIntelligentie, bonusSnelheid;
        bool negeerEvent;
        KarakterService beheerKarakters;
        //public List<Karakter> = new List<Karakter>();


        public MainWindow()
        {
            InitializeComponent();
            beheerKarakters = new KarakterService();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbGeslacht.Items.Add("Man");
            cmbGeslacht.Items.Add("Vrouw");
            
            foreach (string ras in Enum.GetNames(typeof(Rassen)))
            {
                cmbRas.Items.Add(ras);
            }
            grdBasisGegevens.Visibility = Visibility.Hidden;
            grdAttributen.Visibility = Visibility.Hidden;
            grdAchtergrond.Visibility = Visibility.Hidden;
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

        private void SelecteerAvatar()
        {
            bool geslacht;
            Rassen gekozenRas;

            geslacht = Convert.ToBoolean(cmbGeslacht.SelectedIndex);
            gekozenRas = (Rassen) cmbRas.SelectedIndex;

            switch (gekozenRas)
            {
                case Rassen.Mens: //Images met verkort path laden vanuit de Images map lukt niet, ik vrees dat de images niet zullen laden op andere pc's.
                    if (geslacht) imgAvatar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"C:\Users\dane_\OneDrive\Programmeren 1\PE04\PE04\Karakter.Lib\Images\HumanGirl.png");
                    else imgAvatar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"C:\Users\dane_\OneDrive\Programmeren 1\PE04\PE04\Karakter.Lib\Images\HumanMale.jpg");
                    break;
                case Rassen.Elf:
                    if (geslacht) imgAvatar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"C:\Users\dane_\OneDrive\Programmeren 1\PE04\PE04\Karakter.Lib\Images\ElfGirl.jpg");
                    else imgAvatar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"C:\Users\dane_\OneDrive\Programmeren 1\PE04\PE04\Karakter.Lib\Images\ElfMale.jpg");
                    break;
                case Rassen.Dwerg:
                    if (geslacht) imgAvatar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"C:\Users\dane_\OneDrive\Programmeren 1\PE04\PE04\Karakter.Lib\Images\DwarfGirl.jpg");
                    else imgAvatar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"C:\Users\dane_\OneDrive\Programmeren 1\PE04\PE04\Karakter.Lib\Images\DwarfMale.jpg");
                    break;
                case Rassen.Ork:
                    if (geslacht) imgAvatar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"C:\Users\dane_\OneDrive\Programmeren 1\PE04\PE04\Karakter.Lib\Images\OrkGirl.jpg");
                    else imgAvatar.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(@"C:\Users\dane_\OneDrive\Programmeren 1\PE04\PE04\Karakter.Lib\Images\OrcMale.jpg");
                    break;
                default:
                    break;
            }
        }

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

            
    }

        private void CmbRas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!negeerEvent)
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

