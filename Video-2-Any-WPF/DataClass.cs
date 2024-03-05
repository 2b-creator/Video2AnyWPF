using FFmpeg.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_2_Any_WPF
{
    public class DataClass
    {
        public int Id {  get; set; }
        public string sourcePath { get; set; }
        public string targetPath { get; set; }
        public string format { get; set; }
        public string conversionOptions { get; set; }
        public int status { get; set; }
    }
    public class FormatClass2
    {
        public int Height { get; set; }
        public int Fps { get; set; }
        public int Bitrate { get; set; }
    }
}
