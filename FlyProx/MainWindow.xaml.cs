using FlyProxCore;
using System.Windows;

namespace FlyProx
{
    public partial class MainWindow : Window
    {
        private readonly FlyProxContext Context;

        public MainWindow()
        {
            InitializeComponent();
            Context = new FlyProxContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Context.Initialize();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Context.Uninitialize();
            Context.Dispose();
        }
    }
}
