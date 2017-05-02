Welcome to iOSTorch. I've tried to create this to be as simple and easy to use as possible to serve as an example for real world native interaction on iOS as well as a drop and go flashlight app. Included is an example scene to demonstrate how to use the plugin but really all you have to do is call iOSTorch.On(); or iOSTorch.Off(); from a running script in your scene. There's no plugin setup to do beyond dropping this package in your assets folder. The Plugins/iOS folder automatically copies .mm files to your xcodeproject when you build.
It takes a bit for the camera to initialize the first time so if you want that first touch to instantly turn on the torch you can call Init() in your script's Start() method. This will initialize and keep a camera session running. It takes up a bit of CPU to do that, but not a ton.

If you're curious about how it works though, here's the basic program flow. You first call a static C# method in the iOSTorch class. That calls a static external method in the C# script. Which then calls an extern C function in the .mm file. The extern C function calls an Objective C function which finally calls the iOS api. 

The C# code is pretty heavily commented, so if you don't understand something go ahead and take a look. This was a learning experience for me though, so the native stuff may be a little rough around the edges. The .mm file is self contained. It's header stuff is not in a separate .h file as those don't seem to auto copy.

If you have questions or comments feel free to email me at: feedback@humeware.com


These functions require the user to be running iOS 4 unless otherwise specified. This is the earliest iOS a iPhone 4 can have, which was the first one with a camera flash. If you call the functions on a pre ios 4 device, it should fail gracefully.

Full function list:
void iOSTorch.Set(bool state) 		Sets the specified state of the torch.
void iOSTorch.Set(float level) 		(iOS 6 only) Sets the specified level of the torch's brightness. (0-1)
bool iOSTorch.Get()			Gets the current state of the torch. (true is on, false is off)
float iOSTorch.GetLevel()		(iOS 6 only) Gets the current brightness level of the torch
void iOSTorch.On(float level = 1)	Turns the torch on (and if specified, with the a set brightness (iOS 6 only))
void iOSTorch.Off()			Turns the torch off
void iOSTorch.Init()			Sets up a capture session so there is no delay when turning the torch on. Not required, but recommended.
void iOSTorch.Cleanup()			Disposes of the capture session setup in Init. This will be done automatically on program close if you don't do it.


Properties:
bool CurrentState			Gets or sets the current state of the torch. True means on, False means off.
float Level				(iOS 6 only)Gets or sets the currentbrightness level of the torch. (0-1)