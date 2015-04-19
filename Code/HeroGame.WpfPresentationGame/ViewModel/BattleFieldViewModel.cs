using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroGame.ModelGame.Fighters;
using System.Threading;
using HeroGame.WpfPresentationGame.Proxy;
using HeroGame.ModelGame;
using System.Windows;
using HeroGame.WpfPresentationGame.View;

namespace HeroGame.WpfPresentationGame.ViewModel
{
    public sealed class BattleFieldViewModel : ViewModelBase
    {
        #region Constructors
        public BattleFieldViewModel(FieldViewModel fieldViewModel, User you, BattleFieldView battleFieldView)
        {
            BattleFieldView = battleFieldView;
            You = you;
            FieldViewModel = fieldViewModel;
            FieldViewModel.BattleFieldViewModel = this;
            Proxy = new GameProxy(this);
            Proxy.RegistrationGame(Game, you);
            BattleFieldView.Closed += BattleFieldView_Closed;
        }

        void BattleFieldView_Closed(object sender, EventArgs e)
        {
            FieldViewModel.Game.Winner = Enemy;
            FieldViewModel.BattleFieldViewModel.BattleFieldView = null;
            FieldViewModel.BattleFieldViewModel.Proxy.EndGame(FieldViewModel.Game);
        }
        #endregion

        #region Properties
        public GameProxy Proxy { get; set; }
        public User You { get; set; }
        public User Enemy 
        { 
            get
            {
                if (Game.Gamer1._user.Equals(You))
                    return Game.Gamer2._user;
                else
                    return Game.Gamer1._user;
            }
        }
        public Game Game { get { return FieldViewModel.Field.Game; } }
        public FieldViewModel FieldViewModel { get; private set; }
        public BattleFieldView BattleFieldView { get; private set; }
        #region GameViewProperties
        public int EnergyG1
        {
            get
            {
                return FieldViewModel.Field.Game.Gamer1.Energy;
            }
            set
            {
                RaisePropertyChanged("EnergyG1");
            }
        }
        public int EnergyG2
        {
            get
            {
                return FieldViewModel.Field.Game.Gamer2.Energy;
            }
            set
            {
                RaisePropertyChanged("EnergyG2");
            }
        }
        public int EnergyPlusedG1
        {
            get
            {
                return FieldViewModel.Field.Game.Gamer1.EnergyPlused;
            }
            set
            {
                RaisePropertyChanged("EnergyPlusedG1");
            }
        }
        public int EnergyPlusedG2
        {
            get
            {
                return FieldViewModel.Field.Game.Gamer2.EnergyPlused;
            }
            set
            {
                RaisePropertyChanged("EnergyPlusedG2");
            }
        }
        public string NickNameG1
        {
            get
            {
                return FieldViewModel.Field.Game.Gamer1.Name;
            }
        }
        public string NickNameG2 
        { get { return FieldViewModel.Field.Game.Gamer2.Name; } }
        public string NickNameTurnPlayer 
        { 
            get 
            {
                if (FieldViewModel.Field.Game.WhoTurn._user.Equals(You))
                    return "You";
                return "Enemy";
            }
            set
            {
                RaisePropertyChanged("NickNameTurnPlayer");
            }
        }
        public Visibility IsYouTurnV
        {
            get
            {
                if (Game.WhoTurn._user.Equals(You))
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
        }
        #endregion
        #endregion

        #region Command AddNewFightre
        private RelayCommand<string> _addNewFighterCommand;
        public RelayCommand<string> AddNewFighterCommand
        {
            get { return _addNewFighterCommand ?? (_addNewFighterCommand = new RelayCommand<string>(AddNewFighterCommandExecute)); }
        }
        private void AddNewFighterCommandExecute(string strTypeFighter)
        {
            switch (strTypeFighter)
            {
                case "Wizard":
                    {
                        SetHelpersMethod(Fighter.TypeFighter.WizardFighter);
                        break;
                    }
                case "Archer":
                    {
                        SetHelpersMethod(Fighter.TypeFighter.ArcherFighter);
                        break;
                    }
                case "Barbarian":
                    {
                        SetHelpersMethod(Fighter.TypeFighter.BarbarianFighter);
                        break;
                    }
                default:
                    break;
            }
        }

        private void SetHelpersMethod(Fighter.TypeFighter typeFighter)
        {
            FieldViewModel.MarkCellsForAddNewFigure();
            FieldViewModel.HelperIsAddNewFigure.IsAddedCommand = true;
            FieldViewModel.HelperIsAddNewFigure.TypeFighter = typeFighter;
        }
        #endregion

        #region Command EndTurn
        private RelayCommand _endTurnCommand;
        public RelayCommand EndTurnCommand
        {
            get { return _endTurnCommand ?? (_endTurnCommand = new RelayCommand(EndTurnCommandExecute)); }
        }

        private void EndTurnCommandExecute()
        {
            Proxy.EndTurn(Game);
        }
        #endregion

        #region WcfEndTurn
        public void WcfEndTurn()
        {
            FieldViewModel.Field.EndTurn(You, Enemy);
            FieldViewModel.ClearAllCellsFromSelection();
            
            RefreshPanelEnergyAndWhoTurn();
        }
        #endregion

        #region public members
        public void RefreshPanelEnergyAndWhoTurn()
        {
            RaisePropertyChanged("EnergyG1");
            RaisePropertyChanged("EnergyG2");
            RaisePropertyChanged("EnergyPlusedG1");
            RaisePropertyChanged("EnergyPlusedG2");
            RaisePropertyChanged("NickNameTurnPlayer");
            RaisePropertyChanged("IsYouTurn");
            RaisePropertyChanged("IsYouTurnV");
        }
        #endregion
    }
}
