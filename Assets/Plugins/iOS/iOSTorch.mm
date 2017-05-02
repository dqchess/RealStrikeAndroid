#import <AVFoundation/AVFoundation.h>
@interface Torch : NSObject
{
    
}

- (void)setTorch:(bool)state;


@end

@implementation Torch


- (id)init
{
    self = [super init];
    return self;
}

- (void) setTorch: (bool)state
{
    
    AVCaptureDevice* device = nil; // find a device by position
    NSArray* allDevices = [AVCaptureDevice devices];
    for (AVCaptureDevice* currentDevice in allDevices) {
        if (currentDevice.position == AVCaptureDevicePositionBack) {     device = currentDevice;   }
    }
    if(device != nil){
//    AVCaptureDevice *device = [AVCaptureDevice defaultDeviceWithMediaType:AVMediaTypeVideo];
    if ([device hasTorch] && [device hasFlash])
    {
        [device lockForConfiguration:nil];
        if (state)
        {
            [device setTorchMode:AVCaptureTorchModeOn];
            [device setFlashMode:AVCaptureFlashModeOn];
        }
        else
        {
            [device setTorchMode:AVCaptureTorchModeOff];
            [device setFlashMode:AVCaptureFlashModeOff];
        }
        [device unlockForConfiguration];
    }
        NSLog(@"手电筒: %d",state);
    }
}
@end


static Torch *torch = nil;
extern "C"
{
    void _SetTorch(bool state)
    {
        if (torch == nil)
            torch = [[Torch alloc] init];
        
        [torch setTorch:state];
    }
}
