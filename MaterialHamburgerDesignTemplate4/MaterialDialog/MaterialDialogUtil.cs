using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace MaterialDialog
{
    public class MaterialProgressDialogController
    {
        public DialogHost dh = null;
        private ProgressDialog pd;
        public MaterialProgressDialogController(ProgressDialog pd)
        {
            this.pd = pd;
        }
   
        public void SetMessage(string message)
        {
            pd.message_tbk.Text = message;
        }

        public void Close()
        {
            if(dh != null)
            {
                dh.DialogContent = null;
                dh.IsOpen = false;
            }
        }

        public async Task CloseWait(int millisecond = 500)
        {
            Close();
            await Task.Delay(millisecond);
        }
    }

    public class MaterialDialogUtil
    {
        public static async Task<MaterialProgressDialogController> ShowMaterialProgressDialog(Window window, string message)
        {
            ProgressDialog pd = new ProgressDialog(message);
            pd.MaxWidth = window.Width / 2;
            DialogHost dh = GetFirstDialogHost(window);
            dh.DialogContent = pd;
            dh.IsOpen = true;
            MaterialProgressDialogController mpdc = new MaterialProgressDialogController(pd) { dh = dh };
            return mpdc;
        }

        private static DialogHost GetFirstDialogHost(Window window)
        {
            if (window == null) throw new ArgumentNullException(nameof(window));

            var dialogHost = VisualDepthFirstTraversal(window).OfType<DialogHost>().FirstOrDefault();

            if (dialogHost == null)
                throw new InvalidOperationException("Unable to find a DialogHost in visual tree");

            return dialogHost;
        }

        public static IEnumerable<DependencyObject> VisualDepthFirstTraversal(DependencyObject node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            yield return node;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++)
            {
                var child = VisualTreeHelper.GetChild(node, i);
                foreach (var descendant in VisualDepthFirstTraversal(child))
                {
                    yield return descendant;
                }
            }
        }

        public static async Task<bool?> ShowMaterialMessageDialog(Window window, string title, string message)
        {
            bool? re = null;
            MessageDialog md = new MessageDialog(title, message, false);
            md.MaxWidth = window.ActualWidth / 2;
            var result = await DialogHostEx.ShowDialog(window, md);
            re = result as bool?;
            return re;
        }

        public static async Task<bool?> ShowMaterialMessageYesNoDialog(Window window, string title, string message)
        {
            bool? re = null;
            MessageDialog md = new MessageDialog(title, message, true);
            md.MaxWidth = window.ActualWidth / 2;
            var result = await DialogHostEx.ShowDialog(window, md);
            re = result as bool?;
            return re;
        }
    }
}
