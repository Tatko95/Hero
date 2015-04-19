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
    public interface ICallBackGame
    {
        [OperationContract(IsOneWay = true)]
        void DoTurn(Cell fromCell, Cell toCell);

        [OperationContract(IsOneWay = true)]
        void EndTurn();

        [OperationContract(IsOneWay = true)]
        void AddFighter(Fighter.TypeFighter typeFighter, Cell addedCell);

        [OperationContract(IsOneWay = true)]
        void EndGame(Game game);
    }
}
