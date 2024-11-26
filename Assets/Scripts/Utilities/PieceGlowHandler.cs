using UnityEngine;
using DG.Tweening;

public class PieceGlowHandler : MonoBehaviour
{
    public SpriteRenderer glowSprite;

    public void PiecePlacementSuccessful()
    {
        glowSprite.color = Color.white;
        glowSprite.DOFade(1f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            glowSprite.DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        });
    }

    public void PiecePlacementFailed()
    {
        glowSprite.color = Color.red;
        glowSprite.DOFade(1f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            glowSprite.DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        });
    }
}
