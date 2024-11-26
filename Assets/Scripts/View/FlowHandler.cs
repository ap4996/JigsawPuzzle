using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowHandler : MonoBehaviour
{
    public Button tryAgainButton;
    public Action StartGameAgain;

    private void Start()
    {
        SetButton();
    }

    private void SetButton()
    {
        tryAgainButton.onClick.RemoveAllListeners();
        tryAgainButton.onClick.AddListener(TryAgainButtonDelegate);
    }

    private void TryAgainButtonDelegate()
    {
        StartGameAgain?.Invoke();
    }
}
