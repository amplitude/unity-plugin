using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmplitudeDemo : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Debug.Log ("awake");
		Amplitude amplitude = Amplitude.Instance;
		amplitude.logging = true;
		amplitude.trackSessionEvents(true);
		amplitude.init("a2dbce0e18dfe5f8e74493843ff5c053");
		Debug.Log(amplitude.getDeviceId());
	}

	void Start() {
		Amplitude amplitude = Amplitude.Instance;
		amplitude.setUserId("unity_plugin");
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
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnApplicationFocus(bool focus) {}

	public void OnApplicationPause(bool pause) {}
}
