
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
using System.Collections;

namespace ProInputSystem {
    public class BaseInputClass {

        protected ButtonType _type;
		protected KeyCode _keycode;
		protected AccessType _accesstype = AccessType.PC;
		protected int _controllerIndex = 0;
		protected int _buttonControllerIndex = 0;
		protected string _name;
		protected bool _enabled = true; //enabled by default

		virtual public bool IsPressed() {
			throw new System.NotImplementedException("needs to be inherited this is for subclasses");
		}
		virtual public bool IsUp() {
			throw new System.NotImplementedException("needs to be inherited this is for subclasses");
		}
        virtual public float GetAxis() {
            throw new System.NotImplementedException("needs to be inherited this is for subclasses");
        }
    }
}