using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour 
{
	private void Awake()
	{
		Analytics.IAnalytics analytics = Analytics.Flurry.Instance;

		analytics.Start();
		analytics.LogEvent("game-started");
	}
}
