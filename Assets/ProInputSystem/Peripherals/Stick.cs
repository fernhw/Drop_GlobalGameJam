
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

using UnityEngine;
/** Turns two analogue buttons into Analogue Input Abstract, needs to be updated*/
namespace ProInputSystem {
    
    public class Stick : AnalogueInput {
        bool isJoystickUndefined = false;

        readonly DirectionalKeys dPadFromJStick;
        
        IButton _XAxisButton, _YAxisButton;
        Vector2 inputVector;
        IButton toggleButton = new Button(0, ButtonType.BUTTON, AccessType.NONE);

        ///<summary>Joystick, those nubs on the controller, analogue,
        /// requires 2 axises to be created and requires to be updated via .Update()</summary>
        /// <param name="xAxisNameInput"> Set in IButton with axis information.</param>
        /// <param name="yAxisNameInput"> Set in IButton with axis information.</param>
        /// <param name="toggle"> OPTIONAL: Set in IButton with Boolean to turn on the Stick.</param>

        public Stick(IButton xAxisNameInput, IButton yAxisNameInput, IButton toggle = null) {

            _XAxisButton = xAxisNameInput;
            _YAxisButton = yAxisNameInput;
            if (toggle != null) {
                toggleButton = toggle;
            }
            dPadFromJStick = new DirectionalKeys();
        }

        ///<summary>Joystick, those nubs on the controller, analogue,
        /// requires 2 axises to be created and requires to be updated via .Update()</summary>
  
        public Stick() {
            isJoystickUndefined = true;
            dPadFromJStick = new DirectionalKeys();
        }

        /// <summary>
        /// Joysticks need to be updated
        /// </summary>
       
        public void Update() {

            if (!toggleButton.IsPressed() && toggleButton.Enabled ) {
                Coords = Vector2.zero;
                dPadFromJStick.DisableAll();
            }
            else {
                if (isJoystickUndefined) {
                    Coords = new Vector2();
                    return;
                }                               
                inputVector.x = _XAxisButton.GetAxis;
                inputVector.y = _YAxisButton.GetAxis;
                Coords = inputVector;
                if (Distance > DeadZone) {
                    dPadFromJStick.FromAngle(Angle);
                }
                else {
                    dPadFromJStick.DisableAll();
                }
            }

        }

        public DirectionalKeys ReturnDpad() {
            return dPadFromJStick;
        }

        public IButton XAxisButton {
            get {
                return _XAxisButton;
            }
        }
        public IButton YAxisButton {
            get {
                return _YAxisButton;
            }
        }
    }
}