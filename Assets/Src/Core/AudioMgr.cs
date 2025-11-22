using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviourSingleton<AudioManager> , IInitable
{
    /// <summary>
    /// 音乐开关
    /// </summary>
    public bool MusicEnable = true;
    /// <summary>
    /// 音效开关
    /// </summary>
    public bool SoundEnable = true;

    public Action<bool> OnMusicEnableChanged;

    public Action<bool> OnSoundEnableChanged;

    [SerializeField]
    protected AudioSource s_sound;
    [SerializeField]
    protected AudioSource m_sound;

    protected Dictionary<string, AudioCfg> mAudioClips = new();

    protected LinkedList<AudioCfg> mBgmAudioStack = new();
    
    private AudioListener _audio_listener;

    public bool _isInited {set; get;}

    protected override void Start()
    {
        Init();
    }

    public void Init()
    {
        // 将AudioListener附加到当前的gameobject上
        if (!gameObject.GetComponent<AudioListener>())
            _audio_listener = gameObject.AddComponent<AudioListener>();

        List<AudioCfg> audioCfgs = Resources.LoadAll<AudioCfg>("Data/Audio").ToList();

        foreach (var audioCfg in audioCfgs)
        {
            AddAudioClip(audioCfg);
        }

        // EventManager.AddListener<bool>(Def.EVENT_SOUND_SWITCH, OnSoundSwitch);
        // EventManager.AddListener<bool>(Def.EVENT_MUSIC_SWITCH, OnMusicSwitch);
        
        ((IInitable)this).MarkInitEnd();
    }

    public void OnSoundSwitch(bool enable)
    {
        SetSoundEnable(enable);

      
    }

    public void OnMusicSwitch(bool enable)
    {
        SetMusicEnable(enable);
        
    }

    public bool PlayBgm(string name, bool loop = true)
    {
        var cfg = FindAudioClip(name);
        return PlayBgm(cfg, loop);
    }

    public bool PlayBgm(AudioCfg cfg, bool loop = true)
    {
        if (mBgmAudioStack.Count > 0)
        { 
            mBgmAudioStack.RemoveFirst();
            m_sound.Stop();
            m_sound.clip = null;
        }
        
        if(cfg == null) return false;
        mBgmAudioStack.AddFirst(cfg);
        m_sound.clip = cfg.clip;
        m_sound.volume = cfg.volume;
        if(!MusicEnable) return false;
        m_sound.loop = loop;
        m_sound.Play();
        return true;
    }

    // 以堆栈的方式播放背景音乐
    public bool PushPlayBgm(string name)
    {
        var cfg = FindAudioClip(name);
        return PushPlayBgm(cfg);
        
    }

    public bool PushPlayBgm(AudioCfg cfg,bool loop = true)
    {
        if(cfg == null) return false;
        mBgmAudioStack.AddFirst(cfg);
        m_sound.Stop();
        m_sound.clip = cfg.clip;
        m_sound.volume = cfg.volume;
        if(!MusicEnable) return false;
        m_sound.Play();
        m_sound.loop = loop;
        return true;
    }
    // 停止当前背景音乐，回复上一个背景音乐播放
    public bool PopPlayBgm()
    {
        if(mBgmAudioStack.Count <= 0) return false;
        mBgmAudioStack.RemoveFirst();
        m_sound.Stop();
        m_sound.clip = null;
        var cfg = mBgmAudioStack?.First?.Value;
        if(cfg == null)
        {
            m_sound.Stop();
            m_sound.clip = null;
            return false;
        }
        m_sound.clip = cfg.clip;
        m_sound.volume = cfg.volume;
        m_sound.loop = true;                  // 默认bgm循环播放
        if (!MusicEnable) return false;
        m_sound.Play();
        return true;
    }

    public bool PauseBGM()
    {
        m_sound.Pause();
        return false;
    }

    public void ResumeBGM()
    {
        m_sound.UnPause();
    }

    public void SetBgmLoop(bool loop)
    {
        m_sound.loop = loop;
    }

    public void StopAll()
    {
        s_sound.Stop();
        m_sound.Stop();
    }

    /// <summary>
    /// 注意，这里仅能用来播放音效，不能用来播放背景音乐，并且不能暂停，可暂停的需要手动实现
    /// </summary>
    /// <param name="name">配置文件中指定的名称</param>
    /// <returns></returns>
    public bool PlaySound(string name)
    {
        // Tools.Log("PlaySound:" + name);
        if (!SoundEnable) return false;
        var cfg = FindAudioClip(name);
        if (cfg == null) return false;

        s_sound.PlayOneShot(cfg.clip, cfg.volume);
        return true;
    }

    public bool PlaySound(AudioClip clip)
    {
        if(!SoundEnable) return false;
        s_sound.PlayOneShot(clip);
        return true;
    }

    public bool IsSoundEnable()
    {
        return SoundEnable;
    }
    
    public bool IsMusicEnable()
    {
        return MusicEnable;
    }

    public void SetSoundEnable(bool enable)
    {
        if (SoundEnable == enable) return;

        SoundEnable = enable;

        if (enable)
        {
            s_sound.Play();
        }
        else
        {
            s_sound.Stop();
        }

        OnSoundEnableChanged?.Invoke(SoundEnable);

    }

    public void SetMusicEnable(bool enable)
    {
        if (MusicEnable == enable) return;

        MusicEnable = enable;

        if (enable)
        {
            m_sound.Play();
        }
        else
        {
            m_sound.Stop();
        }

        OnSoundEnableChanged?.Invoke(SoundEnable);
    }

    public void SetEnable(bool enable)
    {
        SetSoundEnable(enable);
        SetMusicEnable(enable);
    }


    public void Preload()
    {

    }

    public void ReleaseAll()
    {
        s_sound.Stop();
        s_sound.clip = null;
        m_sound.Stop();
        m_sound.clip = null;
        mAudioClips.Clear();
        mBgmAudioStack.Clear();
    }

    public bool AddAudioClip(AudioCfg cfg)
    {
        string name = cfg.Name;

#if UNITY_EDITOR
        mAudioClips.Add(name, cfg);
        return true;
            
#else
        return mAudioClips.TryAdd(name, cfg);
#endif
    }

//     public void AddAudioClips(List<AudioCfg> cfgs)
//     {
//         for(int i = 0; i < cfgs.Count; i++)
//         {
// #if UNITY_EDITOR
//             // if(!mAudioClips.TryAdd(cfgs[i].Name, cfgs[i]))
//             // {
//             //     Debug.LogWarning($"添加音频配置失败，已存在同名配置：{cfgs[i].Name}");
//             // }
//             mAudioClips[cfgs[i].Name] = cfgs[i];
// #else
//             // mAudioClips.TryAdd(cfgs[i].Name, cfgs[i]);// 实际使用时直接覆盖
//             mAudioClips[cfgs[i].Name] = cfgs[i];
// #endif
            
//         }
//     }

    // public void RemoveAudioClip(string name)
    // {
    //     mAudioClips.Remove(name);
    // }

    // public void RemoveAudioClips(List<string> names)
    // {
    //     for(int i = 0; i < names.Count; i++)
    //     {
    //         mAudioClips.Remove(names[i]);
    //     }
    // }

    // public void RemoveAudioClips(List<AudioCfg> cfgs)
    // {
    //     for(int i = 0; i < cfgs.Count; i++)
    //     {
    //         mAudioClips.Remove(cfgs[i].Name);
    //     }
    // }

    public AudioCfg FindAudioClip(string name)
    {
        if(mAudioClips.TryGetValue(name, out var clip))
        {
            return clip;
        }
        return null;
    }


}
