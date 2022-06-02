//
//  EventBridge.h
//  EventBridge
//
//  Created by Dean Shi on 05/27/22.
//


#import <Foundation/Foundation.h>

@class AnalyticsEvent;
@class EventBridge;


@protocol EventBridge <NSObject>
- (void)setEventReceiver:(void (^_Nonnull)(AnalyticsEvent * _Nonnull))eventReceiver;
- (void)logEvent:(AnalyticsEvent *_Nonnull)event;

@end


@interface AnalyticsEvent : NSObject
@property (nonatomic, strong, readonly) NSString * _Nonnull eventType;
@property (nonatomic, strong, readonly) NSDictionary * _Nonnull eventProperties;
@property (nonatomic, strong, readonly) NSDictionary * _Nonnull userProperties;

@end


@interface EventBridge : NSObject <EventBridge>

@end
