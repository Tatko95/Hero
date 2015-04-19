using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeroGame.ModelGame;
using HeroGame.WpfPresentationMain.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeroGame.WpfPresentationMain.ViewModel
{
    public class LobyViewModel : ViewModelBase
    {
        #region Fields
        public LobyView _lobyView;
        public Loby _loby;
        MainWindowViewModel _mainWindowViewModel;
        #endregion

        #region Properties
        public string Creater
        {
            get { return _loby.Creater.NickName; }
        }
        public string Enemy
        {
            get 
            {
                if (_loby.Enemy != null)
                    return _loby.Enemy.NickName;
                return "";
            }
        }
        public string NameLoby
        { get { return _loby.Name; } }
        public string PassLoby
        { get { return _loby.Password; } }
        public bool CanIsStart
        {
            get
            {
                if (_loby.Enemy != null)
                    if (_mainWindowViewModel.UserProp.Equals(_loby.Creater))
                    {
                        if (!_loby.IsReadyCreater)
                            return true;
                    }
                    else
                        if (!_loby.IsReadyEnemy)
                            return true;
                return false;
            }
            set 
            {
                CanIsStart = value;
                RaisePropertyChanged("CanIsStart");
            }
        }
        public Visibility IsReadyCreater
        {
            get
            {
                if (_loby.IsReadyCreater)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
        }
        public Visibility IsReadyEnemy
        {
            get
            {
                if (_loby.IsReadyEnemy)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
        }
        #endregion

        #region Construstor
        public LobyViewModel(LobyView lobyView, Loby loby, MainWindowViewModel mainWindowViewModel)
        {
            _lobyView = lobyView;
            _loby = loby;
            RaisePropertyChanged("Creater");
            RaisePropertyChanged("CanIsStart");
            _mainWindowViewModel = mainWindowViewModel;
            _lobyView.Closed += _lobyView_Closed;
        }

        void _lobyView_Closed(object sender, EventArgs e)
        {
            if (_loby.Creater.Equals(_mainWindowViewModel.UserProp))
                _mainWindowViewModel.Proxy.DropLoby(_loby);
            else
                _mainWindowViewModel.Proxy.LeaveLoby(_loby);
        }
        #endregion

        #region public Methods
        public void Refresh()
        {
            RaisePropertyChanged("CanIsStart");
            RaisePropertyChanged("Enemy");
            RaisePropertyChanged("Creater");
            RaisePropertyChanged("IsReadyCreater");
            RaisePropertyChanged("IsReadyEnemy");
        }
        #endregion

        #region Command Start
        private RelayCommand _startCommand;

        public RelayCommand StartCommand
        {
            get { return _startCommand ?? (_startCommand = new RelayCommand(StartCommandExecute)); }
        }

        private void StartCommandExecute()
        {
            //_lobyView.btnStart.IsEnabled = false;

            if (_loby.Creater.Equals(_mainWindowViewModel.UserProp))
                _mainWindowViewModel.Proxy.IsReady(_loby, true);
            else
                _mainWindowViewModel.Proxy.IsReady(_loby, false);
            RaisePropertyChanged("CanIsStart");
        }
        #endregion
    }
}
