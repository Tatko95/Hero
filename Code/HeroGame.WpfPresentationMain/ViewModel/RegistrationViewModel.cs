using GalaSoft.MvvmLight.Command;
using HeroGame.ILibrary;
using HeroGame.WpfPresentationMain.Proxy;
using HeroGame.WpfPresentationMain.UIElements;
using HeroGame.WpfPresentationMain.View;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Media;

namespace HeroGame.WpfPresentationMain.ViewModel
{
    public class RegistrationViewModel
    {
        #region Fields
        private RegistrationView _registrationView;
        #endregion

        #region Costructor
        public RegistrationViewModel(RegistrationView view)
        {
            _registrationView = view;
        }
        #endregion

        #region Command Registration
        private RelayCommand _registrationCommand;

        public RelayCommand RegistrationCommand
        {
            get { return _registrationCommand ?? (_registrationCommand = new RelayCommand(RegistrationExecute)); }
        }

        private void RegistrationExecute()
        {
            AvtorisationProxy avtorisationProxy = new AvtorisationProxy();
            if (_registrationView.tbLogin.Text == string.Empty || _registrationView.tbPass.Password == string.Empty)
            {
                new MsgBox("Не всі поля заповнені").ShowDialog();
                return;
            }
            if (avtorisationProxy.Registration(_registrationView.tbLogin.Text, _registrationView.tbPass.Password))
            {
                new MsgBox("Ви вдало зареєстровані").ShowDialog();
                _registrationView.Close();
            }
            else
            {
                _registrationView.tbEror.Visibility = System.Windows.Visibility.Visible;
                _registrationView.tbLogin.Background = Brushes.Red;
            }
        }
        #endregion
    }
}
