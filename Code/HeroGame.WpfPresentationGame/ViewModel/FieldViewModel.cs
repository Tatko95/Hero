using GalaSoft.MvvmLight;
using HeroGame.ModelGame;
using HeroGame.ModelGame.Fighters;
using HeroGame.WpfPresentationGame.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.WpfPresentationGame.ViewModel
{
    public sealed class FieldViewModel : ViewModelBase
    {
        #region Fields
        private Field _field;
        #endregion

        #region Constructor
        public FieldViewModel(Field field)
        {
            _field = field;
            Cells = _field.Cells.Select(cell => new CellViewModel(cell, this)).ToList();
            HelperIsAddNewFigure = new AddNewFigureClass();
        }
        #endregion

        #region Properties
        public bool? IsWin { get { return _field.Game.IsWin; } }
        public Hero WhoTurn { get { return _field.Game.WhoTurn; } }
        public List<CellViewModel> Cells { get; private set; }
        public AddNewFigureClass HelperIsAddNewFigure { get; set; }
        public Field Field { get { return _field; } }
        public Game Game { get { return _field.Game; } }
        public BattleFieldViewModel BattleFieldViewModel { get; set; }
        public bool IsYouTurn
        {
            get
            {
                if (Game.WhoTurn._user.Equals(BattleFieldViewModel.You))
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region Public members
        public void MarkCellsForTurn(CellViewModel fromCell)
        {
            ClearAllCellsFromSelection();

            fromCell.IsSelected = true;
            if (fromCell.Fighter != null && fromCell.Fighter.IsCanGo)
                MarkCells(TransportationCellInCellViewModel(fromCell.Fighter.GetPossibleTurns()),true);
            else 
                if (fromCell.Fighter != null && !fromCell.Fighter.IsCanGo)
                    MarkCells(TransportationCellInCellViewModel(fromCell.Fighter.GetPossibleTurns()), false);
        }

        public void MarkCellsForAddNewFigure()
        {
            ClearAllCellsFromSelection();
            MarkCells(TransportationCellInCellViewModel(Cell.GetPossibleAddCell(_field.Game.WhoTurn)), false, true);
        }

        public void ClearAllCellsFromSelection()
        {
            foreach (var cell in Cells)
            {
                cell.IsSelected = false;
                cell.IsMarked = false;
                cell.IsAdded = false;
                cell.IsMarkedEnemy = false;
                cell.IsMarkedDontCanTurn = false;
            }
            RaisePropertyChanged("IsYouTurn");
        }
        #endregion

        #region Private members
        private static void MarkCells(List<CellViewModel> cellViewModels, bool IsCanGo, bool IsAdd = false)
        {
            foreach (CellViewModel cellViewModel in cellViewModels)
            {
                if (IsAdd)
                    cellViewModel.IsAdded = true;
                else
                    if (IsCanGo)
                    {
                        if (cellViewModel.Cell.Fighter == null)
                            cellViewModel.IsMarked = true;
                        else
                            if (cellViewModel.Cell.Fighter.Orientation != cellViewModel.Cell.Field.Game.WhoTurn.Orientation)
                                cellViewModel.IsMarkedEnemy = true;
                    }
                    else
                        if (cellViewModel.Cell.Fighter == null)
                            cellViewModel.IsMarkedDontCanTurn = true;
            }
        }

        private List<CellViewModel> TransportationCellInCellViewModel(List<Cell> cells)
        {
            List<CellViewModel> returnList = new List<CellViewModel>();
            foreach (Cell cell in cells)
                foreach (CellViewModel cellVM in Cells)
                    if (cellVM.Cell.HorizontalPosition == cell.HorizontalPosition && cellVM.Cell.VerticalPosition == cell.VerticalPosition)
                        returnList.Add(cellVM);
            return returnList;
        }
        #endregion

        #region Input class
        public sealed class AddNewFigureClass
        {
            public bool IsAddedCommand { get; set; }
            public Fighter.TypeFighter TypeFighter { get; set; }
        }
        #endregion
    }
}