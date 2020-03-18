Amplitude Unity Plugin
============

A plugin to simplify the integration of [Amplitude](https://www.amplitude.com) iOS and Android SDKs into your Unity project. This respository also contains a sample project with the Unity plugin integrated.

# Setup and Documentation #
Please see our [installation guide](https://amplitude.zendesk.com/hc/en-us/articles/115002991968-Unity-Plugin-Installation) for instructions on installing and using our Unity Plugin.

NOTE: If you use proguard for obfuscation, please add following exception to your exception rules file (`proguard-android.txt` or `proguard-rules.pro`)

```
-keep class com.amplitude.unity.plugins.AmplitudePlugin
```

# Changelog #
Click [here](https://github.com/amplitude/unity-plugin/blob/master/CHANGELOG.md) to view the Unity Plugin Changelog.

# Android Dependencies Management
Our `com.amplitude.android-sdk` is a transitive library, it doesn't include any other dependencies by itself. Other dependencies for `com.amplitude.android-sdk` are placed into `Assets/Plugins/Android`. We only use okhttp, the other dependencies you see are ones okhttp depends on (e.g. okio, jetbrain).

If by any chance you have okhttp included in your project, feel free to choose not to include okhttp and its related dependencies by unchecking them.

<img src="https://github.com/amplitude/unity-plugin/blob/master/import_tutorial.png" width="500">

# What if you're also using `unity-jar-resolver`?
Some users use `unity-jar-resolver` themselves. When they force resolve dependencies, it will clean up Amplitude related jars. For this case, what you would need to do is to declare those dependencies in your `*Dependency.xml` file.

Please add our native dependencies under `androidPackage` tag.
```
    <androidPackage spec="com.amplitude:android-sdk:2.25.0">
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

# Need Help? #
If you have any problems or issues over our SDK, feel free to create a github issue or submit a request on [Amplitude Help](https://help.amplitude.com/hc/en-us/requests/new).

# License #
```text
Amplitude

The MIT License (MIT)

Copyright (c) 2014 Amplitude

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
```
