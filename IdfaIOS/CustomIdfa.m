#import <AdSupport/AdSupport.h>
#import <AppTrackingTransparency/AppTrackingTransparency.h>
#import <CoreLocation/CoreLocation.h>

//Please ensure this path points to the file 'Assets/Plugins/iOS/Amplitude/Amplitude.h'
#import "../../Plugins/iOS/Amplitude/Amplitude.h"

typedef NSString* (^AMPAdSupportBlock)(void);
typedef NSDictionary* (^AMPLocationInfoBlock)(void);

// Converts C style string to NSString
// Note, this will produce C++ compiler/linker errors if this function is renamed to ToNSString or any function in Objective C++ files
NSString* ToNSString2(const char* string)
{
    if (string)
        return [NSString stringWithUTF8String: string];
    else
        return nil;
}

void setIdfaBlockInternal(const char* instanceName)
{
    NSLog(@"Setting idfa block");
    AMPAdSupportBlock adSupportBlock = ^{
        NSUUID *adUuid = [[ASIdentifierManager sharedManager] advertisingIdentifier];
        NSString *result = [adUuid UUIDString];
        return result;
    };
    [[Amplitude instanceWithName:ToNSString2(instanceName)] setAdSupportBlock:adSupportBlock];
}

@interface MyLocationManager : NSObject<CLLocationManagerDelegate> 
{
    CLLocationManager *locationManager;
    CLLocation *currentLocation;
} 
@end

@implementation MyLocationManager 
- (void)currentLocationIdentifier
{
    locationManager = [[CLLocationManager alloc] init];
    locationManager.delegate = self;
    if ([CLLocationManager locationServicesEnabled] && 
            [CLLocationManager authorizationStatus] == kCLAuthorizationStatusAuthorizedWhenInUse) {
        NSLog(@"Authorized location");
        locationManager.desiredAccuracy = kCLLocationAccuracyKilometer;
        locationManager.distanceFilter = 500; 
        [locationManager startUpdatingLocation];
    }
    else {
        NSLog(@"Not authorized to access location, asking for permission");
        [locationManager requestWhenInUseAuthorization];
    }
}

- (void)locationManager:(CLLocationManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus *)status {
    if ([CLLocationManager locationServicesEnabled] && status == kCLAuthorizationStatusAuthorizedWhenInUse) {
        NSLog(@"Authorized location with user prompt");
        locationManager.desiredAccuracy = kCLLocationAccuracyKilometer;
        locationManager.distanceFilter = 500; 
        [locationManager startUpdatingLocation];
    }
    else {
        NSLog(@"Did not authorize through user prompt");
    }
}

- (void)locationManager:(CLLocationManager *)manager didUpdateLocations:(NSArray *)locations {
    CLLocation* location = [locations lastObject];
    float lat = location.coordinate.latitude;
    float lng = location.coordinate.longitude;
	NSLog(@"Getting latitude %+.6f, longitude %+.6f\n",
            lat,
            lng);
    AMPLocationInfoBlock locationInfoBlock = ^{
        return @{
            @"lat" : [NSNumber numberWithFloat:lat],
            @"lng" : [NSNumber numberWithFloat:lng]
        };
    };
    [[Amplitude instanceWithName:ToNSString2(nil)] setLocationInfoBlock:locationInfoBlock];
}
@end

static MyLocationManager *myLocationManager = nil;

#pragma mark - Functions Exposed to C#

void setLocationInfoBlock(const char* instanceName) {
    if (!myLocationManager) {
        myLocationManager = [[MyLocationManager alloc] init];
    }
    [myLocationManager currentLocationIdentifier];
}

void setIdfaBlock(const char* instanceName) {
    if (@available(iOS 14, *)) {
        if ([ATTrackingManager trackingAuthorizationStatus] == ATTrackingManagerAuthorizationStatusAuthorized) {
            setIdfaBlockInternal(instanceName);
        }
        else {
            [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
                if (status == ATTrackingManagerAuthorizationStatusAuthorized) {
                    setIdfaBlockInternal(instanceName);
                }
            }];
        }
    }
    else {
        setIdfaBlockInternal(instanceName);
    }
}