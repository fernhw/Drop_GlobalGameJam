
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**Aiding class to map a new controller*/
namespace ProInputSystem {
    
    public static class InputMapper{

        public delegate void ReturnInput(int InputAccess, int inputIndex, ButtonType type);

        /**Check for switch presses*/
        public static void GetSwitchPress(ReturnInput KeyDetected) {
            int inputIndex = 0;//input Index in PC inputs
            foreach (KeyCode keyboardKey in InputNames.KeyboardInput) {
                if (Input.GetKeyUp(keyboardKey)) {
                    KeyDetected(0, inputIndex, ButtonType.BUTTON);
                    return;
                }
                inputIndex++;
            }

            int controller = 0;
            inputIndex = 0;//Input in Controller input
            foreach (List<KeyCode> playerInput in InputNames.ControllerInput) {
                foreach (KeyCode controllerButton in playerInput) {
                    if (Input.GetKeyUp(controllerButton)) {
                        KeyDetected(controller + 1, inputIndex, ButtonType.BUTTON);
                        return;
                    }
                    inputIndex++;
                }
                inputIndex = 0;//restart input per controller
                controller++;
            }

            inputIndex = 0;
            foreach (string axisPC in InputNames.PcAxes) {
                float axis = Input.GetAxis(axisPC);
                float absAxis = Mathf.Abs(axis);
                if (absAxis > .61f && absAxis <= 1f) {
                    if (axis > 0) {
                        KeyDetected(0, inputIndex, ButtonType.ANALOGUE);
                    }
                    else {
                        KeyDetected(0, inputIndex, ButtonType.ANALOGUE_REVERSED);
                    }
                    return;
                }
            }

            controller = 0;
            inputIndex = 0;
            foreach (List<string> playerAxes in InputNames.ControllerAxes) {
                foreach (string controllerAxis in playerAxes) {
                    float axis = Input.GetAxis(controllerAxis);
                    float absAxis = Mathf.Abs(axis);

                    if (absAxis > .61f && absAxis <= 1f) {
                        if (axis > 0) {
                            KeyDetected(controller + 1, inputIndex, ButtonType.ANALOGUE);
                        }
                        else {
                            KeyDetected(controller + 1, inputIndex, ButtonType.ANALOGUE_REVERSED);
                        }
                        return;
                    }
                    inputIndex++;
                }
                inputIndex = 0;//restart input per controller
                controller++;
            }
        }
    }
}