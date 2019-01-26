
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
    /// Promises returning all the data any 20 input Xbox360 like controller has.
    /// </summary>

    public interface IController {

        string Name {
            get;
        }
        IButton UpDpad {
            get; set;
        }
        IButton DownDpad {
            get; set;
        }
        IButton LeftDpad {
            get; set;
        }
        IButton RightDpad {
            get; set;
        }
        DpadButton Dpad {
            get; set;
        }
        IButton XAxisLeft {
            get;
        }
        IButton YAxisLeft {
            get;
        }
        IButton XAxisRight {
            get;
        }
        IButton YAxisRight {
            get;
        }
        Stick RightStick {
            get; 
        }
        Stick LeftStick {
            get; 
        }
        IButton A {
            get; set;
        }
        IButton B {
            get; set;
        }
        IButton X {
            get; set;
        }
        IButton Y {
            get; set;
        }
        IButton R {
            get; set;
        }
        IButton L {
            get; set;
        }
        IButton L2 {
            get; set;
        }
        IButton R2 {
            get; set;
        }
        IButton JoyClickLeft {
            get; set;
        }
        IButton JoyClickRight {
            get; set;
        }
        IButton Start {
            get; set;
        }
        IButton Select {
            get; set;
        }
        void Update(float delta);
        string GetDetailedInput();
    }
}