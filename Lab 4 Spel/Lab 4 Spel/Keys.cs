using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class Keys
    {
        private bool hasRed = false;
        private bool hasGreen = false;
        private bool hasBlue = false;

        public bool getRed()
        {
            return hasRed;
        }
        public void setRed(bool value)
        {
            hasRed = value;
        }
        public bool getBlue()
        {
            return hasBlue;
        }
        public void setBlue(bool value)
        {
            hasBlue = value;
        }
        public bool getGreen()
        {
            return hasGreen;
        }
        public void setGreen(bool value)
        {
            hasGreen = value;
        }
    }
}
