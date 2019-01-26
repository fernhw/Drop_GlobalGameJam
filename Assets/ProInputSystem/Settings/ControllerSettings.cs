
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
static class ControllerSettings  {

//not indented on purpose, don't indent.


public const float

BASE_CONTROLLER_DEAD_ZONE = 0.5f, //TODO SEND IT TO OPTIONS MAYBE
DPAD_FROM_ANGLE_MODIFIER_IN_RADIANS = .1f,

ANALOGUE_TO_BUTTON_EMULATION_DISTANCE_BUTTON_DOWN = .68f,
ANALOGUE_TO_BUTTON_EMULATION_DISTANCE_BUTTON_UP = .2f,

JOYSTICK_EMULATOR_MINIUM_DISTANCE_ANGLE_ANIMATION = .4f,
JOYSTICK_EMULATOR_COORD_SPEED = .12f,
JOYSTICK_EMULATOR_ANGLE_DISTANCE_TO_ACTIVATE_COORD_ANIMATION = Mathf.PI*.6f,
JOYSTICK_EMULATOR_ANIMATION_TIME_PER_RADIAN = .1f,
JOYSTICK_EMULATOR_MAX_ANIMATION_TIME_PER_CHANGE = .1f,
JOYSTICK_EMULATOR_DISTANCE_SPEED = 4f

;

        
}
}
