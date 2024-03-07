using FFmpeg.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Video_2_Any_WPF
{
    using System.ComponentModel;

    public class DataClass : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _sourcePath;
        public string SourcePath
        {
            get { return _sourcePath; }
            set
            {
                if (_sourcePath != value)
                {
                    _sourcePath = value;
                    OnPropertyChanged(nameof(SourcePath));
                }
            }
        }

        private string _targetPath;
        public string TargetPath
        {
            get { return _targetPath; }
            set
            {
                if (_targetPath != value)
                {
                    _targetPath = value;
                    OnPropertyChanged(nameof(TargetPath));
                }
            }
        }

        private string _format;
        public string Format
        {
            get { return _format; }
            set
            {
                if (_format != value)
                {
                    _format = value;
                    OnPropertyChanged(nameof(Format));
                }
            }
        }

        private string _conversionOptions;
        public string ConversionOptions
        {
            get { return _conversionOptions; }
            set
            {
                if (_conversionOptions != value)
                {
                    _conversionOptions = value;
                    OnPropertyChanged(nameof(ConversionOptions));
                }
            }
        }

        private int _status;
        public int Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FormatClass2
    {
        public int Height { get; set; }
        public int Fps { get; set; }
        public int Bitrate { get; set; }
    }
    public class DeleteOrClear
    {
        public static int RadioButtonStatus = 1;
        public static int DeleteId {  get; set; }
    }
}
