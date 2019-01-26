
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

    ///<summary>
    /// <para>Up, Down, Left, Right</para>
    ///  <para>Classic four directional keys that are used to move your character, menus etc.</para>
    ///  <para>This class stores all 4 directions and includes tools to manipulate the </para>
    ///  <para>data in unique ways. This is the Abstract class to manage directional keys input,
    ///  not the actual DPADs thats another class that uses this one.</para>
    ///</summary>

    public class DirectionalKeys {

        const float
            ZONE_ONE = 1f,
            ZONE_TWO = 3f,
            ZONE_THREE = 5f,
            ZONE_FOUR = 7f,
            OUTSIDE_OF_ZONES = 9f;

        protected bool _rightKey, _upKey, _leftKey, _downKey;
        JoystickEmulator Joystick;

        public DirectionalKeys() {
            Set(false, false, false, false);
            Joystick = new JoystickEmulator(this);
        }

        public void Set(bool right, bool up, bool left, bool down) {
            _rightKey = right;
            _upKey = up;
            _leftKey = left;
            _downKey = down;
        }

        virtual public void FromAngle(float angle) {
            DisableAll();

            /**
            *Splitting a circle in 16 parts
            *I can use every couple of pieces to have the circle split giving equal priority
            *To each cardinal side of a D-Pad. later increased the size of the north, south, east, and west
            *cardinals so it feels better.
            */

            float DiagonalPriority = ControllerSettings.DPAD_FROM_ANGLE_MODIFIER_IN_RADIANS;

            if (angle < Trigonometry.CardinalDirection(ZONE_ONE + DiagonalPriority) && angle >= Trigonometry.CardinalDirection(-ZONE_ONE - DiagonalPriority)) {
                _rightKey = true;
            }
            else if ((angle >= Trigonometry.CardinalDirection(ZONE_FOUR - DiagonalPriority) && angle <= Trigonometry.CardinalDirection(OUTSIDE_OF_ZONES)) ||
               (angle >= Trigonometry.CardinalDirection(-OUTSIDE_OF_ZONES) && angle <= Trigonometry.CardinalDirection(-ZONE_FOUR + DiagonalPriority))) {
                _leftKey = true;
            }
            else if (angle >= Trigonometry.CardinalDirection(ZONE_TWO - DiagonalPriority) && angle < Trigonometry.CardinalDirection(ZONE_THREE + DiagonalPriority)) {
                _downKey = true;
            }
            else if (angle >= Trigonometry.CardinalDirection(-ZONE_THREE - DiagonalPriority) && angle < Trigonometry.CardinalDirection(-ZONE_TWO + DiagonalPriority)) {
                _upKey = true;
            }
            else if (angle >= Trigonometry.CardinalDirection(-ZONE_TWO) && angle < Trigonometry.CardinalDirection(-ZONE_ONE)) {
                _upKey = true;
                _rightKey = true;
            }
            else if (angle >= Trigonometry.CardinalDirection(ZONE_ONE) && angle < Trigonometry.CardinalDirection(ZONE_TWO)) {
                _rightKey = true;
                _downKey = true;
            }
            else if (angle >= Trigonometry.CardinalDirection(ZONE_THREE) && angle < Trigonometry.CardinalDirection(ZONE_FOUR)) {
                _downKey = true;
                _leftKey = true;
            }
            else if (angle >= Trigonometry.CardinalDirection(-ZONE_FOUR) && angle < Trigonometry.CardinalDirection(-ZONE_THREE)) {
                _leftKey = true;
                _upKey = true;
            }
        }

        virtual public void FromAnalogueInput(AnalogueInput joystick) {
            if (joystick.IsActive()) {
                FromAngle(joystick.Angle);
            }
            else {
                DisableAll();
            }
        }

        public float AngleFromKey() {
            if (UpKey) {
                if (RightKey) {
                    return Mathf.PI * 1.75f;
                }
                else if (LeftKey) {
                    return Mathf.PI * 1.25f;
                }
                else {
                    return Mathf.PI * 1.5f;
                }
            }
            else if (DownKey) {
                if (RightKey) {
                    return Mathf.PI * 0.25f;
                }
                else if (LeftKey) {
                    return Mathf.PI * 0.75f;
                }
                else {
                    return Mathf.PI * 0.5f;
                }
            }
            else if (RightKey) {
                return Mathf.PI * 0f;
            }
            else if (LeftKey) {
                return Mathf.PI * 1f;
            }
            return 0f;

        }


        public void Update(float delta) {

            /**
             * EMULATE JOYSTICK
             * */

            Joystick.Update(delta);

        }



        virtual public void DisableAll() {
            _rightKey = _upKey = _leftKey = _downKey = false;
        }

        public AnalogueInput GetEmulatedJoystick() {

            return Joystick.Joystick;

        }

        public bool IsActive() {

            return RightKey || UpKey || LeftKey || DownKey;
        }

        //PROPERTIES

        virtual public bool RightKey {
            get {
                return _rightKey;
            }
            set {
                _rightKey = value;
            }
        }

        virtual public bool UpKey {
            get {
                return _upKey;
            }
            set {
                _upKey = value;
            }
        }

        virtual public bool LeftKey {
            get {
                return _leftKey;
            }
            set {
                _leftKey = value;
            }
        }

        virtual public bool DownKey {
            get {
                return _downKey;
            }
            set {
                _downKey = value;
            }
        }




    }
}