﻿
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
//using MathUtils;
/**
 * The physical controller, all joysticks and buttons.
*/
namespace ProInputSystem {
    public class Controller:ControllerBase, IController {

        MultiButton combinedA;
        
        public Controller(string joystickParseData) {

            CreateControllerFromParseString(joystickParseData);

        }

        public Controller(ControllerProfile pureControllerData) {

            ControllerData controlData = new ControllerData {
                upDpad = pureControllerData.UpDpad.CreateButton(),
                downDpad = pureControllerData.DownDpad.CreateButton(),
                leftDpad = pureControllerData.LeftDpad.CreateButton(),
                rightDpad = pureControllerData.RightDpad.CreateButton(),
                xAxisJoystickLeft = pureControllerData.XAxisLeft.CreateButton(),
                yAxisJoystickLeft = pureControllerData.YAxisLeft.CreateButton(),
                xAxisJoystickRight = pureControllerData.XAxisRight.CreateButton(),
                yAxisJoystickRight = pureControllerData.YAxisRight.CreateButton(),
                a = pureControllerData.A.CreateButton(),
                b = pureControllerData.B.CreateButton(),
                x = pureControllerData.X.CreateButton(),
                y = pureControllerData.Y.CreateButton(),
                l = pureControllerData.L.CreateButton(),
                r = pureControllerData.R.CreateButton(),
                l2 = pureControllerData.L2.CreateButton(),
                r2 = pureControllerData.R2.CreateButton(),
                jClickLeft = pureControllerData.JoyClickLeft.CreateButton(),
                jClickRight = pureControllerData.JoyClickRight.CreateButton(),
                start = pureControllerData.Start.CreateButton(),
                select = pureControllerData.Select.CreateButton(),
                controllerIndex = 0,
                name = pureControllerData.Name,
                leftJoystickToggle = pureControllerData.JoyToggleLeft.CreateButton(),
                rightJoystickToggle = pureControllerData.JoyToggleRight.CreateButton(),
                altA = pureControllerData.ExtraA.CreateButton()
            };
            InitControllerFromData(controlData);

        }

        public Controller(ControllerData data) {

            InitControllerFromData(data);

        }


        void CreateControllerFromParseString(string joystickParseData) {

            int Index = 0;
            string[] stringToParse = joystickParseData.Split(nameSepparationChar);
            string[] parsedString = stringToParse[1].Split(parserSwitchSepparationChar);

            _name = stringToParse[0];

            _inputs = new List<IButton>();

            foreach (string toParseIButton in parsedString) {
                if (toParseIButton == "")
                    break;
                IButton buttonToAdd = Button.CreateButton(toParseIButton);
                _inputs.Add(buttonToAdd);
                Index++;
            }
            InitController();

        }
        
        override protected void InitControllerFromData(ControllerData data) {

            List<IButton> initializerInputs = new List<IButton>(new[]{
            data.upDpad, data.downDpad, data.leftDpad, data.rightDpad,data.xAxisJoystickLeft,data.yAxisJoystickLeft, data.xAxisJoystickRight, data.yAxisJoystickRight,
            data.a, data.b, data.x, data.y, data.l, data.r,data.l2, data.r2, data.jClickLeft, data.jClickRight, data.start, data.select,
            data.leftJoystickToggle, data.rightJoystickToggle,data.altA});

            _inputs = new List<IButton>();
            _name =  data.name;
            _controllerId =  data.controllerIndex;

            foreach (IButton button in initializerInputs) {
                _inputs.Add(button);
            }

            InitController();

        }

        override protected void InitController() {

            _inputNames = new List<string>(new[]{
                "UP_DPAD","DOWN_DPAD","LEFT_DPAD","RIGHT_DPAD","JOY_LEFT","JOY_RIGHT",
                "A","B","X","Y","L","R","L2","R2","CLICK_L","CLICK_R","START","SELECT","J_TOGGLE_L","J_TOGGLE_R","ALT_A"});

            _leftStick = new Stick(XAxisLeft, YAxisLeft, ToggleLeftStick);
            _rightStick = new Stick(XAxisRight, YAxisRight, ToggleRightStick);
            _actionDPad = new DpadButton(UpDpad, DownDpad, LeftDpad, RightDpad);
            combinedA = new MultiButton(new List<IButton> { MainA, AltA });
            controllerInitted = true;

        }

