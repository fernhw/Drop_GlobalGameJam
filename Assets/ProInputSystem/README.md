# ProInput 0.5 <br/><br/>
### by fernhw (@fern_hw, fernhw.com)
* Manage Controllers in unity!<br/>
* Keep hold of controller profiles,<br/>
* Swap between them, map buttons in-game.<br/>
Unity doesn't have this out of the box WHY!? I Don't Know...<br/>

Unity's input system to put it in simple terms, sucks, you can't save controllers or even have full control of simple stuff like joystick axes without having to go through several hoops, and going through unity's regular controller mapping box is a pain, and let's be honest-- it's painfully awful.<br/>

I tried other controller systems and was terribly dissapointed.<br/>

This was created for OOP programmers like myself who want a good amount of customization and control this is not for beginners looking for an easy way. Help me improve this and make it better.<br/>

This is still in development and at the moment the thing is not where I want it to be, it works you can map controllers with the files in the Models folder, give it a whirl. It works out of the box with the XBOX360 controller, and Nintendo JOYCONs but I want to have it eventually detect these and change profiles on the fly.<br/>

## What so special about it.

* It stores Controller Profiles for easy access.<br/>
* It has a very well constructed <b>joystick emulator</b> that's way better than unity's.
* It can use both the controller and the keyboard as one. with <b>globalController</b>
* It stores and returns angle distance and position of joystick automatically for easy use in-game.
* It can use Joystick movements as buttons, if you press joystick up you can set it up as a button press.
* (Not yet) It lets you remap joysticks automatically
* (Not yet) It has native remapping.
* (Not yet) Use up to 11 individually mapped joystick controllers.
* (Not yet) It has a sweet set of debugging systems in it.
* It's free. MIT license. Go nuts.

## Getting Started
ProInput system is simple to use once it's set up. The system has 3 controller profiles that run in tandem:

* The main joystick controller. (an Xbox1/Ps4 like controller)
* The keyboard and mouse counterpart.
* A 2nd keyboard and mouse based controller.

Why 2 pc controllers? to have <u>alternate</u> keys, when you play a pc game you want the user to have more than one action button if they want, keyboards are big, you can ignore the third controller if you want.<br/> You can run as many as 11 joystick controllers in tandem with this system.



* After everything's set up, all you need to do is:<br/>

```
        if (ProInput.UpDpad) { //pressing up in the DPad.
            //Do stuff
        }
```

* Or,
```
        if (ProInput.L) { //pressing L in the controller.
            //Shoot
        }
```
* Combined action, the reason why this system was a must make for me.
```
//Pressing UP in the DPad, or Key UP, or W (Wasd) will return true
        if (ProInput.UpDpad) { 
            //Do stuff
        }
//Professional games have multiple inputs so people get to change quickly to their pcs without a moment's notice.
```

* This picks up UpDpad or L from all 3 controller profiles you can swap with both controller and your keyboard, after the profile set up you don't have to think of keycodes, or axes, or your sanity dripping out of your ear, just worry about what's important.<br/>

* Joysticks are accesible and grant you Angle, Distance, Cosine, and Sine, all extremely useful.
```
        //I use this in my game to move the character
        prototypePoint.moveX(ProInput.RightStick.Cosine*ProInput.RightStick.Distance*charSpeed, delta);
        prototypePoint.moveZ(-ProInput.RightStick.Sine*ProInput.RightStick.Distance*charSpeed, delta);
```
* The joysticks, populated with useful stuff for in-game stuff.
```
        ProInput.LeftStick // Left Joystick
        ProInput.RightStick // Left Joystick
        
        ProInput.RightStick.Coords; //Vector2 return X and Y as is
        ProInput.RightStick.Distance; // 0-1 movement from the center of the stick.
        ProInput.RightStick.Angle; //Angle in radians
        ProInput.RightStick.isActive; // is movement of the joystick outside of it's deadzone
        ProInput.GetDirectionals() //Get and emulated D-Pad (UpKey, DownKey, LeftKey, RightKey) great for UI
```

