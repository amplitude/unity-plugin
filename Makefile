
CWD:=$(shell pwd)
UNITY_BIN=/Applications/Unity/Unity.app/Contents/MacOS/Unity
UNITY_OUT=amplitude-unity.unitypackage
UNITY_DIRS=Assets/Amplitude Assets/Editor Assets/Plugins
UNITY_FLAGS=-batchmode -exportPackage $(UNITY_DIRS) $(UNITY_OUT) -projectPath $(CWD) -quit

release:
	$(UNITY_BIN) $(UNITY_FLAGS)
