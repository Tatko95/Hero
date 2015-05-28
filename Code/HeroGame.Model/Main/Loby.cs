using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame
{
    [DataContract]
    public class Loby
    {
        #region Constructor
        public Loby(string name, User creater, string pass)
        {
            Creater = creater;
            Name = name;
            Password = pass;
        }
        public Loby()
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public User Creater { get; set; }
        [DataMember]
        public User Enemy { get; set; }
        [DataMember]
        public bool IsReadyCreater { get; set; }
        [DataMember]
        public bool IsReadyEnemy { get; set; }
        [DataMember]
        public Game Game { get; set; }
        #endregion

        #region Override
        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Loby input = (Loby)obj;
            if (this.Name == input.Name)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        #endregion
    }
}
