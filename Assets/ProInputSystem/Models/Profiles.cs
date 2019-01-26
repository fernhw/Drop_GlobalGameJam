
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

namespace ProInputSystem {

    /// <summary>
    /// <para>ProInputSystem.Profiles</para>
    /// <para>Controller profiles setup as X360 controllers.</para>
    /// Storing all controller configurations for further use.
    /// </summary>

    public static class Profiles {
        
       public static readonly ControllerProfile XBOX_360_MAC = new ControllerProfile {
            Name = "XBOX 360 MAC", //TODO Create exception for the mapping systemn cuz of the R2 and L2.
            UpDpad = new ButtonModel(AccessType.CONTROLLER, 5),
            DownDpad = new ButtonModel(AccessType.CONTROLLER, 6),
            LeftDpad = new ButtonModel(AccessType.CONTROLLER, 7),
            RightDpad = new ButtonModel(AccessType.CONTROLLER, 8),
            A = new ButtonModel(AccessType.CONTROLLER, 16),
            B = new ButtonModel(AccessType.CONTROLLER, 17),
            X = new ButtonModel(AccessType.CONTROLLER, 18),
            Y = new ButtonModel(AccessType.CONTROLLER, 19),
            L = new ButtonModel(AccessType.CONTROLLER, 13),
            R = new ButtonModel(AccessType.CONTROLLER, 14),
            L2 = new ButtonModel(AccessType.CONTROLLER, 5, ButtonType.ANALOGUE),
            R2 = new ButtonModel(AccessType.CONTROLLER, 4, ButtonType.ANALOGUE),
            XAxisRight = new ButtonModel(AccessType.CONTROLLER, 2, ButtonType.ANALOGUE),
            YAxisRight = new ButtonModel(AccessType.CONTROLLER, 3, ButtonType.ANALOGUE),
            XAxisLeft = new ButtonModel(AccessType.CONTROLLER, 0, ButtonType.ANALOGUE),
            YAxisLeft = new ButtonModel(AccessType.CONTROLLER, 1, ButtonType.ANALOGUE),
            JoyClickLeft = new ButtonModel(AccessType.CONTROLLER, 11),
            JoyClickRight = new ButtonModel(AccessType.CONTROLLER, 12),
            Start = new ButtonModel(AccessType.CONTROLLER, 9),
            Select = new ButtonModel(AccessType.CONTROLLER, 9),
        };

        public static readonly ControllerProfile XBOX_360_PC_WIRELESS = new ControllerProfile{
            Name = "XBOX 360 PC",
            UpDpad = new ButtonModel(AccessType.CONTROLLER, 6, ButtonType.ANALOGUE),
			DownDpad = new ButtonModel(AccessType.CONTROLLER, 6, ButtonType.ANALOGUE_REVERSED),
			LeftDpad = new ButtonModel(AccessType.CONTROLLER,5, ButtonType.ANALOGUE_REVERSED),
            RightDpad = new ButtonModel(AccessType.CONTROLLER, 5, ButtonType.ANALOGUE),
            A = new ButtonModel(AccessType.CONTROLLER, 0),
			B = new ButtonModel(AccessType.CONTROLLER, 1),
			X = new ButtonModel(AccessType.CONTROLLER, 2),
            Y = new ButtonModel(AccessType.CONTROLLER, 3),
			L = new ButtonModel(AccessType.CONTROLLER, 4),
			R = new ButtonModel(AccessType.CONTROLLER, 5),
            L2 = new ButtonModel(AccessType.CONTROLLER, 2, ButtonType.ANALOGUE),
            R2 = new ButtonModel(AccessType.CONTROLLER, 2, ButtonType.ANALOGUE_REVERSED),
			XAxisRight = new ButtonModel(AccessType.CONTROLLER, 3, ButtonType.ANALOGUE),
			YAxisRight = new ButtonModel(AccessType.CONTROLLER, 4, ButtonType.ANALOGUE),
            XAxisLeft = new ButtonModel(AccessType.CONTROLLER, 0, ButtonType.ANALOGUE),
            YAxisLeft = new ButtonModel(AccessType.CONTROLLER, 1, ButtonType.ANALOGUE),
            JoyClickLeft = new ButtonModel(AccessType.CONTROLLER,8),
            JoyClickRight = new ButtonModel(AccessType.CONTROLLER,9),
            Start = new ButtonModel(AccessType.CONTROLLER, 7),
            Select = new ButtonModel(AccessType.CONTROLLER, 6),
		};

