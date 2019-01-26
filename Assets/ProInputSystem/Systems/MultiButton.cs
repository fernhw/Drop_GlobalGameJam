

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
using System.Collections.Generic;

/// <summary>
/// Manage inputs form several IButtons at the same time.
/// </summary>
/// 
namespace ProInputSystem {
    public class MultiButton : BaseInputClass, IButton {
        
        List<IButton> buttons;
        int i = 0;
        int buttonsLength;

        public MultiButton(List<IButton> buttonList) {
            buttons = buttonList;
            buttonsLength = buttonList.Count;
        }

        public override bool IsPressed() {
            i = 0;
            while (i < buttonsLength){
				if (buttons[i].IsPressed()) {
					return true;
				}
                i++;
            }
            return false;
        }

        public override bool IsUp() {
			i = 0;
			while (i < buttonsLength) {
                if (buttons[i].IsUp()) {
                    return true;
                }
				i++;
			}
            return false;
        }

        /**because using averages in axises will break easily it works on priority
         * Priority 1 is the joystick controller, second PC, and third pc_alt
         * */        
        public new float GetAxis {
            get {
                i = 0;
                while (i < buttonsLength) {
                    IButton button = buttons[i];
                    float axis = button.GetAxis;
                    if (button.Enabled && Mathf.Abs(axis) > 0) {
                        return axis;
                    }
                    i++;
                }
                return 0f;
            }
        }

        public string ReturnParseData() {
            throw new System.TypeLoadException("MULTIBUTTON - Does not give parse data.");
        }

        public bool Enabled {
            get {
				i = 0;
				while (i < buttonsLength) {
                    if (buttons[i].Enabled) {
                        return true;
                    }
                    i++;
                }
                return true;
            }
        }

        public ButtonType GetButtonType() {

            return ButtonType.BUTTON;
        }
    }
}