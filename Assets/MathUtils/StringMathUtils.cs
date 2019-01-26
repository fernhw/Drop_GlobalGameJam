/** 
* Copyright (c) 2011-2017 Fernando Holguin Weber , and Studio Libeccio - All Rights Reserved
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
* Fernando Holguin Weber, <contact@fernhw.com>,<http://fernhw.com>,<@fern_hw>
* Studio Libeccio, <@studiolibeccio> <studiolibeccio.com>
* 
*/

using UnityEngine;

namespace MathUtils {
    public static class StringMathUtils {

        static readonly char dot = char.Parse(".");

        /// <summary>
        /// Create a string with added decimals for string consistency
        /// </summary>

        public static string AddDecimals(double Input, int decimalsMinimum = 1,
                                         int integerminimum = 1, bool addSymbol = false,
                                         string integerFiller = "0", string decimalFiller = "0") {
            return AddDecimals((float)Input, decimalsMinimum, integerminimum, addSymbol,
                               integerFiller,decimalFiller);
        }


        public static string AddDecimals(float Input, int decimalsMinimum = 1,
                                         int integerminimum = 1, bool addSymbol = false,
                                         string integerFiller = "0", string decimalFiller = "0") {
            float decimalsPower = Mathf.Pow(10, decimalsMinimum);
            Input *= decimalsPower;
            string result = (Mathf.Round(Input) / decimalsPower) + "";
            string addAfter = "";
            string addABefore = "";
            string[] breakString = result.Split(dot);
            if (breakString.Length == 1) {
                if (decimalsMinimum > 0) {
                    addAfter = "." + decimalFiller;
                }
                int decimalsAdded = 1;
                while (decimalsAdded < decimalsMinimum) {
                    decimalsAdded++;
                    addAfter += decimalFiller;
                }
				while (breakString[0].Length + addABefore.Length < integerminimum) {
					addABefore += integerFiller;
				}
            }
            else {
                while (breakString[1].Length + addAfter.Length < decimalsMinimum) {
                    addAfter += decimalFiller;
                }
                while (breakString[0].Length + addABefore.Length < integerminimum) {
                    addABefore += integerFiller;
                }
            }
            result = addABefore + (Mathf.Round(Input) / decimalsPower) + addAfter;

            if (!addSymbol)
                return result;

            if (Mathf.Round(Input) >= 0) {
                result = "+" + result;
            }
            return result;
        }


    }
}