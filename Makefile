
ANDROID_URL=https://raw.githubusercontent.com/amplitude/Amplitude-Android/master
ANDROID_VERSION:=$(shell curl "$(ANDROID_URL)/pom.xml" | grep -o "version>[^<]*</version" | head -n 1 | sed -e "s/[^0-9.]//g")
ANDROID_JAR=amplitude-unity-$(ANDROID_VERSION).jar
ANDROID_ASSETS=Assets/Plugins/Android

IOS_MASTER_URL=https://github.com/amplitude/Amplitude-iOS/archive/master.zip
IOS_ASSETS=Assets/Plugins/iOS/Amplitude


update: update-android update-ios

update-android:
	-git rm $(ANDROID_ASSETS)/*jar
	wget "$(ANDROID_URL)/$(ANDROID_JAR)" -O $(ANDROID_ASSETS)/$(ANDROID_JAR)
	git add $(ANDROID_ASSETS)/$(ANDROID_JAR)

update-ios:
	wget "$(IOS_MASTER_URL)" -O master.zip
	unzip master.zip
	cp -R Amplitude-iOS-master/Amplitude/* $(IOS_ASSETS)/
	git add -A $(IOS_ASSETS)/
	rm -rf master.zip Amplitude-iOS-master/

.PHONY: clean
.PHONY: test
