using UnityEngine;

public abstract class SectionRenderer : MonoBehaviour
{
    public BoardCollection boardCollection;

    protected abstract BoardRenderType BoardRenderType { get; set; }

    public void RenderReference(int board)
    {
        RenderBoard(boardCollection.GetLevel(board, BoardRenderType));
    }

    public void RenderBoard(BoardDetails boardDetails)
    {
        if (boardDetails != null)
        {
            CheckAndInstantiateRequiredPieces(boardDetails.sprites.Count, boardDetails.piecePrefab);
            RenderPieces(boardDetails);
        }
    }

    private void RenderPieces(BoardDetails boardDetails)
    {
        for (int i = 0; i < boardDetails.sprites.Count; ++i)
        {
            IndividualPiece p = transform.GetChild(i).GetComponent<IndividualPiece>();
            if (p != null)
            {
                //p.transform.SetParent(transform, false);
                p.SetPieceDetails(boardDetails.sprites[i], BoardRenderType == BoardRenderType.REFERENCE ? boardDetails.positions[i] : boardDetails.jumbledPositions[i], i + 1, BoardRenderType);
                p.gameObject.SetActive(true);
            }
        }
    }

    private void CheckAndInstantiateRequiredPieces(int requiredPieces, GameObject piecePrefab)
    {
        int existingPieces = transform.childCount;
        int newPiecesRequired = requiredPieces - existingPieces;

        if(newPiecesRequired > 0)
        {
            for (int i = 0; i < newPiecesRequired; ++i)
            {
                Instantiate(piecePrefab, transform);
            }
        }

        DisableAllChildren();
    }

    private void DisableAllChildren()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}