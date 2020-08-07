using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public float SoundValue;
    public float MusicValue;
    public bool[] LevelUnlock;
    public float[] LevelHiscore;
    public int LastLevelComplete;
    public bool PostProcess;

    public GameData()
    {
        SoundValue = 0.1f;
        MusicValue = 0.1f;
        LevelUnlock = new bool[] { true, false, false, false, false, false, false };
        LevelHiscore = new float[] { 0, 0, 0, 0, 0, 0, 0 };
        LastLevelComplete = 0;
        PostProcess = true;
    }
}
