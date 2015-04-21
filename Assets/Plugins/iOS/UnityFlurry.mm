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

#import "Flurry.h"

/*
* Helper
*/
NSString* strToNSStr(const char* str)
{
	if (!str)
		return [NSString stringWithUTF8String: ""];

	return [NSString stringWithUTF8String: str];
}

/*
* Helper
*/
char* strDup(const char* str)
{
	if (!str)
		return NULL;
	
	return strcpy((char*)malloc(strlen(str) + 1), str);
}

/*
* Helper
*/
NSMutableDictionary* keyValueToDict(const char* keys, const char* values)
{
    if (!keys || !values)
    {
        return nil;
    }
    
	NSMutableDictionary* dict = [[NSMutableDictionary alloc] init];

	NSArray* keysArray = [strToNSStr(keys) componentsSeparatedByString : @"\n"];
	NSArray* valuesArray = [strToNSStr(values) componentsSeparatedByString : @"\n"];

	for (int i = 0; i < [keysArray count]; i++)
	{
		[dict setObject:[valuesArray objectAtIndex: i] forKey:[keysArray objectAtIndex: i]];
	}

	return dict;
}

extern "C"
{
	/*
	*
	*/
	void StartSessionImpl(const char* apiKey)
	{
		[Flurry startSession: strToNSStr(apiKey)];
	}

	/*
	*
	*/
	bool ActiveSessionExistsImpl()
	{
		return [Flurry activeSessionExists];
	}

	/*
	*
	*/
	void PauseBackgroundSessionImpl()
	{
		[Flurry pauseBackgroundSession];
	}

	/*
	*
	*/
	void AddOriginImplA(const char* originName, const char* originVersion)
	{
        [Flurry addOrigin:strToNSStr(originName) withVersion:strToNSStr(originVersion)];
	}

	/*
	*
	*/
	void AddOriginImplB(const char* originName, const char* originVersion, const char* keys, const char* values)
	{
        [Flurry addOrigin:strToNSStr(originName) withVersion:strToNSStr(originVersion) withParameters:keyValueToDict(keys, values)];
	}

	/*
	*
	*/
	void SetAppVersionImpl(const char* version)
	{
        [Flurry setAppVersion:strToNSStr(version)];
	}

	/*
	*
	*/
	const char* GetFlurryAgentVersionImpl()
	{
        return strDup([[Flurry getFlurryAgentVersion] UTF8String]);
	}

	/*
	*
	*/
	void SetShowErrorInLogEnabledImpl(bool value)
	{
        [Flurry setShowErrorInLogEnabled:value];
	}

	/*
	*
	*/
	void SetDebugLogEnabledImpl(bool value)
	{
        [Flurry setDebugLogEnabled:value];
	}

	/*
	*
	*/
	void SetLogLevelImpl(int level)
	{
        [Flurry setLogLevel:(FlurryLogLevel)level];
	}

	/*
	*
	*/
	void SetSessionContinueSecondsImpl(int seconds)
	{
        [Flurry setSessionContinueSeconds:seconds];
	}

	/*
	*
	*/
	void SetCrashReportingEnabledImpl(bool value)
	{
        [Flurry setCrashReportingEnabled:value];
	}

	/*
	*
	*/
	int LogEventImplA(const char* eventName)
	{
        return [Flurry logEvent:strToNSStr(eventName)];
	}

	/*
	*
	*/
	int LogEventImplB(const char* eventName, const char* keys, const char* values)
	{
		return [Flurry logEvent:strToNSStr(eventName) withParameters:keyValueToDict(keys, values)];
	}

	/*
	*
	*/
	void LogErrorImpl(const char* errorID, const char* message, const char* exceptionName, const char* exceptionReason)
	{
        [Flurry logError:strToNSStr(errorID) message:strToNSStr(message) exception:[[NSException alloc] initWithName:strToNSStr(exceptionName) reason:strToNSStr(exceptionReason) userInfo:nil]];
	}

	/*
	*
	*/
	int LogEventImplC(const char* eventName, bool timed)
	{
        return [Flurry logEvent:strToNSStr(eventName) timed:timed];
	}

	/*
	*
	*/
	int LogEventImplD(const char* eventName, const char* keys, const char* values, bool timed)
	{
        return [Flurry logEvent:strToNSStr(eventName) withParameters:keyValueToDict(keys, values) timed:timed];
	}

	/*
	*
	*/
	void EndTimedEventImpl(const char* eventName, const char* keys, const char* values)
	{
        [Flurry endTimedEvent:strToNSStr(eventName) withParameters:keyValueToDict(keys, values)];
	}

	/*
	*
	*/
	void LogPageViewImpl()
	{
		[Flurry logPageView];
	}

	/*
	*
	*/
	void SetUserIdImpl(const char* userID)
	{
        [Flurry setUserID:strToNSStr(userID)];
	}

	/*
	*
	*/
	void SetAgeImpl(int age)
	{
        [Flurry setAge:age];
    }

	/*
	*
	*/
	void SetGenderImpl(const char* gender)
	{
        [Flurry setGender:strToNSStr(gender)];
	}

	/*
	*
	*/
	void SetLatitudeImpl(double latitude, double longitude, float horizontalAccuracy, float verticalAccuracy)
	{
        [Flurry setLatitude:latitude longitude:longitude horizontalAccuracy:horizontalAccuracy verticalAccuracy:verticalAccuracy];
	}

	/*
	*
	*/
	void SetSessionReportsOnCloseEnabledImpl(bool sendSessionReportsOnClose)
	{
        [Flurry setSessionReportsOnCloseEnabled:sendSessionReportsOnClose];
	}

	/*
	*
	*/
	void SetSessionReportsOnPauseEnabledImpl(bool setSessionReportsOnPauseEnabled)
	{
        [Flurry setSessionReportsOnPauseEnabled:setSessionReportsOnPauseEnabled];
	}

	/*
	*
	*/
	void SetBackgroundSessionEnabledImpl(bool setBackgroundSessionEnabled)
	{
        [Flurry setBackgroundSessionEnabled:setBackgroundSessionEnabled];
	}

	/*
	*
	*/
	void SetEventLoggingEnabledImpl(bool value)
	{
        [Flurry setEventLoggingEnabled:value];
	}
}
