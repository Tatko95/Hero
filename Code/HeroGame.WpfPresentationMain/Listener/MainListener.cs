using GalaSoft.MvvmLight;
using HeroGame.ILibrary;
using HeroGame.ModelGame;
using HeroGame.WpfPresentationGame;
using HeroGame.WpfPresentationMain.View;
using HeroGame.WpfPresentationMain.ViewModel;
using System;

namespace HeroGame.WpfPresentationMain.Listener
{
    public class MainListener : ICallBackMain
    {
        #region Fields
        private MainWindowViewModel _mainWindowViewModel;
        #endregion

        #region Constructor
        public MainListener(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
        #endregion

        #region Chat
        public void Say(string msg, User fromUser)
        {
            _mainWindowViewModel.MainWindowViewProp.lbChat.Items.Add("[" + DateTime.Now.Hour.ToString() +":" + DateTime.Now.Minute.ToString() +"][" + fromUser.NickName + "]" + " " + msg.ToString());
            _mainWindowViewModel.MainWindowViewProp.lbChat.ScrollIntoView("[" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + "][" + fromUser.NickName + "]" + " " + msg.ToString());
        }
        #endregion

        #region User
        public void JoinUser(User user)
        {
            _mainWindowViewModel.MainWindowViewProp.lbUsers.Items.Add(user);
            _mainWindowViewModel.MainWindowViewProp.lbUsers.ScrollIntoView(user);
        }
        public void LeaveUser(User user)
        {
            _mainWindowViewModel.MainWindowViewProp.lbUsers.Items.Remove(user);
        }
        #endregion

        #region Loby
        public void AddLoby(Loby loby)
        {
            _mainWindowViewModel.MainWindowViewProp.lbLoby.Items.Add(loby);
            _mainWindowViewModel.MainWindowViewProp.lbLoby.ScrollIntoView(loby);
        }
        public void DropLoby(Loby loby, bool isPlayer)
        {
            if (isPlayer)
            {
                _mainWindowViewModel.LobyViewModelProp._lobyView.Close();
                _mainWindowViewModel.LobyViewModelProp = null;
                return;
            }
            _mainWindowViewModel.MainWindowViewProp.lbLoby.Items.Remove(loby);
        }
        public void RefreshLoby(Loby loby)
        {
            _mainWindowViewModel.LobyViewModelProp._loby = loby;
            _mainWindowViewModel.LobyViewModelProp.Refresh();
        }
        #endregion

        public void StartGame(Game game, User you)
        {
            _mainWindowViewModel.LobyViewModelProp._lobyView.Visibility = System.Windows.Visibility.Hidden;
            StartNewGame.StartNewGameGo(game, you);
            _mainWindowViewModel.LobyViewModelProp._lobyView.Close();
        }
    }
}
