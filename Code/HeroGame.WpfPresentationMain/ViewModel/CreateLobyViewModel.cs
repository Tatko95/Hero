using GalaSoft.MvvmLight.Command;
using HeroGame.ModelGame;
using HeroGame.WpfPresentationMain.Proxy;
using HeroGame.WpfPresentationMain.UIElements;
using HeroGame.WpfPresentationMain.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeroGame.WpfPresentationMain.ViewModel
{
    public class CreateLobyViewModel
    {
        #region Fields
        CreateLobyView _createLobyView;
        MainWindowViewModel _mainWindowViewModel;
        #endregion

        #region Constructor
        public CreateLobyViewModel(CreateLobyView view, MainWindowViewModel mainViewModel)
        {
            _mainWindowViewModel = mainViewModel;
            _createLobyView = view;
        }
        #endregion

        #region Command Create
        private RelayCommand _createLobyCommand;

        public RelayCommand CreateLobyCommand
        {
            get { return _createLobyCommand ?? (_createLobyCommand = new RelayCommand(CreateLobyCommandExecute)); }
        }

        private void CreateLobyCommandExecute()
        {
            Loby loby = new Loby(_createLobyView.tbName.Text, _mainWindowViewModel.UserProp, _createLobyView.tbPass.Password);
            try
            {
                _mainWindowViewModel.Proxy.CreateLoby(loby);

                LobyView lobyView = new LobyView();
                LobyViewModel lobyViewModel = new LobyViewModel(lobyView, loby, _mainWindowViewModel);
                lobyView.DataContext = lobyViewModel;

                _mainWindowViewModel.LobyViewModelProp = lobyViewModel;

                _mainWindowViewModel.MainWindowViewProp.Visibility = System.Windows.Visibility.Hidden;
                _createLobyView.Close();
                lobyView.ShowDialog();
                _mainWindowViewModel.MainWindowViewProp.Visibility = System.Windows.Visibility.Visible;
            }
            catch
            {
                new MsgBox("Лобі з таким іменем вже існує").ShowDialog();
                _createLobyView.Close();
            }

            //if (_mainWindowViewModel.Proxy.CreateLoby(loby))
            //{
            //    LobyView lobyView = new LobyView();
            //    LobyViewModel lobyViewModel = new LobyViewModel(lobyView, loby, _mainWindowViewModel);
            //    lobyView.DataContext = lobyViewModel;

            //    _mainWindowViewModel.LobyViewModelProp = lobyViewModel;

            //    _mainWindowViewModel.MainWindowViewProp.Visibility = System.Windows.Visibility.Hidden;
            //    _createLobyView.Close();
            //    lobyView.ShowDialog();
            //    _mainWindowViewModel.MainWindowViewProp.Visibility = System.Windows.Visibility.Visible;
            //}
            //else
            //{
            //    new MsgBox("Лобі з таким іменем вже існує").ShowDialog();
            //    _createLobyView.Close();
            //}
        }
    
        #endregion

        #region Command Cancel
        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(CancelCommandExecute)); }
        }

        private void CancelCommandExecute()
        {
            _createLobyView.Close();
        }
        #endregion

        #region Command Pass
        private RelayCommand _havePassCommand;

        public RelayCommand HavePassCommand
        {
            get { return _havePassCommand ?? (_havePassCommand = new RelayCommand(HavePassCommandExecute)); }
        }

        private void HavePassCommandExecute()
        {
            if ((bool)_createLobyView.cbIsPass.IsChecked)
                _createLobyView.tbPass.IsEnabled = true;
            else
                _createLobyView.tbPass.IsEnabled = false;
        }
        #endregion
    }
}
