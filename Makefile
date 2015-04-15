
ANDROID_URL=https://raw.githubusercontent.com/amplitude/Amplitude-Android/master
ANDROID_VERSION:=$(shell curl "$(ANDROID_URL)/pom.xml" | grep -o "version>[^<]*</version" | head -n 1 | sed -e "s/[^0-9.]//g")
ANDROID_JAR=amplitude-unity-$(ANDROID_VERSION).jar
ANDROID_ASSETS=Assets/Plugins/Android

update: update-android update-ios

update-android:
	-git rm $(ANDROID_ASSETS)/*jar
	wget "$(ANDROID_URL)/$(ANDROID_JAR)" -O $(ANDROID_ASSETS)/$(ANDROID_JAR)
	git add $(ANDROID_ASSETS)/$(ANDROID_JAR)
	git mv $(ANDROID_ASSETS)/amplitude-unity-*.jar.meta $(ANDROID_ASSETS)/$(ANDROID_JAR).meta


update-ios:


.PHONY: clean
.PHONY: test
