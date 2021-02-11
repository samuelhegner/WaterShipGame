using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(GameTileController))]
public class GameTileControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		GameTileController controller = (GameTileController)target;

		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Start Tile"))
		{
			controller.startTile();
		}

		if (GUILayout.Button("Stop Tile"))
		{
			controller.stopTile();
		}

		GUILayout.EndHorizontal();

		base.OnInspectorGUI();
	}


}
