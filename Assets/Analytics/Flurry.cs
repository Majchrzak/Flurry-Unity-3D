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
    /// Flurry multiplatform implementation
    /// </summary>
    public sealed class Flurry : MonoSingleton<Flurry>, IAnalytics
    {
		#region [Initialization]
        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
#if UNITY_5
			Application.logMessageReceived += ErrorHandler;
#else
            Application.RegisterLogCallback(ErrorHandler);
#endif
        }
		#endregion

		#region [Internal Event Handlers]
        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroy()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            FlurryAndroid.Dispose();
#endif
			base.OnDestroy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logString"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        private void ErrorHandler(string condition, string stackTrace, LogType type)
        {
			if (type != LogType.Error) 
			{
				return;
			}

			LogError("Uncaught Unity Exception", condition, this);
        }
		#endregion

		#region [Session Calls]
        /// <summary>
        /// Start or continue a Flurry session for the project denoted by apiKey.
        /// </summary>
        /// <param name="apiKey">The API key for this project.</param>
        public void StartSession(string apiKeyIOS, string apiKeyAndroid)
        {
#if UNITY_EDITOR

#elif UNITY_IOS
		    FlurryIOS.StartSession(apiKeyIOS);
#elif UNITY_ANDROID
            FlurryAndroid.Init(apiKeyAndroid);
		    FlurryAndroid.OnStartSession();
#endif
        }
		#endregion

		#region [Pre-Session Calls]
        /// <summary>
        /// Explicitly specifies the App Version that Flurry will use to group Analytics data.
        /// </summary>
        /// <param name="version">The custom version name.</param>
        public void LogAppVersion(string version)
        {
#if UNITY_EDITOR

#elif UNITY_IOS
            FlurryIOS.SetAppVersion(version);
#elif UNITY_ANDROID
            FlurryAndroid.SetVersionName(version);
#endif
        }

		/// <summary>
		/// Generates debug logs to console.
		/// </summary>
		/// <param name="level">Log level.</param>
		public void SetLogLevel(LogLevel level)
		{
#if UNITY_EDITOR

#elif UNITY_IOS
			FlurryIOS.SetLogLevel(level);
#elif UNITY_ANDROID
			FlurryAndroid.SetLogLevel(level);
#endif
		}
		#endregion

		#region [Event and Error Logging]
		/// <summary>
		/// Records a custom event specified by eventName.
		/// </summary>
		/// <param name="eventName">
		/// Name of the event. For maximum effectiveness, we recommend using a naming scheme 
		/// that can be easily understood by non-technical people in your business domain.
		/// </param>
		public EventRecordStatus LogEvent(string eventName)
        {
#if UNITY_EDITOR
            return EventRecordStatus.Failed;
#elif UNITY_IOS
        	return FlurryIOS.LogEvent(eventName);
#elif UNITY_ANDROID
        	return FlurryAndroid.LogEvent(eventName);
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
        public EventRecordStatus LogEvent(string eventName, Dictionary<string, string> parameters)
        {
#if UNITY_EDITOR
            return EventRecordStatus.Failed;
#elif UNITY_IOS
        	return FlurryIOS.LogEvent(eventName, parameters);
#elif UNITY_ANDROID
        	return FlurryAndroid.LogEvent(eventName, parameters);
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
        public EventRecordStatus LogEvent(string eventName, bool timed)
        {
#if UNITY_EDITOR
            return EventRecordStatus.Failed;
#elif UNITY_IOS
        	return FlurryIOS.LogEvent(eventName, timed);
#elif UNITY_ANDROID
        	return FlurryAndroid.LogEvent(eventName, timed);
#endif
        }

		/// <summary>
		/// Records a timed event specified by eventName.
		/// </summary>
		/// <param name="eventName">
		/// Name of the event. For maximum effectiveness, we recommend using a naming scheme 
		/// that can be easily understood by non-technical people in your business domain.
		/// </param>
		public EventRecordStatus BeginLogEvent(string eventName)
        {
#if UNITY_EDITOR
            return EventRecordStatus.Failed;
#elif UNITY_IOS
			return FlurryIOS.LogEvent(eventName, true);
#elif UNITY_ANDROID
			return FlurryAndroid.LogEvent(eventName, true);
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
		public EventRecordStatus BeginLogEvent(string eventName, Dictionary<string, string> parameters)
		{
#if UNITY_EDITOR
			return EventRecordStatus.Failed;
#elif UNITY_IOS
			return FlurryIOS.LogEvent(eventName, parameters, true);
#elif UNITY_ANDROID
			return FlurryAndroid.LogEvent(eventName, parameters, true);
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
		public void EndLogEvent(string eventName)
        {
#if UNITY_EDITOR

#elif UNITY_IOS
            FlurryIOS.EndTimedEvent(eventName, new Dictionary<string, string>());
#elif UNITY_ANDROID
            FlurryAndroid.EndTimedEvent(eventName);
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
		public void EndLogEvent(string eventName, Dictionary<string, string> parameters)
		{
#if UNITY_EDITOR
			
#elif UNITY_IOS
			FlurryIOS.EndTimedEvent(eventName, parameters);
#elif UNITY_ANDROID
			FlurryAndroid.EndTimedEvent(eventName, parameters);
#endif
		}

		/// <summary>
		/// Records an app exception. Commonly used to catch unhandled exceptions.
		/// </summary>
		/// <param name="errorID">Name of the error.</param>
		/// <param name="message">The message to associate with the error.</param>
		/// <param name="exception">The exception object to report.</param>
		public void LogError(string errorID, string message, object target)
		{
#if UNITY_EDITOR
			
#elif UNITY_IOS
			FlurryIOS.LogError(errorID, message, null);
#elif UNITY_ANDROID
			FlurryAndroid.OnError(errorID, message, target.GetType().Name);
#endif
        }
		#endregion

		#region [User Info]
        /// <summary>
        /// Assign a unique id for a user in your app.
        /// </summary>
        /// <param name="userID">The app id for a user.</param>
        public void LogUserID(string userID)
        {
#if UNITY_EDITOR
            
#elif UNITY_IOS
            FlurryIOS.SetUserId(userID);
#elif UNITY_ANDROID
            FlurryAndroid.SetUserId(userID);
#endif
        }

        /// <summary>
        /// Set your user's age in years.
        /// </summary>
        /// <param name="age">Reported age of user.</param>
        public void LogUserAge(int age)
        {
#if UNITY_EDITOR
            
#elif UNITY_IOS
            FlurryIOS.SetAge(age);
#elif UNITY_ANDROID
            FlurryAndroid.SetAge(age);
#endif
        }

        /// <summary>
        /// Set your user's gender.
        /// </summary>
        /// <param name="gender">
        /// Reported gender of user. Allowable values are 'm' or 'c' 'f'
        /// </param>
		public void LogUserGender(UserGender gender)
        {
#if UNITY_EDITOR

#elif UNITY_IOS
            FlurryIOS.SetGender(gender == UserGender.Male ? "m" : gender == UserGender.Female ? "f" : "c");
#elif UNITY_ANDROID
            FlurryAndroid.SetGender((byte)(gender == UserGender.Male ? 1 : gender == UserGender.Female ? 0 : -1));
#endif
        }
		#endregion
	}
}