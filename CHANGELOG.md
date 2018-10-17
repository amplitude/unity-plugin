## October 17, 2018
* Add ability to send data to multiple apps. The SDK supports multiple instances with different names and API keys. For example you would do:
```c#
Amplitude.getInstance("app2").init("API_KEY_FOR_APP_2");
Amplitude.getInstance("app2").logEvent("this event goes to app 2");
```
This maintains backwards compatability so you can still use `Amplitude.Instance` which maps to the default instance with no name: `Amplitude.getInstance(null)`.
* Add ability to customize the automatic tracking of user properties in the SDK (such as IP Address, language, platform, etc). To use pass a Dictionary mapping the respective disable option string to `true`. Example:
```
Dictionary<string, bool> trackingOptions = new Dictionary<string, bool>();
trackingOptions.Add("disableCity", true);
trackingOptions.Add("disableIPAddress", true);
trackingOptions.Add("disableIDFV", true);
trackingOptions.Add("disableIDFA", true);
trackingOptions.Add("disableCountry", true);
trackingOptions.Add("disableADID", true);
Amplitude.Instance.setTrackingOptions(trackingOptions);
```
* Update iOS to v4.4.0 [release notes](https://github.com/amplitude/Amplitude-iOS/releases/latest)
* Update Android to v2.20.0 [release notes](https://github.com/amplitude/Amplitude-Android/releases/latest)

## September 24, 2018
* Fix issue with regenerateDeviceId() that was causing crashes in iOS

## July 16, 2018
* Added `getSessionId()` to fetch the current session ID.
* Updated iOS to v4.2.1 [release notes](https://github.com/amplitude/Amplitude-iOS/releases/latest)
* Updated Android to v2.18.1 [release notes](https://github.com/amplitude/Amplitude-Android/releases/latest)

## November 27, 2017
* Updated Android to v2.16.0 [release notes](https://github.com/amplitude/Amplitude-Android/releases/latest)

## October 23, 2017
* Updated iOS to v4.0.4 [release notes](https://github.com/amplitude/Amplitude-iOS/releases/latest)

## October 16, 2017
* Updated iOS to v4.0.3 [release notes](https://github.com/amplitude/Amplitude-iOS/releases/latest)

## October 4, 2017
* Updated Android to v2.15.0 [release notes](https://github.com/amplitude/Amplitude-Android/releases/latest)

## September 18, 2017
* Updated iOS to v4.0.1 [release notes](https://github.com/amplitude/Amplitude-iOS/releases)

## August 2, 2017
* Fix tvOS support

## April 24, 2017
* Migrate setup instructions and plugin documentation in README to Zendesk

## April 17, 2017
* Updated Android to v2.13.3 [release notes](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Updated iOS to v3.14.1 [release notes](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Add support for tvOS

## October 12, 2016
* Updated Android to v2.10.0 [release notes](https://github.com/amplitude/Amplitude-Android/releases/tag/v2.10.0)

## October 7, 2016
* Updated iOS to v3.9.0 [release notes](https://github.com/amplitude/Amplitude-iOS/releases/tag/v3.9.0)

## August 9, 2016
* Updated iOS to v3.8.3 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Updated Android to v2.9.2 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Added support for logging revenue with revenueType and event properties [(see readme)](https://github.com/amplitude/unity-plugin#tracking-revenue)

## March 29, 2016
* Updated Android to v2.6.0 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Updated iOS to v3.6.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## March 16, 2016
* Updated Android to v2.5.1 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)

## March 7, 2016
* Updated iOS to v3.5.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Updated Android to v2.5.0 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Added support for User Property Operations. Please see [README](https://github.com/amplitude/unity-plugin#user-properties-and-user-property-operations) for more information.

## January 3, 2016
* Update iOS to v3.4.1 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Remove dependency on FMDB, use built-in SQLite3 library.
* Updated README to including directions on adding native SQLite3 to iOS build.

## October 26, 2015
* Update Android to v2.2.0 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Update iOS to v3.2.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Add toggle to track session events.

## August 28, 2015
* Update Android to v2.0.2 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Deprecated startSession and endSession

## August 27, 2015
* Update iOS to v3.0.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## April 17, 2015
* Update Android to v1.6.2 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)

## April 15, 2015
* Update iOS to v2.4.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Update Android to v1.6.1 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)

## January 4, 2015
* Update iOS library to v2.2.4 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## December 29, 2014
* Update iOS to v2.2.3, fix PostprocessBuildPlayer_Amplitude [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## November 4, 2014
* Update android to v1.4.2 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Update iOS to v2.2.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## October 8, 2014
* Update Android to v1.4.1 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Rename purchaseIdentifier to productId

## July 1, 2014
* Update Android to v1.4.0 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)

## June 2, 2014
* Add additional logRevenue and getDeviceId methods

## March 30, 2014
* Update demo scripts
