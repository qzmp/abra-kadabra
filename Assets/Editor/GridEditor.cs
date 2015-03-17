/**
 * DO NOT change the file path. Assets\Editor\GridEditor.cs
 * 
 * 1) Select an object (prefab) on the scene. 
 * 2) press 'a' - create new object
 * 3) press 'd' - delete
 * 4) ctrl+z - undo
 * 
 * Hint: if it doesn't work - hide and show grid (checkbox)
 * 
 * tut:
 * http://code.tutsplus.com/tutorials/how-to-add-your-own-tools-to-unitys-editor--active-10047
 **/

using UnityEngine;
using UnityEditor; 
using System.Collections;


[CustomEditor (typeof(Grid))] 
public class GridEditor : Editor {

	Grid grid;

	public void OnEnable()
	{
		grid = (Grid)target;
		SceneView.onSceneGUIDelegate = GridUpdate;
	}

	void GridUpdate(SceneView sceneview)
	{
		Event e = Event.current;
		//get mouse position
		Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
		Vector3 mousePos = r.origin;
		
		if (e.isKey && e.character.Equals('a'))//if 'a' pressed
		{
			GameObject obj;

			GameObject prefab = PrefabUtility.GetPrefabParent(Selection.activeGameObject) as GameObject;
			if (prefab)
			{
				Vector3 aligned;
				Undo.IncrementCurrentGroup();

				obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

				if(obj.GetComponent<BoxCollider2D>().size.x/grid.width % 2 == 0){
					aligned = new Vector3(Mathf.Floor(mousePos.x/grid.width)*grid.width ,
				                          Mathf.Floor(mousePos.y/grid.height)*grid.height + grid.height/2.0f, 0.0f);
				}else{
					aligned = new Vector3(Mathf.Floor(mousePos.x/grid.width)*grid.width + grid.width/2.0f,
					                      Mathf.Floor(mousePos.y/grid.height)*grid.height + grid.width/2.0f, 0.0f);
				}
				//align to grid
				obj.transform.position = aligned;

				Selection.activeGameObject = obj;

				Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
			}

		}
		else if (e.isKey && e.character == 'd')
		{
			Undo.IncrementCurrentGroup();
			Undo.RegisterSceneUndo("Delete Selected Objects");
			foreach (GameObject obj in Selection.gameObjects)
				DestroyImmediate(obj);
		}
	}

	public override void OnInspectorGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label(" Grid Width ");
		grid.width = EditorGUILayout.FloatField(grid.width, GUILayout.Width(50));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label(" Grid Height ");
		grid.height = EditorGUILayout.FloatField(grid.height, GUILayout.Width(50));
		GUILayout.EndHorizontal();

		SceneView.RepaintAll();
	}
}