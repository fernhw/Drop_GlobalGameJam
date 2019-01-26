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

namespace ProInputSystem {

    /**
     * Create a joystick from directional keys.
         */

    public class JoystickEmulator {

        AnalogueInput _joystick;

        bool directionalKeysWhereActiveThePreviousFrame = false;
        bool animationThroughCoords = false;

        float previousAngle = 0;
        float timeInAngleGoal = 0;

        float timeLimitOnAngle = 1;
        float angleDistance = 0;
        float lastAngle;
        float angleFromDirectionalKeys = 0;               
        float angleGoal;

        Vector2 previousPositionVector, newPositionVector, totalMovementVector;
     
        DirectionalKeys directionalKeys;

        public JoystickEmulator(DirectionalKeys KeysToEmulate) {
            directionalKeys = KeysToEmulate;
            _joystick = new AnalogueInput();
        }

        public void Update( float delta ) {
            delta*=1f;
            if (directionalKeysWhereActiveThePreviousFrame == false) {
                if (directionalKeys.IsActive()) {
                    directionalKeysWhereActiveThePreviousFrame = true;
                    angleFromDirectionalKeys = angleGoal = previousAngle = directionalKeys.AngleFromKey();
                    angleDistance = 0;
                    timeInAngleGoal = 0;
                    _joystick.Angle = lastAngle;
                    _joystick.Distance = ControllerSettings.JOYSTICK_EMULATOR_MINIUM_DISTANCE_ANGLE_ANIMATION;
                    return;
                }
                else {
                    return;
                }
            }

            if (directionalKeys.IsActive()) {
               // wasActive = true;
                angleFromDirectionalKeys = directionalKeys.AngleFromKey();
               
                if (angleFromDirectionalKeys != angleGoal) {
                    previousAngle = NormalizeAngle(lastAngle);
                    angleGoal = angleFromDirectionalKeys;
                    angleDistance =   angleGoal - previousAngle;
                    timeInAngleGoal = 0;
                    
                    if (angleDistance > Mathf.PI || angleDistance < -Mathf.PI) {
                        angleDistance = angleDistance - Mathf.PI * 2;
                    }
                    if (angleDistance > ControllerSettings.JOYSTICK_EMULATOR_ANGLE_DISTANCE_TO_ACTIVATE_COORD_ANIMATION || angleDistance < -ControllerSettings.JOYSTICK_EMULATOR_ANGLE_DISTANCE_TO_ACTIVATE_COORD_ANIMATION) {

                        animationThroughCoords = true;
                        previousPositionVector = new Vector2(Mathf.Cos(previousAngle), Mathf.Sin(previousAngle));
                        newPositionVector = new Vector2(Mathf.Cos(angleGoal), Mathf.Sin(angleGoal));

                        totalMovementVector = previousPositionVector - newPositionVector;
                        timeLimitOnAngle = ControllerSettings.JOYSTICK_EMULATOR_COORD_SPEED;

                    }
                    else {
                        animationThroughCoords = false;
                                               
                        timeLimitOnAngle =  Mathf.Abs(angleDistance)*ControllerSettings.JOYSTICK_EMULATOR_ANIMATION_TIME_PER_RADIAN + Mathf.Epsilon;
                        if (timeLimitOnAngle > ControllerSettings.JOYSTICK_EMULATOR_MAX_ANIMATION_TIME_PER_CHANGE)
                            timeLimitOnAngle = ControllerSettings.JOYSTICK_EMULATOR_MAX_ANIMATION_TIME_PER_CHANGE;
                    }                  
                }
            }
            else {
                _joystick.Distance = 0;
                directionalKeysWhereActiveThePreviousFrame = false;
                animationThroughCoords=false;
            }

            float percentageOfAnimation = (timeInAngleGoal / timeLimitOnAngle);

            if (percentageOfAnimation > 1) {
                percentageOfAnimation = 1;
            }
            if (animationThroughCoords) {
                lastAngle = previousAngle + angleDistance * percentageOfAnimation;
                Vector2 currentPositionVector = previousPositionVector - totalMovementVector * percentageOfAnimation;
                _joystick.Distance = Mathf.Sqrt(currentPositionVector.y * currentPositionVector.y +  currentPositionVector.x * currentPositionVector.x);
                _joystick.Angle = Mathf.Atan2(currentPositionVector.y, currentPositionVector.x);
            }
            else{

                if (_joystick.Distance < 1) {
                    _joystick.Distance += ControllerSettings.JOYSTICK_EMULATOR_DISTANCE_SPEED * delta;
                    if (_joystick.Distance >1) {
                        _joystick.Distance = 1;
                    }
                }

                if (percentageOfAnimation >= 1 && !directionalKeys.IsActive()) {
                    _joystick.Distance = ControllerSettings.JOYSTICK_EMULATOR_MINIUM_DISTANCE_ANGLE_ANIMATION;
                }
                lastAngle = previousAngle + angleDistance * percentageOfAnimation;
                _joystick.Angle = lastAngle;
            }
            timeInAngleGoal += delta;
        }

        float NormalizeAngle(float angleIn) {
            float cos = Mathf.Cos(angleIn);
            float sin = Mathf.Sin(angleIn);
            return Mathf.Atan2(sin, cos);
        }

        public AnalogueInput Joystick {
            get {
                return _joystick;
            }
            set {
                _joystick=value;
            }
        }

    }
}