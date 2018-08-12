using FlyProx.MVVM;
using FlyProxCore;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace FlyProx.ViewModels
{
    public class ProviderSelectViewModel : ViewModelBase
    {
        public ObservableCollection<FlyProxContext> Providers { get; }

        private FlyProxContext selectedContext;
        public FlyProxContext SelectedContext
        {
            get => selectedContext;
            set => NotifyPropertyChanged(ref selectedContext, value);
        }

        public ICommand LoadCommand { get; }

        public ProviderSelectViewModel()
        {
            Providers = new ObservableCollection<FlyProxContext>();
            LoadCommand = new Command(Load_Click);
            LoadProviders();
        }

        private void LoadProviders()
        {
            // Add default provider
            Providers.Add(new FlyProxContext());

            var libraries = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Providers"), "*.dll");

            libraries
                .Select(x => Assembly.LoadFile(x))
                .SelectMany(x => x.GetTypes()
                .Where(y => y.IsSubclassOf(typeof(FlyProxContext))))
                .Select(x => Activator.CreateInstance(x) as FlyProxContext)
                .ToList()
                .ForEach(x => Providers.Add(x));
        }

        private void Load_Click()
        {
            new MainViewModel()
            {
                Context = SelectedContext
            }.ShowDialog();
        }
    }
}