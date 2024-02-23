using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class Createboard : EditorWindow
{
    int row;
    int column;

    [MenuItem("Tools/Simple2049/CreateBoard")]
    public static void OpenWindow()
    {
        Createboard window = (Createboard)GetWindow(typeof(Createboard));
        window.minSize = new UnityEngine.Vector2(600, 1000);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Space(15);
        GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
        EditorGUILayout.LabelField("Row: ");
        row = EditorGUILayout.IntSlider(row, 1, 10);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
        EditorGUILayout.LabelField("Column: ");
        column = EditorGUILayout.IntSlider(column, 1, 10);
        GUILayout.EndHorizontal();

        var buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold, fixedHeight = 30 };
        if (GUILayout.Button("CREATE", buttonStyle))
        {
            Debug.Log(row + "x" + column);
            EditorSceneManager.SaveOpenScenes();
            EditorApplication.isPlaying = true;
        }

    }

    int cloneTimesX = 1;
    int cloneTimesY = 1;
    int cloneTimesZ = 1;
    int spacing = 2;
    void CloneSelected()
    {
        if (!Selection.activeGameObject)
        {
            Debug.LogError("Select a GameObject first");
            return;
        }

        for (int i = 0; i < cloneTimesX; i++)
            for (int j = 0; j < cloneTimesY; j++)
                for (int k = 0; k < cloneTimesZ; k++)
                    Instantiate(Selection.activeGameObject, new Vector3(i, j, k) * spacing, Selection.activeGameObject.transform.rotation);
    }
}
