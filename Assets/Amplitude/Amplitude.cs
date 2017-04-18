using AmplitudeNS.MiniJSON;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if (UNITY_IPHONE || UNITY_TVOS)
using System.Runtime.InteropServices;
#endif

public class Amplitude {

#if UNITY_ANDROID
	private static readonly string androidPluginName = "com.amplitude.unity.plugins.AmplitudePlugin";
	private AndroidJavaClass pluginClass;
#endif

	private static Amplitude instance;
	public bool logging = false;

#if (UNITY_IPHONE || UNITY_TVOS)
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
	private static extern void _Amplitude_logRevenueWithReceiptAndProperties(string productIdentifier, int quantity, double price, string receipt, string revenueType, string propertiesJson);
	[DllImport ("__Internal")]
	private static extern string _Amplitude_getDeviceId();
	[DllImport ("__Internal")]
	private static extern string _Amplitude_regenerateDeviceId();
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_logRevenueWithReceipt(productId, quantity, price, receipt);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", productId, quantity, price, receipt, receiptSignature);
		}
#endif
	}

	public void logRevenue(string productId, int quantity, double price, string receipt, string receiptSignature, string revenueType, IDictionary<string, object> eventProperties) {
		string propertiesJson;
		if (eventProperties != null) {
			propertiesJson = Json.Serialize(eventProperties);
		} else {
			propertiesJson = Json.Serialize(new Dictionary<string, object>());
		}

		Log (string.Format("C# logRevenue {0}, {1}, {2}, {3}, {4} (with receipt)", productId, quantity, price, revenueType, propertiesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_logRevenueWithReceiptAndProperties(productId, quantity, price, receipt, revenueType, propertiesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", productId, quantity, price, receipt, receiptSignature, revenueType, propertiesJson);
		}
#endif
	}

	public string getDeviceId() {
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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

	public void regenerateDeviceId() {
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_regenerateDeviceId();
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("regenerateDeviceId");
		}
#endif
	}

	public void trackSessionEvents(bool enabled) {
		Log (string.Format("C# trackSessionEvents {0}", enabled));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
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
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_clearUserProperties();
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("clearUserProperties");
		}
#endif
	}

// Unset
	public void unsetUserProperty(string property) {
		Log (string.Format("C# unsetUserProperty {0}", property));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_unsetUserProperty(property);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("unsetUserProperty", property);
		}
#endif
	}

// setOnce
	public void setOnceUserProperty(string property, bool value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyBool(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, double value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyDouble(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, float value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyFloat(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, int value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyInt(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, long value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyLong(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, string value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyString(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}

		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyDict(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserPropertyDict", property, valuesJson);
		}
#endif
	}

	public void setOnceUserProperty<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}

		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyList(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserPropertyList", property, valuesJson);
		}
#endif
	}

	public void setOnceUserProperty(string property, bool[] array) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyBoolArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, array);
		}
#endif
	}

	public void setOnceUserProperty(string property, double[] array) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyDoubleArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, array);
		}
#endif
	}

	public void setOnceUserProperty(string property, float[] array) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyFloatArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, array);
		}
#endif
	}
	
	public void setOnceUserProperty(string property, int[] array) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyIntArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, array);
		}
#endif
	}

	public void setOnceUserProperty(string property, long[] array) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyLongArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, array);
		}
#endif
	}
	
	public void setOnceUserProperty(string property, string[] array) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyStringArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", property, array);
		}
#endif
	}

// set
	public void setUserProperty(string property, bool value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyBool(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, value);
		}
#endif
	}

	public void setUserProperty(string property, double value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyDouble(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, value);
		}
#endif
	}

	public void setUserProperty(string property, float value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyFloat(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, value);
		}
#endif
	}

	public void setUserProperty(string property, int value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyInt(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, value);
		}
#endif
	}

	public void setUserProperty(string property, long value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyLong(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, value);
		}
#endif
	}

	public void setUserProperty(string property, string value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyString(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, value);
		}
#endif
	}

	public void setUserProperty(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}
		
		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# setUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyDict(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, valuesJson);
		}
#endif
	}
	
	public void setUserProperty<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}
		
		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# setUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyList(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserPropertyList", property, valuesJson);
		}
#endif
	}

	public void setUserProperty(string property, bool[] array) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyBoolArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, array);
		}
#endif
	}

	public void setUserProperty(string property, double[] array) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyDoubleArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, array);
		}
#endif
	}

	public void setUserProperty(string property, float[] array) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyFloatArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, array);
		}
#endif
	}

	public void setUserProperty(string property, int[] array) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyIntArray(property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, array);
		}
#endif
	}

	public void setUserProperty(string property, long[] array) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyLongArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, array);
		}
#endif
	}

	public void setUserProperty(string property, string[] array) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyStringArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", property, array);
		}
#endif
	}


// add
	public void addUserProperty(string property, double value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyDouble(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", property, value);
		}
#endif
	}

	public void addUserProperty(string property, float value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyFloat(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", property, value);
		}
#endif
	}

	public void addUserProperty(string property, int value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyInt(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", property, value);
		}
#endif
	}

	public void addUserProperty(string property, long value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyLong(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", property, value);
		}
#endif
	}

	public void addUserProperty(string property, string value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyString(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", property, value);
		}
#endif
	}

	public void addUserProperty(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}
		
		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# addUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyDict(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserPropertyDict", property, valuesJson);
		}
#endif
	}

// append
	public void appendUserProperty(string property, bool value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyBool(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, value);
		}
#endif
	}
	
	public void appendUserProperty(string property, double value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyDouble(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, value);
		}
#endif
	}
	
	public void appendUserProperty(string property, float value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyFloat(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, value);
		}
#endif
	}
	
	public void appendUserProperty(string property, int value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyInt(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, value);
		}
#endif
	}
	
	public void appendUserProperty(string property, long value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyLong(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, value);
		}
#endif
	}
	
	public void appendUserProperty(string property, string value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyString(property, value);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, value);
		}
#endif
	}
	
	public void appendUserProperty(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}
		
		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# appendUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyDict(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserPropertyDict", property, valuesJson);
		}
#endif
	}
	
	public void appendUserProperty<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}
		
		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# appendUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyList(property, valuesJson);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserPropertyList", property, valuesJson);
		}
#endif
	}
	
	public void appendUserProperty(string property, bool[] array) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyBoolArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, array);
		}
#endif
	}
	
	public void appendUserProperty(string property, double[] array) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyDoubleArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, array);
		}
#endif
	}
	
	public void appendUserProperty(string property, float[] array) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyFloatArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, array);
		}
#endif
	}
	
	public void appendUserProperty(string property, int[] array) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyIntArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, array);
		}
#endif
	}
	
	public void appendUserProperty(string property, long[] array) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyLongArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, array);
		}
#endif
	}
	
	public void appendUserProperty(string property, string[] array) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, array));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyStringArray(property, array, array.Length);
		}
#endif
		
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", property, array);
		}
#endif
	}



	// This method is deprecated
	public void startSession() { return; }

	// This method is deprecated
	public void endSession() { return; }
}
