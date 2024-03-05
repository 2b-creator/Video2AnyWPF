using FFmpeg.NET;
using FFmpeg.NET.Events;
using iNKORE.UI.WPF.Modern.Controls;
using Microsoft.Toolkit.Uwp.Notifications;
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
using Windows.Networking.NetworkOperators;
using MessageBoxEx = iNKORE.UI.WPF.Modern.Controls.MessageBox;


namespace Video_2_Any_WPF.PagesControl
{
    /// <summary>
    /// MutiTask.xaml 的交互逻辑
    /// </summary>
    public partial class MutiTask : System.Windows.Controls.Page
    {
        public static List<DataGridTextColumn> columns = new List<DataGridTextColumn>();
        private int i = 0;
        private int j = 0;
        public MutiTask()
        {
            InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                dgQueue.ItemsSource = MainPage.dataClasses;
                //for (int i = 0; i < 5; i++)
                //{
                //    string[] stringsHeader = new string[] { "编号", "源路径", "目标路径", "格式", "状态" };
                //    string[] stringsBinding = new string[] { "Id", "sourcePath", "targetPath", "format", "status" };
                //    DataGridTextColumn dataGridTextColumn = new DataGridTextColumn();
                //    dataGridTextColumn.Header = stringsHeader[i];
                //    dataGridTextColumn.Binding = new Binding($"DataClass.{stringsBinding[i]}");
                //    columns.Add(dataGridTextColumn);
                //}
                //foreach (var item in columns)
                //{
                //    dgQueue.Columns.Add(item);
                //}
                //columns.Clear();
            }));
        }

        private async void StartQueue_Click(object sender, RoutedEventArgs e)
        {
            string[] paths = { System.Environment.CurrentDirectory, "ffmpeg.exe" };
            string workPath = System.IO.Path.Combine(paths);

            var ffmpeg = new Engine(workPath);
            if (MainPage.dataClasses.Count == 0)
            {
                await MessageBoxEx.ShowAsync("列表为空！", "错误！", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                QueueProgressBarL.ShowPaused = false;
                i = MainPage.dataClasses.Count;
                j = 0;
                foreach (var item in MainPage.dataClasses)
                {
                    j++;
                    Dispatcher.Invoke(() =>
                    {
                        ProText.Text = $"进度：第{j}个，共{MainPage.dataClasses.Count}个";
                    });
                    i--;
                    var inputFile = new InputFile(item.sourcePath);
                    var outputFile = new OutputFile(item.targetPath);
                    var metadata = await ffmpeg.GetMetaDataAsync(inputFile, CancellationToken.None);
                    long totalFrame = metadata.FileInfo.Length;
                    TimeSpan totalTime = new TimeSpan();
                    totalTime = metadata.Duration;
                    string frameSize = metadata.VideoData.FrameSize;
                    double wTHRatio = Controller.WithToHeightRatio(frameSize);
                    string[] pathOfPreset = { System.Environment.CurrentDirectory, "Preset", item.conversionOptions };
                    string presetPath = System.IO.Path.Combine(pathOfPreset);
                    ConversionOptions options = Controller.OptionsHandler(presetPath, wTHRatio);
                    if (i == 0)
                    {
                        ffmpeg.Complete += OnComplete;
                    }
                    await ffmpeg.ConvertAsync(inputFile, outputFile, options, CancellationToken.None).ConfigureAwait(false);
                }
                Dispatcher.Invoke(() =>
                {
                    MainPage.dataClasses.Clear();
                });

            }
        }
        private void OnComplete(object? sender, ConversionCompleteEventArgs e)
        {
            if (i == 0)
            {
                Dispatcher.Invoke(() =>
                {
                    new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conservationId", 9814)
                    .AddText("Queue Complete!")
                    .AddText("Enjoy your work!")
                    .Show();
                    QueueProgressBarL.ShowPaused = true;
                    ProText.Text = "进度：请先启动队列";

                });

            }
        }
    }
}
