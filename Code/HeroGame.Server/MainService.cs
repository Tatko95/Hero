using HeroGame.ILibrary;
using HeroGame.ModelGame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeroGame.Server
{
    public class MainService : IMain
    {
        public void SubscribeToCallBack(User user)
        {
            foreach (var callBack in CachData.UsersOnline.Values)
                callBack.JoinUser(user);

            CachData.UsersOnline.Add(user, OperationContext.Current.GetCallbackChannel<ICallBackMain>());

            foreach (var usersOnline in CachData.UsersOnline.Keys)
                CachData.UsersOnline[user].JoinUser(usersOnline);

            foreach (var loby in CachData.Lobys)
                CachData.UsersOnline[user].AddLoby(loby);


            //foreach (DataRow callBack in CacheData.dataTableUsersOnline.Rows)
            //    ((ICallBackMain)callBack[1]).JoinUser(user);

            //CacheData.AddNew(user, OperationContext.Current.GetCallbackChannel<ICallBackMain>());

            //int count = CacheData.dataTableUsersOnline.Rows.Count-1;

            //foreach (DataRow callBack in CacheData.dataTableUsersOnline.Rows)
            //    ((ICallBackMain)CacheData.dataTableUsersOnline.Rows[count][1]).JoinUser((User)callBack[0]);

            //foreach (Loby loby in CachDataLocal.Lobys)
            //    ((ICallBackMain)CacheData.dataTableUsersOnline.Rows[count][1]).AddLoby(loby);
        }

        public void UnsubscribeToCallBack(User user)
        {
            CachData.UsersOnline.Remove(user);

            foreach (var usersOnline in CachData.UsersOnline.Keys)
                CachData.UsersOnline[usersOnline].LeaveUser(user);
        }

        public User AddFriend(string nickname, User you)
        {
            foreach (var user in CachData.Users)
                if (user.NickName == nickname)
                {
                    CachData.Users.Find(us => us.Equals(you)).Friends.Add(user);
                    return user;
                }
            return null;
        }

        public void DeleteFriend(User deleteUser, User you)
        {
            CachData.Users.Find(us => us.Equals(you)).Friends.Remove(deleteUser);
        }

        public void Say(string msg, User fromUser)
        {
            foreach (var user in CachData.UsersOnline.Values)
                user.Say(msg, fromUser);
        }

        public void CreateLoby(Loby loby)
        {
            if (CachData.Lobys.Contains(loby))
                return;
            CachData.Lobys.Add(loby);
            foreach (var callBack in CachData.UsersOnline.Values)
                callBack.AddLoby(loby);
        }

        public void DropLoby(Loby loby)
        {
            CachData.Lobys.Remove(loby);
            CachData.UsersOnline[loby.Creater].DropLoby(loby, true);
            if (loby.Enemy != null)
                CachData.UsersOnline[loby.Enemy].DropLoby(loby, true);

            foreach (var callBack in CachData.UsersOnline.Values)
                callBack.DropLoby(loby, false);
        }

        public void LeaveLoby(Loby loby)
        {
            Loby returnLoby = CachData.Lobys.Find(lobyInDataAll => lobyInDataAll.Equals(loby));
            if (returnLoby != null)
            {
                returnLoby.Enemy = null;
                returnLoby.IsReadyCreater = false;
                returnLoby.IsReadyEnemy = false;
                foreach (var users in CachData.UsersOnline.Keys)
                    if (users.Equals(loby.Creater))
                        CachData.UsersOnline[users].RefreshLoby(returnLoby);
            }
        }

        public void ConnectToLoby(Loby loby, User user)
        {
            Loby returnLoby = CachData.Lobys.Find(lobyInData => loby.Equals(lobyInData));

            if (returnLoby != null)
            {
                returnLoby.Enemy = user;
                foreach (var userInData in CachData.UsersOnline.Keys)
                    if (userInData.Equals(returnLoby.Creater) || userInData.Equals(returnLoby.Enemy))
                        CachData.UsersOnline[userInData].RefreshLoby(returnLoby);
            }
        }

        public bool IsCanConnect(Loby loby)
        {
            if (CachData.Lobys.Find(lobys => lobys.Equals(loby)).Enemy != null)
                return false;
            return true;
        }

        public void IsReady(Loby loby, bool IsCreater)
        {
            Loby returnLoby = CachData.Lobys.Find(lobys => lobys.Equals(loby));
            if (IsCreater)
                returnLoby.IsReadyCreater = true;
            else
                returnLoby.IsReadyEnemy = true;
            CachData.UsersOnline[loby.Creater].RefreshLoby(returnLoby);
            CachData.UsersOnline[loby.Enemy].RefreshLoby(returnLoby);

            if (returnLoby.IsReadyCreater == true && returnLoby.IsReadyEnemy == true)
            {
                Game game = new Game(returnLoby);
                CachData.UsersOnline[loby.Creater].StartGame(game, loby.Creater);
                CachData.UsersOnline[loby.Enemy].StartGame(game, loby.Enemy);

                CachData.Games.Remove(game);
                foreach (var callBack in CachData.UsersOnline.Values)
                    callBack.DropLoby(loby, false);
            }
        }

        public async Task<List<Game>> RefreshResult(User user)
        {
            using (DataGame dataGame = new DataGame())
            {
                Console.WriteLine(user.NickName + " Refresh result");
                return await dataGame.RefreshResult(user);
            }
        }

        public void AddClan(Clan clan)
        {
            DataGame dataGame = new DataGame();
            dataGame.CreateClan(clan);
        }
    }
}
