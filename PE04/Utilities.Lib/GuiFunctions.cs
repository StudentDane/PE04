using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Utilities.Lib
{
    public class GuiFunctions
    {
        /*public static void EnabledSchakelaar(bool enable, Panel toClear)
        {
            if(enable)
            foreach (object control in toClear.Children)
            {
                Console.WriteLine(control.ToString());
                control.isEnabled = true;
            }
            else
            {
                Console.WriteLine(control.ToString());
                control.isEnabled = false;
            }

        }*/

        public static void ClearTextBoxes(Panel toClear, bool enableBoxes)
        {
            foreach (object control in toClear.Children)
            {
                Console.WriteLine(control.ToString());
                if (control is TextBox)
                {
                    TextBox castedControl = (TextBox)control;
                    castedControl.Text = string.Empty;
                    if (enableBoxes) castedControl.IsEnabled = true;
                }
                else if (control is Panel)
                {
                    Panel castedControl = (Panel)control;
                    ClearTextBoxes(castedControl, enableBoxes);
                }
            }

        }

        public static void ClearPanel(Panel toClear)
        {
            foreach (object control in toClear.Children)
            {
                Console.WriteLine(control.ToString());
                if (control is TextBox)
                {
                    TextBox castedControl = (TextBox)control;
                    castedControl.Text = string.Empty;
                }
                else if (control is Label)
                {
                    Label castedControl = (Label)control;
                    if (castedControl.Name.Length > 0)
                    {
                        castedControl.Content = "";
                    }
                }
                else if (control is TextBlock)
                {
                    TextBlock castedControl = (TextBlock)control;
                    if (castedControl.Name.Length > 0)
                    {
                        castedControl.Text = "";
                    }
                }
                else if (control is Selector)
                {
                    Selector castedControl = (Selector)control;
                    castedControl.SelectedIndex = -1;
                }
                else if (control is DatePicker)
                {
                    DatePicker castedControl = (DatePicker)control;
                    castedControl.SelectedDate = DateTime.Today;
                }
                else if (control is Slider)
                {
                    Slider castedControl = (Slider)control;
                    castedControl.Value = castedControl.Minimum;
                }
                else if (control is CheckBox)
                {
                    CheckBox castedControl = (CheckBox)control;
                    castedControl.IsChecked = false;
                }
                else if(control is Panel)
                {
                    Panel castedControl = (Panel)control;
                    ClearPanel(castedControl);
                }
            }

        }
       
    }
}

