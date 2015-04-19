using HeroGame.ILibrary;
using HeroGame.ModelGame;
using HeroGame.WpfPresentationMain.Listener;
using HeroGame.WpfPresentationMain.View;
using HeroGame.WpfPresentationMain.ViewModel;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace HeroGame.WpfPresentationMain.Proxy
{
    public class MainProxy : IMain
    {
        private IMain _chanel;

        public MainProxy(MainWindowViewModel mainWindowViewModel)
        {
            Uri _adress = new Uri("net.tcp://localhost/IMain");
            EndpointAddress _endPoint = new EndpointAddress(_adress);
            NetTcpBinding _binding = new NetTcpBinding();

            InstanceContext context = new InstanceContext(new MainListener(mainWindowViewModel));

            DuplexChannelFactory<IMain> factory = new DuplexChannelFactory<IMain>(context, _binding, _endPoint);
            _chanel = factory.CreateChannel();
        }

        public void SubscribeToCallBack(User user)
        {
            _chanel.SubscribeToCallBack(user);
        }

        public void UnsubscribeToCallBack(User user)
        {
            _chanel.UnsubscribeToCallBack(user);
        }

        public User AddFriend(string nickname, User you)
        {
            return (_chanel.AddFriend(nickname, you));
        }

        public void DeleteFriend(User deleteUser, User you)
        {
            _chanel.DeleteFriend(deleteUser, you);
        }

        public void Say(string msg, User fromUser)
        {
            _chanel.Say(msg, fromUser);
        }

        public void CreateLoby(Loby loby)
        {
            _chanel.CreateLoby(loby);
        }

        public void DropLoby(Loby loby)
        {
            _chanel.DropLoby(loby);
        }

        public void LeaveLoby(Loby loby)
        {
            _chanel.LeaveLoby(loby);
        }

        public void ConnectToLoby(Loby loby, User user)
        {
            _chanel.ConnectToLoby(loby, user);
        }

        public bool IsCanConnect(Loby loby)
        {
            return _chanel.IsCanConnect(loby);
        }

        public void IsReady(Loby loby, bool IsCreater)
        {
            _chanel.IsReady(loby, IsCreater);
        }

        public List<Game> RefreshResult(User user)
        {
            return _chanel.RefreshResult(user);
        }

        public void AddClan(Clan clan)
        {
            _chanel.AddClan(clan);
        }
    }
}
