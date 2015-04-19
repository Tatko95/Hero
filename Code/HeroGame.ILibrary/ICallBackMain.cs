using HeroGame.ModelGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ILibrary
{
    public interface ICallBackMain
    {
        [OperationContract(IsOneWay = true)]
        void JoinUser(User user);

        [OperationContract(IsOneWay = true)]
        void LeaveUser(User user);

        [OperationContract(IsOneWay = true)]
        void Say(string msg, User fromUser);

        [OperationContract(IsOneWay = true)]
        void AddLoby(Loby loby);

        [OperationContract(IsOneWay = true)]
        void DropLoby(Loby loby, bool isPlayer);

        [OperationContract(IsOneWay = true)]
        void RefreshLoby(Loby loby);

        [OperationContract(IsOneWay = true)]
        void StartGame(Game game, User you);
    }
}
