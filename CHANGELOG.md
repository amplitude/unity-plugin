# Versioned Releases

## 2.2.2
* Update Android version to 2.32.0 to resolve Cursor related errors.

## 2.2.1
* Fix .unitypackage overriding default editor preferences with asmdef files. Asmdef files removed.

## 2.2.0 (May 12, 2021)
* Add User property functions prepend/preInsert/postInsert/remove.

## 2.1.0 (Sep 13, 2020)
* Add `setEventUploadPeriodSeconds` API.

## 2.0.0 (Jan 13, 2020)
* PLEASE READ BEFORE UPDATING!
* Upgrade native iOS SDK to [v7.2.2](https://github.com/amplitude/Amplitude-iOS/blob/main/CHANGELOG.md).
* Major change to be aware is that Amplitude no longer helps to fetch idfa automatically.
* We will provide a customer driven approach to fetch idfa in our next version. 
* NOTE: If you need idfa at this moment, please continue to use v1.6.0. If you don't need idfa, you can upgrade to this version.

## 1.6.0 (Sep 13, 2020)
* Add `setMinTimeBetweenSessionsMillis` API.

## 1.5.0 (Aug 27, 2020)
* Add `setOffline` API, if offline is true, then the SDK will not upload events to Amplitude servers.

## 1.4.0 (Jul 15, 2020)
* Add `setServerUrl` API, you can use it to customize the destination the events go.

## 1.3.1 (May 13, 2020)
* Fix APIs (`uploadEvents`, `useAdvertisingIdForDeviceId`, `setDeviceId`) missing for Android.

## 1.3.0 (May 7, 2020)
* Add a couple APIs.
1. `uploadEvents` - Use this to flush events.
2. `useAdvertisingIdForDeviceId` - Use idfa (iOS) / adid (Android) as device Id.
3. `setDeviceId` - Set custom deviceId if you have your own strategy.

## 1.2.0 (May 5, 2020)
* Updated Unity to 2019.3.11f, also changed post processing script for iOS.
* Include `AmplitudeDependencies.xml` as part of the package.

## 1.1.0 (Apr 5, 2020)
* Updated Android version to 2.25.1, this version won't add location permission automatically to your manifest. You can choose whether your App will require location permissions or not.

## 1.0.0 (Mar 18, 2020)
* Add Coppa control APIs (`enableCoppaControl`, `disableCoppaControl`) in `Amplitude`.
* Add `AmplitudePostProcessor.cs` in `Editor` directory to automatically add `sqlite` dependencies and set the proper flags while building iOS App. Kudo to [@EtienneMarbotic](https://github.com/EtienneMarbotic) for submitting a PR.
* Now `library` field will be shown as `amplitude-unity/x.x.x` instead of showing information of the native library.

# Unversioned Releases (Old)
## Feb 11, 2020
* Fix event properties with floating numbers not showing up for some regions due to decimal separator with comma causing events not properly
  parsed.

## Feb 6, 2020
* Get rid of using com.amplitude.android-sdk fat jar, now it's transitive.
* Other dependencies for com.amplitude.android-sdk are also placed into Assets/Plugins/Android, but you can choose not to include those if
  some have been included already.
* If you wanna build it yourself, notice that now we use [unity jar resolver](https://github.com/googlesamples/unity-jar-resolver) to automatic     manage dependencies.
* Update Android to v2.24.1 [release notes](https://github.com/amplitude/Amplitude-Android/releases/tag/v2.24.1)

## Oct 23, 2018
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

## Sep 24, 2018
* Fix issue with regenerateDeviceId() that was causing crashes in iOS

## Jul 16, 2018
* Added `getSessionId()` to fetch the current session ID.
* Updated iOS to v4.2.1 [release notes](https://github.com/amplitude/Amplitude-iOS/releases/latest)
* Updated Android to v2.18.1 [release notes](https://github.com/amplitude/Amplitude-Android/releases/latest)

## Nov 27, 2017
* Updated Android to v2.16.0 [release notes](https://github.com/amplitude/Amplitude-Android/releases/latest)

## Oct 23, 2017
* Updated iOS to v4.0.4 [release notes](https://github.com/amplitude/Amplitude-iOS/releases/latest)

## Oct 16, 2017
* Updated iOS to v4.0.3 [release notes](https://github.com/amplitude/Amplitude-iOS/releases/latest)

## Oct 4, 2017
* Updated Android to v2.15.0 [release notes](https://github.com/amplitude/Amplitude-Android/releases/latest)

## Sep 18, 2017
* Updated iOS to v4.0.1 [release notes](https://github.com/amplitude/Amplitude-iOS/releases)

## Aug 2, 2017
* Fix tvOS support

## Apr 24, 2017
* Migrate setup instructions and plugin documentation in README to Zendesk

## Apr 17, 2017
* Updated Android to v2.13.3 [release notes](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Updated iOS to v3.14.1 [release notes](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Add support for tvOS

## Oct 12, 2016
* Updated Android to v2.10.0 [release notes](https://github.com/amplitude/Amplitude-Android/releases/tag/v2.10.0)

## Oct 7, 2016
* Updated iOS to v3.9.0 [release notes](https://github.com/amplitude/Amplitude-iOS/releases/tag/v3.9.0)

## Aug 9, 2016
* Updated iOS to v3.8.3 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Updated Android to v2.9.2 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Added support for logging revenue with revenueType and event properties [(see readme)](https://github.com/amplitude/unity-plugin#tracking-revenue)

## Mar 29, 2016
* Updated Android to v2.6.0 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Updated iOS to v3.6.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## Mar 16, 2016
* Updated Android to v2.5.1 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)

## Mar 7, 2016
* Updated iOS to v3.5.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Updated Android to v2.5.0 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Added support for User Property Operations. Please see [README](https://github.com/amplitude/unity-plugin#user-properties-and-user-property-operations) for more information.

## Jan 3, 2016
* Update iOS to v3.4.1 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Remove dependency on FMDB, use built-in SQLite3 library.
* Updated README to including directions on adding native SQLite3 to iOS build.

## Oct 26, 2015
* Update Android to v2.2.0 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Update iOS to v3.2.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Add toggle to track session events.

## Aug 28, 2015
* Update Android to v2.0.2 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Deprecated startSession and endSession

## Aug 27, 2015
* Update iOS to v3.0.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## Apr 17, 2015
* Update Android to v1.6.2 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)

## Apr 15, 2015
* Update iOS to v2.4.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)
* Update Android to v1.6.1 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)

## Jan 4, 2015
* Update iOS library to v2.2.4 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## Dec 29, 2014
* Update iOS to v2.2.3, fix PostprocessBuildPlayer_Amplitude [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## Nov 4, 2014
* Update android to v1.4.2 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Update iOS to v2.2.0 [iOS Changelog](https://github.com/amplitude/Amplitude-iOS/blob/master/CHANGELOG.md)

## Oct 8, 2014
* Update Android to v1.4.1 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)
* Rename purchaseIdentifier to productId

## Jul 1, 2014
* Update Android to v1.4.0 [Android Changelog](https://github.com/amplitude/Amplitude-Android/blob/master/CHANGELOG.md)

## Jun 2, 2014
* Add additional logRevenue and getDeviceId methods

## Mar 30, 2014
* Update demo scripts
