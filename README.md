Amplitude Unity Plugin
============

A plugin to simplify the integration of [Amplitude](https://www.amplitude.com) iOS and Android SDKs into your Unity project. This respository also contains a sample project with the Unity plugin integrated.

# Setup and Documentation #
Please see our [installation guide](https://amplitude.zendesk.com/hc/en-us/articles/115002991968-Unity-Plugin-Installation) for instructions on installing and using our Unity Plugin.

# Changelog #
Click [here](https://github.com/amplitude/unity-plugin/blob/master/CHANGELOG.md) to view the Unity Plugin Changelog.

# Android Dependencies Management
Our com.amplitude.android-sdk is a transitive library, it doesn't include any other dependencies. Other dependencies for com.amplitude.android-sdk are placed into Assets/Plugins/Android, but you can choose not to include those when importing our plugin if some have been included already.

# Questions? #
If you have questions about using or installing our Unity Plugin, you can send an email to [Amplitude Support](mailto:platform@amplitude.com).

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
