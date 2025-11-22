using UnityEngine;

[CreateAssetMenu(fileName = "audioCfg", menuName = "TATA/Cfg/Audio Cfg", order = 1)]
public class AudioCfg : ScriptableObject
{
    /// <summary>
    /// AudioClip对象
    /// </summary>
    public AudioClip clip;

    /// <summary>
    /// 音乐/音效的唯一名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 音乐/音效的音量
    /// </summary>
    public float volume = 1f;
}
