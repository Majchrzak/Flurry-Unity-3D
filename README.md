# Flurry plugin for Unity 3D
---
It's free Flurry Analytics iOS and Android implementation for Unity 3D.

Fully supported:

 * Flurry iOS 6.2.0 - [FlurryIOS.cs](https://github.com/Majchrzak/Flurry-Unity-3D/blob/master/Assets/Analytics/FlurryIOS.cs)
 * Flurry Android 5.4.0 - [FlurryAndroid.cs](https://github.com/Majchrzak/Flurry-Unity-3D/blob/master/Assets/Analytics/FlurryAndroid.cs)

You can also find cross-platform analytics implementation in [Flurry.cs](https://github.com/Majchrzak/Flurry-Unity-3D/blob/master/Assets/Analytics/Flurry.cs) file.

### Usage
```cpp
private void Start()
{
    // For Flurry Android only:
    FlurryAndroid.SetLogEnabled(true);
    
    // For Flurry iOS only:
    FlurryIOS.SetDebugLogEnabled(true);

    // Cross-platform
    Flurry.Instance.StartSession("ios_api_key", "android_api_key");
    Flurry.Instance.LogUserID("Github User");
    Flurry.Instance.LogEvent("event", new Dictionary<string, string> {{ "platform", "Github" }});
}
```

See TestScene.unity for more details.

### Installation

####Android:
Due to Google restrictions, under Plugins/Android you can find Google Play Games library that is required for proper working of Flurry for Android. Feel free to replace that library with your existing Google Play Games implementation.

 * generate unique app id in Google Developer Console under Game Services tab.
 * replace 'APP_ID' field in AndroidMainfest.xml to your existing app id value:
 
 ```
<meta-data android:name="com.google.android.gms.games.APP_ID" android:value="\ APP_ID" />
 ```
 * set proper package name in Unity project settings.
 
####iOS:
 * Add Security.framework

### Version
0.9.9

### License
MIT
