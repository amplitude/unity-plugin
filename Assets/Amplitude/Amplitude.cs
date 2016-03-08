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
	private static extern void _Amplitude_logOutOfSessionEvent(string evt, string propertiesJson);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserId(string userId);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserProperties(string propertiesJson);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOptOut(bool enabled);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenueAmount(double amount);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenue(string productIdentifier, int quantity, double price);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenueWithReceipt(string productIdentifier, int quantity, double price, string receipt);
	[DllImport ("__Internal")]
	private static extern string _Amplitude_getDeviceId();
	[DllImport ("__Internal")]
	private static extern void _Amplitude_trackingSessionEvents(bool enabled);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_clearUserProperties();
	[DllImport ("__Internal")]
	private static extern void _Amplitude_unsetUserProperty(string property);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyBool(string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyDouble(string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyFloat(string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyInt(string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyLong(string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyString(string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyDict(string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyList(string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyBoolArray(string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyDoubleArray(string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyFloatArray(string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyIntArray(string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyLongArray(string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyStringArray(string property, string[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyBool(string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyDouble(string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyFloat(string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyInt(string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyLong(string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyString(string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyDict(string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyList(string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyBoolArray(string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyDoubleArray(string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyFloatArray(string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyIntArray(string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyLongArray(string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyStringArray(string property, string[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyDouble(string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyFloat(string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyInt(string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyLong(string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyString(string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyDict(string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyBool(string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyDouble(string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyFloat(string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyInt(string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyLong(string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyString(string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyDict(string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyList(string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyBoolArray(string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyDoubleArray(string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyFloatArray(string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyIntArray(string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyLongArray(string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyStringArray(string property, string[] value, int length);
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
					using(AndroidJavaObject unityApplication = unityActivity.Call<AndroidJavaObject>("getApplication")) {
						pluginClass.CallStatic("init", unityActivity, apiKey);
						pluginClass.CallStatic("enableForegroundTracking", unityApplication);
					}
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
					using (AndroidJavaObject unityApplication = unityActivity.Call<AndroidJavaObject>("getApplication")) {
						pluginClass.CallStatic("init", unityActivity, apiKey, userId);
						pluginClass.CallStatic("enableForegroundTracking", unityApplication);
					}
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

	public void logEvent(string evt, IDictionary<string, object> properties, bool outOfSession) {
		string propertiesJson;
		if (properties != null) {
			propertiesJson = Json.Serialize(properties);
		} else {
			propertiesJson = Json.Serialize(new Dictionary<string, object>());
		}

		Log(string.Format("C# sendEvent {0} with properties {1} and outOfSession {2}", evt, propertiesJson, outOfSession));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			if (outOfSession) {
				_Amplitude_logOutOfSessionEvent(evt, propertiesJson);
			} else {
				_Amplitude_logEvent(evt, propertiesJson);
			}
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logEvent", evt, propertiesJson, outOfSession);
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

	public void setOptOut(bool enabled) {
		Log (string.Format("C# setOptOut {0}", enabled));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOptOut(enabled);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOptOut", enabled);
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

	public void logRevenue(string productId, int quantity, double price) {
		Log (string.Format("C# logRevenue {0}, {1}, {2}", productId, quantity, price));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_logRevenue(productId, quantity, price);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void logRevenue(string productId, int quantity, double price, string receipt, string receiptSignature) {
		Log (string.Format("C# logRevenue {0}, {1}, {2} (with receipt)", productId, quantity, price));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_logRevenueWithReceipt(productId, quantity, price, receipt);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", productId, quantity, price, receipt, receiptSignature);
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

	public void trackSessionEvents(bool enabled) {
		Log (string.Format("C# trackSessionEvents {0}", enabled));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_trackingSessionEvents(enabled);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("trackSessionEvents", enabled);
		}
#endif
	}

// User Property Operations
// ClearUserProperties
	public void clearUserProperties() {
		Log (string.Format("C# clearUserProperties"));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_clearUserProperties();
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

// Unset
	public void unsetUserProperty(string property) {
		Log (string.Format("C# unsetUserProperty {0}", property));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_unsetUserProperty(property);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

// setOnce
	public void setOnceUserPropertyBool(string property, bool value) {
		Log (string.Format("C# setOnceUserPropertyBool {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyBool(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setOnceUserPropertyDouble(string property, double value) {
		Log (string.Format("C# setOnceUserPropertyDouble {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyDouble(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setOnceUserPropertyFloat(string property, float value) {
		Log (string.Format("C# setOnceUserPropertyFloat {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyFloat(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setOnceUserPropertyInt(string property, int value) {
		Log (string.Format("C# setOnceUserPropertyInt {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyInt(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setOnceUserPropertyLong(string property, long value) {
		Log (string.Format("C# setOnceUserPropertyLong {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyLong(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setOnceUserPropertyString(string property, string value) {
		Log (string.Format("C# setOnceUserPropertyString {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyString(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setOnceUserPropertyDict(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}

		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# setOnceUserPropertyDict {0}, {1}", property, valuesJson));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyDict(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperties", propertiesJson);
		}
#endif
	}

	public void setOnceUserPropertyList<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}

		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# setOnceUserPropertyList {0}, {1}", property, valuesJson));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyList(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperties", propertiesJson);
		}
#endif
	}

	public void setOnceUserPropertyBoolArray(string property, bool[] array) {
		Log (string.Format("C# setOnceUserPropertyBoolArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyBoolArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setOnceUserPropertyDoubleArray(string property, double[] array) {
		Log (string.Format("C# setOnceUserPropertyDoubleArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyDoubleArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setOnceUserPropertyFloatArray(string property, float[] array) {
		Log (string.Format("C# setOnceUserPropertyFloatArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyFloatArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void setOnceUserPropertyIntArray(string property, int[] array) {
		Log (string.Format("C# setOnceUserPropertyIntArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyIntArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setOnceUserPropertyLongArray(string property, long[] array) {
		Log (string.Format("C# setOnceUserPropertyLongArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyLongArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void setOnceUserPropertyStringArray(string property, string[] array) {
		Log (string.Format("C# setOnceUserPropertyStringArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setOnceUserPropertyStringArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

// set
	public void setUserPropertyBool(string property, bool value) {
		Log (string.Format("C# setUserPropertyBool {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyBool(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyDouble(string property, double value) {
		Log (string.Format("C# setUserPropertyDouble {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyDouble(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyFloat(string property, float value) {
		Log (string.Format("C# setUserPropertyFloat {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyFloat(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyInt(string property, int value) {
		Log (string.Format("C# setUserPropertyInt {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyInt(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyLong(string property, long value) {
		Log (string.Format("C# setUserPropertyLong {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyLong(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyString(string property, string value) {
		Log (string.Format("C# setUserPropertyString {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyString(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyDict(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}
		
		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# setUserPropertyDict {0}, {1}", property, valuesJson));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyDict(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperties", propertiesJson);
		}
#endif
	}
	
	public void setUserPropertyList<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}
		
		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# setUserPropertyList {0}, {1}", property, valuesJson));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyList(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperties", propertiesJson);
		}
#endif
	}

	public void setUserPropertyBoolArray(string property, bool[] array) {
		Log (string.Format("C# setUserPropertyBoolArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyBoolArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyDoubleArray(string property, double[] array) {
		Log (string.Format("C# setUserPropertyDoubleArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyDoubleArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyFloatArray(string property, float[] array) {
		Log (string.Format("C# setUserPropertyFloatArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyFloatArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyIntArray(string property, int[] array) {
		Log (string.Format("C# setUserPropertyIntArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyIntArray(property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyLongArray(string property, long[] array) {
		Log (string.Format("C# setUserPropertyLongArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyLongArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void setUserPropertyStringArray(string property, string[] array) {
		Log (string.Format("C# setUserPropertyStringArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_setUserPropertyStringArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}


// add
	public void addUserPropertyDouble(string property, double value) {
		Log (string.Format("C# addUserPropertyDouble {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_addUserPropertyDouble(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void addUserPropertyFloat(string property, float value) {
		Log (string.Format("C# addUserPropertyFloat {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_addUserPropertyFloat(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void addUserPropertyInt(string property, int value) {
		Log (string.Format("C# addUserPropertyInt {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_addUserPropertyInt(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void addUserPropertyLong(string property, long value) {
		Log (string.Format("C# addUserPropertyLong {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_addUserPropertyLong(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void addUserPropertyString(string property, string value) {
		Log (string.Format("C# addUserPropertyString {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_addUserPropertyString(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//			pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}

	public void addUserPropertyDict(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}
		
		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# addUserPropertyDict {0}, {1}", property, valuesJson));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_addUserPropertyDict(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperties", propertiesJson);
		}
#endif
	}

// append
	public void appendUserPropertyBool(string property, bool value) {
		Log (string.Format("C# appendUserPropertyBool {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyBool(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyDouble(string property, double value) {
		Log (string.Format("C# appendUserPropertyDouble {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyDouble(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyFloat(string property, float value) {
		Log (string.Format("C# appendUserPropertyFloat {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyFloat(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyInt(string property, int value) {
		Log (string.Format("C# appendUserPropertyInt {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyInt(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyLong(string property, long value) {
		Log (string.Format("C# appendUserPropertyLong {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyLong(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyString(string property, string value) {
		Log (string.Format("C# appendUserPropertyString {0}, {1}", property, value));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyString(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyDict(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}
		
		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# appendUserPropertyDict {0}, {1}", property, valuesJson));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyDict(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperties", propertiesJson);
		}
#endif
	}
	
	public void appendUserPropertyList<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}
		
		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# appendUserPropertyList {0}, {1}", property, valuesJson));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyList(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperties", propertiesJson);
		}
#endif
	}
	
	public void appendUserPropertyBoolArray(string property, bool[] array) {
		Log (string.Format("C# appendUserPropertyBoolArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyBoolArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyDoubleArray(string property, double[] array) {
		Log (string.Format("C# appendUserPropertyDoubleArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyDoubleArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyFloatArray(string property, float[] array) {
		Log (string.Format("C# appendUserPropertyFloatArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyFloatArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyIntArray(string property, int[] array) {
		Log (string.Format("C# appendUserPropertyIntArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyIntArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyLongArray(string property, long[] array) {
		Log (string.Format("C# appendUserPropertyLongArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyLongArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}
	
	public void appendUserPropertyStringArray(string property, string[] array) {
		Log (string.Format("C# appendUserPropertyStringArray {0}, {1}", property, array));
#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_Amplitude_appendUserPropertyStringArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			//          pluginClass.CallStatic("logRevenue", productId, quantity, price);
		}
#endif
	}



	// This method is deprecated
	public void startSession() { return; }

	// This method is deprecated
	public void endSession() { return; }
}
