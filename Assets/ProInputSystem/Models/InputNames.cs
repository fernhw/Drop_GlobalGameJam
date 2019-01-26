
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

namespace ProInputSystem {

    public class InputNames
    {

        /*              NOTE FROM CREATOR:
         * 
         * These are the 311 Axes that are set with the InputSettings file I provide.
         * Make sure to set it up, Unity does not have any other way to access the axes
         * The are split for individual controller detection. If you have any other axes
         * just throw it in the pile. Don't forget of replacing the InputSettings files
         * in project settings pls thank u.
         * 
         *          -Fernhw
        */

        private static readonly List<string> _axesInputs = new List<string>(new[]
        {
        "mouse_axis_0","mouse_axis_1","mouse_axis_2"
    });

        static readonly List<string> controller_0_axes = new List<string>(new[]
       {
        "joy_0_axis_0","joy_0_axis_1","joy_0_axis_2","joy_0_axis_3","joy_0_axis_4","joy_0_axis_5","joy_0_axis_6",
        "joy_0_axis_7","joy_0_axis_8","joy_0_axis_9","joy_0_axis_10","joy_0_axis_11","joy_0_axis_12","joy_0_axis_13","joy_0_axis_14",
        "joy_0_axis_15","joy_0_axis_16","joy_0_axis_17","joy_0_axis_18","joy_0_axis_19","joy_0_axis_20","joy_0_axis_21",
        "joy_0_axis_22","joy_0_axis_23","joy_0_axis_24","joy_0_axis_25","joy_0_axis_26","joy_0_axis_27"
    });
        static readonly List<string> controller_1_axes = new List<string>(new[]
        {
        "joy_1_axis_0", "joy_1_axis_1", "joy_1_axis_2", "joy_1_axis_3", "joy_1_axis_4", "joy_1_axis_5", "joy_1_axis_6",
        "joy_1_axis_7", "joy_1_axis_8", "joy_1_axis_9", "joy_1_axis_10", "joy_1_axis_11", "joy_1_axis_12", "joy_1_axis_13", "joy_1_axis_14",
        "joy_1_axis_15", "joy_1_axis_16", "joy_1_axis_17", "joy_1_axis_18", "joy_1_axis_19", "joy_1_axis_20", "joy_1_axis_21",
        "joy_1_axis_22", "joy_1_axis_23", "joy_1_axis_24", "joy_1_axis_25", "joy_1_axis_26", "joy_1_axis_27"
    });
        static readonly List<string> controller_2_axes = new List<string>(new[]
        {
        "joy_2_axis_0","joy_2_axis_1","joy_2_axis_2","joy_2_axis_3","joy_2_axis_4","joy_2_axis_5","joy_2_axis_6",
        "joy_2_axis_7","joy_2_axis_8","joy_2_axis_9","joy_2_axis_10","joy_2_axis_11","joy_2_axis_12","joy_2_axis_13","joy_2_axis_14",
        "joy_2_axis_15","joy_2_axis_16","joy_2_axis_17","joy_2_axis_18","joy_2_axis_19","joy_2_axis_20","joy_2_axis_21",
        "joy_2_axis_22","joy_2_axis_23","joy_2_axis_24","joy_2_axis_25","joy_2_axis_26","joy_2_axis_27"
    });
        static readonly List<string> controller_3_axes = new List<string>(new[]
        {
        "joy_3_axis_0","joy_3_axis_1","joy_3_axis_2","joy_3_axis_3","joy_3_axis_4","joy_3_axis_5","joy_3_axis_6",
        "joy_3_axis_7","joy_3_axis_8","joy_3_axis_9","joy_3_axis_10","joy_3_axis_11","joy_3_axis_12","joy_3_axis_13","joy_3_axis_14",
        "joy_3_axis_15","joy_3_axis_16","joy_3_axis_17","joy_3_axis_18","joy_3_axis_19","joy_3_axis_20","joy_3_axis_21",
        "joy_3_axis_22","joy_3_axis_23","joy_3_axis_24","joy_3_axis_25","joy_3_axis_26","joy_3_axis_27"

    });
        static readonly List<string> controller_4_axes = new List<string>(new[]
        {
         "joy_4_axis_0","joy_4_axis_1","joy_4_axis_2","joy_4_axis_3","joy_4_axis_4","joy_4_axis_5","joy_4_axis_6",
        "joy_4_axis_7","joy_4_axis_8","joy_4_axis_9","joy_4_axis_10","joy_4_axis_11","joy_4_axis_12","joy_4_axis_13","joy_4_axis_14",
        "joy_4_axis_15","joy_4_axis_16","joy_4_axis_17","joy_4_axis_18","joy_4_axis_19","joy_4_axis_20","joy_4_axis_21",
        "joy_4_axis_22","joy_4_axis_23","joy_4_axis_24","joy_4_axis_25","joy_4_axis_26","joy_4_axis_27"
    });
        static readonly List<string> controller_5_axes = new List<string>(new[]
        {
         "joy_5_axis_0","joy_5_axis_1","joy_5_axis_2","joy_5_axis_3","joy_5_axis_4","joy_5_axis_5","joy_5_axis_6",
        "joy_5_axis_7","joy_5_axis_8","joy_5_axis_9","joy_5_axis_10","joy_5_axis_11","joy_5_axis_12","joy_5_axis_13","joy_5_axis_14",
        "joy_5_axis_15","joy_5_axis_16","joy_5_axis_17","joy_5_axis_18","joy_5_axis_19","joy_5_axis_20","joy_5_axis_21",
        "joy_5_axis_22","joy_5_axis_23","joy_5_axis_24","joy_5_axis_25","joy_5_axis_26","joy_5_axis_27"
    });
        static readonly List<string> controller_6_axes = new List<string>(new[]
        {
         "joy_6_axis_0","joy_6_axis_1","joy_6_axis_2","joy_6_axis_3","joy_6_axis_4","joy_6_axis_5","joy_6_axis_6",
        "joy_6_axis_7","joy_6_axis_8","joy_6_axis_9","joy_6_axis_10","joy_6_axis_11","joy_6_axis_12","joy_6_axis_13","joy_6_axis_14",
        "joy_6_axis_15","joy_6_axis_16","joy_6_axis_17","joy_6_axis_18","joy_6_axis_19","joy_6_axis_20","joy_6_axis_21",
        "joy_6_axis_22","joy_6_axis_23","joy_6_axis_24","joy_6_axis_25","joy_6_axis_26","joy_6_axis_27"

    });
        static readonly List<string> controller_7_axes = new List<string>(new[]
        {
        "joy_7_axis_0","joy_7_axis_1","joy_7_axis_2","joy_7_axis_3","joy_7_axis_4","joy_7_axis_5","joy_7_axis_6",
        "joy_7_axis_7","joy_7_axis_8","joy_7_axis_9","joy_7_axis_10","joy_7_axis_11","joy_7_axis_12","joy_7_axis_13","joy_7_axis_14",
        "joy_7_axis_15","joy_7_axis_16","joy_7_axis_17","joy_7_axis_18","joy_7_axis_19","joy_7_axis_20","joy_7_axis_21",
        "joy_7_axis_22","joy_7_axis_23","joy_7_axis_24","joy_7_axis_25","joy_7_axis_26","joy_7_axis_27"
    });

