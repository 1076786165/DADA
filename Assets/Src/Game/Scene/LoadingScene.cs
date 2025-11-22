using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(InitStream());
    }

    private IEnumerator InitStream()
    {
        //加载Manager类
        Instantiate(Resources.Load("Prefab/Component/ManagerGroup"));

        //加载存档数据
        if (((IInitable)PlayerManager.I).IsInited() == false) yield return null;
        PlayerManager.I.LoadData();
        PlayerManager.I.SaveData();

        //初始化AudioManager
        if (((IInitable)AudioManager.I).IsInited() == false) yield return null;

        //初始化AdManager
        if (((IInitable)AdManager.I).IsInited() == false) yield return null;

        //初始化DOTween
        DOTween.Init(useSafeMode: true, logBehaviour: LogBehaviour.Verbose); 
        yield return null;

        Application.targetFrameRate = Config.TargetFrameRate;
        
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Lobby");
        loadOperation.allowSceneActivation = true;

        InitEnd();
    }

    private void InitEnd(){
        
    }

}
