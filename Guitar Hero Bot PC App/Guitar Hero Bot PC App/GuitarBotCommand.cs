using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Hero_Bot_PC_App
{
    class GuitarBotCommand
    {
        public GuitarBotCommand()
        {
            m_dMillisecondDelay = 0;
            m_bGreenActive = false;
            m_bRedActive = false;
            m_bYellowActive = false;
            m_bBlueActive = false;
            m_bOrangeActive = false;
            m_bDoStrum = false;
        }

        // How many milliseconds to delay before executing this command
        public double m_dMillisecondDelay;

        public bool m_bGreenActive;
        public bool m_bRedActive;
        public bool m_bYellowActive;
        public bool m_bBlueActive;
        public bool m_bOrangeActive;
        public bool m_bDoStrum;
    }
}
