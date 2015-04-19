using HeroGame.ILibrary;
using HeroGame.ModelGame;
using HeroGame.ModelGame.Fighters;
using HeroGame.WpfPresentationGame.Listener;
using HeroGame.WpfPresentationGame.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.WpfPresentationGame.Proxy
{
    public class GameProxy : IGame
    {
        private IGame _chanel;

        public GameProxy(BattleFieldViewModel battleFieldViewModel)
        {
            Uri _adress = new Uri("net.tcp://localhost/IGame");
            EndpointAddress _endPoint = new EndpointAddress(_adress);
            NetTcpBinding _binding = new NetTcpBinding();

            InstanceContext context = new InstanceContext(new GameListener(battleFieldViewModel));

            DuplexChannelFactory<IGame> factory = new DuplexChannelFactory<IGame>(context, _binding, _endPoint);
            _chanel = factory.CreateChannel();
        }

        public void RegistrationGame(Game game, User user)
        {
            _chanel.RegistrationGame(game, user);
        }

        public void UnregistrationGame(Game game)
        {
            _chanel.UnregistrationGame(game);
        }

        public void DoTurn(Game game, Cell fromCell, Cell toCell)
        {
            _chanel.DoTurn(game, fromCell, toCell);
        }

        public void EndTurn(Game game)
        {
            _chanel.EndTurn(game);
        }

        public void AddFighter(Game game, Fighter.TypeFighter typeFighter, Cell addedCell)
        {
            _chanel.AddFighter(game, typeFighter, addedCell);
        }

        public void EndGame(Game game)
        {
            _chanel.EndGame(game);
        }
    }
}
