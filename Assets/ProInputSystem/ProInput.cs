﻿﻿
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
using ProInputSystem;
/*
*          IMPORTANT NOTE
*
*  Needs to be innited and updated externally, via ->
*  
*           ProInput.Init();
*  
*  And updated inside a MonoBehaviour or a Timer->
*  
*          ProInput.UpdateControllerInput( [ deltaTime or ms passed ] );
*
*/

/// <summary>
/// The one Stop, One class to use and control all input.
///  <para>Don't forget to Init with:</para>
///  <para>ProInput.Init()</para>
/// </summary>
public static class ProInput {

    public static GlobalController Main;
    static bool waitingForKeyPress = false;
    static DirectionalKeys _globalDPad;    
	static Button debugButton, testButton;

    /// <summary>
    /// IMPORTANT: Initialize the system before running.
    /// <para>Don't forget to update to take full advantage if it with:</para>
    /// <para>ProInput.UpdateInput(float delta, bool debug)</para>
    /// </summary>
    public static void Init() {

        Main = new GlobalController(ControllerHub.MainController, ControllerHub.PCInput, ControllerHub.PCInputAlt);
		debugButton = new Button(DebugKeys.MAPPING_KEY);
		testButton = new Button(DebugKeys.TEST_KEY);
        _globalDPad = new DirectionalKeys();

    }
    /// <summary>
    /// Run this in your game Loop, it executes all the logic of ProInput system
    /// <para>This is needed for the joysticks and Emulator systems.</para>
    /// </summary>
    /// <param name="delta"> input a deltaTime, used mostly for the joystick Emulator </param>
    /// <param name="debug"> is Debug Enabled? </param>
    public static void UpdateInput(float delta, bool debug = false) {
        if (debug) {
            if (waitingForKeyPress) {
                InputMapper.GetSwitchPress(KeyDetected);
            }
            else {
                if (debugButton.IsUp()) {
                    waitingForKeyPress = true;
                    Debug.Log("-- Press Any Key, Or touch any Stick --");
                }
            }
        }

        //improve in main
        UpdateControllerInput(delta);

    }

    static void UpdateControllerInput(float delta) {

        Stick leftStick = ControllerHub.MainController.LeftStick;
        Stick rightStick = ControllerHub.MainController.LeftStick;
        Main.Update(delta);
        DirectionalKeys joystickDPadLeft = leftStick.ReturnDpad();
        //DirectionalKeys joystickDPadRight = rightStick.GetDirectionals();//not used yet
        DpadButton pcDpad = ControllerHub.MainController.Dpad;
        DpadButton keyboardDpad = ControllerHub.PCInput.Dpad;
        DpadButton keyboardDpad_alt = ControllerHub.PCInputAlt.Dpad;
        bool rightKeyGlobal = (
            joystickDPadLeft.RightKey ||
            RightDpad);        
        bool upKeyGlobal = (
            joystickDPadLeft.UpKey ||
            UpDpad);
        bool leftKeyGlobal = (
            joystickDPadLeft.LeftKey ||
            LeftDpad);
        bool downKeyGlobal = (
            joystickDPadLeft.DownKey ||
            DownDpad);
            _globalDPad.Set(rightKeyGlobal, upKeyGlobal, leftKeyGlobal, downKeyGlobal);

    }
	static void KeyDetected(int InputAccess, int inputIndex, ButtonType type) {

		waitingForKeyPress = false;
		if (InputAccess == 0) {
            Debug.Log("pressed, Keyboard, index = " + inputIndex);
			testButton = Button.CreateButton(AccessType.PC, inputIndex, type);
		}
		else {
            Debug.Log("pressed, Controller = " + (InputAccess - 1) + ", index = " + inputIndex + " type" + type);
			testButton = Button.CreateButton(AccessType.CONTROLLER, inputIndex, type);
		}
	}

    //PROPERTIES

    /// <summary>
    /// All Dpads combined.
    /// <para>Keyboard directionals + Keyboard ALT directionals</para><para> + Controller Hat Directional pad</para>
    /// </summary>
    public static DpadButton Dpad {
        get {
            return Main.Dpad;
        }
    }

    /// <summary>
    /// All Dpads combined, Plus Dpad Emulation from the Left stick, this is excelent for Menus.
    /// <para>Keyboard directionals + Keyboard ALT directionals +</para>
    /// <para> Controller Hat DPad + LeftJoystick EmulatedDpad</para>
    /// </summary>
    public static DirectionalKeys DpadForUI() {
        return _globalDPad;
    }

    /// <summary>
    /// Left stick, and Emulated joystick from globalDpad in one single input.
    /// </summary>
    public static AnalogueInput GlobalStick {
        get {
            return Main.GlobalStick;
        }
    }
    /// <summary>
    /// Left stick, and Emulated joystick from globalDpad in one single input.
    /// <para>Global action stick also emulates the controller's Dpad</para>
    /// </summary>
    public static AnalogueInput GlobalActionStick {
        get {
            return Main.GlobalActionStick;
        }
    }

    public static Stick LeftStick {
        get {
            return Main.LeftStick;
        }
    }
   
    public static Stick RightStick {
        get {
            return Main.RightStick;
        }
    }

    /// <summary>
    /// Access to only the on-hand game controller
    /// </summary>
    public static IController GameController {
        get {
            return Main.GameController;
        }
        set {
            Main.GameController = value;
        }
    }
    /// <summary>
    /// Access to only the Alternative PC Controller scheme
    /// </summary>
    public static IController PcAltController {
        get {
            return Main.GameController;
        }
        set {
            Main.GameController = value;
        }
    }
    /// <summary>
    /// Access to only the PC Controller scheme
    /// </summary>
    public static IController PcController {
        get {
            return Main.GameController;
        }
        set {
            Main.GameController = value;
        }
    }


    public static string Name {
        get {
            return Main.Name;
        }
    }
    public static bool UpDpad {
        get {
            return Main.UpDpad.IsPressed();
        }
    }
    public static bool DownDpad {
        get {
            return Main.DownDpad.IsPressed();
        }
    }
    public static bool LeftDpad {
        get {
            return Main.LeftDpad.IsPressed();
        }
    }
    public static bool RightDpad {
        get {
            return Main.RightDpad.IsPressed();
        }
    }
    public static bool A {
        get {
            return Main.A.IsPressed();
        }
    }
    public static bool B {
        get {
            return Main.B.IsPressed();
        }
    }
    public static bool X {
        get {
            return Main.X.IsPressed();
        }
    }
    public static bool Y {
        get {
            return Main.Y.IsPressed();
        }
    }
    public static bool R {
        get {
            return Main.R.IsPressed();
        }
    }
    public static bool L {
        get {
            return Main.L.IsPressed();
        }
    }
    public static bool L2 {
        get {
            return Main.L2.IsPressed();
        }
    }
    public static bool R2 {
        get {
            return Main.R2.IsPressed();
        }
    }
    public static bool JoyClickLeft {
        get {
            return Main.JoyClickLeft.IsPressed();
        }
    }
    public static bool JoyClickRight {
        get {
            return Main.JoyClickRight.IsPressed();
        }
    }
    public static bool Start {
        get {
            return Main.Start.IsPressed();
        }
    }
    public static bool Select {
        get {
            return Main.Select.IsPressed();
        }
    }

    public static AnalogueInput DpadStick {
        get {
            return Main.DpadActionStick;
        }
    }

}