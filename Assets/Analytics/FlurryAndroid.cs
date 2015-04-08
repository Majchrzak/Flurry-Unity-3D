/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2014 Mateusz Majchrzak
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Analytics
{
	/// <summary>
	/// Flurry Android SDK 5.4.0 implementation
	/// </summary>
	public static class FlurryAndroid
	{
        #region [Internal]
#if UNITY_ANDROID && !UNITY_EDITOR
		private static readonly string s_FlurryAgentClassName = "com.flurry.android.FlurryAgent";
		private static readonly string s_UnityPlayerClassName = "com.unity3d.player.UnityPlayer";
		private static readonly string s_UnityPlayerActivityName = "currentActivity";
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
		/// <summary>
		/// FlurryAgent instance
		/// </summary>
		private static AndroidJavaClass s_FlurryAgent;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
		/// <summary>
		/// TODO:
		/// </summary>
		private static AndroidJavaClass FlurryAgent
		{
			get
			{
				if (Application.platform != RuntimePlatform.Android)
					return null;
				
				if (s_FlurryAgent == null)
					s_FlurryAgent = new AndroidJavaClass(s_FlurryAgentClassName);
				
				return s_FlurryAgent;
			}
		}
#endif

        /// <summary>
        /// Dispose Android Agent class
        /// </summary>
        public static void Dispose()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			if (s_FlurryAgent != null)
				s_FlurryAgent.Dispose();
			
			s_FlurryAgent = null;
#endif
		}
        #endregion

        #region [Session Calls]
        /// <summary>
        /// Initialize the Flurry SDK.
        /// </summary>
        /// <param name="apiKey">
        /// The API key for your application
        /// </param>
        public static void Init(string apiKey)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass(s_UnityPlayerClassName))
			{
				using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>(s_UnityPlayerActivityName))
				{
					FlurryAgent.CallStatic("init", activity, apiKey);
				}
			}
#endif
        }
		
		/// <summary>
        /// Start or continue a Flurry session for the project.
		/// </summary>
		public static void OnStartSession()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass(s_UnityPlayerClassName))
			{
				using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>(s_UnityPlayerActivityName))
				{
					FlurryAgent.CallStatic("onStartSession", activity);
				}
			}
#endif
		}
		
		/// <summary>
		/// End a Flurry session.
		/// </summary>
		public static void OnEndSession()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass(s_UnityPlayerClassName))
			{
				using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>(s_UnityPlayerActivityName))
				{
					FlurryAgent.CallStatic("onEndSession", activity);
				}
			}
#endif
		}

        /// <summary>
        /// Check to see if there is an active session.
        /// </summary>
        /// <returns>true if a session is currently active, false if not.</returns>
	    public static bool IsSessionActive()
	    {
#if UNITY_ANDROID && !UNITY_EDITOR
			return FlurryAgent.CallStatic<bool>("isSessionActive");
#else
            return false;
#endif
        }

        /// <summary>
        /// Check to see if there is an active session.
        /// </summary>
        /// <returns>The current session id, or null if no session is currently active.</returns>
	    public static string GetSessionId()
	    {
#if UNITY_ANDROID && !UNITY_EDITOR
			return FlurryAgent.CallStatic<string>("getSessionId");
#else
            return string.Empty;
#endif
        }
        #endregion

        #region [SDK Version information]
        /// <summary>
        /// Get the version of the Flurry SDK.
        /// </summary>
        /// <returns> 
        /// The version of the Flurry SDK. 
        /// </returns> 
        public static int GetAgentVersion()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			return FlurryAgent.CallStatic<int>("getAgentVersion");
#else
			return -1;
#endif
		}
		
		/// <summary>
		/// Get the release version of the Flurry SDK.
		/// </summary>
		/// <returns> 
		/// The release version GA/Beta of the Flurry SDK with prefix FLurry_Android.
		/// </returns> 
		public static string GetReleaseVersion()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			return FlurryAgent.CallStatic<string>("getReleaseVersion");
#else
			return string.Empty;
#endif
		}
        #endregion

        #region [Pre-Init Calls - Optional SDK settings that should be called before init]
        /// <summary>
        /// true to enable or false to disable the internal logging for the Flurry SDK.
        /// </summary>
        /// <param name="isEnabled">
        /// true to enable logging, false to disable it.
        /// </param>
        public static void SetLogEnabled(bool isEnabled)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setLogEnabled", isEnabled);
#endif
        }

        /// <summary>
        /// Set the log level of the internal Flurry SDK logging.
        /// </summary>
        /// <param name="logLevel">The level to set it to.</param>
        public static void SetLogLevel(LogLevel logLevel)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setLogLevel", (int)logLevel);
