using HeroGame.ILibrary;
using HeroGame.ModelGame;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace HeroGame.WpfPresentationMain.Proxy
{
    public class AvtorisationProxy
    {
        private IAvtorisation _chanel;

        public AvtorisationProxy()
        {
            Uri _adress = new Uri("net.tcp://localhost/IAvtorisation");
            EndpointAddress _endPoint = new EndpointAddress(_adress);
            NetTcpBinding _binding = new NetTcpBinding();

            ChannelFactory<IAvtorisation> factory = new ChannelFactory<IAvtorisation>(_binding, _endPoint);
            _chanel = factory.CreateChannel();
        }

        public bool Registration(string name, string pass)
        {
            return _chanel.Registration(name, pass);
        }

        public User Avtorisation(string name, string pass)
        {
            User returnUser = _chanel.Avtorisation(name, pass);
            if (returnUser != null)
                if (returnUser.ChekPassword(pass))
                    return returnUser;
                else
                    throw new Exception("Не вірний пароль");
            else
                throw new Exception("Такого користувача не знайдено");
            
        }
    }
}
