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

namespace RM
{
    /// <summary>
    /// Взаимодействие с Диалоговым окном.(AbautWindowBox.xaml)
    /// </summary>
    public partial class AboutWindowBox : Window
    {
        public AboutWindowBox()
        {
            InitializeComponent();
            this.AboutText.Text = Util.GetLanguageResources.GetString("About_Text", Util.Cult);
            this.Ok_Button.Content = Util.GetLanguageResources.GetString("roomFinishes_OK_Button", Util.Cult);
            this.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