        public static readonly ControllerProfile XBOX_360_PC = new ControllerProfile {
            Name = "XBOX 360 PC",
            UpDpad = new ButtonModel(AccessType.CONTROLLER, 6, ButtonType.ANALOGUE),
            DownDpad = new ButtonModel(AccessType.CONTROLLER, 6, ButtonType.ANALOGUE_REVERSED),
            LeftDpad = new ButtonModel(AccessType.CONTROLLER, 5, ButtonType.ANALOGUE_REVERSED),
            RightDpad = new ButtonModel(AccessType.CONTROLLER, 5, ButtonType.ANALOGUE),
            A = new ButtonModel(AccessType.CONTROLLER, 0),
            B = new ButtonModel(AccessType.CONTROLLER, 1),
            X = new ButtonModel(AccessType.CONTROLLER, 2),
            Y = new ButtonModel(AccessType.CONTROLLER, 3),
            L = new ButtonModel(AccessType.CONTROLLER, 4),
            R = new ButtonModel(AccessType.CONTROLLER, 5),
            L2 = new ButtonModel(AccessType.CONTROLLER, 2, ButtonType.ANALOGUE),
            R2 = new ButtonModel(AccessType.CONTROLLER, 2, ButtonType.ANALOGUE_REVERSED),
            XAxisRight = new ButtonModel(AccessType.CONTROLLER, 3, ButtonType.ANALOGUE),
            YAxisRight = new ButtonModel(AccessType.CONTROLLER, 4, ButtonType.ANALOGUE),
            XAxisLeft = new ButtonModel(AccessType.CONTROLLER, 0, ButtonType.ANALOGUE),
            YAxisLeft = new ButtonModel(AccessType.CONTROLLER, 1, ButtonType.ANALOGUE),
            JoyClickLeft = new ButtonModel(AccessType.CONTROLLER, 8),
            JoyClickRight = new ButtonModel(AccessType.CONTROLLER, 9),
            Start = new ButtonModel(AccessType.CONTROLLER, 7),
            Select = new ButtonModel(AccessType.CONTROLLER, 6),
        };

        public static readonly ControllerProfile PC_MAC = new ControllerProfile {
            Name = "KEYBOARD",
            UpDpad = new ButtonModel(AccessType.PC, 136),
            DownDpad = new ButtonModel(AccessType.PC, 124),
            LeftDpad = new ButtonModel(AccessType.PC, 0),
            RightDpad = new ButtonModel(AccessType.PC, 26),
            A = new ButtonModel(AccessType.PC, 31),
            ExtraA = new ButtonModel(AccessType.PC, 93),
            B = new ButtonModel(AccessType.PC, 137),
            X = new ButtonModel(AccessType.PC, 87),
            Start = new ButtonModel(AccessType.PC, 113),
            L = new ButtonModel(AccessType.PC, 36),
            R = new ButtonModel(AccessType.PC, 110),
            L2 = new ButtonModel(AccessType.PC, 85),
            XAxisRight = new ButtonModel(AccessType.PC, 0, ButtonType.ANALOGUE),
            YAxisRight = new ButtonModel(AccessType.PC, 1, ButtonType.ANALOGUE_REVERSED),
            JoyToggleRight = new ButtonModel(AccessType.PC, 94)
		};
        public static readonly ControllerProfile PC_MAC_ALT = new ControllerProfile {
            Name = "KEYBOARD_ALT",
		    UpDpad = new ButtonModel(AccessType.PC, 134),
			DownDpad = new ButtonModel(AccessType.PC, 30),
			LeftDpad = new ButtonModel(AccessType.PC, 82),
			RightDpad = new ButtonModel(AccessType.PC, 117),
			A = new ButtonModel(AccessType.PC, 20),
			B = new ButtonModel(AccessType.PC, 18),
			X = new ButtonModel(AccessType.PC, 135),
			Start = new ButtonModel(AccessType.PC, 34),
			L = new ButtonModel(AccessType.PC, 128),
			R = new ButtonModel(AccessType.PC, 139),
			L2 = new ButtonModel(AccessType.PC, 80)
        };

