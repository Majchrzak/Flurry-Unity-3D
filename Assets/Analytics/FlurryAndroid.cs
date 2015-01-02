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
	/// Flurry Android SDK 4.0.0 implementation
	/// </summary>
	public static class FlurryAndroid
	{
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
		
		/// <summary>
		/// Start or continue a Flurry session for the project denoted by apiKey.
		/// </summary>
		/// <param name="apiKey">The API key for this project.</param>
		public static void OnStartSession(string apiKey)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass(s_UnityPlayerClassName))
			{
				using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>(s_UnityPlayerActivityName))
				{
					FlurryAgent.CallStatic("onStartSession", activity, apiKey);
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
		public static void SetLogLevel(int logLevel)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setLogLevel", (int)logLevel);
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
		/// Enable the use of HTTPS communications.
		/// </summary>
		/// <param name="useHttps">true to use HTTPS, false otherwise.</param>
		public static void SetUseHttps(bool useHttps)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("setUseHttps", useHttps);
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
			using (DictionaryConverter converter = new DictionaryConverter(originParameters))
			{
				FlurryAgent.CallStatic("addOrigin", originName, originVersion, (AndroidJavaObject)converter);
			}
#endif
		}
		
		/// <summary>
		/// Log an event.
		/// </summary>
		/// <param name="eventId">The name/id of the event.</param>
		public static void LogEvent(string eventId)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("logEvent", eventId);
#endif
		}
		
		/// <summary>
		/// Log an event with parameters.
		/// </summary>
		/// <param name="eventId">The name/id of the event.</param>
		/// <param name="parameters">
		/// A Dictionary of the parameters which should be submitted with this event.
		/// </param>
		public static void LogEvent(string eventId, Dictionary<string, string> parameters)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (DictionaryConverter converter = new DictionaryConverter(parameters))
			{
				FlurryAgent.CallStatic("logEvent", eventId, (AndroidJavaObject)converter);
			}
#endif
		}
		
		/// <summary>
		/// Log a timed event.
		/// </summary>
		/// <param name="eventId">The name/id of the event.</param>
		/// <param name="timed">True if the event should be timed, false otherwise.</param>
		public static void LogEvent(string eventId, bool timed)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			FlurryAgent.CallStatic("logEvent", eventId, timed);
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
		public static void LogEvent(string eventId, Dictionary<string, string> parameters, bool timed)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (DictionaryConverter converter = new DictionaryConverter(parameters))
			{
				FlurryAgent.CallStatic("logEvent", eventId, (AndroidJavaObject)converter, timed);
			}
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
			using (DictionaryConverter converter = new DictionaryConverter(parameters))
			{
				FlurryAgent.CallStatic("endTimedEvent", eventId, (AndroidJavaObject)converter);
			}
#endif
		}
		
		/// <summary>
		/// Log an error with the Flurry service.
		/// </summary>
		/// <param name="errorId">The name/ID of the error</param>
		/// <param name="message">The message of the error</param>
		/// <param name="exception">The exception of the error</param>
		public static void OnError(string errorId, string message, Exception exception)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			if (exception != null)
			{
				using (AndroidJavaObject throwable = new AndroidJavaObject("java.lang.Throwable", exception.Message))
				{
					FlurryAgent.CallStatic("onError", errorId, message, throwable);                
				}
			}
			else
			{
				FlurryAgent.CallStatic("onError", errorId, message, null);
			}
#endif
		}
		
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
		
#if UNITY_ANDROID && !UNITY_EDITOR
		/// <summary>
		/// Converts Dictionary<string, string> to java HashMap object
		/// </summary>
		private class DictionaryConverter : AndroidJavaClass
		{
			public DictionaryConverter(Dictionary<string, string> dictionary)
				: base("java.util.HashMap")
			{
				IntPtr put = AndroidJNIHelper.GetMethodID(GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
				
				foreach (KeyValuePair<string, string> entry in dictionary)
				{
					using (AndroidJavaObject key = new AndroidJavaObject("java.lang.String", entry.Key + ""))
					{
						using (AndroidJavaObject value = new AndroidJavaObject("java.lang.String", entry.Value + ""))
						{
							AndroidJNI.CallObjectMethod(GetRawObject(), put, AndroidJNIHelper.CreateJNIArgArray(new object[] { key, value }));
						}
					}
				}
			}
		}
#endif
	}
}