        /*      NOTE FROM CREATOR:
         * 
         * Enums being bit based and not having a defined order(alphabetical who knows), if unity updates their language it could break
         * , also not being constants means their data means nothing, and also inputs have no way to identify which control
         * it is, they are all tossed in a pool, this system is a mess, i had no other choice than to MANUALLY place every single
         * Keycode in a list for keyboard, joysticks etc, they can be accessed via INDEX which is way lighter for save data, and
         * Player# input is split in sepparate lists, so we can save the button regardless of in which port the controller is.
         * 
         *       -Fernhw
        */
        private static readonly List<KeyCode> _keyboardInput = new List<KeyCode>(new[]
            {
        KeyCode.A,KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,
        KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9,KeyCode.AltGr,KeyCode.Ampersand, KeyCode.Asterisk,
        KeyCode.At,KeyCode.B,KeyCode.BackQuote,KeyCode.Backslash,KeyCode.Backspace,KeyCode.Break, KeyCode.C,KeyCode.CapsLock,
        KeyCode.Caret,KeyCode.Clear,KeyCode.Colon,KeyCode.Comma,KeyCode.D,KeyCode.Delete,KeyCode.Dollar,KeyCode.DoubleQuote,
        KeyCode.DownArrow,KeyCode.E,KeyCode.End,KeyCode.Equals,KeyCode.Escape,KeyCode.Exclaim, KeyCode.F,KeyCode.F1,
        KeyCode.F2,KeyCode.F3,KeyCode.F4,KeyCode.F5,KeyCode.F6,KeyCode.F7,KeyCode.F8,KeyCode.F9,KeyCode.F10,KeyCode.F11,
        KeyCode.F12,KeyCode.F13,KeyCode.F14,KeyCode.F15,KeyCode.G,KeyCode.Greater,KeyCode.H,KeyCode.Hash,KeyCode.Help,
        KeyCode.Home,KeyCode.I,KeyCode.Insert,KeyCode.J,KeyCode.K,KeyCode.Keypad0,KeyCode.Keypad1,KeyCode.Keypad2,
        KeyCode.Keypad3,KeyCode.Keypad4,KeyCode.Keypad5,KeyCode.Keypad6,KeyCode.Keypad7,KeyCode.Keypad8,KeyCode.Keypad9,
        KeyCode.KeypadDivide,KeyCode.KeypadEnter,KeyCode.KeypadEquals,KeyCode.KeypadMinus,KeyCode.KeypadPlus,
        KeyCode.KeypadPeriod,KeyCode.KeypadMultiply,KeyCode.L,KeyCode.LeftAlt,KeyCode.LeftApple,KeyCode.LeftArrow,
        KeyCode.LeftBracket,KeyCode.LeftCommand,KeyCode.LeftControl,KeyCode.LeftParen,KeyCode.LeftShift,KeyCode.LeftWindows,
        KeyCode.Less,KeyCode.M,KeyCode.Menu,KeyCode.Minus,KeyCode.Mouse0,KeyCode.Mouse1,KeyCode.Mouse2,KeyCode.Mouse3,
        KeyCode.Mouse4,KeyCode.Mouse5,KeyCode.Mouse6,KeyCode.N,/*Skips NONE*/KeyCode.Numlock,KeyCode.O,KeyCode.P,
        KeyCode.PageDown,KeyCode.PageUp,KeyCode.Pause,KeyCode.Period,KeyCode.Plus,KeyCode.Print,KeyCode.Q,KeyCode.Question,
        KeyCode.Quote,KeyCode.R,KeyCode.Return,KeyCode.RightAlt,KeyCode.RightApple,KeyCode.RightArrow,KeyCode.RightBracket,
        KeyCode.RightCommand,KeyCode.RightControl,KeyCode.RightParen,KeyCode.RightShift,KeyCode.RightWindows,KeyCode.S,
        KeyCode.ScrollLock,KeyCode.Semicolon,KeyCode.Slash,KeyCode.Space,KeyCode.SysReq,KeyCode.T,KeyCode.Tab,KeyCode.U,
        KeyCode.Underscore,KeyCode.UpArrow,KeyCode.V,KeyCode.W,KeyCode.X,KeyCode.Y,KeyCode.Z
    });

