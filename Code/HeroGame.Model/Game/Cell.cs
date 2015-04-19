using HeroGame.ModelGame.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame
{
    [DataContract(IsReference=true)]
    public sealed class Cell
    {
        #region Static members
        static public List<Cell> GetPossibleAddCell(Hero gamer)
        {
            List<Cell> returnList = new List<Cell>();
            List<Cell> listCellWizard = gamer.GetAllFighter().Where<Cell>(cell => (cell.Fighter is WizardFighter)).ToList<Cell>();
            foreach (Cell cellWithWizard in listCellWizard)
                foreach (Cell possibleAdd in cellWithWizard.Fighter.GetPossibleTurns())
                    returnList.Add(possibleAdd);
            return returnList;
        }
        #endregion

        #region Properties
        [DataMember]
        public int VerticalPosition { get; private set; }
        [DataMember]
        public int HorizontalPosition { get; private set; }
        [DataMember]
        public Fighter Fighter { get; set; }
        [DataMember]
        public Field Field { get; private set; }
        #endregion

        #region Constructor
        public Cell(int hor, int ver, Field field)
        {
            HorizontalPosition = hor;
            VerticalPosition = ver;
            Field = field;
        }
        public Cell(int hor, int ver)
        {
            HorizontalPosition = hor;
            VerticalPosition = ver;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return this.Fighter.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Cell cell = (Cell) obj;
            if (cell.HorizontalPosition == this.HorizontalPosition && cell.VerticalPosition == this.VerticalPosition)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return this.HorizontalPosition.GetHashCode() * 10 + this.VerticalPosition.GetHashCode();
        }
        #endregion
    }
}
