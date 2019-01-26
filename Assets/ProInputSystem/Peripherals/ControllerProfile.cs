﻿﻿﻿
/** 
 * Copyright (c) 2011-2017 Fernando Holguín Weber , and Studio Libeccio - All Rights Reserved
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of the ProInputSystem 
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute,
 * sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
 * NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * Fernando Holguín Weber, <contact@fernhw.com>,<http://fernhw.com>,<@fern_hw>
 * Studio Libeccio, <@studiolibeccio> <studiolibeccio.com>
 * 
 */

namespace ProInputSystem {
   
    /**Requires ButtonModel data, any button not in the construtor is considered NOT enabled*/
    public class ControllerProfile {

        string _name = "";

        ButtonModel _upDpad = new ButtonModel();
        ButtonModel _downDPad = new ButtonModel();
		ButtonModel _leftDPad = new ButtonModel();
		ButtonModel _rightDPad = new ButtonModel();
        ButtonModel _xAxisLeft = new ButtonModel();
        ButtonModel _yAxisLeft = new ButtonModel();
        ButtonModel _xAxisRight = new ButtonModel();
        ButtonModel _yAxisRight = new ButtonModel();
        ButtonModel _a = new ButtonModel();
        ButtonModel _b = new ButtonModel();
        ButtonModel _x = new ButtonModel(); 
        ButtonModel _y = new ButtonModel();
        ButtonModel _r = new ButtonModel();
        ButtonModel _l = new ButtonModel(); 
        ButtonModel _r2 = new ButtonModel(); 
        ButtonModel _l2 = new ButtonModel();
        ButtonModel _joyClickLeft = new ButtonModel(); 
        ButtonModel _joyClickRight = new ButtonModel();
        ButtonModel _start = new ButtonModel(); 
        ButtonModel _select = new ButtonModel(); 

        //Added, extra buttons for pc only
        ButtonModel _joyToggleLeft = new ButtonModel(); //Left joystick lock 
        ButtonModel _joyToggleRight = new ButtonModel(); //Right joystick lock 
        ButtonModel _altA = new ButtonModel(); //Extra A button for mouse etc

		public string Name {
            get { return _name; }
            set { _name = value; }
        }
        public ButtonModel UpDpad {
            get { return _upDpad; }
            set { _upDpad = value; }
        }
        public ButtonModel DownDpad {
            get { return _downDPad; }
            set { _downDPad = value; }
        }
        public ButtonModel LeftDpad {
            get { return _leftDPad; }
            set { _leftDPad = value; }
        }
        public ButtonModel RightDpad {
            get { return _rightDPad; }
            set { _rightDPad = value; }
        }
        public ButtonModel XAxisLeft {
            get { return _xAxisLeft; }
            set { _xAxisLeft = value; }
        }
        public ButtonModel YAxisLeft {
            get { return _yAxisLeft; }
            set { _yAxisLeft = value; }
        }
        public ButtonModel XAxisRight {
            get { return _xAxisRight; }
            set { _xAxisRight = value; }
        }
        public ButtonModel YAxisRight {
            get { return _yAxisRight; }
            set { _yAxisRight = value; }
        }
		public ButtonModel A {
			get { return _a; }
			set { _a = value; }
		}

		/// <summary>
		/// <para>The extra A button.</para>
		/// 
		/// <para>Setting ExtraA will create another A button that can press A, this is 
		/// useful when needing the mouse also be the Action button, only A has
		/// an alternative button integrated in the controller system, and was 
		/// created for games like Amy's Escape that use the click as another A
		/// button in the keyboard system. for more Alternative buttons you have</para>
		/// </summary>
		public ButtonModel ExtraA {
            get { return _altA; }
			set { _altA = value; }
		}

		public ButtonModel B {
            get { return _b; }
            set { _b = value; }
        }
        public ButtonModel X {
            get { return _x; }
            set { _x = value; }
        }
        public ButtonModel Y {
            get { return _y; }
            set { _y = value; }
        }
        public ButtonModel R {
            get { return _r; }
            set { _r = value; }
        }
        public ButtonModel L {
            get { return _l; }
            set { _l = value; }
        }
        public ButtonModel L2 {
            get { return _l2; }
            set { _l2 = value; }
        }
        public ButtonModel R2 {
            get { return _r2; }
            set { _r2 = value; }
        }

        public ButtonModel JoyClickLeft {
            get { return _joyClickLeft; }
            set { _joyClickLeft = value; }
        }
        public ButtonModel JoyClickRight {
            get { return _joyClickRight; }
            set { _joyClickRight = value; }
        }
        public ButtonModel Start {
            get { return _start; }
            set { _start = value; }
        }
        public ButtonModel Select {
            get { return _select; }
            set { _select = value; }
        }
		/**Having this button enabled will disable the stick connected to it, so lets
	   <example>Example: hold left click to detect mouse movement.</example>*/
		public ButtonModel JoyToggleLeft {
            get { return _joyToggleLeft; }
			set { _joyToggleLeft = value; }
		}
        /**Having this button enabled will disable the stick connected to it, so lets
        <example>Example: hold left click to detect mouse movement.</example>*/
		public ButtonModel JoyToggleRight {
            get { return _joyToggleRight; }
            set { _joyToggleRight = value; }
		}
    }

  
}