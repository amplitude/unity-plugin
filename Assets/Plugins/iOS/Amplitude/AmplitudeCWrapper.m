#import "AmplitudeCWrapper.h"
#import "Amplitude.h"
#import "AMPIdentify.h"
#import "AMPRevenue.h"
#import "AMPTrackingOptions.h"

// Used to allocate a C string on the heap for C#
char* MakeCString(const char* string) {
    if (string == NULL) {
        return NULL;
    }

    char* result = (char*) malloc(strlen(string) + 1);
    strcpy(result, string);

    return result;
}

// Converts C style string to NSString
NSString* ToNSString(const char* string) {
    if (string)
        return [NSString stringWithUTF8String: string];
    else
        return nil;
}

// Helper method to safe get boolean from NSDictionary
BOOL safeGetBoolFromDictionary(NSDictionary *dict, NSString *key, BOOL defaultValue) {
    NSNumber *value = [dict objectForKey:key];
    if (value == nil) {
        return defaultValue;
    }
    return [value boolValue];
}

NSDictionary* ToNSDictionary(const char* data) {
    if (data) {
        NSError *error = nil;
        NSDictionary *result = [NSJSONSerialization JSONObjectWithData:[ToNSString(data) dataUsingEncoding:NSUTF8StringEncoding] options:0 error:&error];

        if (error != nil) {
            NSLog(@"ERROR: Deserialization error:%@", error);
            return nil;
        } else if (![result isKindOfClass:[NSDictionary class]]) {
            NSLog(@"ERROR: invalid type:%@", [result class]);
            return nil;
        } else {
            return result;
        }
    } else {
        return nil;
    }
}

void _Amplitude_init(const char* instanceName, const char* apiKey, const char* userId) {
    if (userId) {
        [[Amplitude instanceWithName:ToNSString(instanceName)] initializeApiKey:ToNSString(apiKey)
                                        userId:ToNSString(userId)];
    } else {
        [[Amplitude instanceWithName:ToNSString(instanceName)] initializeApiKey:ToNSString(apiKey)];
    }
}

void _Amplitude_setTrackingOptions(const char* instanceName, const char* trackingOptionsJson) {
    // convert dictionary of tracking options into AMPTrackingOptions object
    NSDictionary *trackingOptionsDict = ToNSDictionary(trackingOptionsJson);
    AMPTrackingOptions *trackingOptions = [AMPTrackingOptions options];
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableCarrier", NO)) {
        [trackingOptions disableCarrier];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableCity", NO)) {
        [trackingOptions disableCity];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableCountry", NO)) {
        [trackingOptions disableCountry];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableDeviceManufacturer", NO)) {
        [trackingOptions disableDeviceManufacturer];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableDeviceModel", NO)) {
        [trackingOptions disableDeviceModel];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableDMA", NO)) {
        [trackingOptions disableDMA];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableIDFA", NO)) {
        [trackingOptions disableIDFA];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableIDFV", NO)) {
        [trackingOptions disableIDFV];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableIPAddress", NO)) {
        [trackingOptions disableIPAddress];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableLanguage", NO)) {
        [trackingOptions disableLanguage];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableLatLng", NO)) {
        [trackingOptions disableLatLng];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableOSName", NO)) {
        [trackingOptions disableOSName];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableOSVersion", NO)) {
        [trackingOptions disableOSVersion];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disablePlatform", NO)) {
        [trackingOptions disablePlatform];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableRegion", NO)) {
        [trackingOptions disableRegion];
    }
    if (safeGetBoolFromDictionary(trackingOptionsDict, @"disableVersionName", NO)) {
        [trackingOptions disableVersionName];
    }
    [[Amplitude instanceWithName:ToNSString(instanceName)] setTrackingOptions:trackingOptions];
}

void _Amplitude_logEvent(const char* instanceName, const char* event, const char* properties) {
    if (properties) {
        [[Amplitude instanceWithName:ToNSString(instanceName)] logEvent:ToNSString(event) withEventProperties:ToNSDictionary(properties)];
    } else {
        [[Amplitude instanceWithName:ToNSString(instanceName)] logEvent:ToNSString(event)];
    }
}

