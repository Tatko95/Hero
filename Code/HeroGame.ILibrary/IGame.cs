using HeroGame.ModelGame;
using HeroGame.ModelGame.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ILibrary
{
    [ServiceContract(CallbackContract = typeof(ICallBackGame))]
    public interface IGame
    {
        [OperationContract]
        void RegistrationGame(Game game, User user);

        [OperationContract]
        void UnregistrationGame(Game game);

        [OperationContract(IsOneWay=true)]
        void DoTurn(Game game, Cell fromCell, Cell toCell);

        [OperationContract(IsOneWay=true)]
        void EndTurn(Game game);

        [OperationContract(IsOneWay=true)]
        void AddFighter(Game game, Fighter.TypeFighter typeFighter, Cell addedCell);

        [OperationContract(IsOneWay = true)]
        void EndGame(Game game);
    }
}
