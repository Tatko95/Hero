using HeroGame.ModelGame;
using HeroGame.WpfPresentationGame.View;
using HeroGame.WpfPresentationGame.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroGame.WpfPresentationGame
{
    public static class StartNewGame
    {
        static public void StartNewGameGo(Game game, User you)
        {
            Field field = new Field(game);
            FieldViewModel fieldViewModel = new FieldViewModel(field);

            //game.Gamer1.SetField(field);
            //game.Gamer2.SetField(field);

            BattleFieldView batleFieldView = new BattleFieldView();
            BattleFieldViewModel battleFieldViewModel = new BattleFieldViewModel(fieldViewModel, you, batleFieldView);
            batleFieldView.DataContext = battleFieldViewModel;
            batleFieldView.ShowDialog();
        }
    }
}