        static readonly List<KeyCode> _controller_1_input = new List<KeyCode>(new[]{
         KeyCode.Joystick1Button0,KeyCode.Joystick1Button1,KeyCode.Joystick1Button2,KeyCode.Joystick1Button3,
        KeyCode.Joystick1Button4,KeyCode.Joystick1Button5,KeyCode.Joystick1Button6,KeyCode.Joystick1Button7,
        KeyCode.Joystick1Button8,KeyCode.Joystick1Button9,KeyCode.Joystick1Button10,KeyCode.Joystick1Button11,
        KeyCode.Joystick1Button12,KeyCode.Joystick1Button13,KeyCode.Joystick1Button14,KeyCode.Joystick1Button15,
        KeyCode.Joystick1Button16,KeyCode.Joystick1Button17,KeyCode.Joystick1Button18,KeyCode.Joystick1Button19
    });
        static readonly List<KeyCode> _controller_2_input = new List<KeyCode>(new[]{
        KeyCode.Joystick2Button0,KeyCode.Joystick2Button1,KeyCode.Joystick2Button2,KeyCode.Joystick2Button3,
        KeyCode.Joystick2Button4,KeyCode.Joystick2Button5,KeyCode.Joystick2Button6,KeyCode.Joystick2Button7,
        KeyCode.Joystick2Button8,KeyCode.Joystick2Button9,KeyCode.Joystick2Button10,KeyCode.Joystick2Button11,
        KeyCode.Joystick2Button12,KeyCode.Joystick2Button13,KeyCode.Joystick2Button14,KeyCode.Joystick2Button15,
        KeyCode.Joystick2Button16,KeyCode.Joystick2Button17,KeyCode.Joystick2Button18,KeyCode.Joystick2Button19
    });
        static readonly List<KeyCode> _controller_3_input = new List<KeyCode>(new[]{
         KeyCode.Joystick3Button0,KeyCode.Joystick3Button1,KeyCode.Joystick3Button2,KeyCode.Joystick3Button3,
        KeyCode.Joystick3Button4,KeyCode.Joystick3Button5,KeyCode.Joystick3Button6,KeyCode.Joystick3Button7,
        KeyCode.Joystick3Button8,KeyCode.Joystick3Button9,KeyCode.Joystick3Button10,KeyCode.Joystick3Button11,
        KeyCode.Joystick3Button12,KeyCode.Joystick3Button13,KeyCode.Joystick3Button14,KeyCode.Joystick3Button15,
        KeyCode.Joystick3Button16,KeyCode.Joystick3Button17,KeyCode.Joystick3Button18,KeyCode.Joystick3Button19
    });
        static readonly List<KeyCode> _controller_4_input = new List<KeyCode>(new[]{
        KeyCode.Joystick4Button0,KeyCode.Joystick4Button1,KeyCode.Joystick4Button2,KeyCode.Joystick4Button3,
        KeyCode.Joystick4Button4,KeyCode.Joystick4Button5,KeyCode.Joystick4Button6,KeyCode.Joystick4Button7,
        KeyCode.Joystick4Button8,KeyCode.Joystick4Button9,KeyCode.Joystick4Button10,KeyCode.Joystick4Button11,
        KeyCode.Joystick4Button12,KeyCode.Joystick4Button13,KeyCode.Joystick4Button14,KeyCode.Joystick4Button15,
        KeyCode.Joystick4Button16,KeyCode.Joystick4Button17,KeyCode.Joystick4Button18,KeyCode.Joystick4Button19
    });
        static readonly List<KeyCode> _controller_5_input = new List<KeyCode>(new[]{
         KeyCode.Joystick5Button0,KeyCode.Joystick5Button1,KeyCode.Joystick5Button2,KeyCode.Joystick5Button3,
        KeyCode.Joystick5Button4,KeyCode.Joystick5Button5,KeyCode.Joystick5Button6,KeyCode.Joystick5Button7,
        KeyCode.Joystick5Button8,KeyCode.Joystick5Button9,KeyCode.Joystick5Button10,KeyCode.Joystick5Button11,
        KeyCode.Joystick5Button12,KeyCode.Joystick5Button13,KeyCode.Joystick5Button14,KeyCode.Joystick5Button15,
        KeyCode.Joystick5Button16,KeyCode.Joystick5Button17,KeyCode.Joystick5Button18,KeyCode.Joystick5Button19
    });
        static readonly List<KeyCode> _controller_6_input = new List<KeyCode>(new[]{
        KeyCode.Joystick6Button0,KeyCode.Joystick6Button1,KeyCode.Joystick6Button2,KeyCode.Joystick6Button3,
        KeyCode.Joystick6Button4,KeyCode.Joystick6Button5,KeyCode.Joystick6Button6,KeyCode.Joystick6Button7,
        KeyCode.Joystick6Button8,KeyCode.Joystick6Button9,KeyCode.Joystick6Button10,KeyCode.Joystick6Button11,
        KeyCode.Joystick6Button12,KeyCode.Joystick6Button13,KeyCode.Joystick6Button14,KeyCode.Joystick6Button15,
        KeyCode.Joystick6Button16,KeyCode.Joystick6Button17,KeyCode.Joystick6Button18,KeyCode.Joystick6Button19
    });
        static readonly List<KeyCode> _controller_7_input = new List<KeyCode>(new[]{
        KeyCode.Joystick7Button0,KeyCode.Joystick7Button1,KeyCode.Joystick7Button2,KeyCode.Joystick7Button3,
        KeyCode.Joystick7Button4,KeyCode.Joystick7Button5,KeyCode.Joystick7Button6,KeyCode.Joystick7Button7,
        KeyCode.Joystick7Button8,KeyCode.Joystick7Button9,KeyCode.Joystick7Button10,KeyCode.Joystick7Button11,
        KeyCode.Joystick7Button12,KeyCode.Joystick7Button13,KeyCode.Joystick7Button14,KeyCode.Joystick7Button15,
        KeyCode.Joystick7Button16,KeyCode.Joystick7Button17,KeyCode.Joystick7Button18,KeyCode.Joystick7Button19
    });
        static readonly List<KeyCode> _controller_8_input = new List<KeyCode>(new[]{
        KeyCode.Joystick8Button0,KeyCode.Joystick8Button1,KeyCode.Joystick8Button2,KeyCode.Joystick8Button3,
        KeyCode.Joystick8Button4,KeyCode.Joystick8Button5,KeyCode.Joystick8Button6,KeyCode.Joystick8Button7,
        KeyCode.Joystick8Button8,KeyCode.Joystick8Button9,KeyCode.Joystick8Button10,KeyCode.Joystick8Button11,
        KeyCode.Joystick8Button12,KeyCode.Joystick8Button13,KeyCode.Joystick8Button14,KeyCode.Joystick8Button15,
        KeyCode.Joystick8Button16,KeyCode.Joystick8Button17,KeyCode.Joystick8Button18,KeyCode.Joystick8Button19
    });

