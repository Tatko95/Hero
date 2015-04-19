using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame.Fighters
{
    [DataContract]
    public class WizardFighter : Fighter
    {
        public WizardFighter(bool orientation, Cell cell, TypeFighter type)
            : base(orientation, cell, type)
        {
            if (orientation)
                base.Cell.Field.Game.Gamer1.EnergyPlused += 2;
            else
                base.Cell.Field.Game.Gamer2.EnergyPlused += 2;
        }

        #region Const
        const int PriceFighter = 10;
        #endregion

        #region Get Possible Turns
        public override List<Cell> GetPossibleTurns()
        {
            int vert = this.Cell.VerticalPosition;
            int hor = this.Cell.HorizontalPosition;
            List<Cell> possibleTurns = new List<Cell>()
                                    {
                                        CheckedCellIncludeField(hor + 1, vert),
                                        CheckedCellIncludeField(hor + 1, vert + 1),
                                        CheckedCellIncludeField(hor + 1, vert - 1),
                                                    
                                        CheckedCellIncludeField(hor - 1, vert),
                                        CheckedCellIncludeField(hor - 1, vert + 1),
                                        CheckedCellIncludeField(hor - 1, vert - 1),
                                                    
                                        CheckedCellIncludeField(hor, vert + 1),
                                        CheckedCellIncludeField(hor, vert - 1)
                                    };

            return possibleTurns.Where(cell => cell != null && cell.Fighter == null).ToList();
        }

        private Cell CheckedCellIncludeField(int hor, int ver)
        {
            if (ver > 9 || ver < 0 || hor > 9 || hor < 0)
                return null;
            return base.Cell.Field[hor,ver];
        }
        #endregion
    }
}
