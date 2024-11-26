// Assets/Editor/LevelSettingsGenerator.cs
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BoardGenerator : EditorWindow
{
    private string fileName = "Board1";
    private GameObject sourceParentObject, piecePrefab;

    [MenuItem("Tools/Generate Board From Scene")]
    public static void ShowWindow()
    {
        GetWindow<BoardGenerator>("Board Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Board Generator", EditorStyles.boldLabel);

        fileName = EditorGUILayout.TextField("File Name", fileName);
        sourceParentObject = (GameObject)EditorGUILayout.ObjectField("Source Parent Object", sourceParentObject, typeof(GameObject), true);
        piecePrefab = (GameObject)EditorGUILayout.ObjectField("Piece Prefab Object", piecePrefab, typeof(GameObject), true);

        if (GUILayout.Button("Generate Board"))
        {
            if (sourceParentObject == null)
            {
                EditorUtility.DisplayDialog("Error", "Please select a parent GameObject.", "OK");
                return;
            }

            if (piecePrefab == null)
            {
                EditorUtility.DisplayDialog("Error", "Please select a prefab for the Individual Piece.", "OK");
                return;
            }
            CreateBoard();
        }
    }

    private void CreateBoard()
    {
        BoardDetails asset = ScriptableObject.CreateInstance<BoardDetails>();

        BoardDetails newLevel = new BoardDetails
        {
            id = 1,  // You can modify to generate multiple levels dynamically
            sprites = new List<Sprite>(),
            positions = new List<Vector3>(),
            jumbledPositions = new List<Vector3>(),
            piecePrefab = piecePrefab,
        };

        // Collect all child objects with SpriteRenderer
        SpriteRenderer[] spriteRenderers = sourceParentObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (var sr in spriteRenderers)
        {
            newLevel.sprites.Add(sr.sprite);
            newLevel.positions.Add(sr.transform.localPosition);
            newLevel.jumbledPositions.Add(sr.transform.localPosition);
        }

        asset = newLevel;

        // Save the ScriptableObject asset
        string path = EditorUtility.SaveFilePanelInProject("Save Board", fileName, "asset", "Save your Board");

        if (string.IsNullOrEmpty(path))
            return;

        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

        Debug.Log($"Board '{fileName}' created at {path}");
    }
}
