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
using MathUtils.Consts;

namespace MathUtils {

    /***
     * All operations that use angles, cosines, sines, etc
     */
    static class Trigonometry {

        /** Helps keep angles withing PI2 in size*/
        public static float NormalizeRadians(float angle) {

            float newAngle = angle;
            if (newAngle > Mathf.PI * 2) {
                newAngle = newAngle - Mathf.PI * 2;
            }
            if (newAngle < 0) {
                newAngle = newAngle + Mathf.PI * 2;
            }
            return newAngle;
        }

        //Radiant Quadrants are angles in PI*.125 there are 16 total
        public static float CardinalDirection(float radianQuadrantNumber) {
            return MathConsts.ONE_SIXTEENTH_OF_A_CIRCLE * radianQuadrantNumber;
        }

    }
}
