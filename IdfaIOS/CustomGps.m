#import <CoreLocation/CoreLocation.h>
#import <Amplitude/Amplitude.h>

typedef NSDictionary* (^AMPLocationInfoBlock)(void);

@interface CustomLocationManager : NSObject<CLLocationManagerDelegate> 
@property (nonatomic, strong) CLLocationManager *locationManager;
@property (nonatomic, strong) NSString *instanceName;
@end

@implementation CustomLocationManager 

- (instancetype)init:(NSString *)instanceNameString {
    self = [super init];
    if (self) {
        self.instanceName = instanceNameString;
    }
    return self;
}

- (void)initializeLocationTracking {
    self.locationManager = [[CLLocationManager alloc] init];
    self.locationManager.delegate = self;
    if ([CLLocationManager locationServicesEnabled] && 
            [CLLocationManager authorizationStatus] == kCLAuthorizationStatusAuthorizedWhenInUse) {
        self.locationManager.desiredAccuracy = kCLLocationAccuracyKilometer;
        self.locationManager.distanceFilter = 500; 
        [self.locationManager startUpdatingLocation];
    } else {
        [self.locationManager requestWhenInUseAuthorization];
    }
}

- (void)locationManager:(CLLocationManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus *)status {
    if ([CLLocationManager locationServicesEnabled] && status == kCLAuthorizationStatusAuthorizedWhenInUse) {
        self.locationManager.desiredAccuracy = kCLLocationAccuracyKilometer;
        self.locationManager.distanceFilter = 500; 
        [self.locationManager startUpdatingLocation];
    }
}

- (void)locationManager:(CLLocationManager *)manager didUpdateLocations:(NSArray *)locations {
    CLLocation* location = [locations lastObject];
    float lat = location.coordinate.latitude;
    float lng = location.coordinate.longitude;
    AMPLocationInfoBlock locationInfoBlock = ^{
        return @{
            @"lat" : [NSNumber numberWithFloat:lat],
            @"lng" : [NSNumber numberWithFloat:lng]
        };
    };
    [[Amplitude instanceWithName:self.instanceName] setLocationInfoBlock:locationInfoBlock];
}

@end

static CustomLocationManager *customLocationManager = nil;

#pragma mark - Functions Exposed to C#

void setLocationInfoBlockWithInstanceName(const char* instanceName) {
    if (!customLocationManager) {
        NSString* convertedString = [NSString stringWithFormat:@"%s", instanceName];
        customLocationManager = [[CustomLocationManager alloc] init:convertedString];
    }
    [customLocationManager initializeLocationTracking];
}

void setLocationInfoBlock() {
    setLocationInfoBlockWithInstanceName("");
}
