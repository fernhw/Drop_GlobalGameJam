


using System;
using UnityEngine;
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
	
    /// <summary>
    /// Combine the information of 3 joysticks, designed for the global controller
    /// </summary>
    class CombinedAxis:IButton {

        const float MINIUM_MOVEMENT = .2f;

        Stick _stick1, _stick2, _stick3;
        Cardinal _dimension;
        public CombinedAxis(Stick stick1, Stick stick2, Stick stick3, Cardinal dimension) {
            _stick1 = stick1;
            _stick2 = stick2;
            _stick3 = stick3;
            _dimension = dimension;
        }

        public bool Enabled {
            get {
                return true;
            }
        }

        public ButtonType GetButtonType() {

            return ButtonType.ANALOGUE;
        }

        public float GetAxis {
            get {
                float axis = 0;
                if (Dimension == Cardinal.X) {
                    axis = Stick3.Coords.x;
                    if (Mathf.Abs(axis) > MINIUM_MOVEMENT) {
                        return axis;
                    }
                    axis = Stick2.Coords.x;
                    if (Mathf.Abs(axis) > MINIUM_MOVEMENT) {
                        return axis;
                    }
                    axis = Stick1.Coords.x;
                    if (Mathf.Abs(axis) > MINIUM_MOVEMENT) {
                        return axis;
                    }
                }
                else {
                    axis = Stick3.Coords.y;
                    if (Mathf.Abs(axis) > MINIUM_MOVEMENT) {
                        return axis;
                    }
                    axis = Stick2.Coords.y;
                    if (Mathf.Abs(axis) > MINIUM_MOVEMENT) {
                        return axis;
                    }
                    axis = Stick1.Coords.y;
                    if (Mathf.Abs(axis) > MINIUM_MOVEMENT) {
                        return axis;
                    }
                }

                return 0;
            }
        }

        public Stick Stick1 {
            get {
                return _stick1;
            }

            set {
                _stick1=value;
            }
        }

        public Stick Stick2 {
            get {
                return _stick2;
            }

            set {
                _stick2=value;
            }
        }

        public Stick Stick3 {
            get {
                return _stick3;
            }

            set {
                _stick3=value;
            }
        }

        public Cardinal Dimension {
            get {
                return _dimension;
            }

            set {
                _dimension=value;
            }
        }

        public bool IsPressed() {
            throw new NotImplementedException("Sole Axis is used to return an Axis only form of data it does not return IsPressed() data");
        }

        public bool IsUp() {
            throw new NotImplementedException("Sole Axis is used to return an Axis only form of data it does not return IsUp() data");
        }

        public string ReturnParseData() {
            throw new NotImplementedException("Sole Axis is used to return an Axis only form of data it does not returnParseData() data");
        }
    }
}
