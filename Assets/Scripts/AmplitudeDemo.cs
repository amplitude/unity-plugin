using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmplitudeDemo : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Debug.Log ("awake");
		// Amplitude amplitude = Amplitude.Instance;
		Amplitude amplitude = Amplitude.getInstance();
		amplitude.logging = true;
		amplitude.trackSessionEvents (true);
		amplitude.init("a2dbce0e18dfe5f8e74493843ff5c053");
		Debug.Log(amplitude.getDeviceId());

		Dictionary<string, bool> trackingOptions = new Dictionary<string, bool>();
		trackingOptions.Add("disableCity", true);
		trackingOptions.Add("disableIPAddress", true);
		trackingOptions.Add("disableIDFV", true);
		trackingOptions.Add("disableIDFA", true);
		trackingOptions.Add("disableCountry", true);

		Amplitude app2 = Amplitude.getInstance("app2");
		app2.logging = true;
		app2.trackSessionEvents (true);
		app2.setTrackingOptions(trackingOptions);
		app2.init("3653adbf32717221cacbf722f4671052");
		Debug.Log(app2.getDeviceId());
		app2.logEvent("logging to unity demo 2");
		app2.logEvent("keep logging events");
	}

	void Start() {
		Amplitude amplitude = Amplitude.Instance;
/*		Dictionary<string, object> userProperties = new Dictionary<string, object>()
		{
			{"float_gprop", 1.0}
		};
		amplitude.setUserProperties(userProperties);

		Dictionary<string, object> demoOptions = new Dictionary<string, object>()
		{
			{"Bucket" , "A" },
			{"Credits" , 9001}
		};
		amplitude.logEvent("unity event 2", demoOptions);
		amplitude.logRevenue(0.03);*/
		amplitude.setOnceUserProperty("bool", true);
		amplitude.setOnceUserProperty("boolArray", new bool[]{true, false, false});
		amplitude.setOnceUserProperty("stringArray", new string[]{"this", "is", "a", "test"});
		amplitude.unsetUserProperty("bool");
		amplitude.setUserProperty("string", "this is a test");
		amplitude.setUserProperty("stringArray", new string[]{"replace", "existing", "strings"});
		amplitude.appendUserProperty("stringArray", new string[]{ "append", "more", "strings" });
		amplitude.setUserProperty("floatArray", new float[]{123.45f, 678.9f});
		amplitude.setUserProperty("doubleArray", new double[]{123.45, 678.9});

		Dictionary<string, object> dictValue = new Dictionary<string, object>()
		{
			{"key3", "value3"},
			{"key4", "value4"}
		};
		amplitude.setOnceUserProperty ("dictValue", dictValue);

		List<int> intList = new List<int> ();
		intList.Add (4);
		intList.Add (5);
		intList.Add (6);
		amplitude.setOnceUserProperty ("intList", intList);

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
