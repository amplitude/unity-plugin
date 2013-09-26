#import "AmplitudeCWrapper.h"
#import "Amplitude.h"


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
	    [Amplitude initializeApiKey:ToNSString(apiKey)
	                         userId:ToNSString(userId)];
	} else {
		[Amplitude initializeApiKey:ToNSString(apiKey)];
	}
}

void _Amplitude_logEvent(const char* event, const char* properties)
{
	if (properties) {
    	[Amplitude logEvent:ToNSString(event) withCustomProperties:ToNSDictionary(properties)];
	} else {
		[Amplitude logEvent:ToNSString(event)];
	}
}

void _Amplitude_setUserId(const char* event)
{
	[Amplitude setUserId:ToNSString(event)];
}

void _Amplitude_setGlobalUserProperties(const char* properties)
{
	[Amplitude setGlobalUserProperties:ToNSDictionary(properties)];
}

void _Amplitude_logRevenue(double amount)
{
	[Amplitude logRevenue:[NSNumber numberWithDouble:amount]];
}
