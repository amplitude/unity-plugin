<p align="center">
  <a href="https://amplitude.com" target="_blank" align="center">
    <img src="https://static.amplitude.com/lightning/46c85bfd91905de8047f1ee65c7c93d6fa9ee6ea/static/media/amplitude-logo-with-text.4fb9e463.svg" width="280">
  </a>
  <br />
</p>

# Amplitude Unity Plugin

A plugin to simplify the integration of [Amplitude](https://www.amplitude.com) iOS and Android SDKs into your Unity project. This respository also contains a sample project with the Unity plugin integrated.

## Setup and Documentation
Please see our [installation guide](https://amplitude.zendesk.com/hc/en-us/articles/115002991968-Unity-Plugin-Installation) for instructions on installing and using our Unity Plugin.

NOTE: If you use proguard for obfuscation, please add following exception to your exception rules file (`proguard-android.txt` or `proguard-rules.pro`)

```
-keep class com.amplitude.unity.plugins.AmplitudePlugin
```

## Changelog
Click [here](https://github.com/amplitude/unity-plugin/blob/master/CHANGELOG.md) to view the Unity Plugin Changelog.

## Android Dependencies Management
Our `com.amplitude.android-sdk` is a transitive library, it doesn't include any other dependencies by itself. Other dependencies for `com.amplitude.android-sdk` are placed into `Assets/Plugins/Android`. We only use okhttp, the other dependencies you see are ones okhttp depends on (e.g. okio, jetbrain).

If by any chance you have okhttp included in your project, feel free to choose not to include okhttp and its related dependencies by unchecking them.

<img src="https://github.com/amplitude/unity-plugin/blob/master/import_tutorial.png" width="500">

## What if you're also using `unity-jar-resolver`?
Some users use `unity-jar-resolver` themselves. When they force resolve dependencies, it will clean up Amplitude related jars. For this case, what you would need to do is to declare those dependencies in your `*Dependency.xml` file.

Please add our native dependencies under `androidPackage` tag.
```
    <androidPackage spec="com.amplitude:android-sdk:2.25.1">
      <repositories>
        <repository>https://maven.google.com</repository>
      </repositories>
    </androidPackage>

    <androidPackage spec="com.squareup.okhttp3:okhttp:4.2.2">
      <repositories>
        <repository>https://maven.google.com</repository>
      </repositories>
    </androidPackage>
```

## Running on API 19, 20 (KitKat)
Amplitude SDK depends on okhttp library, since okhttp v3.13, they require android 5.0, Android Lollipop (API 21). [Read details](https://developer.squareup.com/blog/okhttp-3-13-requires-android-5/)

We do not restrict which okhttp version to use, you can downgrade the okttp version to be lower than 3.13 to make it work for API 19, 20.

### How to do downgrade?
If you import library by copying the jar file, you can downgrade okhttp library by replacing it with a version < 3.13.
If you use google dependency resolver, update the dependency version for okhttp in *Dependency.xml file.

## Need Help?
If you have any problems or issues over our SDK, feel free to create a github issue or submit a request on [Amplitude Help](https://help.amplitude.com/hc/en-us/requests/new).
