
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

namespace ProInputSystem {

    /// <summary>
    /// UNIVERSAL INPUT CLASS 
    /// <para>That can be either an axis(float) or binary(bool), 
    /// this converts them, return analogue OR boolean when necesary.</para>
    /// <para>In other words, if you need a joystick but only have a keyboard this takes that job.</para>
    /// </summary>
    /// 
    public class Button:BaseInputClass, IButton {
        
		static readonly char FIRST_SEPPARATION_CHAR = char.Parse("<");
		static readonly char SECOND_SEPPARATION_CHAR = char.Parse("-");

        bool stickIsUp = false;
        bool insteadOfKeycodeUsingString = false;
		float reverseAnalogueInputForButton = 1.0f;


        public Button(string nameIdentifier) {

            insteadOfKeycodeUsingString = true;
            SetButton(nameIdentifier, ButtonType.BUTTON);
        }
        public Button(KeyCode nameIdentifier) {

            SetButton(nameIdentifier, ButtonType.BUTTON);
        }
        public Button(KeyCode nameIdentifier, ButtonType type) {

            if (type != ButtonType.BUTTON) {
                throw new System.ArgumentException("Only buttons can use keycodes!");
            }
            SetButton(nameIdentifier, type);
        }
        public Button(string nameIdentifier, ButtonType type) {

            if (type == ButtonType.BUTTON) {
                insteadOfKeycodeUsingString = true;
            }
            SetButton(nameIdentifier, type);
        }
        public Button(int buttonIndexInController, ButtonType type, AccessType accessType, int controller = 0) {

            _buttonControllerIndex = buttonIndexInController;
            _type = type;
            _controllerIndex = controller;
            if (type == ButtonType.ANALOGUE_REVERSED) {
                reverseAnalogueInputForButton = -1.0f;
            }
            if (accessType == AccessType.PC) {
                if (type == ButtonType.BUTTON) {
                    SetButton(InputNames.KeyboardInput[buttonIndexInController], type);
                }
                else {
                    SetButton(InputNames.PcAxes[buttonIndexInController], type);
                }
            }
            else if (accessType == AccessType.CONTROLLER) {
                if (type == ButtonType.BUTTON) {
                    _keycode = SetKeyCodeFromInputIndex(controller, buttonIndexInController);
                    SetButton(_keycode, type);
                }
                else {
                    _name = SetAxisFromInputIndex(controller, buttonIndexInController);
                    SetButton(_name, type);
                }
            }
            else {
                //ACCESS TYPE NONE
                _enabled = false;
                if (type == ButtonType.BUTTON) {
                    _keycode = SetKeyCodeFromInputIndex(0, 0);
                    SetButton(_keycode, type);
                }
                else {
                    _name = SetAxisFromInputIndex(0, 0);
                    SetButton(_name, type);
                }
            }

        }
       public static Button CreateButton(string parseString, int controller = 0) {
            
            if (parseString == "") {
                throw new System.ArgumentException("Cannot parse nothing.");
            }
            AccessType accessType;
            ButtonType typeOfSwitch = ButtonType.BUTTON;
            int index;
            string[] forType = parseString.Split(FIRST_SEPPARATION_CHAR);
            string[] forIndex = forType[0].Split(SECOND_SEPPARATION_CHAR);
            string accessTypeString = forIndex[0];
            if (accessTypeString == "0") {
                accessType = AccessType.PC;
            }
            else if (accessTypeString == "1") {
                accessType = AccessType.CONTROLLER;
            }
            else {
                accessType = AccessType.NONE;
            }
            string indexString = forIndex[1];
            int.TryParse(indexString, out index);
            string SwitchTypeString = forType[1];
            switch (SwitchTypeString) {
                case "bt":
                    typeOfSwitch = ButtonType.BUTTON;
                    break;
                case "an":
                    typeOfSwitch = ButtonType.ANALOGUE;
                    break;
                case "ar":
                    typeOfSwitch = ButtonType.ANALOGUE_REVERSED;
                    break;
            }
            return CreateButton(accessType, index, typeOfSwitch);
        }

        public string ReturnParseData() {
            
            string dataToParse = "";
            if (_accesstype == AccessType.PC) {
                dataToParse += "0-";
            }
            else if (_accesstype == AccessType.CONTROLLER) {
                dataToParse += "1-";
            }
            else {
                dataToParse += "3-";
            }
            dataToParse += _buttonControllerIndex + "<";
            switch (_type) {
                case ButtonType.BUTTON:
                    dataToParse += "bt";
                    break;
                case ButtonType.ANALOGUE:
                    dataToParse += "an";
                    break;
                case ButtonType.ANALOGUE_REVERSED:
                    dataToParse += "ar";
                    break;
            }
            return dataToParse;

        }

        public static Button CreateButton(AccessType inputAccess, int inputIndex, ButtonType type) {

            if (inputAccess == AccessType.PC) {
                if (type == ButtonType.BUTTON) {
                    return new Button(InputNames.KeyboardInput[inputIndex], type);
                }
                else {
                    return new Button(InputNames.PcAxes[inputIndex], type);
                }
            }
            else if (inputAccess == AccessType.CONTROLLER) {
                return new Button(inputIndex, type, inputAccess);
            }
            else {
                return new Button(inputIndex, type, inputAccess);
            }

        }

