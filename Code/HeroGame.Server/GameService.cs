using HeroGame.ILibrary;
using HeroGame.ModelGame;
using HeroGame.ModelGame.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.Server
{
    public class GameService : IGame
    {
        static object Synchronization = new object();

        public void RegistrationGame(Game game, User user)
        {
            lock (Synchronization)
            {
                if (!CachData.Games.Keys.Contains(game))
                {
                    CallBackGame callBack = new CallBackGame(user, OperationContext.Current.GetCallbackChannel<ICallBackGame>());
                    CachData.Games.Add(game, callBack);
                }
                else
                    CachData.Games[game].AddUser2(user, OperationContext.Current.GetCallbackChannel<ICallBackGame>());
            }
        }

        public void UnregistrationGame(Game game)
        {
            CachData.Games.Remove(game);
        }

        public void DoTurn(Game game, Cell fromCell, Cell toCell)
        {
            CachData.Games[game].CallBack1.DoTurn(fromCell, toCell);
            CachData.Games[game].CallBack2.DoTurn(fromCell, toCell);
        }

        public void EndTurn(Game game)
        {
            CachData.Games[game].CallBack1.EndTurn();
            CachData.Games[game].CallBack2.EndTurn();
        }

        public void AddFighter(Game game, Fighter.TypeFighter typeFighter, Cell addedCell)
        {
            CachData.Games[game].CallBack1.AddFighter(typeFighter, addedCell);
            CachData.Games[game].CallBack2.AddFighter(typeFighter, addedCell);
        }

        public void EndGame(Game game)
        {
            if (CachData.Games.ContainsKey(game))
            {
                CachData.Games[game].CallBack1.EndGame(game);
                CachData.Games[game].CallBack2.EndGame(game);

                CachData.Games.Remove(game);

                DataGame dataGame = new DataGame();
                dataGame.EndGame(game);
            }
        }
    }
}
