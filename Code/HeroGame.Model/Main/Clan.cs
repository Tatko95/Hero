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
        public Clan(string name, User capitan)
        {
            Name = name;
            Creater = capitan;
        }
        #endregion

        #region Properties
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public User Creater { get; private set; }
        [DataMember]
        public List<User> Users { get; private set; }
        #endregion

        #region Override
        public override bool Equals(object obj)
        {
            Clan clan = obj as Clan;
            if (obj == null)
                return false;
            return this.Name == clan.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }
        #endregion
    }
}