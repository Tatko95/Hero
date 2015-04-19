using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame
{
    [DataContract]
    public class Clan
    {
        #region Constructor
        public Clan(string name, User capitan, int? ID)
        {
            Name = name;
            Creater = capitan;
            NeedLvlForJoin = 1;
        }
        public Clan(int? id)
        {
            ID = id;
        }
        #endregion

        #region Public properties
        [DataMember]
        public int? ID { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public User Creater { get; private set; }
        [DataMember]
        public int NeedLvlForJoin { get; private set; }
        [DataMember]
        public List<User> Users { get; private set; }
        #endregion

        #region Override
        public override bool Equals(object obj)
        {
            Clan clan = obj as Clan;
            if (obj == null)
                return false;
            return this.ID == clan.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }
        #endregion
    }
}