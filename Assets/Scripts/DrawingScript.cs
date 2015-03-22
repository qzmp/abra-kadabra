using UnityEngine;
using System.Collections.Generic;

public class DrawingScript : MonoBehaviour {

	private Dictionary<int, TrailHistory> trails = new Dictionary<int, TrailHistory>();
	
	void Update()
	{
		foreach(Touch touch in Input.touches) 
		{
			// -- Drag
			// ------------------------------------------------
			if (touch.phase == TouchPhase.Began)
			{
				// Store this new value
				if (!trails.ContainsKey(touch.fingerId))
				{
					Vector3 position = Camera.main.ScreenToWorldPoint (touch.position);
					position.z = 0; // Make sure the trail is visible

					GameObject trail = SpecialEffectsScript.MakeTrail (position);

								
					if (trail != null)
					{
						Debug.Log (trail);
						TrailHistory trailHistory = new TrailHistory(trail);
						trails.Add (touch.fingerId, trailHistory);
					}
				}
			} 
			else if (touch.phase == TouchPhase.Moved)
			{
				// Move the trail
				if (trails.ContainsKey (touch.fingerId))
				{
					TrailHistory trailHistory = trails[touch.fingerId];

					Vector3 position = Camera.main.ScreenToWorldPoint (touch.position);
					position.z = 0; // Make sure the trail is visible
							
					trailHistory.trail.transform.position = position;
					trailHistory.addPosition(position);

				}
			} 
			else if (touch.phase == TouchPhase.Ended)
			{
				// Clear known trails
				if (trails.ContainsKey (touch.fingerId))
				{
					TrailHistory trailHistory = trails[touch.fingerId];
							
					// Let the trail fade out
					Destroy (trailHistory.trail, trailHistory.trail.GetComponent<TrailRenderer> ().time);
					trails.Remove (touch.fingerId);
				}
			}
		}
	}

	private class TrailHistory
	{
		public GameObject trail{ get; set; }
		public List<Vector3> positions{ get; private set;}

		public TrailHistory(GameObject trail)
		{
			this.trail = trail;
			positions = new List<Vector3>();
		}

		public void addPosition(Vector3 pos)
		{
			positions.Add (pos);
		}
	}
}
