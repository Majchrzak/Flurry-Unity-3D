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

namespace Analytics
{
	/// <summary>
	/// Flurry iPhone SDK 5.0.0 implementation
	/// </summary>
	public static class FlurryIOS
	{
#if UNITY_IOS && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern void StartSessionImpl(string apiKey);
		[DllImport("__Internal")]
		private static extern void PauseBackgroundSessionImpl();
		[DllImport("__Internal")]
		private static extern void AddOriginImplA(string originName, string originVersion);
		[DllImport("__Internal")]
		private static extern void AddOriginImplB(string originName, string originVersion, string keys, string values);
		[DllImport("__Internal")]
		private static extern void SetAppVersionImpl(string version);
		[DllImport("__Internal")]
		private static extern string GetFlurryAgentVersionImpl();
		[DllImport("__Internal")]
		private static extern void SetShowErrorInLogEnabledImpl(bool value);
		[DllImport("__Internal")]
		private static extern void SetDebugLogEnabledImpl(bool value);
		[DllImport("__Internal")]
		private static extern void SetLogLevelImpl(int level);
		[DllImport("__Internal")]
		private static extern void SetSessionContinueSecondsImpl(int seconds);
		[DllImport("__Internal")]
		private static extern void SetSecureTransportEnabledImpl(bool value);
		[DllImport("__Internal")]
		private static extern void SetCrashReportingEnabledImpl(bool value);
		[DllImport("__Internal")]
		private static extern void LogEventImplA(string eventName);
		[DllImport("__Internal")]
		private static extern void LogEventImplB(string eventName, string keys, string values);
		[DllImport("__Internal")]
		private static extern void LogErrorImpl(string errorID, string message, string exceptionName, string exceptionReason);
		[DllImport("__Internal")]
		private static extern void LogEventImplC(string eventName, bool timed);
		[DllImport("__Internal")]
		private static extern void LogEventImplD(string eventName, string keys, string values, bool timed);
		[DllImport("__Internal")]
		private static extern void EndTimedEventImpl(string eventName, string keys, string values);
		[DllImport("__Internal")]
		private static extern void SetUserIdImpl(string userID);
		[DllImport("__Internal")]
		private static extern void SetAgeImpl(int age);
		[DllImport("__Internal")]
		private static extern void SetGenderImpl(string gender);
		[DllImport("__Internal")]
		private static extern void SetLatitudeImpl(double latitude, double longitude, float horizontalAccuracy, float verticalAccuracy);
		[DllImport("__Internal")]
		private static extern void SetSessionReportsOnCloseEnabledImpl(bool sendSessionReportsOnClose);
		[DllImport("__Internal")]
		private static extern void SetSessionReportsOnPauseEnabledImpl(bool setSessionReportsOnPauseEnabled);
		[DllImport("__Internal")]
		private static extern void SetBackgroundSessionEnabledImpl(bool setBackgroundSessionEnabled);
		[DllImport("__Internal")]
		private static extern void SetEventLoggingEnabledImpl(bool value);
#endif

		/// <summary>
		/// Start a Flurry session for the project denoted by apiKey.
		/// </summary>
		/// <param name="apiKey">The API key for this project.</param>
		public static void StartSession(string apiKey)
		{
#if UNITY_IOS && !UNITY_EDITOR
			StartSessionImpl(apiKey);
#endif
		}
		
		/// <summary>
		/// Pauses a Flurry session left running in background.
		/// </summary>
		public static void PauseBackgroundSession()
		{
#if UNITY_IOS && !UNITY_EDITOR
			PauseBackgroundSessionImpl();
#endif
		}
		
		/// <summary>
		/// Adds an SDK origin specified by originName and originVersion.
		/// </summary>
		/// <param name="originName">Name of the origin.</param>
		/// <param name="originVersion">Version string of the origin wrapper</param>
		public static void AddOrigin(string originName, string originVersion)
		{
#if UNITY_IOS && !UNITY_EDITOR
			AddOriginImplA(originName, originVersion);
#endif
		}
		
		/// <summary>
		/// Adds a custom parameterized origin specified by originName with originVersion 
		/// and parameters.
		/// </summary>
		/// <param name="originName">Name of the origin.</param>
		/// <param name="originVersion">Version string of the origin wrapper</param>
		/// <param name="parameters">
		/// An immutable copy of map containing Name-Value pairs of parameters.
		/// </param>
		public static void AddOrigin(string originName, string originVersion, Dictionary<string, string> parameters)
		{
#if UNITY_IOS && !UNITY_EDITOR
			string keys = "";
			string values = "";
			
			AddOriginImplB(originName, originVersion, keys, values);
#endif
		}
		
