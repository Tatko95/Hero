using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame.Fighters
{
    [DataContract]
    public class ArcherFighter : Fighter
    {
        public ArcherFighter(bool orientation, Cell cell, TypeFighter type)
            : base(orientation, cell, type) { }

        #region Const
        const int PriceFighter = 7;
        #endregion

        #region GetPossibleTurn
        public override List<Cell> GetPossibleTurns()
        {
            int ver = this.Cell.VerticalPosition;
            int hor = this.Cell.HorizontalPosition;
            List<Cell> helpList = new List<Cell>()
                                    {
                                        CheckedCellIncludeField(hor + 1, ver),
                                        CheckedCellIncludeField(hor + 1, ver + 1),
                                        CheckedCellIncludeField(hor + 1, ver - 1),
                                                    
                                        CheckedCellIncludeField(hor - 1, ver),
                                        CheckedCellIncludeField(hor - 1, ver + 1),
                                        CheckedCellIncludeField(hor - 1, ver - 1),
                                                    
                                        CheckedCellIncludeField(hor, ver + 1),
                                        CheckedCellIncludeField(hor, ver - 1)
                                    };

            helpList = helpList.Where(cell => cell != null).ToList();
            List<Cell> returnList = new List<Cell>();

            foreach (var cell in helpList)
                returnList.Add(cell);

            foreach (var cell in helpList)
                if (cell.Fighter == null)
                    foreach (var item in CheckNextDiaposone(cell))
                        if (!returnList.Contains(item))
                            returnList.Add(item);

            returnList.Remove(this.Cell);
            return returnList;
        }

        private List<Cell> CheckNextDiaposone(Cell cell)
        {
            int ver = cell.VerticalPosition;
            int hor = cell.HorizontalPosition;
            List<Cell> returnList = new List<Cell>()
            {
                                        CheckedCellIncludeField(hor + 1, ver),
                                        CheckedCellIncludeField(hor + 1, ver + 1),
                                        CheckedCellIncludeField(hor + 1, ver - 1),
                                                    
                                        CheckedCellIncludeField(hor - 1, ver),
                                        CheckedCellIncludeField(hor - 1, ver + 1),
                                        CheckedCellIncludeField(hor - 1, ver - 1),
                                                    
                                        CheckedCellIncludeField(hor, ver + 1),
                                        CheckedCellIncludeField(hor, ver - 1)
            };
            returnList = returnList.Where(cl => cl != null).ToList();
            return returnList;
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
