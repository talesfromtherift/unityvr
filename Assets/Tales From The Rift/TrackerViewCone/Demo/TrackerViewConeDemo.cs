using UnityEngine;
using System.Collections;

namespace TalesFromTheRift {

/*
 * Draw the IR Camera view cone
 * By Peter Koch
 * Keys: R = Reset Orientation
 *       I = Hide/Show view cone
 */

	[RequireComponent(typeof(TrackerViewCone))]
	public class TrackerViewConeDemo : MonoBehaviour {

		public KeyCode ResetTrackerKeyCode = KeyCode.R;
		public KeyCode ShowHideViewConeKeyCode = KeyCode.I;

		void Update ()  {
			
			if (Input.GetKeyDown(KeyCode.R))
			{
				UnityEngine.VR.InputTracking.Recenter();
			}
			if (Input.GetKeyDown(KeyCode.I))
			{
				TrackerViewCone viewCone = GetComponent<TrackerViewCone>(); 
				viewCone.showViewCone = !viewCone.showViewCone;
			}
		}
	}

}