using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Inputs
    {
        //Load list of avalaible keyborad buttons
        private static Hashtable keyTable = new Hashtable();

        //Perform a check to see if particular button is pressed
        public static bool keyPressed(Keys key)
        {
            if (keyTable[key] == null)
            {
                return false;
            }
            return (bool) keyTable[key];
        }

        //detect if a keyboard button is pressed
        public static void ChangeState(Keys key,bool state)
        {
            keyTable[key] = state;
        }
    }
}
