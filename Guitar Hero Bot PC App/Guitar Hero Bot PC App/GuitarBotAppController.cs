using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Hero_Bot_PC_App
{
    public class GuitarBotAppController : INotifyPropertyChanged
    {
        private string m_sFileText;
        private string m_sStatusText;

        public event PropertyChangedEventHandler PropertyChanged;

        public string FileText
        {
            get { return m_sFileText; }
            set
            {
                m_sFileText = value;
                NotifyPropertyChanged("FileText");
            }
        }

        public string StatusText
        {
            get { return m_sStatusText; }
            set
            {
                m_sStatusText = value;
                NotifyPropertyChanged("StatusText");
            }
        }

        public void NotifyPropertyChanged(string sProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(sProperty));
            }
        }
    }
}
