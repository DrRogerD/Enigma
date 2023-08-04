using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{

    class EnigmaRotor
    {
        private string m_rotorLayout;
        public string rotorLayout { get{ return m_rotorLayout; } }

        private int m_currentPos;
        public int currentPos { get
            { return m_currentPos; }
            private set
            { m_currentPos = value; }
        }

        private int m_rotorNotch;
        public int RotorNotch
        {
            get
            { return m_rotorNotch; }
            private set
            { m_rotorNotch = value; }
        }


        public EnigmaRotor(string layout, int rotorNotch, int initialPos)
        {
            this.m_rotorLayout = layout;
            this.currentPos = initialPos;
            this.m_rotorNotch = rotorNotch;
        }

        public void increment()
        {
            currentPos = (currentPos + 1) % 26;
        }
    }
}
