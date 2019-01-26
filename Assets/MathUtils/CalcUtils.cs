


/**
 * Copyright (C) Fernando Holguin Weber, and Studio Libeccio - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential.
 * Written by Fernando Holguin <contact@fernhw.com>, January 2017
 * 
 * UNDER NO CIRCUMSTANCES IS Fernando Holguin Weber, OR STUDIO LIBECCIO, ITS PROGRAM DEVELOPERS OR SUPPLIERS LIABLE FOR ANY OF THE FOLLOWING, EVEN IF INFORMED OF THEIR POSSIBILITY: 
 * LOSS OF, OR DAMAGE TO, DATA;
 * DIRECT, SPECIAL, INCIDENTAL, OR INDIRECT DAMAGES, OR FOR ANY ECONOMIC CONSEQUENTIAL DAMAGES; OR
 * LOST PROFITS, BUSINESS, REVENUE, GOODWILL, OR ANTICIPATED SAVINGS.
 */

using UnityEngine;

namespace MathUtils {

    /// <summary>
    /// Math Shortcuts live here.
    /// </summary>
    public static class CalcUtils {
        
        public static float ToOneDecimal (double target){
            return Mathf.Round((((float)target)) / 1000) * 1000;
        }


        public static float ToOneDecimal(float target) {
			return Mathf.Round(((target)) / 1000) * 1000;
		}


        /// <summary>
        /// if any int is less than 0 it will be turned into zero. Otherwise it will return the float
        /// </summary>
        public static int IntAbs(int giveMeAbs) {
            if (giveMeAbs < 0)
                return -giveMeAbs;
            return giveMeAbs;
        }


        /// <summary>
        /// Make Pythagoras proud.
        /// Give me the hypotenuse.
        /// </summary>
        public static float VectorDistance(float xDifference, float yDifference) {
            return Mathf.Sqrt(xDifference*xDifference +yDifference*yDifference);
        }
        

        //Are floats equal
        /// <summary>
        /// Light and accurate float comparison.
        /// </summary>
        public static bool AreFloatsEqual(float float1, float float2) {
            return (Mathf.Abs(float1 - float2) < Mathf.Epsilon);
        }


        /// <summary>
        /// Light and accurate float comparison for multiple floats at once. 
        /// <para>Add all differences like this 
        /// Mathf.Abs(float1 - float2) + Mathf.Abs(float3 - float4)</para>
        /// <para>It will check if the sum of absolutes is less than epsilon
        /// for memory and readability sake this can't be any different.</para>
        /// </summary>
        /// <param name="addingAllMathAbs">
        ///  Mathf.Abs(_vector1.x - oldVector1X) + Mathf.Abs(_vector1.y - oldVector1Y);
        /// </param>
        public static bool AreTheseFloatsEqual(float addingAllMathAbs) {
            return (addingAllMathAbs < Mathf.Epsilon);
        }
        

        //If less than 0

        /// <summary>
        /// if any float is less than 0 it will be turned into zero. Otherwise it will return the float as is.
        /// </summary>
        public static float IfLessThanZeroReturnZero(float removeNegativity) {
            if (removeNegativity < 0)
                return 0;
            return removeNegativity;
        }


        /// <summary>
        /// if any int is less than 0 it will be turned into zero. Otherwise it will return the int as is.
        /// </summary>
        public static int IfLessThanZeroReturnZero(int removeNegativity) {
            if (removeNegativity < 0)
                return 0;
            return removeNegativity;
        }


        /// <summary>
        /// Grab any Vector 3 and make sure every coord is more than zero.
        /// if any coord is less than 0 it will be turned into zero.
        /// </summary>
        public static void IfLessThanZeroReturnZero(Vector3 removeNegatives) {
            if (removeNegatives.x < 0)
                removeNegatives.x = 0;
            if (removeNegatives.y < 0)
                removeNegatives.y = 0;
            if (removeNegatives.z < 0)
                removeNegatives.z = 0;
        }


        //If less than #

        /// <summary>
        /// if any float is less than # it will be turned into #. Otherwise it will return the float as is.
        /// </summary>
        public static float IfLessThan(float theNumber,float NoLessThan) {
            if (theNumber < NoLessThan)
                return NoLessThan;
            return theNumber;
        }


        /// <summary>
        /// if any int is less than # it will be turned into #. Otherwise it will return the int as is.
        /// </summary>
        public static int IfLessThan(int theNumber, int NoLessThan) {
            if (theNumber < NoLessThan)
                return NoLessThan;
            return theNumber;
        }


        /// <summary>
        /// Grab any Vector 3 and make sure every coord is more than zero.
        /// if any coord is less than 0 it will be turned into zero.
        /// </summary>
        public static void IfLessThan(Vector3 removeNegatives, float NoLessThan) {
            if (removeNegatives.x < NoLessThan)
                removeNegatives.x = NoLessThan;
            if (removeNegatives.y < NoLessThan)
                removeNegatives.y = NoLessThan;
            if (removeNegatives.z < NoLessThan)
                removeNegatives.z = NoLessThan;
        }


                     
    }
}