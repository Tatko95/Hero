using HeroGame.ILibrary;
using System;
using System.ServiceModel;

namespace HeroGame.Server
{
    class Server
    {
        static void Main(string[] args)
        {
            #region DataBase
            //DataMain data = new DataMain();
            #endregion

            #region Services
            #region IAvtorization
            Uri addres = new Uri("net.tcp://localhost/IAvtorisation");
            NetTcpBinding binding = new NetTcpBinding();
            Type contract = typeof(IAvtorisation);

            ServiceHost host = new ServiceHost(typeof(AvtorisationService));
            host.AddServiceEndpoint(contract, binding, addres);
            host.Open();
            Console.WriteLine("Service 'Avtorization' WORK");
            #endregion

            #region IMain
            Uri addres2 = new Uri("net.tcp://localhost/IMain");
            NetTcpBinding binding2 = new NetTcpBinding();
            Type contract2 = typeof(IMain);
            ServiceHost host2 = new ServiceHost(typeof(MainService));
            host2.AddServiceEndpoint(contract2, binding2, addres2);
            host2.Open();
            Console.WriteLine("Service 'Chat' WORK");
            #endregion

            #region IGame
            Uri addres3 = new Uri("net.tcp://localhost/IGame");
            NetTcpBinding binding3 = new NetTcpBinding();
            Type contract3 = typeof(IGame);
            ServiceHost host3 = new ServiceHost(typeof(GameService));
            host3.AddServiceEndpoint(contract3, binding3, addres3);
            host3.Open();
            Console.WriteLine("Service 'Game' WORK");
            #endregion
            #endregion

            Console.ReadKey();

            host.Close();
            host2.Close();
            host3.Close();
            
        }
    }
}
