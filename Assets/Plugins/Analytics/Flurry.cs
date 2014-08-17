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
#if BUILD_TARGET_ANDROID
        private static readonly string FLURRY_UNIQUE_KEY = "KSJKSG5J5WNX86N7284C";
#elif BUILD_TARGET_IPHONE
        private static readonly string FLURRY_UNIQUE_KEY = "";
#elif BUILD_TARGET_IPAD
        private static readonly string FLURRY_UNIQUE_KEY = "";
#endif
		
        /// <summary>
        /// Log level
        /// </summary>
        public enum FlurryLogLevel
        {
            /// <summary>
            /// No output
            /// </summary>
            FlurryLogLevelNone = 0,
            /// <summary>
            /// Default, outputs only critical log events
            /// </summary>
            FlurryLogLevelCriticalOnly,
            /// <summary>
            /// Debug level, outputs critical and main log events
            /// </summary>
            FlurryLogLevelDebug,
            /// <summary>
            /// Highest level, outputs all log events
            /// </summary>
            FlurryLogLevelAll
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FlurryGender
        {
            FlurryGenderNone = 0,
            FlurryGenderMale,
            FlurryGenderFemale
        }

        /// <summary>
        /// 
        /// </summary>
        private bool m_IsSessionStarted;

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            Application.RegisterLogCallback(ErrorHandler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pauseStatus"></param>
        private void OnApplicationPause(bool pauseStatus)
        {
            if (!m_IsSessionStarted)
                return;
			
#if UNITY_EDITOR

#elif UNITY_IOS
            if (pauseStatus)
                iPhonePlatform.PauseBackgroundSession();
#elif UNITY_ANDROID
            if (pauseStatus)
                AndroidPlatform.OnEndSession();
            else
                StartSession();
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroy()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidPlatform.Dispose();
#endif

			base.OnDestroy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logString"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        private void ErrorHandler(string logString, string stackTrace, LogType type)
        {
            if (type != LogType.Error)
                return;

			LogError("Uncaught Unity Exception", logString);
        }

        /// <summary>
        /// Start or continue a Flurry session for the project denoted by apiKey.
        /// </summary>
        /// <param name="apiKey">The API key for this project.</param>
        public void Start()
        {
#if UNITY_EDITOR

#elif UNITY_IOS
		    iPhonePlatform.StartSession(FLURRY_UNIQUE_KEY);
#elif UNITY_ANDROID
		    AndroidPlatform.OnStartSession(FLURRY_UNIQUE_KEY);
#endif

            m_IsSessionStarted = true;
        }

		/// <summary>
		/// Records a custom event specified by eventName.
		/// </summary>
		/// <param name="eventName">
		/// Name of the event. For maximum effectiveness, we recommend using a naming scheme 
		/// that can be easily understood by non-technical people in your business domain.
		/// </param>
		public void LogEvent(string eventName)
        {
#if UNITY_EDITOR

#elif UNITY_IOS
        iPhonePlatform.LogEvent(eventName);
#elif UNITY_ANDROID
        AndroidPlatform.LogEvent(eventName);
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
		public void LogEvent(string eventName, IDictionary<string, string> parameters)
        {
#if UNITY_EDITOR

#elif UNITY_IOS
        iPhonePlatform.LogEvent(eventName, parameters);
#elif UNITY_ANDOIRD
        AndroidPlatform.LogEvent(eventName, parameters);
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
        public void LogEvent(string eventName, bool timed)
        {
#if UNITY_EDITOR

#elif UNITY_IOS
        iPhonePlatform.LogEvent(eventName, timed);
#elif UNITY_ANDROID
        AndroidPlatform.LogEvent(eventName, timed);
#endif
        }

		/// <summary>
		/// Records a timed event specified by eventName.
		/// </summary>
		/// <param name="eventName">
		/// Name of the event. For maximum effectiveness, we recommend using a naming scheme 
		/// that can be easily understood by non-technical people in your business domain.
		/// </param>
		public void BeginLogEvent(string eventName)
        {
#if UNITY_EDITOR

#elif UNITY_IOS
		iPhonePlatform.LogEvent(eventName, parameters, true);
#elif UNITY_ANDROID
		AndroidPlatform.LogEvent(eventName, parameters, true);
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
		public void BeginLogEvent(string eventName, IDictionary<string, string> parameters)
		{
#if UNITY_EDITOR
			
#elif UNITY_IOS
			iPhonePlatform.LogEvent(eventName, parameters, true);
#elif UNITY_ANDROID
			AndroidPlatform.LogEvent(eventName, parameters, true);
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
            iPhonePlatform.EndTimedEvent(eventName, null);
#elif UNITY_ANDROID
            AndroidPlatform.EndTimedEvent(eventName, null);
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
		public void EndLogEvent(string eventName, IDictionary<string, string> parameters)
		{
#if UNITY_EDITOR
			
#elif UNITY_IOS
			iPhonePlatform.EndTimedEvent(eventName, parameters);
#elif UNITY_ANDROID
			AndroidPlatform.EndTimedEvent(eventName, parameters);
#endif
		}

		/// <summary>
		/// Records an app exception. Commonly used to catch unhandled exceptions.
		/// </summary>
		/// <param name="errorID">Name of the error.</param>
		/// <param name="message">The message to associate with the error.</param>
		/// <param name="exception">The exception object to report.</param>
		public void LogError(string errorID, string message)
		{
#if UNITY_EDITOR
			
#elif UNITY_IOS
			iPhonePlatform.LogError(errorName, message, null);
#elif UNITY_ANDROID
			AndroidPlatform.OnError(errorName, message, null);
#endif
		}
	}
}