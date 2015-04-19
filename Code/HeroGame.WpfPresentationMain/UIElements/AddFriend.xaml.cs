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

namespace HeroGame.WpfPresentationMain.UIElements
{
    /// <summary>
    /// Логика взаимодействия для AddFriend.xaml
    /// </summary>
    public partial class AddFriend : Window
    {
        MainWindowViewModel _mainWindowViewModel;

        public AddFriend(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbNickName.Text == _mainWindowViewModel.UserProp.NickName)
            {
                new MsgBox("Eror").ShowDialog();
                this.Close();
                return;
            }
            User returnUser = _mainWindowViewModel.Proxy.AddFriend(tbNickName.Text, _mainWindowViewModel.UserProp);
            if (returnUser != null)
            {
                new MsgBox("Ok").ShowDialog();
                _mainWindowViewModel.UserProp.Friends.Add(returnUser);
                _mainWindowViewModel.RefreshAllFriend(_mainWindowViewModel.UserProp);
            }
            else
                new MsgBox("Not found").ShowDialog();
            this.Close();
        }
    }
}
