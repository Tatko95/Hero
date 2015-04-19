using GalaSoft.MvvmLight.Command;
using HeroGame.WpfPresentationMain.Proxy;
using HeroGame.WpfPresentationMain.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HeroGame.WpfPresentationMain.UIElements;
using HeroGame.ModelGame;

namespace HeroGame.WpfPresentationMain.ViewModel
{
    public class AvtorisationViewModel
    {
        #region Fields
        private AvtorisationView _avtView;
        #endregion

        #region Constructor
        public AvtorisationViewModel(AvtorisationView autView)
        {
            _avtView = autView;
        }
        #endregion

        #region Command Login
        private RelayCommand _loginCommand;

        public RelayCommand LoginCommand
        {
            get { return _loginCommand ?? (_loginCommand = new RelayCommand(LoginCommandExecute)); }
        }

        private void LoginCommandExecute()
        {
            AvtorisationProxy avtorisationProxy = new AvtorisationProxy();
            try
            {
                User user = avtorisationProxy.Avtorisation(_avtView.tbNickName.Text, _avtView.tbPassword.Password);
                MainWindowView mainWindowView = new MainWindowView();
                MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(mainWindowView, user);
                mainWindowView.DataContext = mainWindowViewModel;

                _avtView.Visibility = Visibility.Hidden;
                mainWindowView.ShowDialog();
                _avtView.Close();
            }
            catch (Exception ex)
            {
                MsgBox ms = new MsgBox(ex.Message);
                ms.ShowDialog();
            }
            
            //if (user != null)
            //{
                
            //}
            //else 
            //{
            //    MsgBox ms = new MsgBox("Користувача з таким ім'ям не знайдено, або не вірний пароль");
            //    ms.ShowDialog();
            //}
        }
        #endregion

        #region Command GoRegistration
        private RelayCommand _goRegistrationCommand;

        public RelayCommand GoRegistrationCommand
        {
            get { return _goRegistrationCommand ?? (_goRegistrationCommand = new RelayCommand(GoRegistrationExecute)); }
        }

        private void GoRegistrationExecute()
        {
            _avtView.Visibility = Visibility.Hidden;
            RegistrationView regView = new RegistrationView();
            RegistrationViewModel regViewModel = new RegistrationViewModel(regView);
            regView.DataContext = regViewModel;
            regView.ShowDialog();
            _avtView.Visibility = Visibility.Visible;
        }
        #endregion
    }
}
