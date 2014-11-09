using UnityEngine;
using System.Collections;
using Analytics;

public class Example : MonoBehaviour 
{
	/// <summary>
	/// Create Flurry singleton instance and log single event.
	/// </summary>
	private void Awake()
	{
		IAnalytics analytics = Flurry.Instance;

		analytics.Start();
		analytics.LogEvent("game-started");
	}
}