* Simplify the input commands in the game so you worry only about the game.

# Let's Begin
### Step1 
First of all copy it to your project, don't worry it wont bite, it's in it's own namespace titled ProInputSystem, comes with a class outside of the namespace called ProInput, that's your access point. I would rather you clone the whole thing <u>inside</u> your project so you can ask for pull requests, or improvements to the overall system, or you know... update it.

### Step2
### IMPORTANT
* Open the folder STARTING_UP inside it there's a file called InputManager.asset

* Copy
```
        /Assets/STARTING_UP/InputManager.asset
```

Now copy that file, and paste it in your ProjectSettings folder 
(next to your assets folder, you need to open the project directory)

* Paste and REPLACE to
```
        /ProjectSettings/InputManager.asset
```

This is so the class can read the controller input in the joysticks.

### Step3
All set up, now we Init the system: 

* In the Start of your project:
```
    ProInput.Init();
```

* And to Update:
```
    //delta being what you have for delta it usually is Time.deltaTime
    ProInput.UpdateInput(delta);
```

* Simpler example:

```

    void Start() {
         ProInput.Init(); //initialize
    }
    
    void Update() {
        // update system
        ProInput.UpdateInput(Time.deltaTime); 
        
         if (ProInput.L) {
            // Shoot
        }
        
    }
    
```



### Step4, Setting Up The Controllers.
Setting up. Go to Models/ControllerHub<br/>

You will see this. This is the 3 controllers we talked about: MainController is the Joystick based controller, your regular XBox 360 controller, PCInput is your keyboard and mouse mapped mapped with the same inputs as the main controller, and PCInputAlt is an extra keyboard controller to give the user more keys to work with.
```
    namespace ProInputSystem {
        public static class ControllerHub {
            public static IController MainController = new Controller(Profiles.XBOX_360_PC);
            public static IController PCInput = new Controller(Profiles.PC_MAC);
            public static IController PCInputAlt = new Controller(Profiles.PC_MAC_ALT);
        }
    }
```
Long story short Models/ControllerHub is YOUR HUB it is the place where you pick controllers, you probably want YOUR GAME's controls in here, you can also create your own controller hub, and controller profiles in your game's code.

<b>Controller</b> is the abstract class that manages a full 20 input controller. this is digested by the proInput class so you never need to interact with these don't worry. A controller is created with a <b>ControllerProfile</b>

In short feed OR modify a <b>ControllerProfile</b> so you can use it.


``` 
        public static IController MainController = new Controller(Profiles.XBOX_360_PC);
        //or
        public static IController MainController = new Controller(Profiles.XBOX_360_MAC);
        //or even
        public static IController MainController = new Controller(Profiles.NINTENDO_SWITCH_JOYCON);
```

