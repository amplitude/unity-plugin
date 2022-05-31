//
//  AnalyticsConnector.h
//  AnalyticsConnector
//
//  Created by Brian Giori on 12/20/21.
//


#import <Foundation/Foundation.h>
#import "EventBridge.h"
#import "IdentityStore.h"

//! Project version number for AnalyticsConnector.
FOUNDATION_EXPORT double AnalyticsConnectorVersionNumber;

//! Project version string for AnalyticsConnector.
FOUNDATION_EXPORT const unsigned char AnalyticsConnectorVersionString[];

// In this header, you should import all the public headers of your framework using statements like #import <AnalyticsConnector/PublicHeader.h>
@class AnalyticsConnector;

@interface AnalyticsConnector : NSObject
@property (nonatomic, strong, readonly) EventBridge *eventBridge;
@property (nonatomic, strong, readonly) IdentityStore *identityStore;

+ (AnalyticsConnector *)getInstance:(NSString *)instanceName;
@end
