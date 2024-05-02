using AmplitudeNS.MiniJSON;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if (UNITY_IPHONE || UNITY_TVOS)
using System.Runtime.InteropServices;
#endif

public class Amplitude {
	private static readonly string UnityLibraryName = "amplitude-unity";
	private static readonly string UnityLibraryVersion = "2.8.0";

	private static Dictionary<string, Amplitude> instances;
	private static readonly object instanceLock = new object();

#if UNITY_ANDROID
	private static readonly string androidPluginName = "com.amplitude.unity.plugins.AmplitudePlugin";
	private AndroidJavaClass pluginClass;
#endif

	public bool logging = false;
	private string instanceName = null;

#if (UNITY_IPHONE || UNITY_TVOS)
	[DllImport ("__Internal")]
	private static extern void _Amplitude_init(string instanceName, string apiKey, string userId);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setTrackingOptions(string instanceName, string trackingOptionsJson);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logEvent(string instanceName, string evt, string propertiesJson);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logOutOfSessionEvent(string instanceName, string evt, string propertiesJson);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOffline(string instanceName, bool offline);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserId(string instanceName, string userId);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setDeviceId(string instanceName, string deviceId);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserProperties(string instanceName, string propertiesJson);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setGroup(string instanceName, string groupType, string groupName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setGroupWithStringArray(string instanceName, string groupType, string[] groupName, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOptOut(string instanceName, bool enabled);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setMinTimeBetweenSessionsMillis(string instanceName, long minTimeBetweenSessionsMillis);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setEventUploadPeriodSeconds(string instanceName, int eventUploadPeriodMillis);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setLibraryName(string instanceName, string libraryName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setLibraryVersion(string instanceName, string libraryVersion);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_enableCoppaControl(string instanceName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_disableCoppaControl(string instanceName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setServerUrl(string instanceName, string serverUrl);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setServerZone(string instanceName, string serverZone, bool updateServerUrl);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUseDynamicConfig(string instanceName, bool useDynamicConfig);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenueAmount(string instanceName, double amount);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenue(string instanceName, string productIdentifier, int quantity, double price);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenueWithReceipt(string instanceName, string productIdentifier, int quantity, double price, string receipt);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_logRevenueWithReceiptAndProperties(string instanceName, string productIdentifier, int quantity, double price, string receipt, string revenueType, string propertiesJson);
	[DllImport ("__Internal")]
	private static extern string _Amplitude_getDeviceId(string instanceName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_regenerateDeviceId(string instanceName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_useAdvertisingIdForDeviceId(string instanceName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_trackingSessionEvents(string instanceName, bool enabled);
	[DllImport ("__Internal")]
	private static extern long _Amplitude_getSessionId(string instanceName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_uploadEvents(string instanceName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_clearUserProperties(string instanceName);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_unsetUserProperty(string instanceName, string property);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyBool(string instanceName, string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyDouble(string instanceName, string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyFloat(string instanceName, string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyInt(string instanceName, string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyLong(string instanceName, string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyString(string instanceName, string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyDict(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyList(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyBoolArray(string instanceName, string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyDoubleArray(string instanceName, string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyFloatArray(string instanceName, string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyIntArray(string instanceName, string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyLongArray(string instanceName, string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setOnceUserPropertyStringArray(string instanceName, string property, string[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyBool(string instanceName, string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyDouble(string instanceName, string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyFloat(string instanceName, string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyInt(string instanceName, string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyLong(string instanceName, string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyString(string instanceName, string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyDict(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyList(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyBoolArray(string instanceName, string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyDoubleArray(string instanceName, string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyFloatArray(string instanceName, string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyIntArray(string instanceName, string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyLongArray(string instanceName, string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_setUserPropertyStringArray(string instanceName, string property, string[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyDouble(string instanceName, string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyFloat(string instanceName, string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyInt(string instanceName, string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyLong(string instanceName, string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyString(string instanceName, string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_addUserPropertyDict(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyBool(string instanceName, string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyDouble(string instanceName, string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyFloat(string instanceName, string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyInt(string instanceName, string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyLong(string instanceName, string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyString(string instanceName, string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyDict(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyList(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyBoolArray(string instanceName, string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyDoubleArray(string instanceName, string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyFloatArray(string instanceName, string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyIntArray(string instanceName, string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyLongArray(string instanceName, string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_appendUserPropertyStringArray(string instanceName, string property, string[] value, int length);
      //prepend
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyBool(string instanceName, string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyDouble(string instanceName, string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyFloat(string instanceName, string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyInt(string instanceName, string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyLong(string instanceName, string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyString(string instanceName, string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyDict(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyList(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyBoolArray(string instanceName, string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyDoubleArray(string instanceName, string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyFloatArray(string instanceName, string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyIntArray(string instanceName, string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyLongArray(string instanceName, string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_prependUserPropertyStringArray(string instanceName, string property, string[] value, int length);
	//preInsert
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyBool(string instanceName, string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyDouble(string instanceName, string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyFloat(string instanceName, string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyInt(string instanceName, string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyLong(string instanceName, string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyString(string instanceName, string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyDict(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyList(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyBoolArray(string instanceName, string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyDoubleArray(string instanceName, string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyFloatArray(string instanceName, string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyIntArray(string instanceName, string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyLongArray(string instanceName, string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_preInsertUserPropertyStringArray(string instanceName, string property, string[] value, int length);
	//postInsert
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyBool(string instanceName, string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyDouble(string instanceName, string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyFloat(string instanceName, string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyInt(string instanceName, string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyLong(string instanceName, string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyString(string instanceName, string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyDict(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyList(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyBoolArray(string instanceName, string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyDoubleArray(string instanceName, string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyFloatArray(string instanceName, string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyIntArray(string instanceName, string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyLongArray(string instanceName, string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_postInsertUserPropertyStringArray(string instanceName, string property, string[] value, int length);
	//remove
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyBool(string instanceName, string property, bool value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyDouble(string instanceName, string property, double value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyFloat(string instanceName, string property, float value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyInt(string instanceName, string property, int value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyLong(string instanceName, string property, long value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyString(string instanceName, string property, string value);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyDict(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyList(string instanceName, string property, string values);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyBoolArray(string instanceName, string property, bool[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyDoubleArray(string instanceName, string property, double[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyFloatArray(string instanceName, string property, float[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyIntArray(string instanceName, string property, int[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyLongArray(string instanceName, string property, long[] value, int length);
	[DllImport ("__Internal")]
	private static extern void _Amplitude_removeUserPropertyStringArray(string instanceName, string property, string[] value, int length);
#endif

	public static Amplitude getInstance() {
		return getInstance(null);
	}
	public static Amplitude getInstance(string instanceName) {
		string instanceKey = instanceName;
		if (string.IsNullOrEmpty(instanceKey)) {
			instanceKey = "$default_instance";
		}

		lock(instanceLock)
		{
			if (instances == null) {
				instances = new Dictionary<string, Amplitude>();
			}

			Amplitude instance;
			if (instances.TryGetValue(instanceKey, out instance)) {
				// No logic
			} else {
				instance = new Amplitude(instanceName);
				instances.Add(instanceKey, instance);
			}
			return instance;
		}
	}

	public static Amplitude Instance {
		get
		{
			return getInstance();
		}
	}

	public Amplitude(string instanceName) : base() {
		this.instanceName = instanceName;

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			Debug.Log ("construct instance");
			pluginClass = new AndroidJavaClass(androidPluginName);
		}
#endif

		this.setLibraryName(UnityLibraryName);
		this.setLibraryVersion(UnityLibraryVersion);
	}

	protected void Log(string message) {
		if(!logging) return;

		Debug.Log(message);
	}

	protected void Log<T>(string message, string property, IEnumerable<T> array) {
		Log (string.Format("{0} {1}, {2}: [{3}]", message, property, array, string.Join(", ", array)));
	}

	public void init(string apiKey) {
		Log (string.Format("C# init {0}", apiKey));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_init(instanceName, apiKey, null);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using(AndroidJavaObject unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
					using(AndroidJavaObject unityApplication = unityActivity.Call<AndroidJavaObject>("getApplication")) {
						pluginClass.CallStatic("init", instanceName, unityActivity, apiKey);
						pluginClass.CallStatic("enableForegroundTracking", instanceName, unityApplication);
					}
				}
			}
		}
#endif
	}

	/// <summary>
	/// Initialize the SDK.
	/// </summary>
	/// <param name="apiKey">API key</param>
	/// <param name="userId">user Id</param>
	public void init(string apiKey, string userId) {
		Log (string.Format("C# init {0} with userId {1}", apiKey, userId));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_init(instanceName, apiKey, userId);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using(AndroidJavaObject unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
					using (AndroidJavaObject unityApplication = unityActivity.Call<AndroidJavaObject>("getApplication")) {
						pluginClass.CallStatic("init", instanceName, unityActivity, apiKey, userId);
						pluginClass.CallStatic("enableForegroundTracking", instanceName, unityApplication);
					}
				}
			}
		}
#endif
	}

	public void setTrackingOptions(IDictionary<string, bool> trackingOptions) {
		if (trackingOptions != null) {
			string trackingOptionsJson = Json.Serialize(trackingOptions);

			Log(string.Format("C# setting tracking options {0}", trackingOptionsJson));
#if (UNITY_IPHONE || UNITY_TVOS)
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
				_Amplitude_setTrackingOptions(instanceName, trackingOptionsJson);
			}
#endif

#if UNITY_ANDROID
			if (Application.platform == RuntimePlatform.Android) {
				pluginClass.CallStatic("setTrackingOptions", instanceName, trackingOptionsJson);
			}
#endif
		}
	}

	/// <summary>
	/// Tracks an event. Events are saved locally.
	/// Uploads are batched to occur every 30 events or every 30 seconds (whichever comes first), as well as on app close.
	/// </summary>
	/// <param name="evt">event type</param>
	public void logEvent(string evt) {
		Log (string.Format("C# sendEvent {0}", evt));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_logEvent(instanceName, evt, null);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logEvent", instanceName, evt);
		}
#endif
	}
	
	/// <summary>
	/// Tracks an event. Events are saved locally.
	/// Uploads are batched to occur every 30 events or every 30 seconds (whichever comes first), as well as on app close.
	/// </summary>
	/// <param name="evt">event type</param>
	/// <param name="properties">event properties</param>
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
			_Amplitude_logEvent(instanceName, evt, propertiesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logEvent", instanceName, evt, propertiesJson);
		}
#endif
	}

	/// <summary>
	/// Tracks an event. Events are saved locally.
	/// Uploads are batched to occur every 30 events or every 30 seconds (whichever comes first), as well as on app close.
	/// </summary>
	/// <param name="evt">event type</param>
	/// <param name="properties">event properties</param>
	/// <param name="outOfSession">if this event belongs to current session</param>
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
				_Amplitude_logOutOfSessionEvent(instanceName, evt, propertiesJson);
			} else {
				_Amplitude_logEvent(instanceName, evt, propertiesJson);
			}
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logEvent", instanceName, evt, propertiesJson, outOfSession);
		}
#endif
	}

	/// <summary>
	/// Sets offline. If offline is true, then the SDK will not upload events to Amplitude servers.
	/// However, it will still log events.
	/// </summary>
	/// <param name="offline"></param>
	public void setOffline(bool offline) {
		Log (string.Format("C# setOffline {0}", offline));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOffline(instanceName, offline);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOffline", instanceName, offline);
		}
#endif
	}

	/// <summary>
	/// Set user Id
	/// </summary>
	/// <param name="userId"></param>
	public void setUserId(string userId) {
		Log (string.Format("C# setUserId {0}", userId));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserId(instanceName, userId);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserId", instanceName, userId);
		}
#endif
	}

	/// <summary>
	/// Set user properties
	/// </summary>
	/// <param name="properties">properties dictionary</param>
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
			_Amplitude_setUserProperties(instanceName, propertiesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperties", instanceName, propertiesJson);
		}
#endif
	}

	/// <summary>
	/// Adds a user to a group or groups. You need to specify a groupType and groupName(s).
	/// For example you can group people by their organization. In that case groupType is "orgId", and groupName would be the actual ID(s). groupName can be a string or an array of strings to indicate a user in multiple groups. 
	/// You can also call setGroup multiple times with different groupTypes to track multiple types of groups (up to 5 per app).
	/// **Note:** this will also set groupType: groupName as a user property.
	/// </summary>
	/// <param name="groupType"You need to specify a group type (for example orgId).</param>
	/// <param name="groupName"The value for the group name, can be a string. (for example for groupType orgId, the groupName would be the actual id number, like 15).</param>
	public void setGroup(string groupType, string groupName) {
		Log (string.Format("C# setGroup"));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setGroup(instanceName, groupType, groupName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setGroup", instanceName, groupType, groupName);
		}
#endif
	}

	/// <summary>
	/// Adds a user to a group or groups. You need to specify a groupType and groupName(s).
	/// For example you can group people by their organization. In that case groupType is "orgId", and groupName would be the actual ID(s). groupName can be a string or an array of strings to indicate a user in multiple groups. 
	/// You can also call setGroup multiple times with different groupTypes to track multiple types of groups (up to 5 per app).
	/// **Note:** this will also set groupType: groupName as a user property.
	/// </summary>
	/// <param name="groupType"You need to specify a group type (for example sports).</param>
	/// <param name="groupName"The value for the group name, can be an array of strings. (for example for groupType orgId, the groupName would be array of sport, like [soccer, tennis]).</param>
	public void setGroup(string groupType, string[] groupName) {
		Log (string.Format("C# setGroup"));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setGroupWithStringArray(instanceName, groupType, groupName, groupName.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setGroup", instanceName, groupType, groupName);
		}
#endif
	}

	/// <summary>
	/// Enables tracking opt out.
	/// If the user wants to opt out of all tracking, use this method to enable opt out for them. 
	/// Once opt out is enabled, no events will be saved locally or sent to the server. 
	/// </summary>
	/// <param name="enabled">enable opt out</param>
	public void setOptOut(bool enabled) {
		Log (string.Format("C# setOptOut {0}", enabled));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOptOut(instanceName, enabled);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOptOut", instanceName, enabled);
		}
#endif
	}

	/// <summary>
	/// When a user closes and reopens the app within minTimeBetweenSessionsMillis milliseconds, 
	/// the reopen is considered part of the same session and the session continues. 
	/// Otherwise, a new session is created. The default is 5 minutes.
	/// </summary>
	/// <param name="minTimeBetweenSessionsMillis">minimum time (milliseconds) between sessions</param>
	public void setMinTimeBetweenSessionsMillis(long minTimeBetweenSessionsMillis) {
		Log (string.Format("C# minTimeBetweenSessionsMillis {0}", minTimeBetweenSessionsMillis));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setMinTimeBetweenSessionsMillis(instanceName, minTimeBetweenSessionsMillis);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setMinTimeBetweenSessionsMillis", instanceName, minTimeBetweenSessionsMillis);
		}
#endif
	}
	
	/// <summary>
	/// Sets event upload period seconds. The SDK will attempt to batch upload unsent events
	/// every eventUploadPeriodSecond seconds, or if the unsent event count exceeds the
	/// event upload threshold. Default is 30 seconds.
	/// </summary>
	/// <param name="eventUploadPeriodSeconds">seconds between batch uploads of events</param>
	public void setEventUploadPeriodSeconds(int eventUploadPeriodSeconds) {
		Log (string.Format("C# eventUploadPeriodSeconds {0}", eventUploadPeriodSeconds));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setEventUploadPeriodSeconds(instanceName, eventUploadPeriodSeconds);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setEventUploadPeriodMillis", instanceName, eventUploadPeriodSeconds * 1000);
		}
#endif
	}

	/// <summary>
	/// If your app has its own system for tracking devices, you can set the deviceId.
	/// 
	/// NOTE: not recommended unless you know what you are doing.
	/// </summary>
	/// <param name="deviceId">device Id</param>
	public void setDeviceId(string deviceId) {
		Log (string.Format("C# setDeviceId {0}", deviceId));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setDeviceId(instanceName, deviceId);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setDeviceId", instanceName, deviceId);
		}
#endif
	}

	/// <summary>
	/// Enable COPPA (Children's Online Privacy Protection Act) restrictions on IDFA, IDFV, city, IP address and location tracking.
	/// This can be used by any customer that does not want to collect IDFA, IDFV, city, IP address and location tracking.
	/// </summary>
	public void enableCoppaControl() {
		Log ("C# enableCoppaControl");
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_enableCoppaControl(instanceName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("enableCoppaControl", instanceName);
		}
#endif
	}

	/// <summary>
	/// Disable COPPA (Children's Online Privacy Protection Act) restrictions on IDFA, IDFV, city, IP address and location tracking.
	/// </summary>
	public void disableCoppaControl() {
		Log (string.Format("C# disableCoppaControl"));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_disableCoppaControl(instanceName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("disableCoppaControl", instanceName);
		}
#endif
	}

	/// <summary>
	/// Customize server url events will be forwarded to.
	/// </summary>
	public void setServerUrl(string serverUrl) {
		Log (string.Format("C# setServerUrl"));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setServerUrl(instanceName, serverUrl);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setServerUrl", instanceName, serverUrl);
		}
#endif
	}

	/// <summary>
	/// Turning dynamic config on will find the best server url automatically based on users' geo location.
	/// Note:
	/// 1. If you have your own proxy server and use `setServerUrl` API, please leave this off.
	/// 2. If you have users in China Mainland, we suggest you turn this on.
	/// </summary>
	public void setUseDynamicConfig(bool useDynamicConfig) {
		Log (string.Format("C# setUseDynamicConfig"));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUseDynamicConfig(instanceName, useDynamicConfig);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUseDynamicConfig", instanceName, useDynamicConfig);
		}
#endif
	}

	/// <summary>
	/// Set Amplitude Server Zone, switch to zone related configuration, including dynamic configuration and server url.
    /// To send data to Amplitude's EU servers, you need to configure the serverZone to EU like client.setServerZone("EU");
    /// serverZone could be EU or US. Recommend to keep updateServerUrl to be true for alignment unless use own proxy server.
	/// </summary>
	public void setServerZone(AmplitudeServerZone serverZone, bool updateServerUrl = true) {
		Log (string.Format("C# setServerZone"));
		string serverZoneStr = serverZone.ToString();
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setServerZone(instanceName, serverZoneStr, updateServerUrl);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setServerZone", instanceName, serverZoneStr, updateServerUrl);
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
			_Amplitude_logRevenueAmount(instanceName, amount);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", instanceName, amount);
		}
#endif
	}

	public void logRevenue(string productId, int quantity, double price) {
		Log (string.Format("C# logRevenue {0}, {1}, {2}", productId, quantity, price));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_logRevenue(instanceName, productId, quantity, price);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", instanceName, productId, quantity, price);
		}
#endif
	}
	
	public void logRevenue(string productId, int quantity, double price, string receipt, string receiptSignature) {
		Log (string.Format("C# logRevenue {0}, {1}, {2} (with receipt)", productId, quantity, price));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_logRevenueWithReceipt(instanceName, productId, quantity, price, receipt);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", instanceName, productId, quantity, price, receipt, receiptSignature);
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
			_Amplitude_logRevenueWithReceiptAndProperties(instanceName, productId, quantity, price, receipt, revenueType, propertiesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("logRevenue", instanceName, productId, quantity, price, receipt, receiptSignature, revenueType, propertiesJson);
		}
#endif
	}

	/// <summary>
	/// Get current device Id.
	/// </summary>
	/// <returns></returns>
	public string getDeviceId() {
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			return _Amplitude_getDeviceId(instanceName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			return pluginClass.CallStatic<string>("getDeviceId", instanceName);
		}
#endif
		return null;
	}

	/// <summary>
	/// Regenerates a new random deviceId for current user. 
	/// Note: this is not recommended unless you know what you are doing. This can be used in conjunction with setUserId(null) to anonymize
	/// users after they log out. 
	/// With a null userId and a completely new deviceId, the current user would appear as a brand new user in dashboard.
	/// </summary>
	public void regenerateDeviceId() {
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_regenerateDeviceId(instanceName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("regenerateDeviceId", instanceName);
		}
#endif
	}

	/// <summary>
	/// iOS: 
	/// Use advertisingIdentifier instead of identifierForVendor as the device ID.
	/// Apple prohibits the use of advertisingIdentifier if your app does not have advertising. 
	/// 
	/// Android:
	/// Whether to use the Android advertising ID (ADID) as the user's device ID.
	/// 
	/// **NOTE:** Must be called before `initializeApiKey`.
	/// </summary>
	public void useAdvertisingIdForDeviceId() {
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_useAdvertisingIdForDeviceId(instanceName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("useAdvertisingIdForDeviceId", instanceName);
		}
#endif
	}

	/// <summary>
	/// iOS: 
	/// This method has no equivalent in iOS. See comments in `useAdvertisingIdForDeviceId()`.
	/// 
	/// Android: If this method is called, the setting for Advertising ID will be used first for the user's device ID,
	/// and then if unsuccessful, fall back to Android App Set ID: a user-resetable, per device/per install ID provided by Google.
	/// 
	/// **NOTE:** Must be called before `initializeApiKey`.
	/// </summary>
	public void useAppSetIdForDeviceId() {
#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("useAppSetIdForDeviceId", instanceName);
		}
#endif
	}

	/// <summary>
	/// Whether to automatically log start and end session events corresponding to the start and end of a user's session.
	/// </summary>
	/// <param name="enabled"></param>
	public void trackSessionEvents(bool enabled) {
		Log (string.Format("C# trackSessionEvents {0}", enabled));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_trackingSessionEvents(instanceName, enabled);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("trackSessionEvents", instanceName, enabled);
		}
#endif
	}

	/// <summary>
	/// Get session Id
	/// </summary>
	/// <returns>sessionId</returns>
	public long getSessionId() {

#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			return _Amplitude_getSessionId(instanceName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			return pluginClass.CallStatic<long>("getSessionId", instanceName);
		}
#endif
		return -1;
	}

	/// <summary>
	/// Manually forces the instance to immediately upload all unsent events. 
	/// Use this method to force the class to immediately upload all queued events.
	/// </summary>
	public void uploadEvents() {
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_uploadEvents(instanceName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("uploadEvents", instanceName);
		}
#endif
	}

// User Property Operations
// ClearUserProperties
	public void clearUserProperties() {
		Log (string.Format("C# clearUserProperties"));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_clearUserProperties(instanceName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("clearUserProperties", instanceName);
		}
#endif
	}

// Unset
	public void unsetUserProperty(string property) {
		Log (string.Format("C# unsetUserProperty {0}", property));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_unsetUserProperty(instanceName, property);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("unsetUserProperty", instanceName, property);
		}
#endif
	}

// setOnce
	public void setOnceUserProperty(string property, bool value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyBool(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, double value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyDouble(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, float value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyFloat(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, int value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyInt(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, long value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyLong(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setOnceUserProperty(string property, string value) {
		Log (string.Format("C# setOnceUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyString(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, value);
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
			_Amplitude_setOnceUserPropertyDict(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserPropertyDict", instanceName, property, valuesJson);
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
			_Amplitude_setOnceUserPropertyList(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserPropertyList", instanceName, property, valuesJson);
		}
#endif
	}

	public void setOnceUserProperty(string property, bool[] array) {
		Log ("C# setOnceUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyBoolArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setOnceUserProperty(string property, double[] array) {
		Log ("C# setOnceUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyDoubleArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setOnceUserProperty(string property, float[] array) {
		Log ("C# setOnceUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyFloatArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setOnceUserProperty(string property, int[] array) {
		Log ("C# setOnceUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyIntArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setOnceUserProperty(string property, long[] array) {
		Log ("C# setOnceUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyLongArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setOnceUserProperty(string property, string[] array) {
		Log ("C# setOnceUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setOnceUserPropertyStringArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setOnceUserProperty", instanceName, property, array);
		}
#endif
	}

// set
	public void setUserProperty(string property, bool value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyBool(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setUserProperty(string property, double value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyDouble(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setUserProperty(string property, float value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyFloat(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setUserProperty(string property, int value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyInt(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setUserProperty(string property, long value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyLong(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, value);
		}
#endif
	}

	public void setUserProperty(string property, string value) {
		Log (string.Format("C# setUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyString(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, value);
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
			_Amplitude_setUserPropertyDict(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, valuesJson);
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
			_Amplitude_setUserPropertyList(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserPropertyList", instanceName, property, valuesJson);
		}
#endif
	}

	public void setUserProperty(string property, bool[] array) {
		Log ("C# setUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyBoolArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setUserProperty(string property, double[] array) {
		Log ("C# setUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyDoubleArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setUserProperty(string property, float[] array) {
		Log ("C# setUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyFloatArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setUserProperty(string property, int[] array) {
		Log ("C# setUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyIntArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setUserProperty(string property, long[] array) {
		Log ("C# setUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyLongArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, array);
		}
#endif
	}

	public void setUserProperty(string property, string[] array) {
		Log ("C# setUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setUserPropertyStringArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setUserProperty", instanceName, property, array);
		}
#endif
	}


// add
	public void addUserProperty(string property, double value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyDouble(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", instanceName, property, value);
		}
#endif
	}

	public void addUserProperty(string property, float value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyFloat(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", instanceName, property, value);
		}
#endif
	}

	public void addUserProperty(string property, int value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyInt(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", instanceName, property, value);
		}
#endif
	}

	public void addUserProperty(string property, long value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyLong(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", instanceName, property, value);
		}
#endif
	}

	public void addUserProperty(string property, string value) {
		Log (string.Format("C# addUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_addUserPropertyString(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserProperty", instanceName, property, value);
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
			_Amplitude_addUserPropertyDict(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("addUserPropertyDict", instanceName, property, valuesJson);
		}
#endif
	}

// append
	public void appendUserProperty(string property, bool value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyBool(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, value);
		}
#endif
	}

	public void appendUserProperty(string property, double value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyDouble(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, value);
		}
#endif
	}

	public void appendUserProperty(string property, float value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyFloat(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, value);
		}
#endif
	}

	public void appendUserProperty(string property, int value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyInt(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, value);
		}
#endif
	}

	public void appendUserProperty(string property, long value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyLong(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, value);
		}
#endif
	}

	public void appendUserProperty(string property, string value) {
		Log (string.Format("C# appendUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyString(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, value);
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
			_Amplitude_appendUserPropertyDict(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserPropertyDict", instanceName, property, valuesJson);
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
			_Amplitude_appendUserPropertyList(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserPropertyList", instanceName, property, valuesJson);
		}
#endif
	}

	public void appendUserProperty(string property, bool[] array) {
		Log ("C# appendUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyBoolArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, array);
		}
#endif
	}

	public void appendUserProperty(string property, double[] array) {
		Log ("C# appendUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyDoubleArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, array);
		}
#endif
	}

	public void appendUserProperty(string property, float[] array) {
		Log ("C# appendUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyFloatArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, array);
		}
#endif
	}

	public void appendUserProperty(string property, int[] array) {
		Log ("C# appendUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyIntArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, array);
		}
#endif
	}

	public void appendUserProperty(string property, long[] array) {
		Log ("C# appendUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyLongArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, array);
		}
#endif
	}

	public void appendUserProperty(string property, string[] array) {
		Log ("C# appendUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_appendUserPropertyStringArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("appendUserProperty", instanceName, property, array);
		}
#endif
	}

//prepend
	public void prependUserProperty(string property, bool value) {
		Log (string.Format("C# prependUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyBool(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, value);
		}
#endif
	}

	public void prependUserProperty(string property, double value) {
		Log (string.Format("C# prependUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyDouble(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, value);
		}
#endif
	}

	public void prependUserProperty(string property, float value) {
		Log (string.Format("C# prependUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyFloat(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, value);
		}
#endif
	}

	public void prependUserProperty(string property, int value) {
		Log (string.Format("C# prependUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyInt(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, value);
		}
#endif
	}

	public void prependUserProperty(string property, long value) {
		Log (string.Format("C# prependUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyLong(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, value);
		}
#endif
	}

	public void prependUserProperty(string property, string value) {
		Log (string.Format("C# prependUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyString(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, value);
		}
#endif
	}

	public void prependUserProperty(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}

		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# prependUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyDict(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserPropertyDict", instanceName, property, valuesJson);
		}
#endif
	}

	public void prependUserProperty<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}

		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# prependUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyList(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserPropertyList", instanceName, property, valuesJson);
		}
#endif
	}

	public void prependUserProperty(string property, bool[] array) {
		Log ("C# prependUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyBoolArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, array);
		}
#endif
	}

	public void prependUserProperty(string property, double[] array) {
		Log ("C# prependUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyDoubleArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, array);
		}
#endif
	}

	public void prependUserProperty(string property, float[] array) {
		Log ("C# prependUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyFloatArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, array);
		}
#endif
	}

	public void prependUserProperty(string property, int[] array) {
		Log ("C# prependUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyIntArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, array);
		}
#endif
	}

	public void prependUserProperty(string property, long[] array) {
		Log ("C# prependUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyLongArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, array);
		}
#endif
	}

	public void prependUserProperty(string property, string[] array) {
		Log ("C# prependUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_prependUserPropertyStringArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, array);
		}
#endif
	}

//preInsert
	public void preInsertUserProperty(string property, bool value) {
		Log (string.Format("C# preInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyBool(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void preInsertUserProperty(string property, double value) {
		Log (string.Format("C# preInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyDouble(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void preInsertUserProperty(string property, float value) {
		Log (string.Format("C# preInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyFloat(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void preInsertUserProperty(string property, int value) {
		Log (string.Format("C# preInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyInt(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void preInsertUserProperty(string property, long value) {
		Log (string.Format("C# preInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyLong(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void preInsertUserProperty(string property, string value) {
		Log (string.Format("C# preInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyString(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void preInsertUserProperty(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}

		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# preInsertUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyDict(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserPropertyDict", instanceName, property, valuesJson);
		}
#endif
	}

	public void preInsertUserProperty<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}

		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# preInsertUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyList(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserPropertyList", instanceName, property, valuesJson);
		}
#endif
	}

	public void preInsertUserProperty(string property, bool[] array) {
		Log ("C# preInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyBoolArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void preInsertUserProperty(string property, double[] array) {
		Log ("C# preInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyDoubleArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void preInsertUserProperty(string property, float[] array) {
		Log ("C# preInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyFloatArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void preInsertUserProperty(string property, int[] array) {
		Log ("C# preInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyIntArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void preInsertUserProperty(string property, long[] array) {
		Log ("C# preInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyLongArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("preInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void preInsertUserProperty(string property, string[] array) {
		Log ("C# preInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_preInsertUserPropertyStringArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("prependUserProperty", instanceName, property, array);
		}
#endif
	}

//postInsert
	public void postInsertUserProperty(string property, bool value) {
		Log (string.Format("C# postInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyBool(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void postInsertUserProperty(string property, double value) {
		Log (string.Format("C# postInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyDouble(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void postInsertUserProperty(string property, float value) {
		Log (string.Format("C# postInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyFloat(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void postInsertUserProperty(string property, int value) {
		Log (string.Format("C# postInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyInt(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void postInsertUserProperty(string property, long value) {
		Log (string.Format("C# postInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyLong(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void postInsertUserProperty(string property, string value) {
		Log (string.Format("C# postInsertUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyString(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, value);
		}
#endif
	}

	public void postInsertUserProperty(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}

		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# postInsertUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyDict(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserPropertyDict", instanceName, property, valuesJson);
		}
#endif
	}

	public void postInsertUserProperty<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}

		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# postInsertUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyList(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserPropertyList", instanceName, property, valuesJson);
		}
#endif
	}

	public void postInsertUserProperty(string property, bool[] array) {
		Log ("C# postInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyBoolArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void postInsertUserProperty(string property, double[] array) {
		Log ("C# postInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyDoubleArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void postInsertUserProperty(string property, float[] array) {
		Log ("C# postInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyFloatArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void postInsertUserProperty(string property, int[] array) {
		Log ("C# postInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyIntArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void postInsertUserProperty(string property, long[] array) {
		Log ("C# postInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyLongArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, array);
		}
#endif
	}

	public void postInsertUserProperty(string property, string[] array) {
		Log ("C# postInsertUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_postInsertUserPropertyStringArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("postInsertUserProperty", instanceName, property, array);
		}
#endif
	}

//remove
	public void removeUserProperty(string property, bool value) {
		Log (string.Format("C# removeUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyBool(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, value);
		}
#endif
	}

	public void removeUserProperty(string property, double value) {
		Log (string.Format("C# removeUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyDouble(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, value);
		}
#endif
	}

	public void removeUserProperty(string property, float value) {
		Log (string.Format("C# removeUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyFloat(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, value);
		}
#endif
	}

	public void removeUserProperty(string property, int value) {
		Log (string.Format("C# removeUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyInt(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, value);
		}
#endif
	}

	public void removeUserProperty(string property, long value) {
		Log (string.Format("C# removeUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyLong(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, value);
		}
#endif
	}

	public void removeUserProperty(string property, string value) {
		Log (string.Format("C# removeUserProperty {0}, {1}", property, value));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyString(instanceName, property, value);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, value);
		}
#endif
	}

	public void removeUserProperty(string property, IDictionary<string, object> values) {
		if (values == null) {
			return;
		}

		string valuesJson = Json.Serialize (values);
		Log (string.Format("C# removeUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyDict(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserPropertyDict", instanceName, property, valuesJson);
		}
#endif
	}

	public void removeUserProperty<T>(string property, IList<T> values) {
		if (values == null) {
			return;
		}

		Dictionary<string, object> wrapper = new Dictionary<string, object>()
		{
			{"list", values}
		};
		string valuesJson = Json.Serialize (wrapper);
		Log (string.Format("C# removeUserProperty {0}, {1}", property, valuesJson));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyList(instanceName, property, valuesJson);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserPropertyList", instanceName, property, valuesJson);
		}
#endif
	}

	public void removeUserProperty(string property, bool[] array) {
		Log ("C# removeUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyBoolArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, array);
		}
#endif
	}

	public void removeUserProperty(string property, double[] array) {
		Log ("C# removeUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyDoubleArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, array);
		}
#endif
	}

	public void removeUserProperty(string property, float[] array) {
		Log ("C# removeUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyFloatArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, array);
		}
#endif
	}

	public void removeUserProperty(string property, int[] array) {
		Log ("C# removeUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyIntArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, array);
		}
#endif
	}

	public void removeUserProperty(string property, long[] array) {
		Log ("C# removeUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyLongArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, array);
		}
#endif
	}

	public void removeUserProperty(string property, string[] array) {
		Log ("C# removeUserProperty", property, array);
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_removeUserPropertyStringArray(instanceName, property, array, array.Length);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("removeUserProperty", instanceName, property, array);
		}
#endif
	}

	private void setLibraryName(string libraryName) {
		Log (string.Format("C# setLibraryName {0}", libraryName));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setLibraryName(instanceName, libraryName);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setLibraryName", instanceName, libraryName);
		}
#endif
	}

	private void setLibraryVersion(string libraryVersion) {
		Log (string.Format("C# setLibraryVersion {0}", libraryVersion));
#if (UNITY_IPHONE || UNITY_TVOS)
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS) {
			_Amplitude_setLibraryVersion(instanceName, libraryVersion);
		}
#endif

#if UNITY_ANDROID
		if (Application.platform == RuntimePlatform.Android) {
			pluginClass.CallStatic("setLibraryVersion", instanceName, libraryVersion);
		}
#endif
	}

	// This method is deprecated
	public void startSession() { return; }

	// This method is deprecated
	public void endSession() { return; }
}