### Step 5, Setting Up The Profiles.
You need to set up <b>ControllerProfile</b>s individually
(As of v0.5 there's no in game mapping so this has to be done manually, the system was created for this feature.)

EXAMPLE: (This is an XBOX 360 controller)
```
    ControllerProfile XBOX_360_PC = new ControllerProfile{
        Name = "XBOX 360 PC",
        UpDpad = new ButtonModel(AccessType.CONTROLLER, 6, ButtonType.ANALOGUE),
        DownDpad = new ButtonModel(AccessType.CONTROLLER, 6, ButtonType.ANALOGUE_REVERSED),
        LeftDpad = new ButtonModel(AccessType.CONTROLLER,5, ButtonType.ANALOGUE_REVERSED),
        RightDpad = new ButtonModel(AccessType.CONTROLLER, 5, ButtonType.ANALOGUE),
        A = new ButtonModel(AccessType.CONTROLLER, 0),
        B = new ButtonModel(AccessType.CONTROLLER, 1),
        X = new ButtonModel(AccessType.CONTROLLER, 2),
        Y = new ButtonModel(AccessType.CONTROLLER, 3),
        L = new ButtonModel(AccessType.CONTROLLER, 4),
        R = new ButtonModel(AccessType.CONTROLLER, 5),
        L2 = new ButtonModel(AccessType.CONTROLLER, 2, ButtonType.ANALOGUE),
        R2 = new ButtonModel(AccessType.CONTROLLER, 2, ButtonType.ANALOGUE_REVERSED),
        XAxisRight = new ButtonModel(AccessType.CONTROLLER, 3, ButtonType.ANALOGUE),
        YAxisRight = new ButtonModel(AccessType.CONTROLLER, 4, ButtonType.ANALOGUE),
        XAxisLeft = new ButtonModel(AccessType.CONTROLLER, 0, ButtonType.ANALOGUE),
        YAxisLeft = new ButtonModel(AccessType.CONTROLLER, 1, ButtonType.ANALOGUE),
        JoyClickLeft = new ButtonModel(AccessType.CONTROLLER,8),
        JoyClickRight = new ButtonModel(AccessType.CONTROLLER,9),
        Start = new ButtonModel(AccessType.CONTROLLER, 7),
        Select = new ButtonModel(AccessType.CONTROLLER, 6),
    };
```
It's index based it works based on the key ID like regular controllers.
Each Button in a controller has an id and these can be in different ports unlike
Unity's:
Example 1:
```
        new ButtonModel(AccessType.CONTROLLER, 1, ButtonType.ANALOGUE)
```
```
        //Type=Controller index=1 buttonType=Analogue         
        //This means the Axis is from a controller, it's index is 1, and it's a Joystick
```
Example 2:
```
        new ButtonModel(AccessType.CONTROLLER, 7,ButtonType.BUTTON)
```
```
        //Type=Controller index=1 buttonType=Button        
        //This means the button is from a controller, it's index is 7, and it's a Button
        
        new ButtonModel(AccessType.CONTROLLER, 7)
        
        //This is also acceptable as buttonModels are Buttons by default
```
Example 3:
```
        new ButtonModel(AccessType.PC, 31)
```

```
        //Type=PC (this means it's the keyboard) index=31 buttonType=Button (default)       
        //This means the button is from the computer (keyboard or mouse),
        //it's index is 31, and it's a Button.
```
Example 4:
```
        new ButtonModel(AccessType.CONTROLLER, 2, ButtonType.ANALOGUE_REVERSED)
```
```
        //Type=Controller index=2 buttonType=Analogue Reversed               
        //This means the Axis is from a controller, it's index is 2, and it's a Joystick.
        //The difference here is that the ANALOGUE_REVERSED button type reverses the axis.
        //This is useful when you want to grab a joystick angle as a button which the system does.
```
* Assign these button models into your own controller profile (step 5) and assign that profile to the controller hub (step 4).

###             BUT How do I get the Index? you ask?
Simple as long as Debug is Enabled F12 acts as an input mapper of sorts, ProInputOptions.DEBUG_ENABLED is true by default.

* If proInput is set up and you press F12 during runtime you will get this in the console:

```
        "-- Press Any Key, Or touch any Stick --"
```
* Press ANY KEY or Move ANY Joystick either from the keyboard or controller in hand.
You get this in the console:
```
        new ButtonModel(AccessType.PC, 128, ButtonType.BUTTON))
```
* Copy it to your key of choice in a profile. And populate a new controller.
* Assign new controller profile to one of the 3 controllers or access it in your game.
* Each controller has buttons directly accessible use the different classes as you see fit.

ButtonModel it's an abstract class to create a button, buttons need 3 pieces of data.
Accesstype

## Built With

* Unity

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Fernando Holguin W** - *Amy's Escape* - [@fern_hw](https://github.com/fernhw)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

#Thanks for reading and please enjoy and help improve the project.

## Notes by the author.

This system was created for Amy's Escape, a game I'm working on please support it by following it
@amysEscapeGame (twitter)
amysescape.com (website)
