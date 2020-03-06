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
                Assembly mainAssembly = Assembly.GetEntryAssembly();
                AssemblyName mainAssemName = mainAssembly.GetName();
                // バージョン名（AssemblyVersion属性）を取得
                Version appVersion = mainAssemName.Version;
                string v = appVersion.Major.ToString() + "." +
              appVersion.Minor.ToString() + "." +
              appVersion.Build.ToString() + "." +
              appVersion.Revision.ToString();
                return "Version:" + v;
            }
        }

        public bool DialogIsOpen
        {
            get; set;
        }

        public MainWindow mainWindow;
        public MainWindowViewModel()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
        }
    }
}
