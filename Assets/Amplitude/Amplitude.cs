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
	private static extern void _Amplitude_setGlobalUserProperties(string propertiesJson);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenue(double amount);
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
	
	public void setGlobalUserProperties(IDictionary<string, object> properties) {
		string propertiesJson;
		if (properties != null) {
			propertiesJson = Json.Serialize(properties);
		} else {
			propertiesJson = Json.Serialize(new Dictionary<string, object>());
		}

		Log (string.Format("C# setGlobalUserProperties {0}", propertiesJson));		
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setGlobalUserProperties(propertiesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setGlobalUserProperties", propertiesJson);
		}
#endif
	}
	
	public void logRevenue(double amount) {
		Log (string.Format("C# logRevenue {0}", amount));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_logRevenue(amount);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", amount);
		}
#endif
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