		/// <summary>
		/// Explicitly specifies the App Version that Flurry will use to group Analytics data.
		/// </summary>
		/// <param name="version">The custom version name.</param>
		public static void SetAppVersion(string version)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetAppVersionImpl(version);
#endif
		}
		
		/// <summary>
		/// Retrieves the Flurry Agent Build Version.
		/// </summary>
		/// <returns>The agent version of the Flurry SDK.</returns>
		public static string GetFlurryAgentVersion()
		{
#if UNITY_IOS && !UNITY_EDITOR
			return GetFlurryAgentVersionImpl();
#else
			return string.Empty;
#endif
		}
		
		/// <summary>
		/// Displays an exception in the debug log if thrown during a Session.
		/// </summary>
		/// <param name="value">
		/// true to show errors in debug logs, false to omit errors in debug logs.
		/// </param>
		public static void SetShowErrorInLogEnabled(bool value)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetShowErrorInLogEnabledImpl(value);
#endif
		}
		
		/// <summary>
		/// Generates debug logs to console.
		/// </summary>
		/// <param name="value">true to show debug logs, false to omit debug logs.</param>
		public static void SetDebugLogEnabled(bool value)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetDebugLogEnabledImpl(value);
#endif
		}
		
		/// <summary>
		/// Generates debug logs to console.
		/// </summary>
		/// <param name="level">Log level</param>
		public static void SetLogLevel(int level)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetLogLevelImpl((int)level);
#endif
		}
		
		/// <summary>
		/// Set the timeout for expiring a Flurry session.
		/// </summary>
		/// <param name="seconds">The time in seconds to set the session timeout to.</param>
		public static void SetSessionContinueSeconds(int seconds)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetSessionContinueSecondsImpl(seconds);
#endif
		}
		
		/// <summary>
		/// Send data over a secure transport.
		/// </summary>
		/// <param name="value">true to send data over secure connection.</param>
		public static void SetSecureTransportEnabled(bool value)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetSecureTransportEnabledImpl(value);
#endif
		}
		
		/// <summary>
		/// Enable automatic collection of crash reports.
		/// </summary>
		/// <param name="value">true to enable collection of crash reports.</param>
		public static void SetCrashReportingEnabled(bool value)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetCrashReportingEnabledImpl(value);
#endif
		}
		
		/// <summary>
		/// Records a custom event specified by eventName.
		/// </summary>
		/// <param name="eventName">
		/// Name of the event. For maximum effectiveness, we recommend using a naming scheme 
		/// that can be easily understood by non-technical people in your business domain.
		/// </param>
		public static void LogEvent(string eventName)
		{
#if UNITY_IOS && !UNITY_EDITOR
			LogEventImplA(eventName);
#endif
		}
		
		/// <summary>
		/// Records a custom parameterized event specified by eventName with parameters.
		/// </summary>
		/// <param name="eventName">
		/// Name of the event. For maximum effectiveness, we recommend using a naming scheme 
		/// that can be easily understood by non-technical people in your business domain.
		/// </param>
		/// <param name="parameters">
		/// An immutable copy of map containing Name-Value pairs of parameters.
		/// </param>
		public static void LogEvent(string eventName, Dictionary<string, string> parameters)
		{
#if UNITY_IOS && !UNITY_EDITOR
			string keys, values;
			ToKeyValue(parameters, out keys, out values);
			LogEventImplB(eventName, keys, values);
#endif
		}
		
		/// <summary>
		/// Records an app exception. Commonly used to catch unhandled exceptions.
		/// </summary>
		/// <param name="errorID">Name of the error.</param>
		/// <param name="message">The message to associate with the error.</param>
		/// <param name="exception">The exception object to report.</param>
		public static void LogError(string errorID, string message, Exception exception)
		{
#if UNITY_IOS && !UNITY_EDITOR
			if (exception != null)
				LogErrorImpl(errorID, message, exception.GetType().Name, exception.Message);
			else
				LogErrorImpl(errorID, message, null, null);
#endif
		}
		
		/// <summary>
		/// Records a timed event specified by eventName.
		/// </summary>
		/// <param name="eventName">
		/// Name of the event. For maximum effectiveness, we recommend using a naming scheme 
		/// that can be easily understood by non-technical people in your business domain.
		/// </param>
		/// <param name="timed">Specifies the event will be timed.</param>
		public static void LogEvent(string eventName, bool timed)
		{
#if UNITY_IOS && !UNITY_EDITOR
			LogEventImplC(eventName, timed);
#endif
		}
		
		/// <summary>
		/// Records a custom parameterized timed event specified by eventName with parameters.
		/// </summary>
		/// <param name="eventName">
		/// Name of the event. For maximum effectiveness, we recommend using a naming scheme 
		/// that can be easily understood by non-technical people in your business domain.
		/// </param>
		/// <param name="parameters">
		/// An immutable copy of map containing Name-Value pairs of parameters.
		/// </param>
		/// <param name="timed">Specifies the event will be timed.</param>
		public static void LogEvent(string eventName, Dictionary<string, string> parameters, bool timed)
		{
#if UNITY_IOS && !UNITY_EDITOR
			string keys, values;
			ToKeyValue(parameters, out keys, out values);
			LogEventImplD(eventName, keys, values, timed);
#endif
		}
		
		/// <summary>
		/// Ends a timed event specified by eventName and optionally updates parameters with 
		/// parameters.
		/// </summary>
		/// <param name="eventName">
		/// Name of the event. For maximum effectiveness, we recommend using a naming scheme 
		/// that can be easily understood by non-technical people in your business domain.
		/// </param>
		/// <param name="parameters">
		/// An immutable copy of map containing Name-Value pairs of parameters.
		/// </param>
		public static void EndTimedEvent(string eventName, Dictionary<string, string> parameters)
		{
#if UNITY_IOS && !UNITY_EDITOR
			string keys, values;
			ToKeyValue(parameters, out keys, out values);
			EndTimedEventImpl(eventName, keys, values);
#endif
		}
		
		/// <summary>
		/// Assign a unique id for a user in your app.
		/// </summary>
		/// <param name="userID">The app id for a user.</param>
		public static void SetUserId(string userID)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetUserIdImpl(userID);
