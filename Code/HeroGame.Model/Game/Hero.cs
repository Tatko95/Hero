using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame
{
    [DataContract]
    public sealed class Hero
    {
        #region Field
        [DataMember]
        public User _user;
        #endregion

        #region Properties
        [DataMember]
        public string Name { get { return _user.NickName; } set { } }
        [DataMember]
        public Field Field { get; set; }
        [DataMember]
        public int Energy { get; set; }
        [DataMember]
        public int EnergyPlused { get; set; }
        [DataMember]
        public bool Orientation { get; set; }
        #endregion

        #region Constructor
        public Hero(User user, bool orientation, int energy = 0, int energyPlused = 0)
        {
            _user = user;
            Orientation = orientation;
            Energy = energy;
            EnergyPlused = energyPlused;
        }
        #endregion

        #region Public members
        public List<Cell> GetAllFighter()
        {
            return Field.Cells.Where(rec => rec.Fighter != null && rec.Fighter.Orientation == Orientation).ToList();
        }

        public void SetField(Field field)
        {
            this.Field = field;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
