using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeroGame.ModelGame;
using HeroGame.ModelGame.Fighters;
using HeroGame.WpfPresentationGame.UI_Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HeroGame.WpfPresentationGame.ViewModel
{
    public sealed class CellViewModel : ViewModelBase
    {
        #region Fields
        private readonly Cell _cell;
        private readonly FieldViewModel _fieldViewModel;
        private bool _isMarked;
        private bool _isSelected;
        private bool _isAdded;
        private bool _isMarkedEnemy;
        private bool _isMarkedDontCanTurn;
        #endregion

        #region Constructors
        public CellViewModel(Cell cell, FieldViewModel fieldViewModel)
        {
            _cell = cell;
            _fieldViewModel = fieldViewModel;
        }
        #endregion

        #region Properties
        public Cell Cell { get { return _cell; } }
        public FieldViewModel FieldViewModel { get { return _fieldViewModel; } }
        public Fighter Fighter 
        { 
            get { return _cell.Fighter; }
            set 
            {
                if (value != null)
                    _cell.Fighter = value;
                RaisePropertyChanged("Fighter");
            }
        }
        public bool IsMarked
        {
            get { return _isMarked; }
            set
            {
                if (_isMarked != value)
                {
                    _isMarked = value;
                    RaisePropertyChanged("Background");
                }
            }
        }
        public bool IsMarkedDontCanTurn
        {
            get { return _isMarkedDontCanTurn; }
            set
            {
                if (_isMarkedDontCanTurn != value)
                {
                    _isMarkedDontCanTurn = value;
                    RaisePropertyChanged("Background");
                }
            }
        }
        public bool IsMarkedEnemy
        { 
            get { return _isMarkedEnemy; }
            set
            {
                if (_isMarkedEnemy != value)
                {
                    _isMarkedEnemy = value;
                    RaisePropertyChanged("Background");
                }
            }
        }
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged("Background");
                }
            }
        }
        public bool IsAdded
        {
            get { return _isAdded; }
            set 
            {
                if (IsAdded != value)
                {
                    _isAdded = value;
                    RaisePropertyChanged("Background");
                }
            }
        }
        #endregion

        #region Command MakeSelected
        private RelayCommand _makeSelectedCommand;
        public RelayCommand MakeSelectedCommand
        { 
            get { return _makeSelectedCommand ?? (_makeSelectedCommand = new RelayCommand(MakeSelectedExecute)); } 
        }

        private void MakeSelectedExecute()
        {
            if (_fieldViewModel.HelperIsAddNewFigure.IsAddedCommand)
            {
                if (this.Fighter == null && Cell.GetPossibleAddCell(_fieldViewModel.Field.Game.WhoTurn).Contains(_cell))
                    FieldViewModel.BattleFieldViewModel.Proxy.AddFighter(FieldViewModel.Game, _fieldViewModel.HelperIsAddNewFigure.TypeFighter, _cell);
                _fieldViewModel.HelperIsAddNewFigure.IsAddedCommand = false;
                FieldViewModel.ClearAllCellsFromSelection();
            }
            else
                if (!this.IsSelected)
                    _fieldViewModel.MarkCellsForTurn(this);
                else
                    FieldViewModel.ClearAllCellsFromSelection();
        }
        #endregion

        #region WcfAddFighter
        public void WcfAddFighter(Fighter.TypeFighter typeFighter, Cell addedCell)
        {
            addedCell = SearchNeedCell(addedCell);
            CellViewModel cellVM = SearchCellVMtoCell(addedCell);

            if (Fighter.CanAddFighter(typeFighter, _fieldViewModel.Game.WhoTurn))
            {
                switch (typeFighter)
                {
                    case Fighter.TypeFighter.WizardFighter:
                            cellVM.Cell.Fighter = new WizardFighter(_fieldViewModel.Field.Game.WhoTurn.Orientation, addedCell, typeFighter);
                            break;
                    case Fighter.TypeFighter.ArcherFighter:
                            cellVM.Cell.Fighter = new ArcherFighter(_fieldViewModel.Field.Game.WhoTurn.Orientation, addedCell, typeFighter);
                            break;
                    case Fighter.TypeFighter.BarbarianFighter:
                            cellVM.Cell.Fighter = new BarbarianFighter(_fieldViewModel.Field.Game.WhoTurn.Orientation, addedCell, typeFighter);
                            break;
                    default:
                        break;
                }
                _fieldViewModel.BattleFieldViewModel.RefreshPanelEnergyAndWhoTurn();
                FieldViewModel.ClearAllCellsFromSelection();
                cellVM.Fighter = null; //refresh
            }
            else
                if (FieldViewModel.Game.WhoTurn._user.Equals(_fieldViewModel.BattleFieldViewModel.You))
                    new MsgBoxGame("Not enough energy").ShowDialog();
        }

        private Cell SearchNeedCell(Cell cell)
        {
            foreach (var cells in _fieldViewModel.Field.Cells)
                if (cell.Equals(cells))
                    return cells;
            return null;
        }
        #endregion

        #region Command MakeTurn
        private RelayCommand _makeTurnCommand;
        public RelayCommand MakeTurnCommand
        {
            get { return _makeTurnCommand ?? (_makeTurnCommand = new RelayCommand(MakeTurnCommandExecute)); }
        }

        private void MakeTurnCommandExecute()
        {
            foreach (CellViewModel fromCellVM in FieldViewModel.Cells)
                if (fromCellVM.IsSelected)
                    if (fromCellVM._cell.Fighter != null && fromCellVM != this)
                        if (fromCellVM._cell.Fighter.Orientation == FieldViewModel.Game.WhoTurn.Orientation)
                            if (this.Cell.Fighter != null)
                            {
                                if (fromCellVM.Cell.Fighter.Orientation != this.Cell.Fighter.Orientation)
                                    FieldViewModel.BattleFieldViewModel.Proxy.DoTurn(FieldViewModel.Game, fromCellVM.Cell, this.Cell);
                            }
                            else
                                FieldViewModel.BattleFieldViewModel.Proxy.DoTurn(FieldViewModel.Game, fromCellVM.Cell, this.Cell);
        }
        #endregion

        #region IsEndGame
        private bool IsEndGame()
        {
            foreach (var cell in FieldViewModel.Game.TurnEnemy.GetAllFighter())
                return false;
            return true;
        }
        #endregion

        #region WcfMakeTurn
        public void WcfMakeTurn(Cell fromCell, Cell toCell)
        {
            CellViewModel fromCellVM = SearchCellVMtoCell(fromCell);
            CellViewModel toCellVM = SearchCellVMtoCell(toCell);
            FieldViewModel.Field.MakeTurn(fromCellVM._cell, toCellVM.Cell);
            toCellVM.RaisePropertyChanged("Fighter");
            fromCellVM.RaisePropertyChanged("Fighter");
            FieldViewModel.ClearAllCellsFromSelection();
            FieldViewModel.BattleFieldViewModel.RefreshPanelEnergyAndWhoTurn();
            if (IsEndGame())
            {
                FieldViewModel.Game.Winner = FieldViewModel.Game.WhoTurn._user;
                FieldViewModel.BattleFieldViewModel.Proxy.EndGame(FieldViewModel.Game);
            }
        }

        private CellViewModel SearchCellVMtoCell(Cell cell)
        {
            foreach (var cellVM in FieldViewModel.Cells)
                if (cellVM.Cell.Equals(cell))
                    return cellVM;
            return null;
        }
        #endregion

        #region For View
        public SolidColorBrush Background
        {
            get
            {
                if (IsAdded)
                    return new SolidColorBrush(Colors.LawnGreen);
                if (IsMarked)
                    return new SolidColorBrush(Colors.Aquamarine);
                if (IsMarkedEnemy)
                    return new SolidColorBrush(Colors.Red);
                if (IsMarkedDontCanTurn)
                    return new SolidColorBrush(Colors.Gray);
                if (IsSelected)
                    return new SolidColorBrush(Colors.Green);
                return new SolidColorBrush(Color.FromArgb(58, 112, 78, 58)); // color of Field
            }
        }
        #endregion
    }
}
