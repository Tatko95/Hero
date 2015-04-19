using HeroGame.WpfPresentationMain.Proxy;
using HeroGame.WpfPresentationMain.View;
using HeroGame.WpfPresentationMain.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HeroGame.WpfPresentationMain
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AvtorisationView avtView = new AvtorisationView();
            AvtorisationViewModel avtViewModel = new AvtorisationViewModel(avtView);
            avtView.DataContext = avtViewModel;
            avtView.Show();
        }
    }
}
