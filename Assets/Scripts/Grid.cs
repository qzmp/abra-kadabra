using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	
	public float width = 1.28f; //grid width
	public float height = 1.28f;//grid height
	//128px is cube size (16*8)

	public Color color = new Color((105.0f/255.0f),(163.0f/255.0f),(163.0f/255.0f)); //'marine' color
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = color;
	
		int k = 50;//number of lines
		for (int i = 0; i<=k; i++) {
			Gizmos.DrawLine (new Vector3 (-k*height, i*height, 0), new Vector3 (k*height, i*height, 0));
			Gizmos.DrawLine (new Vector3 (-k*height, -i*height, 0), new Vector3 (k*height, -i*height, 0));

			Gizmos.DrawLine (new Vector3 (i*width,-k*width, 0), new Vector3 (i*width, k*width, 0));
			Gizmos.DrawLine (new Vector3 (-i*width,-k*width, 0), new Vector3 (-i*width, k*width, 0));
		}
	}
}