using HeroGame.ModelGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ILibrary
{
    [ServiceContract(CallbackContract = typeof(ICallBackMain))]
    public interface IMain
    {
        [OperationContract(IsOneWay = true)]
        void SubscribeToCallBack(User user);

        [OperationContract(IsOneWay = true)]
        void UnsubscribeToCallBack(User user);

        [OperationContract(IsOneWay = false)]
        User AddFriend(string nickname, User you);

        [OperationContract(IsOneWay = true)]
        void DeleteFriend(User deleteUser, User you);

        [OperationContract(IsOneWay = true)]
        void Say(string msg, User fromUser);

        [OperationContract(IsOneWay = true)]
        void CreateLoby(Loby loby);

        [OperationContract(IsOneWay = true)]
        void DropLoby(Loby loby);

        [OperationContract(IsOneWay = true)]
        void LeaveLoby(Loby loby);

        [OperationContract(IsOneWay = true)]
        void ConnectToLoby(Loby loby, User user);

        [OperationContract]
        bool IsCanConnect(Loby loby);

        [OperationContract(IsOneWay = true)]
        void IsReady(Loby loby, bool IsCreater);

        [OperationContract]
        List<Game> RefreshResult(User user);

        [OperationContract(IsOneWay=true)]
        void AddClan(Clan clan);
    }
}