#endif
		}
		
		/// <summary>
		/// Set your user's age in years.
		/// </summary>
		/// <param name="age">Reported age of user.</param>
		public static void SetAge(int age)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetAgeImpl(age);
#endif
		}
		
		/// <summary>
		/// Set your user's gender.
		/// </summary>
		/// <param name="gender">
		/// Reported gender of user. Allowable values are 'm' or 'c' 'f'
		/// </param>
		public static void SetGender(string gender)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetGenderImpl(gender);
#endif
		}
		
		/// <summary>
		/// Set the location of the session.
		/// </summary>
		/// <param name="latitude">The latitude.</param>
		/// <param name="longitude">The longitude.</param>
		/// <param name="horizontalAccuracy">
		/// The radius of uncertainty for the location in meters.
		/// </param>
		/// <param name="verticalAccuracy">
		/// The accuracy of the altitude value in meters.
		/// </param>
		public static void SetLatitude(double latitude, double longitude, float horizontalAccuracy, float verticalAccuracy)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetLatitudeImpl(latitude, longitude, horizontalAccuracy, verticalAccuracy);
#endif
		}
		
		/// <summary>
		/// Set session to report when app closes.
		/// </summary>
		/// <param name="sendSessionReportsOnClose">
		/// true to send on close, false to omit reporting on close.
		/// </param>
		public static void SetSessionReportsOnCloseEnabled(bool sendSessionReportsOnClose)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetSessionReportsOnCloseEnabledImpl(sendSessionReportsOnClose);
#endif
		}
		
		/// <summary>
		/// Set session to report when app is sent to the background.
		/// </summary>
		/// <param name="setSessionReportsOnPauseEnabled">
		/// true to send on pause, false to omit reporting on pause.
		/// </param>
		public static void SetSessionReportsOnPauseEnabled(bool setSessionReportsOnPauseEnabled)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetSessionReportsOnPauseEnabledImpl(setSessionReportsOnPauseEnabled);
#endif
		}
		
		/// <summary>
		/// Set session to support background execution.
		/// </summary>
		/// <param name="setBackgroundSessionEnabled">
		/// true to enbale background support and continue log events and errors for running session.
		/// </param>
		public static void SetBackgroundSessionEnabled(bool setBackgroundSessionEnabled)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetBackgroundSessionEnabledImpl(setBackgroundSessionEnabled);
#endif
		}
		
		/// <summary>
		/// Enable custom event logging.
		/// </summary>
		/// <param name="value">true to enable event logging, false to stop custom logging.</param>
		public static void SetEventLoggingEnabled(bool value)
		{
#if UNITY_IOS && !UNITY_EDITOR
			SetEventLoggingEnabledImpl(value);
#endif
		}
		
		/// <summary>
		/// Converts string dictionary to separated key and value
		/// </summary>
		/// <param name="dictionary"></param>
		/// <param name="keys"></param>
		/// <param name="values"></param>
		private static void ToKeyValue(Dictionary<string, string> dictionary, out string keys, out string values)
		{
			keys = string.Empty;
			values = string.Empty;
			
			foreach (KeyValuePair<string, string> pair in dictionary)
			{
				keys = string.Format("{0}\n{1}", keys, pair.Key);
				values = string.Format("{0}\n{1}", values, pair.Value);
			}
		}
	}
}