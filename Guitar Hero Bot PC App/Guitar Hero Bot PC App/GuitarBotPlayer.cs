using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Hero_Bot_PC_App
{
    class GuitarBotPlayer
    {
        public void Initialize(DataFileParser fileParser, GuitarBotAppController controller)
        {
            fileParser.GetParsedData(out m_dMillisecondsPerFrame, out m_qBotCommands);
            m_controller = controller;
        }

        public void StartPlayback()
        {
            m_methodDelegate = new MethodDelegate(this.DoPlayback);
            m_asyncResult = m_methodDelegate.BeginInvoke(null, null);
        }

        delegate void MethodDelegate();

        private void DoPlayback()
        {
            while (m_qBotCommands.Count() > 0)
            {
                GuitarBotCommand command = m_qBotCommands.Dequeue();

                if (command.m_dMillisecondDelay > 0)
                {
                    System.Threading.Thread.Sleep((int)command.m_dMillisecondDelay);
                }

                string sOutput = "";
                if (command.m_bGreenActive)
                    sOutput += " G ";
                else
                    sOutput += "   ";

                if (command.m_bRedActive)
                    sOutput += " R ";
                else
                    sOutput += "   ";

                if (command.m_bYellowActive)
                    sOutput += " Y ";
                else
                    sOutput += "   ";

                if (command.m_bBlueActive)
                    sOutput += " B ";
                else
                    sOutput += "   ";

                if (command.m_bOrangeActive)
                    sOutput += " O ";
                else
                    sOutput += "   ";

                m_controller.StatusText = sOutput;
            }
        }

        private MethodDelegate m_methodDelegate;
        private IAsyncResult m_asyncResult;
        private Queue<GuitarBotCommand> m_qBotCommands;
        private double m_dMillisecondsPerFrame;
        private GuitarBotAppController m_controller;
    }
}
