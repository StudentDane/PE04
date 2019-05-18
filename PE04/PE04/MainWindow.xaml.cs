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

namespace PE04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int totaalTeBestedenPunten = 20;
        int levenspunten, kracht, intelligentie, snelheid;
        int bonusLevenspunten, bonusKracht, bonusIntelligentie, bonusSnelheid;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbGeslacht.Items.Add("Man");
            cmbGeslacht.Items.Add("Vrouw");


            foreach(string ras in Enum.GetNames(typeof(Rassen)))
            {
                cmbRas.Items.Add(ras);
            }
        }
        void AttributenToevoegen(string geselecteerdAttribuut)
        {

        }

        void BonusAttributen(Rassen gekozenRas)
        {
            bonusLevenspunten = 3;

            switch (gekozenRas)
            {
                case Rassen.Mens: bonusSnelheid++;
                    break;
                case Rassen.Elf: bonusIntelligentie++;
                    break;
                case Rassen.Dwerg: bonusLevenspunten++;
                    break;
                case Rassen.Ork: bonusKracht++;
                    break;
            }
            if((string) cmbGeslacht.SelectedValue == "Man")
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

        #region Events

        private void CmbGeslacht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool gekozenGeslacht;

            gekozenGeslacht = Convert.ToBoolean(cmbGeslacht.SelectedIndex);
            cmbRas.IsEnabled = true;
            cmbGeslacht.IsEnabled = false;
        }

        private void CmbRas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rassen gekozenRas = (Rassen)Enum.Parse(typeof(Rassen), cmbRas.SelectedItem.ToString());
            BonusAttributen(gekozenRas);
            //Als Ras en Geslacht beide gekozen zijn kan een afbeelding van dat ras en geslacht getoont worden bv mannelijke ork
            cmbRas.IsEnabled = false;
            grdAttributen.IsEnabled = true;
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
    }
}
