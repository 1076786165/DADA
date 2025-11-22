using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class PlayerData
{
    //data
    public int AdvLevel;
    public int IsAdvAllPass;
    public int ClassicHighScore;
    public int ClassicBonusProtectFlag;
    public int ClassicRoundId;
    public int Coins;
    public int Heart;
    public long HeartCutDown;
    public long SuperHeartEndingTime;

    //setting
    public bool SoundSwitch;
    public bool MusicSwitch;

    //weapons
    public int WeaponHammerNums;
    public int WeaponShuffleNums;

    //home bg
    public int HomeBgId;


    public PlayerData()
    {
        AdvLevel = 1;
        IsAdvAllPass = 0;
        ClassicHighScore = 0;
        ClassicBonusProtectFlag = 0;
        ClassicRoundId = 0;
        Coins = 0;
        Heart = 5;

        SoundSwitch = true;
        MusicSwitch = true;

        WeaponHammerNums = 5;
        WeaponShuffleNums = 5;

        HeartCutDown = -1;
        SuperHeartEndingTime = -1;

        HomeBgId = 1;
    }


}
