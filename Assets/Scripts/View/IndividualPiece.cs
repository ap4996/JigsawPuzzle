using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualPiece : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer glowSprite;
    public DragHandler dragHandler;
    public PieceGlowHandler pieceGlowHandler;

    public static event Action<IndividualPiece> CheckPlacement;
    public int Id { get => id; set => id = value; }

    private int id;


    public void SetPieceDetails(Sprite sprite, Vector3 position, int id, BoardRenderType type)
    {
        SetPieceSprite(sprite, type);
        SetPieceLocalPosition(position);
        SetPieceName($"{id}");
        SetId(id);
        EnableDraggingAndSubscribe(type == BoardRenderType.JUMBLED);
    }
    public void EnableDraggingAndSubscribe(bool enable)
    {
        dragHandler.enabled = enable;
        dragHandler.OnDragComplete = OnDragCompleted;
    }

    public void PiecePlacementSuccessful(Vector3 correctPosition)
    {
        transform.position = correctPosition;
        EnableDraggingAndSubscribe(false);
        StartCoroutine(SetSpriteOrderInLayerTemporarily());
        pieceGlowHandler.PiecePlacementSuccessful();
    }

    public void PiecePlacementFailed()
    {
        StartCoroutine(SetSpriteOrderInLayerTemporarily());
        pieceGlowHandler.PiecePlacementFailed();
    }

    private IEnumerator SetSpriteOrderInLayerTemporarily()
    {
        SetSpriteOrderInLayer(1);
        yield return new WaitForSeconds(1);
        SetSpriteOrderInLayer(0);
    }

    private void SetSpriteOrderInLayer(int order)
    {
        spriteRenderer.sortingOrder = order;
        glowSprite.sortingOrder = order;
    }

    private void SetId(int id)
    {
        this.id = id;
    }


    private void SetPieceSprite(Sprite sprite, BoardRenderType type)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = new Color(1, 1, 1, type == BoardRenderType.JUMBLED ? 1 : 0.4f);
        glowSprite.sprite = sprite;
    }

    private void SetPieceLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    private void SetPieceName(string name)
    {
        gameObject.name = name;
    }

    private void OnDragCompleted()
    {
        CheckPlacement?.Invoke(this);
    }
}
