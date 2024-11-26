using UnityEngine;
using UnityEngine.UI;

public class ScaleCanvas : MonoBehaviour
{
    public CanvasScaler canvasScaler;

    private void Start()
    {
        SetCanvas();
    }

    private void SetCanvas()
    {
        canvasScaler.matchWidthOrHeight = IsTabletResolution() ? 1 : 0;
    }

    private bool IsTabletResolution()
    {
        return GetScreenResolution() <= 1.77f;
    }

    private float GetScreenResolution()
    {
        return (float)Screen.height / (float)Screen.width;
    }
}
