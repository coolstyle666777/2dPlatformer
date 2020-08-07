using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundSlider : MonoBehaviour, IPointerUpHandler
{
    private AudioSource _hitSound;

    private void Awake()
    {
        _hitSound = GetComponentInChildren<AudioSource>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _hitSound.volume = GameDataWriter.GameData.SoundValue;
        _hitSound.Play();
    }
}
