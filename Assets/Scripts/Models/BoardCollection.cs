using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Board Collection", menuName = "Board/Board Collection")]
public class BoardCollection : ScriptableObject
{
    public List<BoardDetails> boards = new List<BoardDetails>();

    public BoardDetails GetLevel(int id, BoardRenderType type)
    {
        BoardDetails board = boards.Find(x => x.id == id);
        if (board != null)
        {
            if(type == BoardRenderType.JUMBLED)
            {
                board.ShuffleJumbledPositions();
            }
            return board;
        }
        return null;
    }
}
