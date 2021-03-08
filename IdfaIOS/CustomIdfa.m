#import <AdSupport/AdSupport.h>
#import <AppTrackingTransparency/AppTrackingTransparency.h>

//Please ensure this path points to the file 'Assets/Plugins/iOS/Amplitude/Amplitude.h'
#import "../../Plugins/iOS/Amplitude/Amplitude.h"

#import "../../Plugins/iOS/Amplitude/AmplitudeCWrapper.h"

typedef NSString* (^AMPAdSupportBlock)(void);

void setIdfaBlockInternal(const char* instanceName) {
    AMPAdSupportBlock adSupportBlock = ^{
        NSUUID *idfaUUID = [[ASIdentifierManager sharedManager] advertisingIdentifier];
        NSString *idfaString = [idfaUUID UUIDString];
        return idfaString;
    };
    [[Amplitude instanceWithName:ToNSString(instanceName)] setAdSupportBlock:adSupportBlock];
}

#pragma mark - Functions Exposed to C#

void setIdfaBlock(const char* instanceName) {
    if (@available(iOS 14, *)) {
        if ([ATTrackingManager trackingAuthorizationStatus] == ATTrackingManagerAuthorizationStatusAuthorized) {
            setIdfaBlockInternal(instanceName);
        } else {
            [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
                if (status == ATTrackingManagerAuthorizationStatusAuthorized) {
                    setIdfaBlockInternal(instanceName);
                }
            }];
        }
    } else {
        setIdfaBlockInternal(instanceName);
    }
}
