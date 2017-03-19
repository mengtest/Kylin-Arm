using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 音效单例管理类
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    // 音频源
    private AudioSource audioSource;

    void Awake()
    {
        // 设置单例
        _instance = this;
        // 获取组件
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void PlayClip(string clip)
    {
        // 加载资源并播放
        audioSource.PlayOneShot(Resources.Load(clip) as AudioClip);
    }
}
