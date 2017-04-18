#import "AmplitudeCWrapper.h"
#import "Amplitude.h"
#import "AMPARCMacros.h"
#import "AMPIdentify.h"
#import "AMPRevenue.h"


// Used to allocate a C string on the heap for C#
char* MakeCString(const char* string)
{
    if (string == NULL) {
        return NULL;
    }

    char* result = (char*) malloc(strlen(string) + 1);
    strcpy(result, string);

    return result;
}

// Converts C style string to NSString
NSString* ToNSString(const char* string)
{
    if (string)
        return [NSString stringWithUTF8String: string];
    else
        return nil;
}

NSDictionary* ToNSDictionary(const char* data)
{
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

void _Amplitude_init(const char* apiKey, const char* userId)
{
    if (userId) {
        [[Amplitude instance] initializeApiKey:ToNSString(apiKey)
                                        userId:ToNSString(userId)];
    } else {
        [[Amplitude instance] initializeApiKey:ToNSString(apiKey)];
    }
}

void _Amplitude_logEvent(const char* event, const char* properties)
{
    if (properties) {
        [[Amplitude instance] logEvent:ToNSString(event) withEventProperties:ToNSDictionary(properties)];
    } else {
        [[Amplitude instance] logEvent:ToNSString(event)];
    }
}

void _Amplitude_logOutOfSessionEvent(const char* event, const char* properties)
{
    if (properties) {
        [[Amplitude instance] logEvent:ToNSString(event) withEventProperties:ToNSDictionary(properties) outOfSession:true];
    } else {
        [[Amplitude instance] logEvent:ToNSString(event) withEventProperties:nil outOfSession:true];
    }
}

void _Amplitude_setUserId(const char* event)
{
    [[Amplitude instance] setUserId:ToNSString(event)];
}

void _Amplitude_setUserProperties(const char* properties)
{
    [[Amplitude instance] setUserProperties:ToNSDictionary(properties)];
}

void _Amplitude_setOptOut(const bool enabled)
{
    [[Amplitude instance] setOptOut:enabled];
}

void _Amplitude_logRevenueAmount(double amount)
{
    [[Amplitude instance] logRevenue:[NSNumber numberWithDouble:amount]];
}

void _Amplitude_logRevenue(const char* productIdentifier, int quantity, double price)
{
    [[Amplitude instance] logRevenue:ToNSString(productIdentifier) quantity:quantity price:[NSNumber numberWithDouble:price]];
}

void _Amplitude_logRevenueWithReceipt(const char* productIdentifier, int quantity, double price, const char* receipt)
{
    NSData *receiptData = [[NSData alloc] initWithBase64EncodedString:ToNSString(receipt) options:0];
    [[Amplitude instance] logRevenue:ToNSString(productIdentifier) quantity:quantity price:[NSNumber numberWithDouble:price] receipt:receiptData];
    SAFE_ARC_RELEASE(receiptData);
}

void _Amplitude_logRevenueWithReceiptAndProperties(const char* productIdentifier, int quantity, double price, const char* receipt, const char* revenueType, const char* properties)
{
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
    [[Amplitude instance] logRevenueV2:revenue];
    if (receiptData) {
        SAFE_ARC_RELEASE(receiptData)
    }
}

const char * _Amplitude_getDeviceId()
{
    return MakeCString([[[Amplitude instance] getDeviceId] UTF8String]);
}

void _Amplitude_regenerateDeviceId()
{
    [[Amplitude instance] regenerateDeviceId];
}

void _Amplitude_trackingSessionEvents(const bool enabled)
{
    [[Amplitude instance] setTrackingSessionEvents:enabled];
}

// User Property Operations
void _Amplitude_clearUserProperties()
{
    [[Amplitude instance] clearUserProperties];
}

void _Amplitude_unsetUserProperty(const char* property)
{
    AMPIdentify *identify = [[AMPIdentify identify] unset:ToNSString(property)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyBool(const char* property, const bool value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyDouble(const char* property, const double value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyFloat(const char* property, const float value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyInt(const char* property, const int value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyLong(const char* property, const long long value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyString(const char* property, const char* value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyDict(const char* property, const char* values)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyList(const char* property, const char* values)
{
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyBoolArray(const char* property, const bool value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyDoubleArray(const char* property, const double value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyFloatArray(const char* property, const float value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyIntArray(const char* property, const int value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyLongArray(const char* property, const long long value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceUserPropertyStringArray(const char* property, const char* value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyBool(const char* property, const bool value)
{
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyDouble(const char* property, const double value)
{
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyFloat(const char* property, const float value)
{
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyInt(const char* property, const int value)
{
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyLong(const char* property, const long long value)
{
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyString(const char* property, const char* value)
{
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyDict(const char* property, const char* values)
{
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyList(const char* property, const char* values)
{
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyBoolArray(const char* property, const bool value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyDoubleArray(const char* property, const double value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyFloatArray(const char* property, const float value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyIntArray(const char* property, const int value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyLongArray(const char* property, const long long value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setUserPropertyStringArray(const char* property, const char* value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] set:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_addUserPropertyDouble(const char* property, const double value)
{
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_addUserPropertyFloat(const char* property, const float value)
{
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_addUserPropertyInt(const char* property, const int value)
{
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_addUserPropertyLong(const char* property, const long long value)
{
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_addUserPropertyString(const char* property, const char* value)
{
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_addUserPropertyDict(const char* property, const char* values)
{
    AMPIdentify *identify = [[AMPIdentify identify] add:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyBool(const char* property, const bool value)
{
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyDouble(const char* property, const double value)
{
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyFloat(const char* property, const float value)
{
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyInt(const char* property, const int value)
{
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyLong(const char* property, const long long value)
{
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyString(const char* property, const char* value)
{
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyDict(const char* property, const char* values)
{
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:ToNSDictionary(values)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyList(const char* property, const char* values)
{
    NSDictionary *dict = ToNSDictionary(values);
    if (dict == nil) {
        return;
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:[dict objectForKey:@"list"]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyBoolArray(const char* property, const bool value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyDoubleArray(const char* property, const double value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithDouble:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyFloatArray(const char* property, const float value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithFloat:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyIntArray(const char* property, const int value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyLongArray(const char* property, const long long value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithLongLong:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_appendUserPropertyStringArray(const char* property, const char* value[], const int length)
{
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:ToNSString(value[i])];
    }
    AMPIdentify *identify = [[AMPIdentify identify] append:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}