        public static KeyCode SetKeyCodeFromInputIndex(int controllerIndex, int inputIndex) {
            KeyCode controllerKey = InputNames.ControllerInput[controllerIndex][inputIndex];
            return controllerKey;
        }

        public static string SetAxisFromInputIndex(int controllerIndex, int inputIndex) {
            return InputNames.ControllerAxes[controllerIndex][inputIndex];
        }
        
        public int ControllerIndex {

            get {
                return _controllerIndex;
            }
            set {
                _controllerIndex = value;
                if (_type == ButtonType.BUTTON) {
                    _keycode = SetKeyCodeFromInputIndex(_controllerIndex, _buttonControllerIndex);
                    SetButton(_keycode, _type);
                }
                else {
                    _name = SetAxisFromInputIndex(_controllerIndex, _buttonControllerIndex);
                    SetButton(_name, _type);
                }
            }
        }

        public ButtonType GetButtonType() {

            return _type;
        }
        
        public override bool IsPressed() {

            //TODO: Xbox360 controller on mac needs exceptions for the triggers

            if (!Enabled) {
                return false;
            }

            switch (_type) {
                case ButtonType.ANALOGUE:
                case ButtonType.ANALOGUE_REVERSED:
                    //axis is only reversed for button emulations
                    float axis = Input.GetAxis(_name) * reverseAnalogueInputForButton;
                    if (axis < 0)
                        axis = 0;
                    return Mathf.Abs(axis) > ControllerSettings.ANALOGUE_TO_BUTTON_EMULATION_DISTANCE_BUTTON_DOWN;
                case ButtonType.BUTTON:
                    if (insteadOfKeycodeUsingString) {
                        return Input.GetKey(_name);
                    }
                    else {
                        return Input.GetKey(_keycode);
                    }
                default:
                    return false;
            }
        }

        public override bool IsUp() {

            if (!Enabled) {
                return false;
            }
            switch (_type) {
                case ButtonType.ANALOGUE:
                case ButtonType.ANALOGUE_REVERSED:
                    bool stickSnap = false;
                    float axis = Input.GetAxis(_name) * reverseAnalogueInputForButton;
                    if (axis < 0)
                        axis = 0;
                    axis = Mathf.Abs(axis);
                    if (axis > ControllerSettings.ANALOGUE_TO_BUTTON_EMULATION_DISTANCE_BUTTON_DOWN) {
                        stickIsUp = true;
                    }
                    if (stickIsUp && axis < ControllerSettings.ANALOGUE_TO_BUTTON_EMULATION_DISTANCE_BUTTON_UP) {
                        stickIsUp = false;
                        stickSnap = true;
                    }
                    return stickSnap;
                case ButtonType.BUTTON:
                    if (insteadOfKeycodeUsingString) {
                        return Input.GetKeyUp(_name);
                    }
                    else {
                        return Input.GetKeyUp(_keycode);
                    }
                default:
                    return false;
            }
        }

        public static Button Empty() {

            return new Button(0, ButtonType.BUTTON, AccessType.NONE);
        }

        private void SetButton(string nameIdentifier, ButtonType type) {
            
            if (type == ButtonType.ANALOGUE_REVERSED) {
                reverseAnalogueInputForButton = -1.0f;
            }
            _type = type;
            _name = nameIdentifier;
        }

        private void SetButton(KeyCode nameIdentifier, ButtonType type) {

            _type = type;
            _keycode = nameIdentifier;
        }
        
       //PROPERTIES

        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }
        public KeyCode Keycode {
            get {
                return _keycode;
            }
            set {
                _keycode = value;
            }
        }
        public AccessType ControllerAccessType {
            get {
                return _accesstype;
            }
            set {
                _accesstype = value;
            }
        }
        public int ButtonControllerIndex {
            get {
                return _buttonControllerIndex;
            }
            set {
                _buttonControllerIndex = value;
            }
        }
        public bool Enabled {
            get { return _enabled; }
        }
        // Interpret Input as an Axis Even if type is BUTTON.
        public new float GetAxis {
            get {
                if (!Enabled) {
                    return 0.0f;
                }
                switch (_type) {
                    case ButtonType.ANALOGUE:
                    case ButtonType.ANALOGUE_REVERSED:
                        return Input.GetAxis(_name)*reverseAnalogueInputForButton;
                    case ButtonType.BUTTON:
                        float buttonToFloat = 0.0f;
                        if (insteadOfKeycodeUsingString) {
                            if (Input.GetKey(_name)) {
                                buttonToFloat = 1.0f;
                            }
                        }
                        else {
                            if (Input.GetKey(_keycode)) {
                                buttonToFloat = 1.0f;
                            }
                        }
                        return buttonToFloat;
                    default:
                        return 0.0f;
                }
            }
        }
      
    }
}