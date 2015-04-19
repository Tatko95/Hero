using HeroGame.ModelGame;
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
using System.Windows.Shapes;

namespace HeroGame.WpfPresentationMain.UIElements
{
    /// <summary>
    /// Логика взаимодействия для CheckPass.xaml
    /// </summary>
    public partial class CheckPass : Window
    {
        Loby _loby;
        public bool IsTrue { get; set; }

        public CheckPass(Loby loby)
        {
            InitializeComponent();
            _loby = loby;
            IsTrue = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbPass.Text == _loby.Password)
            {
                IsTrue = true;
                this.Close();
            }
            else
            {
                new MsgBox("Не вірний пароль").ShowDialog();
                this.Close();
            }

        }
    }
}
