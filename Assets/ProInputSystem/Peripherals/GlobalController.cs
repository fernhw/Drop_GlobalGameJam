
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

    public class GlobalController : ControllerBase, IController {

        readonly new string _name = "GLOBAL_CONTROLLER";

        private AnalogueInput _dpadActionStick, _dpadStick, _globalActionStick, _globalStick;

        IButton
            _upActionDpad, _downActionDPad, _leftActionDPad, _rightActionDPad,
            _upDpad, _downDPad, _leftDPad, _rightDPad,
            _xAxisLeft, _yAxisLeft, _xAxisRight, _yAxisRight,
            _a, _b, _x, _y,
            _r, _l, _r2, _l2,
            _joyClickLeft, _joyClickRight,
            _start, _select;

        private CombinedAxis
           _xAxisLeftPasser,
           _yAxisLeftPasser,
           _xAxisRightPasser,
           _yAxisRightPasser;

        IController _gameController, _pcController, _pcAltController;

        public GlobalController(IController gameController, IController pcController, IController pcAltController) {

            _gameController = gameController;
            _pcController = pcController;
            _pcAltController = pcAltController;
            
            _upActionDpad = new MultiButton(new List<IButton>{
            gameController.UpDpad,
            pcController.UpDpad,
            pcAltController.UpDpad,
            });
            _downActionDPad = new MultiButton(new List<IButton>{
            gameController.DownDpad,
            pcController.DownDpad,
            pcAltController.DownDpad,
            });
            _leftActionDPad = new MultiButton(new List<IButton>{
            gameController.LeftDpad,
            pcController.LeftDpad,
            pcAltController.LeftDpad,
            });
            _rightActionDPad = new MultiButton(new List<IButton>{
            gameController.RightDpad,
            pcController.RightDpad,
            pcAltController.RightDpad,
            });

            _upDpad = new MultiButton(new List<IButton>{
            pcController.UpDpad,
            pcAltController.UpDpad,
            });
            _downDPad = new MultiButton(new List<IButton>{
            pcController.DownDpad,
            pcAltController.DownDpad,
            });
            _leftDPad = new MultiButton(new List<IButton>{
            pcController.LeftDpad,
            pcAltController.LeftDpad,
            });
            _rightDPad = new MultiButton(new List<IButton>{
            pcController.RightDpad,
            pcAltController.RightDpad,
            });

            _a = new MultiButton(new List<IButton>{
            gameController.A,
            pcController.A,
            pcAltController.A,
            });
            _b = new MultiButton(new List<IButton>{
            gameController.B,
            pcController.B,
            pcAltController.B,
            });
            _x = new MultiButton(new List<IButton>{
            gameController.X,
            pcController.X,
            pcAltController.X,
            });
            _y = new MultiButton(new List<IButton>{
            gameController.Y,
            pcController.Y,
            pcAltController.Y,
            });
            _r = new MultiButton(new List<IButton>{
            gameController.R,
            pcController.R,
            pcAltController.R,
            });
            _l = new MultiButton(new List<IButton>{
            gameController.L,
            pcController.L,
            pcAltController.L,
            });
            _r2 = new MultiButton(new List<IButton>{
            gameController.R2,
            pcController.R2,
            pcAltController.R2,
            });
            _l2 = new MultiButton(new List<IButton>{
            gameController.L2,
            pcController.L2,
            pcAltController.L2,
            });
            _joyClickLeft = new MultiButton(new List<IButton>{
            gameController.JoyClickLeft,
            pcController.JoyClickLeft,
            pcAltController.JoyClickLeft,
            });
            _joyClickRight = new MultiButton(new List<IButton>{
            gameController.JoyClickRight,
            pcController.JoyClickRight,
            pcAltController.JoyClickRight,
            });
            _start = new MultiButton(new List<IButton>{
            gameController.Start,
            pcController.Start,
            pcAltController.Start,
             });
            _select = new MultiButton(new List<IButton>{
            gameController.Select,
            pcController.Select,
            pcAltController.Select,
            });

            ControllerData globalControllerData = new ControllerData {
                upDpad = _upActionDpad,
                downDpad = _downActionDPad,
                leftDpad = _leftActionDPad,
                rightDpad = _rightActionDPad,
                a = _a,
                b = _b,
                x = _x,
                y = _y,
                l = _l,
                r = _r,
                l2 = _l2,
                r2 = _r2,
                jClickLeft = _joyClickLeft,
                jClickRight = _joyClickRight,
                start = _start,
                select = _select,
                controllerIndex = 0,
                name = _name
            };

            _xAxisLeftPasser = new CombinedAxis(PcAltController.LeftStick, PcController.LeftStick, GameController.LeftStick, Cardinal.X);
            _yAxisLeftPasser = new CombinedAxis(PcAltController.LeftStick, PcController.LeftStick, GameController.LeftStick, Cardinal.Y);
            _xAxisRightPasser = new CombinedAxis(PcAltController.RightStick, PcController.RightStick, GameController.RightStick, Cardinal.X);
            _yAxisRightPasser = new CombinedAxis(PcAltController.RightStick, PcController.RightStick, GameController.RightStick, Cardinal.Y);

            InitControllerFromData(globalControllerData);
        }

        public void Update(float delta) {
            GameController.Update(delta);
            PcController.Update(delta);
            PcAltController.Update(delta);

            RightStick.Update();
            LeftStick.Update();

            //Updating Dpad to use it's joystick emulator
            _actionDPad.Update(delta);
            //Updating KeyboardOnlyDpad to use it's joystick emulator
            _keyboardDPad.Update(delta);

            UpdateSticks();
        }

        protected override void InitController() {
            _inputNames = new List<string>(new[]{
                "UP_DPAD","DOWN_DPAD","LEFT_DPAD","RIGHT_DPAD","JOY_LEFT","JOY_RIGHT",
                "A","B","X","Y","L","R","L2","R2","CLICK_L","CLICK_R","START","SELECT"});

            LeftStick = new Stick(XAxisLeft, YAxisLeft);
            RightStick = new Stick(XAxisRight, YAxisRight);
            _actionDPad = new DpadButton(UpDpad, DownDpad, LeftDpad, RightDpad);
            _keyboardDPad = new DpadButton(_upDpad, _downDPad, _leftDPad, _rightDPad);
            controllerInitted = true;

			_dpadActionStick = _actionDPad.GetEmulatedJoystick();
            _dpadStick = _keyboardDPad.GetEmulatedJoystick();

            _globalActionStick = new AnalogueInput();
            _globalStick = new AnalogueInput();
        }

        public void UpdateSticks() {
            if (LeftStick.IsActive()) {
                _globalActionStick.Angle = LeftStick.Angle;
                _globalActionStick.Distance = LeftStick.Distance;
            }
            else if (_dpadActionStick.IsActive()) {
                _globalActionStick.Angle = _dpadActionStick.Angle;
                _globalActionStick.Distance = _dpadActionStick.Distance;
            }
            else{
                _globalActionStick.Distance = 0;
            }

            if (LeftStick.IsActive()) {
                _globalStick.Angle = LeftStick.Angle;
                _globalStick.Distance = LeftStick.Distance;
            }
            else if (_dpadStick.IsActive()) {
                _globalStick.Angle = _dpadStick.Angle;
                _globalStick.Distance = _dpadStick.Distance;
            }
            else {
                _globalStick.Distance = 0;
            }

        }
        public override string GetDetailedInput() {
            string detailedInput = "";
            int index = 0;

            //TODO Search a quicker way on memory or replacement method, strings are somewhat unstable.

            detailedInput += Name + "\n";
            detailedInput += "-DPAD[" + "Up:" + BoolToX(_inputs[index++]) + ",Down:" + BoolToX(_inputs[index++]);
            detailedInput += ",Left:" + BoolToX(_inputs[index++]) + ",Right:" + BoolToX(_inputs[index++]);
            detailedInput += "] | -";
            detailedInput += _inputNames[index++] + ":[";
            detailedInput += StringMathUtils.AddDecimals(LeftStick.x) + "," + StringMathUtils.AddDecimals(LeftStick.y) + "] -";
            detailedInput += _inputNames[index++] + ":[";
            detailedInput += StringMathUtils.AddDecimals(RightStick.x) + "," + StringMathUtils.AddDecimals(RightStick.y) + "]" + " ";
            detailedInput += ButtonVerbose(index++, A);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1) + "\n";
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1) + "\n";
            return detailedInput;
        }
               

        //ENCAPSULATED PROPERTIES

        public override Stick LeftStick {
            get {
                return _leftStick;
            }
        }
        public override Stick RightStick {
            get {
                return _rightStick;
            }
        }

        /// <summary>
        /// All Dpads combined.
        /// <para>Keyboard directionals + Keyboard ALT directionals</para><para> + Controller Hat Directional pad</para>
        /// </summary>
        public override DpadButton Dpad {
            get {
                return _actionDPad;
            }
            set {
                _actionDPad = value;
            }
        }
        public DpadButton KeyboardOnlyDpad {
            get {
                return _keyboardDPad;
            }
            set {
                _keyboardDPad = value;
            }
        }
       
        public override string Name {
            get {
                return _name;
            }
        }


        public override IButton UpDpad {
            get {
                return _upActionDpad;
            }
            set {
                _upActionDpad = value;
            }
        }
        public override IButton DownDpad {
            get {
                return _downActionDPad;
            }
            set {
                _downActionDPad = value;
            }
        }
        public override IButton LeftDpad {
            get {
                return _leftActionDPad;
            }
            set {
                _leftActionDPad = value;
            }
        }
        public override IButton RightDpad {
            get {
                return _rightActionDPad;
            }
            set {
                _rightActionDPad = value;
            }
        }

        public IButton KeyboardUpDpad {
            get {
                return _upDpad;
            }
            set {
                _upDpad = value;
            }
        }
        public IButton KeyboardDownDpad {
            get {
                return _downDPad;
            }
            set {
                _downDPad = value;
            }
        }
        public IButton KeyboardLeftDpad {
            get {
                return _leftDPad;
            }
            set {
                _leftDPad = value;
            }
        }
        public IButton KeyboardRightDpad {
            get {
                return _rightDPad;
            }
            set {
                _rightDPad = value;
            }
        }

        public override IButton XAxisLeft {
            get {

                return _xAxisLeftPasser;
            }
        }
        public override IButton YAxisLeft {
            get {
                return _yAxisLeftPasser;
            }
        }
        public override IButton XAxisRight {
            get {
                return _xAxisRightPasser;
            }
        }
        public override IButton YAxisRight {
            get {
                return _yAxisRightPasser;
            }
        }
        public override IButton A {
            get {
                return _a;
            }
            set {
                _a = value;
            }
        }
        public override IButton B {
            get {
                return _b;
            }
            set {
                _b = value;
            }
        }
        public override IButton X {
            get {
                return _x;
            }
            set {
                _x = value;
            }
        }
        public override IButton Y {
            get {
                return _y;
            }
            set {
                _y = value;
            }
        }
        public override IButton R {
            get {
                return _r;
            }
            set {
                _r = value;
            }
        }
        public override IButton L {
            get {
                return _l;
            }
            set {
                _l = value;
            }
        }
        public override IButton L2 {
            get {
                return _l2;
            }
            set {
                _l2 = value;
            }
        }
        public override IButton R2 {
            get {
                return _r2;
            }
            set {
                _r2 = value;
            }
        }
        public override IButton JoyClickLeft {
            get {
                return _joyClickLeft;
            }
            set {
                _joyClickLeft = value;
            }
        }
        public override IButton JoyClickRight {
            get {
                return _joyClickRight;
            }
            set {
                _joyClickRight = value;
            }
        }
        public override IButton Start {
            get {
                return _start;
            }
            set {
                _start = value;
            }
        }
        public override IButton Select {
            get {
                return _select;
            }
            set {
                _select = value;
            }
        }

        public IController GameController {
            get {
                return _gameController;
            }
            set {
                _gameController = value;
            }
        }

        public IController PcAltController {
            get {
                return _pcAltController;
            }
            set {
                _pcAltController = value;
            }
        }

        public IController PcController {
            get {
                return _pcController;
            }
            set {
                _pcController = value;
            }
        }

        public AnalogueInput DpadActionStick {
            get {
                return _dpadActionStick;
            }

            set {
                _dpadActionStick = value;
            }
        }

        public AnalogueInput GlobalActionStick {
            get {
                return _globalActionStick;
            }
            set {
                _globalActionStick = value;
            }
        }

        public AnalogueInput GlobalStick {
            get {
                return _globalStick;
            }
            set {
                _globalStick = value;
            }
        }

    }
}
