#import <AdSupport/AdSupport.h>
#import <AppTrackingTransparency/AppTrackingTransparency.h>
#import <Amplitude/Amplitude.h>

typedef NSString* (^AMPAdSupportBlock)(void);

void setIdfaBlockInternal(const char* instanceName) {
    AMPAdSupportBlock adSupportBlock = ^{
        NSUUID *idfaUUID = [[ASIdentifierManager sharedManager] advertisingIdentifier];
        NSString *idfaString = [idfaUUID UUIDString];
        return idfaString;
    };
    NSString* convertedString = [NSString stringWithFormat:@"%s", instanceName];
    [[Amplitude instanceWithName:convertedString] setAdSupportBlock:adSupportBlock];
}

#pragma mark - Functions Exposed to C#

void setIdfaBlockWithInstanceName(const char* instanceName) {
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

void setIdfaBlock() {
    setIdfaBlockWithInstanceName("");
}
