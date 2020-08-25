using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    private AudioSource[] _playerSounds;

    private void Awake()
    {
        _playerSounds = GetComponentsInChildren<AudioSource>();
    }

    private void Start()
    {
        foreach (AudioSource sound in _playerSounds)
        {
            sound.volume = GameDataWriter.GameData.SoundValue;
        }
    }

    public void SetCurrentVolume()
    {
        foreach (AudioSource sound in _playerSounds)
        {
            sound.volume = GameDataWriter.GameData.SoundValue;
        }
    }
}
