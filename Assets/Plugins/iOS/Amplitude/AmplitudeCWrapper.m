#import "AmplitudeCWrapper.h"
#import "Amplitude.h"
#import "AmplitudeARCMacros.h"


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
	    [Amplitude initializeApiKey:ToNSString(apiKey)
	                         userId:ToNSString(userId)];
	} else {
		[Amplitude initializeApiKey:ToNSString(apiKey)];
	}
}

void _Amplitude_logEvent(const char* event, const char* properties)
{
	if (properties) {
    	[Amplitude logEvent:ToNSString(event) withEventProperties:ToNSDictionary(properties)];
	} else {
		[Amplitude logEvent:ToNSString(event)];
	}
}

void _Amplitude_setUserId(const char* event)
{
	[Amplitude setUserId:ToNSString(event)];
}

void _Amplitude_setUserProperties(const char* properties)
{
	[Amplitude setUserProperties:ToNSDictionary(properties)];
}

void _Amplitude_logRevenueAmount(double amount)
{
	[Amplitude logRevenue:[NSNumber numberWithDouble:amount]];
}

void _Amplitude_logRevenue(const char* productIdentifier, int quantity, double price)
{
    [Amplitude logRevenue:ToNSString(productIdentifier) quantity:quantity price:[NSNumber numberWithDouble:price]];
}

void _Amplitude_logRevenueWithReceipt(const char* productIdentifier, int quantity, double price, const char* receipt)
{
    NSData *receiptData = [[NSData alloc] initWithBase64EncodedString:ToNSString(receipt) options:0];
    [Amplitude logRevenue:ToNSString(productIdentifier) quantity:quantity price:[NSNumber numberWithDouble:price] receipt:receiptData];
    SAFE_ARC_RELEASE(receiptData);
}

const char * _Amplitude_getDeviceId()
{
    return MakeCString([[Amplitude getDeviceId] UTF8String]);
}
