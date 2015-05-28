using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame
{
    [DataContract]
    public class User
    {
        #region Field
        //private int _experience;
        //private int _lvl;
        #endregion

        #region Constructor
        public User(string nick, string pass, Clan clan)
        {
            NickName = nick;
            Password = pass;
            Friends = new List<User>();
            Clan = clan;
            //_lvl = 1;
            //_experience = 0;
        }
        #endregion

        #region Properties
        [DataMember]
        public string NickName { get; set; }
        [DataMember]
        private string Password { get; set; }
        [DataMember]
        public Clan Clan { get; set; }
        [DataMember]
        public List<User> Friends { get; set; }
        #endregion

        #region Public members
        public bool ChekPassword(string pass)
        {
            if (Password == pass)
                return true;
            return false;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return this.NickName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            User inPut = (User)obj;
            if (this.NickName == inPut.NickName)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return NickName.GetHashCode();
        }
        #endregion
    }
}
