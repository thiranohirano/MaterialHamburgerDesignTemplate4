using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MaterialHamburgerDesignTemplate4.ViewModel;
using MaterialDialog;
using System.Windows.Input;


namespace MaterialHamburgerDesignTemplate4
{
    public class MainWindowViewModel : BindableClasses
    {
        public string Title
        {
            get { return Application.ResourceAssembly.GetName().Name; }
        }

        public string Version
        {
            get
            {
                return GetAssemblyVersion();
            }
        }

        private bool _DialogIsOpen;
        public bool DialogIsOpen
        {
            get { return _DialogIsOpen; }
            set { this.SetProperty(ref _DialogIsOpen, value); }
        }

        private int _SelectedMenuIndex = 0;
        public int SelectedMenuIndex
        {
            get { return _SelectedMenuIndex; }
            set { this.SetProperty(ref _SelectedMenuIndex, value); }
        }

        public MainWindow mainWindow;
        public MainWindowViewModel()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        public string GetAssemblyVersion()
        {
            Assembly mainAssembly = Assembly.GetEntryAssembly();
            AssemblyName mainAssemName = mainAssembly.GetName();
            // バージョン名（AssemblyVersion属性）を取得
            Version appVersion = mainAssemName.Version;
            string v = appVersion.Major.ToString() + "." +
          appVersion.Minor.ToString() + "." +
          appVersion.Build.ToString() + "." +
          appVersion.Revision.ToString();
            return v;
        }
    }
}
