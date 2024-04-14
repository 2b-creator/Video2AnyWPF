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
using System.Data;
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
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using MessageBoxEx = iNKORE.UI.WPF.Modern.Controls.MessageBox;
using iNKORE.UI.WPF.Modern.Controls;
using iNKORE.UI.WPF.Modern.Controls.Primitives;

namespace Video_2_Any_WPF.PagesControl
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public delegate void SetNavEnable();

    public partial class MainPage : System.Windows.Controls.Page
    {
        public MainPage()
        {
            InitializeComponent();
            string[] paths = { System.Environment.CurrentDirectory, "Preset" };
            string folderPath = System.IO.Path.Combine(paths);
            string[] files = Directory.GetFiles(folderPath, "*.json", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string addFile = file.Replace($"{folderPath}\\", "");
                PresetChooser.Items.Add(addFile);
            }
        }

        private async void openSource_Click(object sender, RoutedEventArgs e)
        {
            Controller controller = new Controller();
            string? path = controller.SelectFileWpf();
            sourcePath.Text = path;
            string[] paths = { System.Environment.CurrentDirectory, "ffmpeg.exe" };
            string workPath = System.IO.Path.Combine(paths);
            var ffmpeg = new Engine(workPath);
            var inputFile = new InputFile(sourcePath.Text);
            var metadata = await ffmpeg.GetMetaDataAsync(inputFile, CancellationToken.None);
            var frameSize = metadata.VideoData.FrameSize;
            var fps = metadata.VideoData.Fps;
            SourceInformation.Text = $"帧宽高：{frameSize.ToString()}\n帧速率：{fps.ToString()}";
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
        public string presetPathSender;

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
                string[] pathsOfPreset = { System.Environment.CurrentDirectory, "Preset", PresetChooser.Text };
                string presetPath = System.IO.Path.Combine(pathsOfPreset);
                presetPathSender = presetPath;
                ffmpeg.Progress += OnProgress;
                ffmpeg.Complete += OnComplete;
                ffmpeg.Error += OnError;
                if (PresetChooser.Text == "Default")
                {
                    await ffmpeg.ConvertAsync(inputFile, outputFile, CancellationToken.None).ConfigureAwait(false);
                }
                else
                {
                    var options = Controller.OptionsHandler(presetPath, Controller.WithToHeightRatio(metadata.VideoData.FrameSize));
                    await ffmpeg.ConvertAsync(inputFile, outputFile, options, CancellationToken.None).ConfigureAwait(false);
                }
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
        private static void OnError(object sender, ConversionErrorEventArgs e)
            => Console.WriteLine("[{0} => {1}]: Error: {2}\n{3}", e.Input.Name, e.Output?.Name, e.Exception.ExitCode, e.Exception.InnerException);
        private void OnComplete(object? sender, ConversionCompleteEventArgs e)
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
        
        public static int IdCode = 0;
        private void addToQueue_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sourcePath.Text) == false && string.IsNullOrEmpty(savePath.Text) == false)
            {
                IdCode++;
                DataClass dataClass = new DataClass();
                dataClass.Id = IdCode;
                dataClass.ConversionOptions = PresetChooser.Text;
                dataClass.Format = formatChooser.Text;
                dataClass.SourcePath = sourcePath.Text;
                dataClass.TargetPath = savePath.Text;
                dataClass.Status = 0;
                Data.DataOfAssembly.DataClasses.Add(dataClass);
            }

        }

        private async void PresetFolder_Click(object sender, RoutedEventArgs e)
        {
            string[] pathsOfPreset = { System.Environment.CurrentDirectory, "Preset" };
            string presetPath = System.IO.Path.Combine(pathsOfPreset);
            System.Diagnostics.Process.Start("Explorer.exe", presetPath);
            await MessageBoxEx.ShowAsync("修改预设后请重启程序应用更改", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
