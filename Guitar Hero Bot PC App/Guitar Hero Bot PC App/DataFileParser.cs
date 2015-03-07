using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Hero_Bot_PC_App
{
    class DataFileParser
    {
        public void Init(string sFileName)
        {
            m_qBotCommands = new Queue<GuitarBotCommand>();
            m_sFileName = sFileName;
            m_sStatus = "";
            m_nFramesPerSecond = 0;
            m_dMillisecondsPerFrame = 0;
        }

        public bool ParseDataFile()
        {
            try
            {
                var oFile = new System.IO.StreamReader(m_sFileName);

                string sLine;
                while ((sLine = oFile.ReadLine()) != null)
                {
                    if (!ParseDataLine(sLine))
                    {
                        // Status error set
                        return false;
                    }
                }
            }
            catch (System.IO.IOException)
            {
                // File not found
                m_sStatus = "File not found.";
                return false;
            }

            return true;
        }

        private bool ParseDataLine(string sLine)
        {
            if (String.IsNullOrWhiteSpace(sLine) || sLine.StartsWith("#"))
            {
                // Comment line
                return true;
            }
            else if (sLine.StartsWith("fps:"))
            {
                if (m_nFramesPerSecond > 0)
                {
                    m_sStatus = "Second fps line found.";
                    return false;
                }

                string[] parts = sLine.Split(':');
                if (parts.Count() != 2)
                {
                    m_sStatus = "Invalid fps line encountered.";
                    return false;
                }

                int nFrames;
                if (!Int32.TryParse(parts[1], out nFrames))
                {
                    m_sStatus = "Unable to parse frames per second.";
                    return false;
                }

                if (nFrames <= 0 || nFrames > 1000)
                {
                    m_sStatus = "Frames out of bounds (1,1000)";
                    return false;
                }

                m_nFramesPerSecond = nFrames;
                m_dMillisecondsPerFrame = 1000.0 / m_nFramesPerSecond;
            }
            else
            {
                if (m_nFramesPerSecond == 0)
                {
                    m_sStatus = "Encountered command line before fps line";
                    return false;
                }

                string[] parts = sLine.Split(',');
                if (parts.Count() != 2)
                {
                    m_sStatus = "Invalid line found: " + sLine;
                    return false;
                }

                int nFrames;
                if (!Int32.TryParse(parts[0], out nFrames))
                {
                    m_sStatus = "Unable to parse frame delay on line: " + sLine;
                    return false;
                }

                if (nFrames < 0)
                {
                    m_sStatus = "Frame delay count must be 0 or greater.";
                    return false;
                }

                if (parts[1].Count() != 6)
                {
                    m_sStatus = "Incorrect format for button inputs: " + sLine;
                    return false;
                }

                var BotCommand = new GuitarBotCommand();
                BotCommand.m_dMillisecondDelay = (double)nFrames * m_dMillisecondsPerFrame;

                if (parts[1].ElementAt(0) == 'G')
                {
                    BotCommand.m_bGreenActive = true;
                }
                if (parts[1].ElementAt(1) == 'R')
                {
                    BotCommand.m_bRedActive = true;
                }
                if (parts[1].ElementAt(2) == 'Y')
                {
                    BotCommand.m_bYellowActive = true;
                }
                if (parts[1].ElementAt(3) == 'B')
                {
                    BotCommand.m_bBlueActive = true;
                }
                if (parts[1].ElementAt(4) == 'O')
                {
                    BotCommand.m_bOrangeActive = true;
                }
                if (parts[1].ElementAt(5) == 'S')
                {
                    BotCommand.m_bDoStrum = true;
                }

                m_qBotCommands.Enqueue(BotCommand);
            }

            return true;
        }

        public string GetErrorStatus()
        {
            return m_sStatus;
        }

        public bool GetParsedData(out double dMillisecondsPerFrame, out Queue<GuitarBotCommand> qBotCommands)
        {
            if (m_nFramesPerSecond == 0)
            {
                dMillisecondsPerFrame = 0;
                qBotCommands = new Queue<GuitarBotCommand>();
                return false;
            }

            dMillisecondsPerFrame = m_dMillisecondsPerFrame;
            qBotCommands = m_qBotCommands;

            return true;
        }

        private string m_sFileName;
        private string m_sStatus;
        private int m_nFramesPerSecond;
        private double m_dMillisecondsPerFrame;
        private Queue<GuitarBotCommand> m_qBotCommands;
    }
}
