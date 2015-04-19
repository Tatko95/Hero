using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame
{
    [DataContract(IsReference=true)]
    public class Game
    {
        #region Fields
        [DataMember]
        Loby _loby;
        [DataMember]
        Guid _id;
        
        #endregion

        #region Constructor
        public Game(Loby loby)
        {
            _id = Guid.NewGuid();
            _loby = loby;
            Gamer1 = new Hero(_loby.Creater, true);
            Gamer2 = new Hero(_loby.Enemy, false);
            //#region First turn
            //Random rnd = new Random();
            //if (rnd.Next(2) == 0)
            //    WhoTurn = Gamer1;
            //else
            //    WhoTurn = Gamer2;
            //#endregion
            WhoTurn = Gamer1;
            Winner = null;
        }

        public Game(User user1, User user2)
        {
            Winner = user1;
            Gamer1 = new Hero(user1, true);
            Gamer2 = new Hero(user2, false);
        }
        #endregion

        #region Properties
        [DataMember]
        public Hero WhoTurn { get; set; }
        public Hero TurnEnemy
        {
            get
            {
                if (WhoTurn.Equals(Gamer1))
                    return Gamer2;
                else
                    return Gamer1;
            }
        }
        [DataMember]
        public User Winner { get; set; }
        public User Loser { get 
        {
            if (Gamer1._user.Equals(Winner))
                return Gamer2._user;
            else
                return Gamer1._user;
            } 
        }
        [DataMember]
        public Hero Gamer1 { get; private set; }
        [DataMember]
        public Hero Gamer2 { get; private set; }
        public bool? IsWin { get; set; }
        #endregion

        #region Override
        public override string ToString()
        {
            return "Winner: " +  Winner.ToString() + "      Loser: " + Loser.ToString();
            //return this._loby.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Game inPut = (Game)obj;
            if (_id == inPut._id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        #endregion
    }
}
