using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public ReferenceRenderer referenceBoard;
    public JumbledSectionRenderer jumbledBoard;
    public FlowHandler flowHandler;
    public int board;

    public static event Action OnPiecePlacedCorrectly;
    public static event Action OnPieceMisplaced;

    private Dictionary<int, Vector3> correctPositions;
    private int successfullyPlacedPieces;

    private void Start()
    {
        StartGame();
        IndividualPiece.CheckPlacement += CheckPlacementAndSnap;
        flowHandler.StartGameAgain = StartGame;
    }

    private void OnDestroy()
    {
        IndividualPiece.CheckPlacement -= CheckPlacementAndSnap;
    }

    private void StartGame()
    {
        successfullyPlacedPieces = 0;
        RenderBoards();
        SetCorrectPositions();
        flowHandler.gameObject.SetActive(false);
    }

    private void RenderBoards()
    {
        referenceBoard.RenderReference(board);
        jumbledBoard.RenderReference(board);
    }

    private void SetCorrectPositions()
    {
        correctPositions = referenceBoard.GetIdAndPositionMapping();
    }

    private void CheckPlacementAndSnap(IndividualPiece piece)
    {
        Vector3 correctPosition = default(Vector3);
        if(correctPositions.TryGetValue(piece.Id, out correctPosition))
        {
            float snapThreshold = 0.5f;
            if (Vector3.Distance(piece.transform.position, correctPosition) < snapThreshold)
            {
                piece.PiecePlacementSuccessful(correctPosition);
                ++successfullyPlacedPieces;
                if(successfullyPlacedPieces >= correctPositions.Count)
                {
                    flowHandler.gameObject.SetActive(true);
                }
                OnPiecePlacedCorrectly?.Invoke();
            }
            else
            {
                piece.PiecePlacementFailed();
                OnPieceMisplaced?.Invoke();
            }
        }
    }
}
