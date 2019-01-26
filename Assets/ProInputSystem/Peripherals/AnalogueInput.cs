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

using UnityEngine;
using MathUtils;

namespace ProInputSystem {
	/// <summary>
	/// Analogue input is joystick coordinates but also returns angles and distance.
    /// This is the abstract class for the sticks not the actual joysticks
    /// </summary>
    public class AnalogueInput {

        protected float _angle;
        protected float _distance;
        protected float _cos;
        protected float _sine;
        protected float _deadZone = ControllerSettings.BASE_CONTROLLER_DEAD_ZONE;

        Vector2 localCoords;

        /// <summary>
        /// <para>This is an intermideary class for the Stick class, but it can also be used for emulated joysticks.</para>
        /// Adding or changing the coords will make the AnalogueInput Update it's Angle, and it will process a Cosine and Sine.
        /// </summary>
        /// <value>Input an angle</value>
        /// 
        public Vector2 Coords {
            get {
                return localCoords;
            }
            set {
                localCoords = value;
                UpdateFromCoords(value);
            }
        }

        public void UpdateFromCoords(Vector2 coords) {
            Angle = Mathf.Atan2(coords.y, coords.x);
            float distanceBetweenCoords = Utils.VectorDistance(coords.x, coords.y);
            if (distanceBetweenCoords>1f) {
                distanceBetweenCoords = 1f;
            }
            _distance = distanceBetweenCoords;
        }

        public void SetCoords(float x, float y) {
            Vector2 receivedVector = new Vector2(x, y);
            Coords = receivedVector;
        }

        public void SetCoords(Vector2 receivedVector) {
            Coords = receivedVector;
        }

        /// <summary>
        ///  Is joystick outside Dead Zone
        /// </summary>
        public bool IsActive() {
            bool outDeadzone = (Distance > DeadZone);
            return outDeadzone;
        }

        public float x {
            get {
                if (IsActive())
                    return Coords.x;
                else
                    return 0;
            }
        }
        public float y {
            get {
                if (IsActive())
                    return Coords.y;
                else
                    return 0;
            }
        }

        /// <summary>
        ///  X without the deadZone checker
        /// </summary>
        public float xRaw {
            get {
                return Coords.x;
            }
        }

        /// <summary>
        ///  Y without the deadZone checker
        /// </summary>
        public float yRaw {
            get {
                return Coords.y;
            }
        }

        /// <summary>
        /// Angle in radians.
        /// <para>Use Sine or Cosine from the class instead of using this to extract it.</para>
        /// <para>They are already processed.</para>
        /// </summary>
        public float Angle {
            get {
                return _angle;
            }
            set{
                _angle = value;
				_sine = Mathf.Sin(_angle); //changing angle updates sine and cosine
				_cos = Mathf.Cos(_angle);
            }
        }

        /// <summary>
        /// Sine of the angle, it's already processed.
        /// </summary>
        public float Sine {
            get {
                return _sine;
            }
        }

        /// <summary>
        /// Cosine of the angle, it's already processed.
        /// </summary>
        public float Cosine {
            get {
                return _cos;
            }
        }

        /// <summary>
        /// Distance from 0 to 1.
        /// <para> Unlike combined systems this data is raw and shows true distance without caring for the deadzone</para>
        /// <para> use IsActive() to check if it's within the deadzone, or LockedDistance</para>
        /// </summary>
        public float Distance {
            get {
                return _distance;
            }
            set {
                _distance = value;
            }
        }
        /// <summary>
        /// Distance from 0 to 1.
        /// <para> This version of Distance respects the analogue's deadzone.</para>
        /// </summary>
        public float LockedDistance {
            get {
                if (IsActive()) {
                    return _distance;
                }
                return 0;
            }
        }


        /// <summary>
        /// Modify DeadZone in this analogue input as you see fit.
        /// <para> Default deadzone is picked from:</para>
        /// <para> ProInputSystem.Settings.ControllerSettings.BASE_CONTROLLER_DEAD_ZONE</para>
        /// </summary>
        public float DeadZone {
            get {
                return _deadZone;
            }
            set {
                _deadZone=value;
            }
        }
    }
}