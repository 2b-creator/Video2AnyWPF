using FFmpeg.NET;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Windows.Media.AppBroadcasting;
using FFmpeg.NET.Enums;

namespace Video_2_Any_WPF
{
    internal class Controller
    {
        public string SelectFileWpf()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "All media files|*.mp4;*.m4v;*.avi;*.mkv;*.mov"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }
        public static double WithToHeightRatio(string frameSize)
        {
            string[] wAndH = frameSize.Split("x");
            double width = int.Parse(wAndH[0]);
            double height = int.Parse(wAndH[1]);
            double result = width / height;
            return result;
        }
        public static ConversionOptions OptionsHandler(string presetValue, double wTHRatio)
        {
            ConversionOptions options = new ConversionOptions();
            FormatClass2 formatClass2 = new FormatClass2();
            int height = int.Parse(Readjson(presetValue, "Height"));
            int fps = int.Parse(Readjson(presetValue, "Fps"));
            int videoBitRate = int.Parse(Readjson(presetValue, "VideoBitRate"));
            double width = height * wTHRatio;
            options.VideoSize = VideoSize.Custom;
            options.CustomHeight = height;
            options.CustomWidth = (int)width;
            options.VideoFps = fps;
            options.VideoBitRate = videoBitRate;
            //options.ExtraArguments = Readjson(presetValue, "Rate");
            return options;
        }
        public static string Readjson(string fileName, string key)
        {
            string json;
            using (StreamReader sr = new StreamReader(fileName))
            {
                json = sr.ReadToEnd();
            }
            JObject objs = JObject.Parse(json);
            string value = objs[key].ToString();
            return value;
        }
    }
}
