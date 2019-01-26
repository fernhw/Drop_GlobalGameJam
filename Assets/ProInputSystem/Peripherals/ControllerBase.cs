
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

using System.Collections.Generic;
using MathUtils;

namespace ProInputSystem {

    public class ControllerBase {

		protected static readonly char parserSwitchSepparationChar = char.Parse(">");
		protected static readonly char nameSepparationChar = char.Parse("#");
		
        protected string _name = "";

		protected bool controllerInitted = false;
		protected int _controllerId = 0;
		protected List<IButton> _inputs;
		protected List<string> _inputNames;
        protected Stick _leftStick;
        protected Stick _rightStick;
        protected Stick _leftStickRaw;
        protected Stick _rightStickRaw;


        protected DpadButton _actionDPad;
        protected DpadButton _keyboardDPad;

        protected string ButtonVerbose(int index) {
            return ButtonVerbose(index, index);
        }

        protected string ButtonVerbose(int index, int inputIndex) {
            IButton button = _inputs[inputIndex];
            if (button.Enabled == false)
                return "";
            return ButtonVerbose(index, button);
        }

        protected string ButtonVerbose(int index, IButton button) {
			if (button.Enabled == false)
				return "";
            return "-" + _inputNames[index] + ":" + BoolToX(button) + " ";
        }

        protected string BoolToX(bool toTurnInX) {
            if (toTurnInX)
                return "#";
            return " ";
        }

        protected string BoolToX(IButton toTurnInX) {
            if (toTurnInX.Enabled == false)
                return "×";
            return BoolToX(toTurnInX.IsPressed());
        }

		/** Aug 19 2017
         * leftJoystickToggle, rightJoystickToggle, altA
         * are passed even if not used. This is for overrides.
         * They are not passed here.
        */ 
		protected virtual void InitControllerFromData(ControllerData data) {


            List<IButton> initializerInputs = new List<IButton>(new[]{
            data.upDpad, data.downDpad, data.leftDpad, data.rightDpad,data.xAxisJoystickLeft,data.yAxisJoystickLeft, data.xAxisJoystickRight, data.yAxisJoystickRight,
            data.a, data.b, data.x, data.y, data.l, data.r,data.l2, data.r2, data.jClickLeft, data.jClickRight, data.start, data.select});

            _inputs = new List<IButton>();
			_name = data.name;
			_controllerId = data.controllerIndex;
			foreach (IButton button in initializerInputs) {
				_inputs.Add(button);
			}
			InitController();
		}

        protected virtual void InitController() {
			_inputNames = new List<string>(new[]{
				"UP_DPAD","DOWN_DPAD","LEFT_DPAD","RIGHT_DPAD","JOY_LEFT","JOY_RIGHT",
				"A","B","X","Y","L","R","L2","R2","CLICK_L","CLICK_R","START","SELECT"});

			_leftStick = new Stick(XAxisLeft, YAxisLeft);
			_rightStick = new Stick(XAxisRight, YAxisRight);
            
			_actionDPad = new DpadButton(UpDpad, DownDpad, LeftDpad, RightDpad);

			controllerInitted = true;
		}

        public virtual string GetDetailedInput() {
			
            throw new System.NotImplementedException("not to be used in the controller base");
		}

		virtual public string Name {
			get { return _name; }
			set { _name = value; }
		}

		virtual public IButton UpDpad {
			get { return _inputs[0]; }
			set { _inputs[0] = value; }
		}

		virtual public IButton DownDpad {
			get { return _inputs[1]; }
			set { _inputs[1] = value; }
		}

		virtual public IButton LeftDpad {
			get { return _inputs[2]; }
			set { _inputs[2] = value; }
		}

		virtual public IButton RightDpad {
			get { return _inputs[3]; }
			set { _inputs[3] = value; }
		}

		virtual public DpadButton Dpad {
			get {
				return _actionDPad;
			}
			set { _actionDPad = value; }
		}

		virtual public IButton XAxisLeft {
			get { return _inputs[4]; }
			set { _inputs[4] = value; }
		}

		virtual public IButton YAxisLeft {
			get { return _inputs[5]; }
			set { _inputs[5] = value; }
		}

		virtual public Stick LeftStick {
			get {
				return _leftStick;
			}
			set {
				_leftStick = value;
			}
		}
        virtual public Stick RightStick {
            get {
                return _rightStick;
            }
            set {
                _rightStick = value;
            }
        }

        virtual public IButton XAxisRight {
			get { return _inputs[6]; }
			set { _inputs[6] = value; }
		}

		virtual public IButton YAxisRight {
			get { return _inputs[7]; }
			set { _inputs[7] = value; }
		}	

		virtual public IButton A {
			get {
				return _inputs[8];
			}
			set { _inputs[8] = value; }
		} 
		virtual public IButton B {
			get { return _inputs[9]; }
			set { _inputs[9] = value; }
		}

		virtual public IButton X {
			get { return _inputs[10]; }
			set { _inputs[10] = value; }
		}

		virtual public IButton Y {
			get { return _inputs[11]; }
			set { _inputs[11] = value; }
		}

		virtual public IButton R {
			get { return _inputs[12]; }
			set { _inputs[12] = value; }
		}

		virtual public IButton L {
			get { return _inputs[13]; }
			set { _inputs[13] = value; }
		}

		virtual public IButton L2 {
			get { return _inputs[14]; }
			set { _inputs[14] = value; }
		}

		virtual public IButton R2 {
			get { return _inputs[15]; }
			set { _inputs[15] = value; }
		}

		virtual public IButton JoyClickLeft {
			get { return _inputs[16]; }
			set { _inputs[16] = value; }
		}

		virtual public IButton JoyClickRight {
			get { return _inputs[17]; }
			set { _inputs[17] = value; }
		}

		virtual public IButton Start {
			get { return _inputs[18]; }
			set { _inputs[18] = value; }
		}

		virtual public IButton Select {
			get { return _inputs[19]; }
			set { _inputs[19] = value; }
		}

    }
}