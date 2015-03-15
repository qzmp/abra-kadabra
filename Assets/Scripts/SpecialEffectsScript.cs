using UnityEngine;
using System.Collections;

public class SpecialEffectsScript : MonoBehaviour {

	private static SpecialEffectsScript instance;

	public GameObject trailPrefab;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		if (trailPrefab == null)
			Debug.LogError("Missing Trail Prefab!");
	}
	
	// Update is called once per frame
	public static GameObject MakeTrail(Vector3 position)
	{
		if (instance == null)
		{
			Debug.LogError("There is no SpecialEffectsScript in the scene!");
			return null;
		}
		
		GameObject trail = Instantiate(instance.trailPrefab) as GameObject;
		trail.transform.position = position;
		
		return trail;
	}
}