void _Amplitude_logOutOfSessionEvent(const char* instanceName, const char* event, const char* properties) {
    if (properties) {
        [[Amplitude instanceWithName:ToNSString(instanceName)] logEvent:ToNSString(event) withEventProperties:ToNSDictionary(properties) outOfSession:true];
    } else {
        [[Amplitude instanceWithName:ToNSString(instanceName)] logEvent:ToNSString(event) withEventProperties:nil outOfSession:true];
    }
}

void _Amplitude_setOffline(const char* instanceName, const bool offline) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] setOffline:offline];
}

void _Amplitude_setUserId(const char* instanceName, const char* userId) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] setUserId:ToNSString(userId)];
}

void _Amplitude_setUserProperties(const char* instanceName, const char* properties) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] setUserProperties:ToNSDictionary(properties)];
}

void _Amplitude_setOptOut(const char* instanceName, const bool enabled) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] setOptOut:enabled];
}

void _Amplitude_setMinTimeBetweenSessionsMillis(const char* instanceName, const long long minTimeBetweenSessionsMillis) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] setMinTimeBetweenSessionsMillis:minTimeBetweenSessionsMillis];
}

void _Amplitude_setEventUploadPeriodSeconds(const char* instanceName, const int eventUploadPeriodSeconds) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] setEventUploadPeriodSeconds:eventUploadPeriodSeconds];
}

void _Amplitude_setLibraryName(const char* instanceName, const char* libraryName) {
    [Amplitude instanceWithName:ToNSString(instanceName)].libraryName = ToNSString(libraryName);
}

void _Amplitude_setLibraryVersion(const char* instanceName, const char* libraryVersion) {
    [Amplitude instanceWithName:ToNSString(instanceName)].libraryVersion = ToNSString(libraryVersion);
}

void _Amplitude_setDeviceId(const char* instanceName, const char* deviceId) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] setDeviceId:ToNSString(deviceId)];
}

void _Amplitude_enableCoppaControl(const char* instanceName) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] enableCoppaControl];
}

void _Amplitude_disableCoppaControl(const char* instanceName) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] disableCoppaControl];
}

void _Amplitude_setServerUrl(const char* instanceName, const char* serverUrl) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] setServerUrl:ToNSString(serverUrl)];
}

void _Amplitude_logRevenueAmount(const char* instanceName, double amount) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] logRevenue:[NSNumber numberWithDouble:amount]];
}

void _Amplitude_logRevenue(const char* instanceName, const char* productIdentifier, int quantity, double price) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] logRevenue:ToNSString(productIdentifier) quantity:quantity price:[NSNumber numberWithDouble:price]];
}

void _Amplitude_logRevenueWithReceipt(const char* instanceName, const char* productIdentifier, int quantity, double price, const char* receipt) {
    NSData *receiptData = [[NSData alloc] initWithBase64EncodedString:ToNSString(receipt) options:0];
    [[Amplitude instanceWithName:ToNSString(instanceName)] logRevenue:ToNSString(productIdentifier) quantity:quantity price:[NSNumber numberWithDouble:price] receipt:receiptData];
}

void _Amplitude_logRevenueWithReceiptAndProperties(const char* instanceName, const char* productIdentifier, int quantity, double price, const char* receipt, const char* revenueType, const char* properties) {
    NSData *receiptData = nil;
    AMPRevenue *revenue = [[[AMPRevenue revenue] setQuantity:quantity] setPrice:[NSNumber numberWithDouble:price]];
    if (productIdentifier) {
        [revenue setProductIdentifier:ToNSString(productIdentifier)];
    }
    if (receipt) {
        receiptData = [[NSData alloc] initWithBase64EncodedString:ToNSString(receipt) options:0];
        [revenue setReceipt:receiptData];
    }
    if (revenueType) {
        [revenue setRevenueType:ToNSString(revenueType)];
    }
    if (properties) {
        [revenue setEventProperties:ToNSDictionary(properties)];
    }
    [[Amplitude instanceWithName:ToNSString(instanceName)] logRevenueV2:revenue];
}

