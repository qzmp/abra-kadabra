using UnityEngine;
using System.Collections.Generic;

public class DrawingScript : MonoBehaviour {

	private Dictionary<int, GameObject> trails = new Dictionary<int, GameObject>();
	
	void Update()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch(i);

			// -- Drag
			// ------------------------------------------------
			if (touch.phase == TouchPhase.Began) {
				// Store this new value
				if (trails.ContainsKey (i) == false) {
					Vector3 position = Camera.main.ScreenToWorldPoint (touch.position);
					position.z = 0; // Make sure the trail is visible

					GameObject trail = SpecialEffectsScript.MakeTrail (position);
								
					if (trail != null) {
						Debug.Log (trail);
						trails.Add (i, trail);
					}
				}
			} else if (touch.phase == TouchPhase.Moved) {
				// Move the trail
				if (trails.ContainsKey (i)) {
					GameObject trail = trails [i];
							
					Camera.main.ScreenToWorldPoint (touch.position);
					Vector3 position = Camera.main.ScreenToWorldPoint (touch.position);
					position.z = 0; // Make sure the trail is visible
							
					trail.transform.position = position;
				}
			} else if (touch.phase == TouchPhase.Ended) {
				// Clear known trails
				if (trails.ContainsKey (i)) {
					GameObject trail = trails [i];
							
					// Let the trail fade out
					Destroy (trail, trail.GetComponent<TrailRenderer> ().time);
					trails.Remove (i);
				}
			}
		}
	}
}
