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

                string sOutput = GenerateStatusForCommand(command);

                m_controller.StatusText = sOutput;

                if (command.m_bDoStrum)
                {
                    m_controller.StatusText += "\nSTRUM";
                    if (m_qBotCommands.Count() > 0)
                        m_qBotCommands.ElementAt(0).m_dMillisecondDelay -= m_dMillisecondsForStrum;

                    System.Threading.Thread.Sleep((int)m_dMillisecondsForStrum);
                    m_controller.StatusText = sOutput;
                }

            }
        }

        private string GenerateStatusForCommand(GuitarBotCommand command)
        {
            string sStatusText = "";
            sStatusText = AddFretButton(command.m_bGreenActive, 'G', sStatusText);
            sStatusText = AddFretButton(command.m_bRedActive, 'R', sStatusText);
            sStatusText = AddFretButton(command.m_bYellowActive, 'Y', sStatusText);
            sStatusText = AddFretButton(command.m_bBlueActive, 'B', sStatusText);
            sStatusText = AddFretButton(command.m_bOrangeActive, 'O', sStatusText);

            return sStatusText;
        }

        private string AddFretButton(bool bButton, char cLetter, string sStatusString)
        {
            if (bButton)
                sStatusString += " " + cLetter + " ";
            else
                sStatusString += "   ";

            return sStatusString;
        }

        private MethodDelegate m_methodDelegate;
        private IAsyncResult m_asyncResult;
        private Queue<GuitarBotCommand> m_qBotCommands;
        private double m_dMillisecondsPerFrame;
        private GuitarBotAppController m_controller;
        private const double m_dMillisecondsForStrum = 25.0; // (1 frame is 33.33ms at 30 fps)
    }
}
