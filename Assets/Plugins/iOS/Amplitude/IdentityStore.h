//
//  IdentifyStore.h
//  IdentifyStore
//
//  Created by Dean Shi on 05/27/22.
//


#import <Foundation/Foundation.h>

@class Identity;
@class IdentityStore;
@class IdentityStoreEditor;


@protocol IdentityStore <NSObject>
- (IdentityStoreEditor * _Nonnull)getIdentity;
- (IdentityStoreEditor * _Nonnull)setIdentity:(Identity *_Nonnull) identity;
- (IdentityStoreEditor * _Nonnull)editIdentity;
- (void)addIdentityListener:(NSString *_Nonnull)key listener:(void (^_Nonnull)(Identity *_Nonnull))listener;
- (void)removeIdentityListener:(NSString *_Nonnull)key;

@end


@protocol IdentityStoreEditor <NSObject>
- (IdentityStoreEditor * _Nonnull)setUserId:(nullable NSString *)userId;
- (IdentityStoreEditor * _Nonnull)setDeviceId:(nullable NSString *)deviceId;
- (IdentityStoreEditor * _Nonnull)setUserProperties:(NSDictionary *_Nonnull)userProperties;
- (IdentityStoreEditor * _Nonnull)updateUserProperties:(NSDictionary *_Nonnull)userProperties;
- (void)commit;

@end


@interface IdentityStore : NSObject <IdentityStore>

@end


@interface IdentityStoreEditor : NSObject <IdentityStoreEditor>

@end
