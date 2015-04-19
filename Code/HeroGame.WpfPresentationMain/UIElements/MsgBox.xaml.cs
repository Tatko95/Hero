﻿using System;
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
    /// Логика взаимодействия для MsgBox.xaml
    /// </summary>
    public partial class MsgBox : Window
    {
        public MsgBox(string msg)
        {
            InitializeComponent();
            this.Message.Text = msg;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}