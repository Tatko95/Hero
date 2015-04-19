using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeroGame.ModelGame;
using HeroGame.WpfPresentationMain.Proxy;
using HeroGame.WpfPresentationMain.UIElements;
using HeroGame.WpfPresentationMain.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.WpfPresentationMain.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructor
        public MainWindowViewModel(MainWindowView mainWindowView, User user)
        {
            MainWindowViewProp = mainWindowView;
            UserProp = user;
            Proxy = new MainProxy(this);
            Proxy.SubscribeToCallBack(user);
            MainWindowViewProp.Closed += _mainWindowView_Closed;
        }

        void _mainWindowView_Closed(object sender, EventArgs e)
        {
            Proxy.UnsubscribeToCallBack(UserProp);
        }
        #endregion

        #region Properties
        public MainWindowView MainWindowViewProp { get; set; }
        public User UserProp { get; set; }
        public MainProxy Proxy { get; set; }
        public LobyViewModel LobyViewModelProp { get; set; }
        #endregion

        #region ShowProperties
        public string YouNickname { get { return UserProp.NickName; } }
        #endregion

        #region Command Chat
        private RelayCommand _chatCommand;

        public RelayCommand ChatCommand
        {
            get { return _chatCommand ?? (_chatCommand = new RelayCommand(ChatCommandExecute)); }
        }

        private void ChatCommandExecute()
        {
            Proxy.Say(MainWindowViewProp.tbMsg.Text, UserProp);
            MainWindowViewProp.lbChat.ScrollIntoView(MainWindowViewProp.tbMsg.Text);
            MainWindowViewProp.tbMsg.Text = "";
            MainWindowViewProp.tbMsg.Focus();
        }
        #endregion

        #region Command AddLoby
        private RelayCommand _addLobyCommand;

        public RelayCommand AddLobyCommand
        {
            get { return _addLobyCommand ?? (_addLobyCommand = new RelayCommand(AddLobyCommandExecute)); }
        }

        private void AddLobyCommandExecute()
        {
            CreateLobyView createView = new CreateLobyView();
            CreateLobyViewModel createViewModel = new CreateLobyViewModel(createView, this);
            createView.DataContext = createViewModel;
            createView.ShowDialog();
        }
        #endregion

        #region Command ConnectLoby
        private RelayCommand _connectLobyCommand;

        public RelayCommand ConnectLobyCommand
        {
            get { return _connectLobyCommand ?? (_connectLobyCommand = new RelayCommand(ConnectLobyCommandExecute)); }
        }

        private void ConnectLobyCommandExecute()
        {
            Loby selectedLoby = (Loby)MainWindowViewProp.lbLoby.SelectedItem;
            if (selectedLoby == null)
            {
                new MsgBox("Don`t select loby!").ShowDialog();
                return;
            }
            if (Proxy.IsCanConnect(selectedLoby))
            {
                if (selectedLoby.Password != "")
                {
                    CheckPass checkPass = new CheckPass(selectedLoby);
                    checkPass.ShowDialog();
                    if (checkPass.IsTrue)
                        Connect(selectedLoby);
                }
                else
                    Connect(selectedLoby);
            }
            else
                new MsgBox("Лобі зайняте").ShowDialog();
        }

        private void Connect(Loby selectedLoby)
        {
            LobyView lobyView = new LobyView();
            LobyViewModel lobyViewModel = new LobyViewModel(lobyView, selectedLoby, this);
            lobyView.DataContext = lobyViewModel;
            LobyViewModelProp = lobyViewModel;

            Proxy.ConnectToLoby(selectedLoby, UserProp);

            MainWindowViewProp.Visibility = System.Windows.Visibility.Hidden;
            lobyView.ShowDialog();
            MainWindowViewProp.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion

        #region Command AddFriend
        private RelayCommand _addFriendCommand;

        public RelayCommand AddFriendCommand
        {
            get { return _addFriendCommand ?? (_addFriendCommand = new RelayCommand(AddFriendCommandExecute)); }
        }

        private void AddFriendCommandExecute()
        {
            new AddFriend(this).ShowDialog();
        }

        public void RefreshAllFriend(User user)
        {
            MainWindowViewProp.lbFriend.Items.Clear();
            foreach (var friend in user.Friends)
                MainWindowViewProp.lbFriend.Items.Add(friend);
        }
        #endregion

        #region Command Delete friend
        private RelayCommand _deleteFriendCommand;

        public RelayCommand DeleteFriendCommand
        {
            get { return _deleteFriendCommand ?? (_deleteFriendCommand = new RelayCommand(DeleteFriendCommandExecute)); }
        }

        private void DeleteFriendCommandExecute()
        {
            User selectedUser = (User)MainWindowViewProp.lbFriend.SelectedItem;
            if (selectedUser != null)
            {
                Proxy.DeleteFriend(selectedUser, UserProp);
                UserProp.Friends.Remove(selectedUser);
                RefreshAllFriend(UserProp);
                new MsgBox("OK").ShowDialog();
            }
            else
                new MsgBox("Erorr select").ShowDialog();
        }
        #endregion

        #region Command RefreshResult
        private RelayCommand _refreshResultCommand;

        public RelayCommand RefreshResultCommand
        {
            get { return _refreshResultCommand ?? (_refreshResultCommand = new RelayCommand(RefreshResultCommandExecute)); }
        }

        private void RefreshResultCommandExecute()
        {
            List<Game> games = Proxy.RefreshResult(UserProp);
            MainWindowViewProp.lbGames.ItemsSource = games;
        }
        #endregion

        #region Command CreateClan
        private RelayCommand _createClanCommand;

        public RelayCommand CreateClanCommand
        {
            get { return _createClanCommand ?? (_createClanCommand = new RelayCommand(CreateClanCommandExecute)); }
        }

        private void CreateClanCommandExecute()
        {
            if (UserProp.Clan == null)
            {
                CreateClanView createClanView = new CreateClanView(this);
                createClanView.ShowDialog();
            }
            else
                new MsgBox("Ви вже в клані").ShowDialog();
        }
        #endregion
    }
}
