using FFmpeg.NET;
using System.Text;
using System.Windows;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Video_2_Any_WPF.PagesControl;
using System;

namespace Video_2_Any_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigationView_SelectionChanged(iNKORE.UI.WPF.Modern.Controls.NavigationView sender, iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (iNKORE.UI.WPF.Modern.Controls.NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                string selectedItemTag = (string)selectedItem.Tag;
                //Type pageType = typeof(MainPage).Assembly.GetType(selectedItemTag);
                Type type = Type.GetType("Video_2_Any_WPF.PagesControl."+selectedItemTag); 
                var obj = Activator.CreateInstance(type);
                contentFrame.Navigate(obj);
            }
        }
    }
}