using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Analytics;
using Debug = UnityEngine.Debug;

public class TestScript : MonoBehaviour 
{
	[Header("Flurry Settings")]
	[SerializeField] private string _iosApiKey = string.Empty;
	[SerializeField] private string _androidApiKey = string.Empty;
	
	/// <summary>
	/// Create Flurry singleton instance and log single event.
	/// </summary>
	private void Awake()
	{
		IAnalytics service = Flurry.Instance;

		AssertNotNull(service, "Unable to create Flurry instance!", this);
		Assert(!string.IsNullOrEmpty(_iosApiKey), "_iosApiKey is empty!", this);
		Assert(!string.IsNullOrEmpty(_androidApiKey), "_androidApiKey is empty!", this);

		service.SetLogLevel(LogLevel.All);
		service.StartSession(_iosApiKey, _androidApiKey);
	}

	/// <summary>
	/// Draw GUI controls and call analytics service.
	/// </summary>
	private void OnGUI()
	{
		int offset = 0;
		IAnalytics service = Flurry.Instance;

		if (Button("Log User Name", offset++))
		{
			service.LogUserID("Github User");
		}

		if (Button("Log User Age", offset++))
		{
			service.LogUserAge(24);
		}

		if (Button("Log User Gender", offset++))
		{
			service.LogUserGender(UserGender.Male);
		}
		
		if (Button("Log User Location", offset++))
		{
			//TODO: impl
		}

		if (Button("Log Event", offset++))
		{
			service.LogEvent("event", new Dictionary<string, string>
			{
#if UNITY_5
			    { "AppVersion", Application.version },
#endif
                { "UnityVersion", Application.unityVersion }
			});
		}

		if (Button("Begin Timed Event", offset++))
		{
			service.BeginLogEvent("timed-event");
		}

		if (Button("End Timed Event", offset++))
		{
			service.EndLogEvent("timed-event");
		}

		if (Button("Log Page View", offset++))
		{
			//TODO: impl
		}
		
		if (Button("Log Error", offset))
		{
			service.LogError("test-script-error", "Test Error", this);
		}
	}

	private bool Button(string label, int index)
	{
		float width = Screen.width * 0.7f;
		float height = Screen.height * 0.1f;

		Rect rect = new Rect(Screen.width * 0.5f - width * 0.5f, 
		                     height * index * 1.1f,
		                     width,
		                     height);

		return GUI.Button(rect, label);
	}

	#region [Assert Methods]
    [Conditional("UNITY_EDITOR")]
	private void Assert(bool condition, string message, Object context)
	{
		if (condition)
		{
			return;
		}

		Debug.LogError(message, context);
	}

    [Conditional("UNITY_EDITOR")]
	private void AssertNotNull(object target, string message, Object context)
	{
		Assert(target != null, message, context);
	}
	#endregion
}
