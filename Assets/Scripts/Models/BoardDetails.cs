using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "LevelSettings")]
public class BoardDetails : ScriptableObject
{
    public int id;
    public List<Sprite> sprites;
    public List<Vector3> positions;
    public List<Vector3> jumbledPositions;
    public GameObject piecePrefab;

    public void ShuffleJumbledPositions()
    {
        for(int i = 0; i < jumbledPositions.Count; ++i)
        {
            jumbledPositions[i] = new Vector3(Random.Range(-10f, 0f), Random.Range(5f, 12f), 0);
        }
    }
}

public enum BoardRenderType
{
    REFERENCE,
    JUMBLED
}