        public override string GetDetailedInput() {

            string detailedInput = "";
            int index = 0;
            
            detailedInput += Name + "\n";
            detailedInput += "-DPAD[" + "Up:" + BoolToX(_inputs[index++]) + ",Down:" + BoolToX(_inputs[index++]);
            detailedInput += ",Left:" + BoolToX(_inputs[index++]) + ",Right:" + BoolToX(_inputs[index++]);
            detailedInput += "] | -";

            if (LeftStick.XAxisButton.Enabled == true && LeftStick.YAxisButton.Enabled == true) {
                detailedInput += _inputNames[index++] + ":[";
                detailedInput += StringMathUtils.AddDecimals(LeftStick.x) + "," + StringMathUtils.AddDecimals(LeftStick.y) + "] -";
            }
            else {
                index++;
            }

            if (RightStick.XAxisButton.Enabled == true && RightStick.YAxisButton.Enabled == true) {
                detailedInput += _inputNames[index++] + ":[";
                detailedInput += StringMathUtils.AddDecimals(RightStick.x) + "," + StringMathUtils.AddDecimals(RightStick.y) + "]" + " ";
            }
            else {
                index++;
            }
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
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            detailedInput += ButtonVerbose(index++, index + 1);
            return detailedInput;

        }

        public void Update(float delta) {

            if (!controllerInitted)
                return;
            RightStick.Update();
            LeftStick.Update();

        }

        /// <returns>The parse data.</returns>
        public string ReturnParseData() {

            string resourcedData = "";
            foreach (IButton toParseButton in _inputs) {
                resourcedData += toParseButton.ReturnParseData() + ">";
            }
            return resourcedData;

        }

        public int ControllerId {

            get {
                return _controllerId;
            }
            set {
                _controllerId = value;
                foreach (Button button in _inputs) {
                    button.ControllerIndex = value;
                }
            }
        }


        //PROPERTIES
        override public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }

        override public IButton UpDpad {
            get {
                return _inputs[0];
            }
            set {
                _inputs[0] = value;
            }
        }

        override public IButton DownDpad {
            get {
                return _inputs[1];
            }
            set {
                _inputs[1] = value;
            }
        }

        override public IButton LeftDpad {
            get {
                return _inputs[2];
            }
            set {
                _inputs[2] = value;
            }
        }

        override public IButton RightDpad {
            get {
                return _inputs[3];
            }
            set {
                _inputs[3] = value;
            }
        }
        
        override public IButton XAxisLeft {
            get {
                return _inputs[4];
            }
            set {
                _inputs[4] = value;
            }
        }
        override public IButton YAxisLeft {
            get {
                return _inputs[5];
            }
            set {
                _inputs[5] = value;
            }
        }

        override public IButton XAxisRight {
            get {
                return _inputs[6];
            }
            set {
                _inputs[6] = value;
            }
        }
        override public IButton YAxisRight {
            get {
                return _inputs[7];
            }
            set {
                _inputs[7] = value;
            }
        }

        override public Stick LeftStick {
            get {
                return _leftStick;
            }
        }
        override public Stick RightStick {
            get {
                return _rightStick;
            }
        }

        override public DpadButton Dpad {
            get {
                return _actionDPad;
            }
            set {
                _actionDPad = value;
            }
        }      

        override public IButton A {
            get {
                if (AltA.Enabled) {
                    return combinedA;
                }
                else {
                    return _inputs[8];
                }
            }
            set {
                if (AltA.Enabled) {
                    combinedA = (MultiButton)value;
                }
                else {
                    _inputs[8] = value;
                }
            }
        }

        public IButton MainA {
            get {
                return _inputs[8];
            }
            set {
                _inputs[8] = value;
            }
        }

        override public IButton B {
            get {
                return _inputs[9];
            }
            set {
                _inputs[9] = value;
            }
        }

        override public IButton X {
            get {
                return _inputs[10];
            }
            set {
                _inputs[10] = value;
            }
        }

        override public IButton Y {
            get {
                return _inputs[11];
            }
            set {
                _inputs[11] = value;
            }
        }

        override public IButton L {
            get {
                return _inputs[12];
            }
            set {
                _inputs[12] = value;
            }
        }

        override public IButton R {
            get {
                return _inputs[13];
            }
            set {
                _inputs[13] = value;
            }
        }

        override public IButton L2 {
            get {
                return _inputs[14];
            }
            set {
                _inputs[14] = value;
            }
        }

        override public IButton R2 {
            get {
                return _inputs[15];
            }
            set {
                _inputs[15] = value;
            }
        }

        override public IButton JoyClickLeft {
            get {
                return _inputs[16];
            }
            set {
                _inputs[16] = value;
            }
        }

        override public IButton JoyClickRight {
            get {
                return _inputs[17];
            }
            set {
                _inputs[17] = value;
            }
        }

        override public IButton Start {
            get {
                return _inputs[18];
            }
            set {
                _inputs[18] = value;
            }
        }

        override public IButton Select {
            get {
                return _inputs[19];
            }
            set {
                _inputs[19] = value;
            }
        }

        public IButton ToggleLeftStick {
            get {
                return _inputs[20];
            }
            set {
                _inputs[20] = value;
            }
        }

        public IButton ToggleRightStick {
            get {
                return _inputs[21];
            }
            set {
                _inputs[21] = value;
            }
        }

        public IButton AltA {
            get {
                return _inputs[22];
            }
            set {
                _inputs[22] = value;
            }
        }

    }
}