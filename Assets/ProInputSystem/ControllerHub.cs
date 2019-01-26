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

    ///<summary>
    ///Change the controller profiles used in game, this class parses them ready to use.
    ///</summary>
    public static class ControllerHub {

        /** 
         *  Global Inputs are taken from three Controller Inputs, And 2 pc inputs.
         *  This is a models class, add as many controllers as you wish, controllers
         *  can be created and used individually and you can grab a controller input
         *  like xbox360 and change it's input position to have more controllers.
        */

        //TODO Dynamically detect controllers and swap them

        public static IController MainController = new Controller(Profiles.XBOX_360_PC);
        public static IController PCInput = new Controller(Profiles.PC_MAC);
        public static IController PCInputAlt = new Controller(Profiles.PC_MAC_ALT);
    }
}
