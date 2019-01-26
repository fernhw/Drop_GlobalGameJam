
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


//**Button Managed Dpad*/
namespace ProInputSystem {

    public class DpadButton:DirectionalKeys {

        IButton _up, _down, _left, _right;

        public DpadButton(IButton up, IButton down, IButton left, IButton right) {
            _up = up;
            _down = down;
            _left = left;
            _right = right;
        }

        public override bool UpKey {
            get {
                return _up.IsPressed();
            }
        }

        public override bool DownKey {
            get {
                return _down.IsPressed();
            }
        }

        public override bool LeftKey {
            get {
                return _left.IsPressed();
            }
        }

        public override bool RightKey {
            get {
                return _right.IsPressed();
            }
        }
       

        /**
         * 
         * NOTE: these could mess up with the class since it's inherited
         * 
         * */

        /**NOT SUPPORTED*/
        override public void FromAngle(float angle) {
            throw new System.NotSupportedException("DpadButton is exclusively modified through the Button class");
        }
        /**NOT SUPPORTED*/
        override public void FromAnalogueInput(AnalogueInput joystick) {
            throw new System.NotSupportedException("DpadButton is exclusively modified through the Button class");
        }
        /**NOT SUPPORTED*/
        override public void DisableAll() {
            throw new System.NotSupportedException("DpadButton is exclusively modified through the Button class");
        }

    }
}