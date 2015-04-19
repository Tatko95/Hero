using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.ModelGame.Fighters
{
    [DataContract]
    [KnownType(typeof(WizardFighter))]
    [KnownType(typeof(ArcherFighter))]
    [KnownType(typeof(BarbarianFighter))]
    public abstract class Fighter
    {
        #region Properties
        [DataMember]
        protected Cell Cell { get; set; }
        [DataMember]
        public bool Orientation { get; set; }
        [DataMember]
        public bool IsCanGo { get; set; }
        [DataMember]
        public TypeFighter TypeFighterStatus { get; set; } // в зависимости от этого прорисовка фигуры
        [DataMember]
        public int Price { get; set; }
        #endregion

        #region Constructor
        public Fighter(bool orientation, Cell cell, TypeFighter type)
        {
            Orientation = orientation;
            Cell = cell;
            if (TypeFighter.WizardFighter == type && Cell.Field.Game.WhoTurn.Orientation == orientation)
                IsCanGo = true;
            else
                IsCanGo = false;
            TypeFighterStatus = type;
        }
        #endregion

        #region Enum
        public enum TypeFighter
        {
            WizardFighter = 10, BarbarianFighter = 4, ArcherFighter = 7
        }
        #endregion

        #region static members
        public static bool CanAddFighter(TypeFighter type, Hero hero)
        {
                if (hero.Energy >= (int)type)
                {
                    hero.Energy -= (int)type;
                    return true;
                }
                else
                    return false;
        }
        #endregion

        #region Public members
        public abstract List<Cell> GetPossibleTurns();

        public void DoTurn(Cell toCell)
        {
            toCell.Fighter = this;
            this.Cell.Fighter = null;
            this.Cell = toCell;
            this.IsCanGo = false;
        }

        public void DoTurnAttack(Cell toCell)
        {
            if (toCell.Fighter.TypeFighterStatus == TypeFighter.WizardFighter)
                Cell.Field.Game.TurnEnemy.EnergyPlused -= 2;
            toCell.Fighter = this;
            this.Cell.Fighter = null;
            this.Cell = toCell;
            this.IsCanGo = false;
        }
        #endregion
    }
}
