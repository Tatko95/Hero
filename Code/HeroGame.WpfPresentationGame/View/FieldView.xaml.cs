using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeroGame.WpfPresentationGame.View
{
    /// <summary>
    /// Логика взаимодействия для FieldView.xaml
    /// </summary>
    public partial class FieldView : UserControl
    {
        public FieldView()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            for (int i = 0; i < 100; i++)
            {
                CellView cell = new CellView();
                cell.SetBinding(DataContextProperty, "[" + i + "]");

                this.battleField.Children.Add(cell);
            }
        }
    }
}
