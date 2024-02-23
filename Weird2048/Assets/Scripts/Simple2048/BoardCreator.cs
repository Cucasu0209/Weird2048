using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
public class BoardCreator
{
    public static List<GameObject> CurrentCell = new List<GameObject>();
    public static void Create(int r, int c, Image board, Image prefab)
    {
        int gap = 10;
        if (EditorApplication.isPlaying)
            foreach (var cell in CurrentCell)
            {
                GameObject.Destroy(cell);
                CurrentCell.Clear();
            }
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                Image newCell = GameObject.Instantiate(prefab, board.transform);
                CurrentCell.Add(newCell.gameObject);
                newCell.gameObject.name = $"{i + 1}x{j + 1}";
                newCell.rectTransform.anchorMax = Vector2.zero;
                newCell.rectTransform.anchorMin = Vector2.zero;
                newCell.color = new Color(0.5f, 0.5f, 0.6f);

                newCell.rectTransform.sizeDelta = new Vector2(board.rectTransform.sizeDelta.x / r - gap, board.rectTransform.sizeDelta.y / c - gap);
                newCell.rectTransform.anchoredPosition = new Vector2(
                    board.rectTransform.sizeDelta.x / r * (i + 1 / 2f),
                    board.rectTransform.sizeDelta.y / c * (j + 1 / 2f));
            }
        }
    }
}

public class CreateboardEditor : EditorWindow
{
    public Image Board;
    public Image Prefab;

    int row;
    int column;

    [MenuItem("Tools/Simple2049/CreateBoard")]
    public static void OpenWindow()
    {
        CreateboardEditor window = (CreateboardEditor)GetWindow(typeof(CreateboardEditor));
        window.minSize = new UnityEngine.Vector2(600, 1000);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Board");
        Board = (Image)EditorGUILayout.ObjectField(Board, typeof(Image), true);
        GUILayout.EndHorizontal();

        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        Prefab = (Image)EditorGUILayout.ObjectField(Prefab, typeof(Image), true);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
        EditorGUILayout.LabelField("Row: ");
        row = EditorGUILayout.IntSlider(row, 1, 10);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
        EditorGUILayout.LabelField("Column: ");
        column = EditorGUILayout.IntSlider(column, 1, 10);
        GUILayout.EndHorizontal();

        var buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold, fixedHeight = 30 };
        if (GUILayout.Button("CREATE", buttonStyle))
        {

            //EditorSceneManager.SaveOpenScenes();
            //EditorApplication.isPlaying = true;
            BoardCreator.Create(row, column, Board, Prefab);
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