#endif
        }

        /// <summary>
        /// Set the version name of the app. Set the name of the this version of the app.
        /// This name will appear in the http://dev.flurry.com as a filtering option by 
        /// version for many charts.
        /// </summary>
        /// <param name="versionName">
        /// The version of the app.
        /// </param>
        public static void SetVersionName(string versionName)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setVersionName", versionName);
#endif
        }

        /// <summary>
        /// Set whether Flurry should record location via GPS. Defaults to true.
        /// </summary>
        /// <param name="reportLocation">
        /// True to allow Flurry to record location via GPS, false otherwise
        /// </param>
        public static void SetReportLocation(bool reportLocation)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setReportLocation", reportLocation);
#endif
		}
		
		/// <summary>
		/// Set the default location. Useful when it is necessary to test analytics 
		/// from other countries or places, or if your app is location-aware and you 
		/// do not wish the Flurry SDK to determine the user location via GPS.
		/// </summary>
		/// <param name="lat">The latitude</param>
		/// <param name="lon">The longitude</param>
		public static void SetLocation(float lat, float lon)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setLocation", lat, lon);
#endif
		}
		
		/// <summary>
		/// Clear the default location.
		/// </summary>
		public static void ClearLocation()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("clearLocation");
#endif
		}
		
		/// <summary>
		/// Set the timeout for expiring a Flurry session.
		/// </summary>
		/// <param name="millis">
		/// The time in milliseconds to set the session timeout to. Minimum value of 5000.
		/// </param>
		public static void SetContinueSessionMillis(long millis)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setContinueSessionMillis", millis);
#endif
		}
		
		/// <summary>
		/// true to enable event logging or false to disable event logging.
		/// </summary>
		/// <param name="logEvents">
		/// true to log events, false to disable event logging.
		/// </param>
		public static void SetLogEvents(bool logEvents)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setLogEvents", logEvents);
#endif
		}
		
		/// <summary>
		/// true to enable or false to disable the ability to catch all uncaught 
		/// exceptions and have them reported back to Flurry.
		/// </summary>
		/// <param name="isEnabled">true to enable, false to disable.</param>
		public static void SetCaptureUncaughtExceptions(bool isEnabled)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setCaptureUncaughtExceptions", isEnabled);
#endif
		}
		
		/// <summary>
		/// Add origin attribution.
		/// </summary>
		/// <param name="originName">
		/// The name/id of the origin you wish to attribute.
		/// </param>
		/// <param name="originVersion">
		/// The version of the origin you wish to attribute.
		/// </param>
		public static void AddOrigin(string originName, string originVersion)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("addOrigin", originName, originVersion);
#endif
        }

        /// <summary>
        /// Add origin attribution with parameters.
        /// </summary>
        /// <param name="originName">
        /// The name/id of the origin you wish to attribute.
        /// </param>
        /// <param name="originVersion">
        /// The version of the origin you wish to attribute.
        /// </param>
        /// <param name="originParameters">
        /// Add origin attribution with parameters.
        /// </param>
        public static void AddOrigin(string originName, string originVersion, Dictionary<string, string> originParameters)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (var hashMap = DictionaryToJavaHashMap(originParameters))
			{
				FlurryAgent.CallStatic("addOrigin", originName, originVersion, hashMap);
			}
#endif
        }

        /// <summary>
        /// Disable or enable Flurry Pulse.
        /// </summary>
        /// <param name="isEnabled">true to enable, false to disable.</param>
        public static void SetPulseEnabled(bool isEnabled)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setPulseEnabled", isEnabled);
#endif
        }
        #endregion

        #region [Event and Error Logging Methods for reporting custom events and errors during the session]
        /// <summary>
        /// Log an event.
        /// </summary>
        /// <param name="eventId">The name/id of the event.</param>
        public static EventRecordStatus LogEvent(string eventId)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			return JavaObjectToEventRecordStatus(FlurryAgent.CallStatic<AndroidJavaObject>("logEvent", eventId));
#else
            return EventRecordStatus.Failed;
#endif
        }
		
		/// <summary>
		/// Log an event with parameters.
		/// </summary>
		/// <param name="eventId">The name/id of the event.</param>
		/// <param name="parameters">
		/// A Dictionary of the parameters which should be submitted with this event.
		/// </param>
		public static EventRecordStatus LogEvent(string eventId, Dictionary<string, string> parameters)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (var hashMap = DictionaryToJavaHashMap(parameters))
			{
				return JavaObjectToEventRecordStatus(FlurryAgent.CallStatic<AndroidJavaObject>("logEvent", eventId, hashMap, false));
			}
#else
            return EventRecordStatus.Failed;
