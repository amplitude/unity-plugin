using AmplitudeNS.MiniJSON;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_IPHONE
using System.Runtime.InteropServices;
#endif

public class Amplitude {

#if UNITY_ANDROID
	private static readonly string androidPluginName = "com.amplitude.unity.plugins.AmplitudePlugin";
	private AndroidJavaClass pluginClass;
#endif

	private static Amplitude instance;
	public bool logging = false;
	
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	private static extern void _Amplitude_init(string apiKey, string userId);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logEvent(string evt, string propertiesJson);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserId(string userId);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserProperties(string propertiesJson);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenueAmount(double amount);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenue(string productIdentifier, int quantity, double price);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenueWithReceipt(string productIdentifier, int quantity, double price, string receipt);
	[DllImport ("__Internal")]
	private static extern string _Amplitude_getDeviceId();
#endif

	public static Amplitude Instance {
		get
		{
			if(instance == null) {
				instance = new Amplitude();
			}
			
			return instance;
		}
	}
	
	public Amplitude() : base() {
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			Debug.Log ("construct instance");
			pluginClass = new AndroidJavaClass(androidPluginName);
		}
#endif
	}
	
	protected void Log(string message) {
		if(!logging) return;
		
		Debug.Log(message);
	}
	
	public void init(string apiKey) {
		Log (string.Format("C# init {0}", apiKey));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_init(apiKey, null);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using(AndroidJavaObject unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
					pluginClass.CallStatic("init", unityActivity, apiKey);
				}
			}
		}
#endif
	}
	
	public void init(string apiKey, string userId) {
		Log (string.Format("C# init {0} with userId {1}", apiKey, userId));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_init(apiKey, userId);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using(AndroidJavaObject unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
					pluginClass.CallStatic("init", unityActivity, apiKey, userId);
				}
			}
		}
#endif
	}
	
	public void logEvent(string evt) {
		Log (string.Format("C# sendEvent {0}", evt));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_logEvent(evt, null);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logEvent", evt);
		}
#endif
	}
	
	public void logEvent(string evt, IDictionary<string, object> properties) {
		string propertiesJson;
		if (properties != null) {
			propertiesJson = Json.Serialize(properties);
		} else {
			propertiesJson = Json.Serialize(new Dictionary<string, object>());
		}

		Log(string.Format("C# sendEvent {0} with properties {1}", evt, propertiesJson));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_logEvent(evt, propertiesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logEvent", evt, propertiesJson);
		}
#endif
	}
	
	public void setUserId(string userId) {
		Log (string.Format("C# setUserId {0}", userId));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserId(userId);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserId", userId);
		}
#endif
	}
	
	public void setUserProperties(IDictionary<string, object> properties) {
		string propertiesJson;
		if (properties != null) {
			propertiesJson = Json.Serialize(properties);
		} else {
			propertiesJson = Json.Serialize(new Dictionary<string, object>());
		}

		Log (string.Format("C# setUserProperties {0}", propertiesJson));		
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserProperties(propertiesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperties", propertiesJson);
		}
#endif
	}
	
	[System.Obsolete("Please call setUserProperties instead", false)]
	public void setGlobalUserProperties(IDictionary<string, object> properties) {
		setUserProperties(properties);
	}
	
	public void logRevenue(double amount) {
		Log (string.Format("C# logRevenue {0}", amount));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_logRevenueAmount(amount);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", amount);
		}
#endif
	}

	public void logRevenue(string purchaseIdentifier, int quantity, double price) {
		Log (string.Format("C# logRevenue {0}, {1}, {2}", purchaseIdentifier, quantity, price));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_logRevenue(purchaseIdentifier, quantity, price);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", purchaseIdentifier, quantity, price);
		}
#endif
	}

	public void logRevenue(string purchaseIdentifier, int quantity, double price, string receipt, string receiptSignature) {
		Log (string.Format("C# logRevenue {0}, {1}, {2} (with receipt)", purchaseIdentifier, quantity, price));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_logRevenueWithReceipt(purchaseIdentifier, quantity, price, receipt);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", purchaseIdentifier, quantity, price, receipt, receiptSignature);
		}
#endif
	}

	public string getDeviceId() {
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			return _Amplitude_getDeviceId();
		}
		#endif
		
		#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			return pluginClass.CallStatic<string>("getDeviceId");
		}
		#endif
		return null;
	}
	
	public void startSession() {
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("startSession");
		}
#endif
	}

	public void endSession() {
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("endSession");
		}
#endif
	}
}
