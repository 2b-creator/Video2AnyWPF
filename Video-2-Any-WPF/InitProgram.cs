using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Video_2_Any_WPF
{
    internal class InitProgram
    {
    }
    class ConfiguationSetter
    {
        public void ConfiguationRecorder(string key,string value)
        {
            string oldValue = ConfigurationManager.AppSettings[key];
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);
        }
    }
}