        public static readonly ControllerProfile NINTENDO_SWITCH_JOYCON = new ControllerProfile {
            /*
             * Joycons have no joysticks by themselves, but DPads from the sticks
             * Use only if your game doesn't need a lot of inputs, or Analogue input
             * Joycons have 13 inputs in comparison to the 20 inputs of an X360 controller
             * Currently the system as of 1.0 (aug 21) does not support combined joycons.
             * Besides I can't grab analogue input yet.
            */
            Name = "NINTENDO SWITCH JOY-CON L AND R",
            UpDpad = new ButtonModel(AccessType.CONTROLLER, 11, ButtonType.ANALOGUE_REVERSED),
            DownDpad = new ButtonModel(AccessType.CONTROLLER, 11, ButtonType.ANALOGUE),
            LeftDpad = new ButtonModel(AccessType.CONTROLLER, 10, ButtonType.ANALOGUE_REVERSED),
            RightDpad = new ButtonModel(AccessType.CONTROLLER, 10, ButtonType.ANALOGUE),
            A = new ButtonModel(AccessType.CONTROLLER, 0),
            B = new ButtonModel(AccessType.CONTROLLER, 1),
            X = new ButtonModel(AccessType.CONTROLLER, 2),
            Y = new ButtonModel(AccessType.CONTROLLER, 3),
            L = new ButtonModel(AccessType.CONTROLLER, 4),
            R = new ButtonModel(AccessType.CONTROLLER, 5),
            Start //joycons split have different Start buttons
               = new ButtonModel(new List<ButtonModel> {
                new ButtonModel(AccessType.CONTROLLER, 8),
                new ButtonModel(AccessType.CONTROLLER, 9),
           }),
            Select //joycons split have different Select buttons
               = new ButtonModel(new List<ButtonModel> {
                new ButtonModel(AccessType.CONTROLLER, 12),
                new ButtonModel(AccessType.CONTROLLER, 13),
           }),
            JoyClickLeft //joycons split have different Joystick click buttons
               = new ButtonModel(new List<ButtonModel> {
                new ButtonModel(AccessType.CONTROLLER, 10),
                new ButtonModel(AccessType.CONTROLLER, 11),
           }),
        };
            //The system also generates a string for easy data storage and shareability. ask for ParseString.
        public const string XBOX360_MAC_VIA_STRING_EXAMPLE =
            "XBOXCONTROLLER_MAC#1-5<bt>1-6<bt>1-7<bt>1-8<bt>1-0<an>1-1<an>1-2<an>" +
                "1-3<an>1-16<bt>1-17<bt>1-18<bt>1-19<bt>1-13<bt>1-14<bt>1-4<an>1-5" +
                "<an>1-11<bt>1-12<bt>1-9<bt>1-10<bt>3-0<bt>3-0<bt>3-0<bt>";
        /*  "XBOXCONTROLLER_MAC#" +
            "1-5<bt>1-6<bt>1-7<bt>1-8<bt>" + //Up,Down,Left,Right DPAD
            "1-0<an>1-1<an>1-2<an>1-3<an>" + //JoyXL,JoyYL,JoyXR,JoyYR,
            "1-16<bt>1-17<bt>1-18<bt>1-19<bt>" +//A,B,X,Y
            "1-13<bt>1-14<bt>1-4<an>1-5<an>" +//L1, R1, L2, R2
            "1-11<bt>1-12<bt>1-9<bt>1-10<bt>" +//JoyClickL, JoyClickR, Start, Select
            "3-0<bt>3-0<bt>3-0<bt>";//Analogue lock Joy1 and Joy2
            */

    }
}