        public static List<KeyCode> KeyboardInput {
            get { return _keyboardInput; }
        }
        public static readonly List<List<KeyCode>> ControllerInput = new List<List<KeyCode>>(
            new[]{
        _controller_1_input,_controller_2_input,_controller_3_input,_controller_4_input,
        _controller_5_input,_controller_6_input,_controller_7_input,_controller_8_input
        });

        public static List<string> PcAxes {
            get { return _axesInputs; }
        }

        static readonly List<List<string>> _controllerAxes = new List<List<string>>(
            new[]{
        controller_0_axes,controller_1_axes,controller_2_axes,controller_3_axes,
        controller_4_axes,controller_5_axes,controller_6_axes,controller_7_axes
        });

        public static List<List<string>> ControllerAxes {
            get { return _controllerAxes; }
        }

        public static int GetID(KeyCode keyCode){
            
            return 1;
        }
    }
}



/*              --Leftovers--- LAST NOTE:
 * 
 *  There are 311 possible axes for 12 controllers, but there's only button input for up to 8 controllers, in Unity...
 *  Now you're probably asking yourself. 
 *          WHY!?
 *  Same here no clue. I will keep these listed here. out of consistency and memory.
 * 
 *  In case we ever need them. OR if you want 12 controllers and 4 of them without
 *  button detection, for some reason, be my guest.
 * 
 *
        "joy_8_axis_0","joy_8_axis_1","joy_8_axis_2","joy_8_axis_3","joy_8_axis_4","joy_8_axis_5","joy_8_axis_6",
        "joy_8_axis_7","joy_8_axis_8","joy_8_axis_9","joy_8_axis_10","joy_8_axis_11","joy_8_axis_12","joy_8_axis_13","joy_8_axis_14",
        "joy_8_axis_15","joy_8_axis_16","joy_8_axis_17","joy_8_axis_18","joy_8_axis_19","joy_8_axis_20","joy_8_axis_21",
        "joy_8_axis_22","joy_8_axis_23","joy_8_axis_24","joy_8_axis_25","joy_8_axis_26","joy_8_axis_27",

        "joy_9_axis_0","joy_9_axis_1","joy_9_axis_2","joy_9_axis_3","joy_9_axis_4","joy_9_axis_5","joy_9_axis_6",
        "joy_9_axis_7","joy_9_axis_8","joy_9_axis_9","joy_9_axis_10","joy_9_axis_11","joy_9_axis_12","joy_9_axis_13","joy_9_axis_14",
        "joy_9_axis_15","joy_9_axis_16","joy_9_axis_17","joy_9_axis_18","joy_9_axis_19","joy_9_axis_20","joy_9_axis_21",
        "joy_9_axis_22","joy_9_axis_23","joy_9_axis_24","joy_9_axis_25","joy_9_axis_26","joy_9_axis_27",

        "joy_10_axis_0","joy_10_axis_1","joy_10_axis_2","joy_10_axis_3","joy_10_axis_4","joy_10_axis_5","joy_10_axis_6",
        "joy_10_axis_7","joy_10_axis_8","joy_10_axis_9","joy_10_axis_10","joy_10_axis_11","joy_10_axis_12","joy_10_axis_13","joy_10_axis_14",
        "joy_10_axis_15","joy_10_axis_16","joy_10_axis_17","joy_10_axis_18","joy_10_axis_19","joy_10_axis_20","joy_10_axis_21",
        "joy_10_axis_22","joy_10_axis_23","joy_10_axis_24","joy_10_axis_25","joy_10_axis_26","joy_10_axis_27"

*/
