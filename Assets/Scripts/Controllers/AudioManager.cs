using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip successfulPlacement, failedPlacement;

    private void Start()
    {
        BoardController.OnPiecePlacedCorrectly += OnPlacementSuccessful;
        BoardController.OnPieceMisplaced += OnFailedPlacement;
    }

    private void OnDestroy()
    {
        BoardController.OnPiecePlacedCorrectly -= OnPlacementSuccessful;
        BoardController.OnPieceMisplaced -= OnFailedPlacement;
    }

    private void OnPlacementSuccessful()
    {
        PlayAudio(successfulPlacement);
    }

    private void OnFailedPlacement()
    {
        PlayAudio(failedPlacement);
    }

    private void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
