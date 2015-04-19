using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame.Fighters
{
    [DataContract]
    public class BarbarianFighter : Fighter
    {
        public BarbarianFighter(bool orientation, Cell cell, TypeFighter type)
            : base(orientation, cell, type)
        {
        }

        #region Const
        const int PriceFighter = 4;
        #endregion

        #region Get possible turn
        //Нужно дороботать преграды
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

            return possibleTurns.Where(cell => cell != null).ToList();
        }

        private Cell CheckedCellIncludeField(int hor, int ver)
        {
            if (ver > 9 || ver < 0 || hor > 9 || hor < 0)
                return null;
            return base.Cell.Field[hor,ver];
        }
    }
        #endregion
}
