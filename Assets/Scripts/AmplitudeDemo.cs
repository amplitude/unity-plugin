using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmplitudeDemo : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Debug.Log ("awake");
		Amplitude amplitude = Amplitude.getInstance();
		amplitude.setServerUrl("https://api2.amplitude.com");
		amplitude.logging = true;
		amplitude.trackSessionEvents(true);
		amplitude.useAdvertisingIdForDeviceId();
		amplitude.setUseDynamicConfig(true);
		amplitude.setServerZone(AmplitudeServerZone.US);
		amplitude.init("e7177d872ff62c0356c973848c7bffba");
		Debug.Log(amplitude.getDeviceId());

		Dictionary<string, bool> trackingOptions = new Dictionary<string, bool>();
		trackingOptions.Add("disableCity", true);
		trackingOptions.Add("disableIPAddress", true);
		trackingOptions.Add("disableIDFV", true);
		trackingOptions.Add("disableIDFA", true);
		trackingOptions.Add("disableCountry", true);

		Amplitude app2 = Amplitude.getInstance("app2");
		app2.logging = true;
		app2.trackSessionEvents(true);
		app2.setTrackingOptions(trackingOptions);
		app2.setDeviceId("111111bca");
		app2.init("3653adbf32717221cacbf722f4671052");
		Debug.Log(app2.getDeviceId());
		app2.logEvent("logging to unity demo 2");
		app2.logEvent("keep logging events");
	}

	void Start() {
		Amplitude amplitude = Amplitude.Instance;
		amplitude.logRevenue(0.03);
		amplitude.setOnceUserProperty("bool", true);
		amplitude.setOnceUserProperty("boolArray", new bool[]{true, false, false});
		amplitude.setOnceUserProperty("stringArray", new string[]{"this", "is", "a", "test"});
		amplitude.unsetUserProperty("bool");
		amplitude.setUserProperty("string", "this is a test");
		amplitude.setUserProperty("stringArray", new string[]{"replace", "existing", "strings"});
		amplitude.appendUserProperty("stringArray", new string[]{"append", "more", "strings"});
		amplitude.prependUserProperty("longArray", new long[]{1, 2, 3});
		amplitude.preInsertUserProperty("longArray", new long[]{1111,2222,3333});
		amplitude.postInsertUserProperty("longArray", new long[]{4444,5555,6666});
		amplitude.removeUserProperty("longArray", new long[]{ 1111,2222,3333,4444,5555,6666});
		amplitude.setUserProperty("floatArray", new float[]{123.45f, 678.9f});
		amplitude.setUserProperty("doubleArray", new double[]{123.45, 678.9});

		Dictionary<string, object> dictValue = new Dictionary<string, object>()
		{
			{"key3", "value3"},
			{"key4", "value4"},
			{"keyFloat", (float)1.23},
			{"keyDouble", 2.34}
		};
		amplitude.setOnceUserProperty("dictValue", dictValue);

		List<int> intList = new List<int> ();
		intList.Add (4);
		intList.Add (5);
		intList.Add (6);
		amplitude.setOnceUserProperty("intList", intList);

		List<string> stringList = new List<string> ();
		stringList.Add ("string2");
		stringList.Add ("list2");
		amplitude.setUserProperty("stringList", stringList);

		amplitude.addUserProperty("floatValue", 10.0f);
		amplitude.addUserProperty("intValue", -1);

		amplitude.appendUserProperty("newIntValue", 15);
		amplitude.appendUserProperty("intValue", false);

		amplitude.appendUserProperty("intList", new int[]{7, 8, 9});
		amplitude.appendUserProperty("stringList", stringList);

		amplitude.prependUserProperty("prependStringValue", "prependStr1");
		amplitude.prependUserProperty("prependStringValue", "prependStr2");

		amplitude.preInsertUserProperty("preInsertBooleanValue", true);
		amplitude.preInsertUserProperty("preInsertBooleanValue", false);

		amplitude.postInsertUserProperty("postInsertintList", new int[]{1, 2, 3});
		amplitude.postInsertUserProperty("postInsertintList", new int[]{4, 5, 6});

		amplitude.logEvent("this is a test");
		Debug.Log(amplitude.getDeviceId());
		amplitude.uploadEvents();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnApplicationFocus(bool focus) {}

	public void OnApplicationPause(bool pause) {}

	public void onApplicationQuit() {
		Debug.Log ("Quitting application - attempting to log amplitude event");
		Amplitude.Instance.logEvent ("session over");
	}
}
