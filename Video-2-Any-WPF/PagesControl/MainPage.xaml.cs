using FFmpeg.NET;
using FFmpeg.NET.Enums;
using FFmpeg.NET.Events;
using Microsoft.Win32;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Uwp.Notifications;
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
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public delegate void SetNavEnable();
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void openSource_Click(object sender, RoutedEventArgs e)
        {
            Controller controller = new Controller();
            string? path = controller.SelectFileWpf();
            sourcePath.Text = path;
        }

        private void saveFile_Click(object sender, RoutedEventArgs e)
        {
            string name;
            string format = formatChooser.Text;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"{format} 文件(*.{format})|*.{format}";
            if (saveFileDialog.ShowDialog() == true)
            {
                name = saveFileDialog.FileName;
                savePath.Text = name;
            }
        }
        public long totalFrame;
        public TimeSpan totalTime;

        private async void start_Click(object sender, RoutedEventArgs e)
        {
            string[] paths = { System.Environment.CurrentDirectory, "ffmpeg.exe" };
            string workPath = System.IO.Path.Combine(paths);
            var ffmpeg = new Engine(workPath);
            if (string.IsNullOrEmpty(sourcePath.Text) == false && string.IsNullOrEmpty(savePath.Text) == false)
            {
                MainWindow.disabledValue = 0;
                var inputFile = new InputFile(sourcePath.Text);
                var outputFile = new OutputFile(savePath.Text);
                var metadata = await ffmpeg.GetMetaDataAsync(inputFile, CancellationToken.None);
                totalFrame = metadata.FileInfo.Length;
                totalTime = metadata.Duration;
                ffmpeg.Progress += OnProgress;
                ffmpeg.Complete += OnComplete;

                await ffmpeg.ConvertAsync(inputFile, outputFile, CancellationToken.None).ConfigureAwait(false);
            }
        }
        public void OnProgress(object? sender, ConversionProgressEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                
                if (e.SizeKb.HasValue)
                {
                    progressToComp.Value = e.ProcessedDuration / totalTime * 100;
                    progressText.Text = $"进度：{Math.Round(progressToComp.Value).ToString()}%";
                }
                else
                {
                    progressText.Text = "N/A";
                    progressToComp.Value = 0;
                }
            });
        }
        public void OnComplete(object? sender, ConversionCompleteEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conservationId", 9813)
                .AddText("Complete!")
                .AddText("Enjoy your work")
                .Show();
            });
        }
    }
}
