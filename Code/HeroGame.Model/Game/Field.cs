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
    public sealed class Field
    {
        #region Fields
        [DataMember]
        private List<Cell> _cells;
        #endregion

        #region Constructor
        public Field(Game game)
        {
            Game = game;
            Game.Gamer1.SetField(this);
            Game.Gamer2.SetField(this);
            if (Game.WhoTurn.Equals(game.Gamer1))
                Game.WhoTurn = Game.Gamer1;
            else
                Game.WhoTurn = Game.Gamer2;
            _cells = new List<Cell>(100);
            for (int i = 0; i < 100; i++)
                _cells.Add(new Cell(i / 10, i % 10, this));
            StartInitialize();
        }
        #endregion

        #region Properties
        public Cell this[int hor, int vert]
        {
            get
            {
                Cell cell = _cells[vert + hor * 10];
                return cell;
            }
            set { this[hor, vert] = value; }
        }
        public List<Cell> Cells { get { return _cells; } }
        [DataMember]
        public Game Game { get; private set; }
        #endregion

        #region Public members
        public void MakeTurn(Cell fromCell, Cell toCell)
        {
            if (fromCell.Fighter.GetPossibleTurns().Contains(toCell))
                if (fromCell.Fighter.IsCanGo == true)
                    if (toCell.Fighter != null)
                    {
                        fromCell.Fighter.DoTurnAttack(toCell);
                        fromCell.Fighter = null;
                    }
                    else
                    {
                        fromCell.Fighter.DoTurn(toCell);
                        fromCell.Fighter = null;
                    }
        }

        public void EndTurn(User you, User enemy)
        {
            if (Game.WhoTurn._user.Equals(you))
            {
                if (Game.Gamer1._user.Equals(you))
                {
                    Game.Gamer2.Energy += Game.Gamer2.EnergyPlused;
                    Game.WhoTurn = Game.Gamer2;
                }
                else
                {
                    Game.Gamer1.Energy += Game.Gamer1.EnergyPlused;
                    Game.WhoTurn = Game.Gamer1;
                }
            }
            else
            {
                if (Game.Gamer1._user.Equals(you))
                {
                    Game.Gamer1.Energy += Game.Gamer1.EnergyPlused;
                    Game.WhoTurn = Game.Gamer1;
                }
                else
                {
                    Game.Gamer2.Energy += Game.Gamer2.EnergyPlused;
                    Game.WhoTurn = Game.Gamer2;
                }
            }

            //IS WIN ???!

            foreach (Cell cell in Cells)
                if (cell.Fighter != null)
                    if (cell.Fighter.Orientation == Game.WhoTurn.Orientation)
                        cell.Fighter.IsCanGo = true;
                    else if (cell.Fighter.Orientation != Game.WhoTurn.Orientation)
                        cell.Fighter.IsCanGo = false;
        }
        #endregion

        #region Private members
        private void StartInitialize()
        {
            this[1, 1].Fighter = new WizardFighter(false, this[1, 1], Fighter.TypeFighter.WizardFighter);
            this[8, 8].Fighter = new WizardFighter(true, this[8, 8], Fighter.TypeFighter.WizardFighter);

            //this[9, 7].Fighter = new ArcherFighter(true, this[9, 7], Fighter.TypeFighter.ArcherFighter);
            //this[8, 9].Fighter = new BarbarianFighter(false, this[8, 9], Fighter.TypeFighter.BarbarianFighter);
            //this[7, 9].Fighter = new BarbarianFighter(true, this[7, 9], Fighter.TypeFighter.BarbarianFighter);
        }
        #endregion
    }
}
