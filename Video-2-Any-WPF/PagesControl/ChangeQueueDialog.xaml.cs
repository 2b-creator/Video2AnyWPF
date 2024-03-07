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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Video_2_Any_WPF.PagesControl
{
    /// <summary>
    /// ChangeQueueDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeQueueDialog : UserControl
    {
        public ChangeQueueDialog()
        {
            InitializeComponent();
        }

        private void operateMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteCon_Checked(object sender, RoutedEventArgs e)
        {
            if (DeleteCon.Tag != null)
            {
                DeleteOrClear.RadioButtonStatus = int.Parse(DeleteCon.Tag.ToString());
            }
        }

        private void ClearCon_Checked(object sender, RoutedEventArgs e)
        {
            DeleteOrClear.RadioButtonStatus = int.Parse(ClearCon.Tag.ToString());
        }

        private void DeleteQueueId_TextChanged(iNKORE.UI.WPF.Modern.Controls.AutoSuggestBox sender, iNKORE.UI.WPF.Modern.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            if (!string.IsNullOrEmpty(DeleteQueueId.Text))
            {
                DeleteOrClear.DeleteId = int.Parse(DeleteQueueId.Text);
            }
        }
    }
}
