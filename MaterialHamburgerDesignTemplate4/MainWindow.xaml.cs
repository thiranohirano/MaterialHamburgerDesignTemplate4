using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MaterialDialog;
using MyUtilityMethods;

namespace MaterialHamburgerDesignTemplate4
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        MainWindowViewModel mwvm;
        public MainWindow()
        {
            InitializeComponent();
            mwvm = this.FindResource("mwvm") as MainWindowViewModel;
        }

        private async void MainMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                /* アセンブリバージョンが変わるときにユーザ設定を引き継ぐ */
                var assemblyVersion = ApplicationInfo.GetVersionContainsAssembly();
                if (Properties.Settings.Default.AssemblyVersion != assemblyVersion)
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.AssemblyVersion = assemblyVersion;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                bool? result = await MaterialDialogUtil.ShowMaterialMessageYesNoDialog(this, "Warning", "設定ファイルが壊れています。\n初期化or前バージョンの設定で再起動しますか？");
                string filePath = ((ConfigurationErrorsException)ex.InnerException).Filename;
                File.Delete(filePath);
                if (result.HasValue && result.Value == true)
                {
                    string appName = Application.ResourceAssembly.GetName().Name;
                    Process.Start(appName + ".exe");
                    this.Close();
                    return;
                }
                else
                {
                    this.Close();
                    return;
                }
            }
        }
    }
}
