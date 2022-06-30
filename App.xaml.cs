using OpenTkApp1.ViewModels;
using OpenTkApp1.Views;
using System.Windows;

namespace OpenTkApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var w = new MainView() { DataContext = new MainViewModel() };
            w.Show();
        }
    }
}
