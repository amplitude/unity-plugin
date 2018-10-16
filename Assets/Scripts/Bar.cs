using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {

		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
			Amplitude amplitude = Amplitude.Instance;
			amplitude.logEvent("tapped");
			Dictionary<string, object> userProperties = new Dictionary<string, object>()
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
			amplitude.logRevenue(0.03);
			amplitude.logRevenue("sku", 1, 1.99);
			amplitude.logRevenue("sku", 1, 1.99, "cmVjZWlwdA==", null);
			Dictionary<string, object> revenueProperties = new Dictionary<string, object>()
			{
				{"car", "blue"},
				{"price", 12.99}
			};
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				amplitude.logRevenue ("sku", 1, 1.99, "cmVjZWlwdA==", null, "purchase", revenueProperties);
			} else if (Application.platform == RuntimePlatform.Android) {
				amplitude.logRevenue("sku", 1, 1.99, "receipt", "receiptSignature", "purchase", revenueProperties);
			}
		}
	}
}
