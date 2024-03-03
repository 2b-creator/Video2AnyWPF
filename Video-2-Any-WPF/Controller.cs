using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
