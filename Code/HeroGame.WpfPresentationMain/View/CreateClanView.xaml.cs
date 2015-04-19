using HeroGame.ModelGame;
using HeroGame.WpfPresentationMain.ViewModel;
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
using System.Windows.Shapes;

namespace HeroGame.WpfPresentationMain.View
{
    /// <summary>
    /// Логика взаимодействия для CreateClanView.xaml
    /// </summary>
    public partial class CreateClanView : Window
    {
        private MainWindowViewModel _mainWindowViewModel;

        public CreateClanView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _mainWindowViewModel.Proxy.AddClan(new Clan(this.tbName.Text, _mainWindowViewModel.UserProp, null));
        }
    }
}
