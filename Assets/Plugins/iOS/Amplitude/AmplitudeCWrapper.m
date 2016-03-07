#import "AmplitudeCWrapper.h"
#import "Amplitude.h"
#import "AMPARCMacros.h"
#import "AMPIdentify.h"


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

const char * _Amplitude_getDeviceId()
{
    return MakeCString([[[Amplitude instance] getDeviceId] UTF8String]);
}

void _Amplitude_trackingSessionEvents(const bool enabled)
{
    [[Amplitude instance] setTrackingSessionEvents:enabled];
}

// User Property Operations
void _Amplitude_unsetUserProperty(const char* property)
{
    AMPIdentify *identify = [[AMPIdentify identify] unset:ToNSString(property)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceBoolUserProperty(const char* property, const bool value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithBool:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceDoubleUserProperty(const char* property, const double value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithDouble:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceFloatUserProperty(const char* property, const float value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithFloat:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceIntUserProperty(const char* property, const int value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithInt:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceLongUserProperty(const char* property, const long long value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:[NSNumber numberWithLongLong:value]];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceStringUserProperty(const char* property, const char* value)
{
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:ToNSString(value)];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceBoolArrayUserProperty(const char* property, const bool[] value)
{
    int length = sizeof(value)/sizeof(bool);
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
        [array addObject:[NSNumber numberWithBool:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}

void _Amplitude_setOnceIntArrayUserProperty(const char* property, const int[] value)
{
    int length = sizeof(value)/sizeof(int);
    if (length == 0) return;
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:length];
    for (int i = 0; i < length; i++) {
      [array addObject:[NSNumber numberWithInt:value[i]]];
    }
    AMPIdentify *identify = [[AMPIdentify identify] setOnce:ToNSString(property) value:array];
    [[Amplitude instance] identify:identify];
}


