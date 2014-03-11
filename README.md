unity-plugin
============

Unity plugin for Amplitude

To update iOS:
- Copy source files from https://github.com/amplitude/amplitude-ios into Assets/Plugins/iOS/Amplitude
- Update AmplitudeCWrapper.h and AmplitudeCWrapper.m with appropriate wrapper methods.
- Update PostprocessBuildPlayer_Amplitude with the appropriate files.

To update Android:
- Compile source files from https://github.com/amplitude/amplitude-ios using the command `JAVA_HOME=</path/to/java/home> ANDROID_SDK=</path/to/android/sdk> ANDROID_TARGET=8 ant unity`. Make sure that you use Java 6 to compile.
- Copy the amplitude-unity.jar file into Assets/Plugins/Android/

To update Unity:
- Update Assets/Amplitude/Amplitude.cs with the appropriate wrapper methods.
