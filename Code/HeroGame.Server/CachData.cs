using HeroGame.ILibrary;
using HeroGame.ModelGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.Server
{
    static public class CachData
    {
        static public List<User> Users = new List<User>();

        static public Dictionary<User, ICallBackMain> UsersOnline = new Dictionary<User, ICallBackMain>();

        static public List<Loby> Lobys = new List<Loby>();

        static public Dictionary<Game, CallBackGame> Games = new Dictionary<Game, CallBackGame>();
    }

    public class CallBackGame
    {
        public User User1;
        public User User2;
        public ICallBackGame CallBack1;
        public ICallBackGame CallBack2;

        public CallBackGame(User user, ICallBackGame callBack)
        {
            User1 = user;
            CallBack1 = callBack;
        }

        public void AddUser2(User user, ICallBackGame callBack)
        {
            User2 = user;
            CallBack2 = callBack;
        }
    }
}