#endif
        }
		
		/// <summary>
		/// Log a timed event.
		/// </summary>
		/// <param name="eventId">The name/id of the event.</param>
		/// <param name="timed">True if the event should be timed, false otherwise.</param>
		public static EventRecordStatus LogEvent(string eventId, bool timed)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			return JavaObjectToEventRecordStatus(FlurryAgent.CallStatic<AndroidJavaObject>("logEvent", eventId, timed));
#else
            return EventRecordStatus.Failed;
#endif
        }
		
		/// <summary>
		/// Log a timed event with parameters. Log a timed event with parameters with the 
		/// Flurry service. To end the timer, call endTimedEvent(String) with this eventId.
		/// </summary>
		/// <param name="eventId">The name/id of the event.</param>
		/// <param name="parameters">
		/// A Dictionary of parameters to log with this event.
		/// </param>
		/// <param name="timed">True if this event is timed, false otherwise.</param>
		public static EventRecordStatus LogEvent(string eventId, Dictionary<string, string> parameters, bool timed)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (var hashMap = DictionaryToJavaHashMap(parameters))
			{
				return JavaObjectToEventRecordStatus(FlurryAgent.CallStatic<AndroidJavaObject>("logEvent", eventId, hashMap, timed));
			}
#else
            return EventRecordStatus.Failed;
#endif
        }
		
		/// <summary>
		/// End a timed event.
		/// </summary>
		/// <param name="eventId">The name/id of the event to end the timer on.</param>
		public static void EndTimedEvent(string eventId)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("endTimedEvent", eventId);
#endif
		}
		
		/// <summary>
		/// End a timed event.
		/// </summary>
		/// <param name="eventId">The name/id of the event to end the timer on.</param>
		/// <param name="parameters">
		/// A Dictionary of parameters to log with this event.
		/// </param>
		public static void EndTimedEvent(string eventId, Dictionary<string, string> parameters)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (var hashMap = DictionaryToJavaHashMap(parameters))
			{
				FlurryAgent.CallStatic("endTimedEvent", eventId, hashMap);
			}
#endif
        }

        /// <summary>
        /// Log an error with the Flurry service.
        /// </summary>
        /// <param name="errorId">The name/ID of the error</param>
        /// <param name="message">The message of the error</param>
        /// <param name="errorClass">The class of the error</param>
        public static void OnError(string errorId, string message, string errorClass)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
            FlurryAgent.CallStatic("onError", errorId, message, errorClass);
#endif
		}
        #endregion

        #region [Page View Methods Count page views]
        /// <summary>
        /// Log a page view.
        /// </summary>
        public static void OnPageView()
	    {
#if UNITY_ANDROID && !UNITY_EDITOR
            FlurryAgent.CallStatic("onPageView");
#endif
        }
        #endregion

        #region [User Info Methods to set user information]
        /// <summary>
        /// Sets the age of the user at the time of this session.
        /// </summary>
        /// <param name="age">valid values are 0-110</param>
        public static void SetAge(int age)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setAge", age);
#endif
		}
		
		/// <summary>
		/// Sets the gender of the user.
		/// </summary>
		/// <param name="gender"></param>
		public static void SetGender(byte gender)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setGender", gender);
#endif
        }
		
		/// <summary>
		/// Sets the Flurry userId for this session. The Flurry userId can be used to 
		/// tie a user back to your internal systems. This userId will travel along with 
		/// any event reporting that the Flurry Dev Portal provides.
		/// </summary>
		/// <param name="userId">User id</param>
		public static void SetUserId(string userId)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setUserId", userId);
#endif
		}
        #endregion

        #region [Helpers]
#if UNITY_ANDROID && !UNITY_EDITOR
		/// <summary>
		/// Converts Dictionary<string, string> to java HashMap object
		/// </summary>
		private static AndroidJavaObject DictionaryToJavaHashMap(Dictionary<string, string> dictionary)
		{
			var javaObject = new AndroidJavaObject("java.util.HashMap");
			var put = AndroidJNIHelper.GetMethodID(javaObject.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
				
			foreach (KeyValuePair<string, string> entry in dictionary)
			{
				using (var key = new AndroidJavaObject("java.lang.String", entry.Key))
				{
					using (var value = new AndroidJavaObject("java.lang.String", entry.Value))
					{
						AndroidJNI.CallObjectMethod(javaObject.GetRawObject(), put, AndroidJNIHelper.CreateJNIArgArray(new object[] { key, value }));
					}
				}
			}

		    return javaObject;
		}

        /// <summary>
        /// Converts java EventRecordStatus to EventRecordStatus
        /// </summary>
        /// <param name="javaObject">java object</param>
        /// <returns></returns>
	    private static EventRecordStatus JavaObjectToEventRecordStatus(AndroidJavaObject javaObject)
        {
            return (EventRecordStatus) javaObject.Call<int>("ordinal");
        }
#endif
        #endregion
    }
}