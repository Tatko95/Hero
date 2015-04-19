using HeroGame.ILibrary;
using HeroGame.ModelGame;
using HeroGame.ModelGame.Fighters;
using HeroGame.WpfPresentationGame.UI_Element;
using HeroGame.WpfPresentationGame.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.WpfPresentationGame.Listener
{
    public class GameListener : ICallBackGame
    {
        #region Field
        BattleFieldViewModel _battleFieldViewModel;
        #endregion

        #region Constructor
        public GameListener(BattleFieldViewModel battleFieldViewModel)
        {
            _battleFieldViewModel = battleFieldViewModel;
        }
        #endregion

        public void FirstTurn(ModelGame.Loby user)
        {
            throw new NotImplementedException();
        }

        public void DoTurn(Cell fromCell, Cell toCell)
        {
            _battleFieldViewModel.FieldViewModel.Cells[0].WcfMakeTurn(fromCell, toCell);
        }

        public void EndTurn()
        {
            _battleFieldViewModel.WcfEndTurn();
        }

        public void AddFighter(Fighter.TypeFighter typeFighter, Cell addedCell)
        {
            _battleFieldViewModel.FieldViewModel.Cells[0].WcfAddFighter(typeFighter, addedCell);
        }

        public void EndGame(Game game)
        {
            new MsgBoxGame("Winner: " + game.Winner.NickName).ShowDialog();

            if (_battleFieldViewModel.BattleFieldView != null)
                _battleFieldViewModel.BattleFieldView.Close();
        }
    }
}