const char * _Amplitude_getDeviceId(const char* instanceName) {
    return MakeCString([[[Amplitude instanceWithName:ToNSString(instanceName)] getDeviceId] UTF8String]);
}

void _Amplitude_regenerateDeviceId(const char* instanceName) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] regenerateDeviceId];
}

void _Amplitude_useAdvertisingIdForDeviceId(const char* instanceName) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] useAdvertisingIdForDeviceId];
}

void _Amplitude_trackingSessionEvents(const char* instanceName, const bool enabled) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] setTrackingSessionEvents:enabled];
}

long long _Amplitude_getSessionId(const char* instanceName) {
    return [[Amplitude instanceWithName:ToNSString(instanceName)] getSessionId];
}

void _Amplitude_uploadEvents(const char* instanceName) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] uploadEvents];
}

// User Property Operations
void _Amplitude_clearUserProperties(const char* instanceName) {
    [[Amplitude instanceWithName:ToNSString(instanceName)] clearUserProperties];
}

void _Amplitude_unsetUserProperty(const char* instanceName, const char* property) {
    AMPIdentify *identify = [[AMPIdentify identify] unset:ToNSString(property)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyBool(const char* instanceName, const char* property, const bool value) {
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyDouble(const char* instanceName, const char* property, const double value) {
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyFloat(const char* instanceName, const char* property, const float value) {
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyInt(const char* instanceName, const char* property, const int value) {
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyLong(const char* instanceName, const char* property, const long long value) {
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyString(const char* instanceName, const char* property, const char* value) {
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyDict(const char* instanceName, const char* property, const char* values) {
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyList(const char* instanceName, const char* property, const char* values) {
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyBoolArray(const char* instanceName, const char* property, const bool value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyDoubleArray(const char* instanceName, const char* property, const double value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyFloatArray(const char* instanceName, const char* property, const float value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyIntArray(const char* instanceName, const char* property, const int value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyLongArray(const char* instanceName, const char* property, const long long value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setOnceUserPropertyStringArray(const char* instanceName, const char* property, const char* value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyBool(const char* instanceName, const char* property, const bool value) {
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyDouble(const char* instanceName, const char* property, const double value) {
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyFloat(const char* instanceName, const char* property, const float value) {
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyInt(const char* instanceName, const char* property, const int value) {
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyLong(const char* instanceName, const char* property, const long long value) {
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyString(const char* instanceName, const char* property, const char* value) {
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyDict(const char* instanceName, const char* property, const char* values) {
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyList(const char* instanceName, const char* property, const char* values) {
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyBoolArray(const char* instanceName, const char* property, const bool value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyDoubleArray(const char* instanceName, const char* property, const double value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyFloatArray(const char* instanceName, const char* property, const float value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyIntArray(const char* instanceName, const char* property, const int value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyLongArray(const char* instanceName, const char* property, const long long value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_setUserPropertyStringArray(const char* instanceName, const char* property, const char* value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_addUserPropertyDouble(const char* instanceName, const char* property, const double value) {
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_addUserPropertyFloat(const char* instanceName, const char* property, const float value) {
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_addUserPropertyInt(const char* instanceName, const char* property, const int value) {
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_addUserPropertyLong(const char* instanceName, const char* property, const long long value) {
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_addUserPropertyString(const char* instanceName, const char* property, const char* value) {
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_addUserPropertyDict(const char* instanceName, const char* property, const char* values) {
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyBool(const char* instanceName, const char* property, const bool value) {
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyDouble(const char* instanceName, const char* property, const double value) {
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyFloat(const char* instanceName, const char* property, const float value) {
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyInt(const char* instanceName, const char* property, const int value) {
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyLong(const char* instanceName, const char* property, const long long value) {
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyString(const char* instanceName, const char* property, const char* value) {
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyDict(const char* instanceName, const char* property, const char* values) {
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyList(const char* instanceName, const char* property, const char* values) {
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyBoolArray(const char* instanceName, const char* property, const bool value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyDoubleArray(const char* instanceName, const char* property, const double value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyFloatArray(const char* instanceName, const char* property, const float value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyIntArray(const char* instanceName, const char* property, const int value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyLongArray(const char* instanceName, const char* property, const long long value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_appendUserPropertyStringArray(const char* instanceName, const char* property, const char* value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyBool(const char* instanceName, const char* property, const bool value) {
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyDouble(const char* instanceName, const char* property, const double value) {
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyFloat(const char* instanceName, const char* property, const float value) {
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyInt(const char* instanceName, const char* property, const int value) {
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyLong(const char* instanceName, const char* property, const long long value) {
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyString(const char* instanceName, const char* property, const char* value) {
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyDict(const char* instanceName, const char* property, const char* values) {
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyList(const char* instanceName, const char* property, const char* values) {
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyBoolArray(const char* instanceName, const char* property, const bool value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyDoubleArray(const char* instanceName, const char* property, const double value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyFloatArray(const char* instanceName, const char* property, const float value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyIntArray(const char* instanceName, const char* property, const int value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyLongArray(const char* instanceName, const char* property, const long long value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_prependUserPropertyStringArray(const char* instanceName, const char* property, const char* value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] prepend:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyBool(const char* instanceName, const char* property, const bool value) {
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyDouble(const char* instanceName, const char* property, const double value) {
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyFloat(const char* instanceName, const char* property, const float value) {
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyInt(const char* instanceName, const char* property, const int value) {
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyLong(const char* instanceName, const char* property, const long long value) {
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyString(const char* instanceName, const char* property, const char* value) {
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyDict(const char* instanceName, const char* property, const char* values) {
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyList(const char* instanceName, const char* property, const char* values) {
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyBoolArray(const char* instanceName, const char* property, const bool value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyDoubleArray(const char* instanceName, const char* property, const double value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyFloatArray(const char* instanceName, const char* property, const float value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyIntArray(const char* instanceName, const char* property, const int value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyLongArray(const char* instanceName, const char* property, const long long value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_preInsertUserPropertyStringArray(const char* instanceName, const char* property, const char* value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] preInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyBool(const char* instanceName, const char* property, const bool value) {
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyDouble(const char* instanceName, const char* property, const double value) {
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyFloat(const char* instanceName, const char* property, const float value) {
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyInt(const char* instanceName, const char* property, const int value) {
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyLong(const char* instanceName, const char* property, const long long value) {
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyString(const char* instanceName, const char* property, const char* value) {
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyDict(const char* instanceName, const char* property, const char* values) {
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyList(const char* instanceName, const char* property, const char* values) {
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyBoolArray(const char* instanceName, const char* property, const bool value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyDoubleArray(const char* instanceName, const char* property, const double value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyFloatArray(const char* instanceName, const char* property, const float value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyIntArray(const char* instanceName, const char* property, const int value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyLongArray(const char* instanceName, const char* property, const long long value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_postInsertUserPropertyStringArray(const char* instanceName, const char* property, const char* value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] postInsert:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyBool(const char* instanceName, const char* property, const bool value) {
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyDouble(const char* instanceName, const char* property, const double value) {
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyFloat(const char* instanceName, const char* property, const float value) {
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyInt(const char* instanceName, const char* property, const int value) {
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyLong(const char* instanceName, const char* property, const long long value) {
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyString(const char* instanceName, const char* property, const char* value) {
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyDict(const char* instanceName, const char* property, const char* values) {
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyList(const char* instanceName, const char* property, const char* values) {
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyBoolArray(const char* instanceName, const char* property, const bool value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyDoubleArray(const char* instanceName, const char* property, const double value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyFloatArray(const char* instanceName, const char* property, const float value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyIntArray(const char* instanceName, const char* property, const int value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyLongArray(const char* instanceName, const char* property, const long long value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}

void _Amplitude_removeUserPropertyStringArray(const char* instanceName, const char* property, const char* value[], const int length) {
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] remove:ToNSString(property) value:array];
    [[Amplitude instanceWithName:ToNSString(instanceName)] identify:identify];
}