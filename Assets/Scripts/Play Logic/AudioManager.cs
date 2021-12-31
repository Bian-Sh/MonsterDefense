using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// 管理的音频片段
    /// </summary>
    public List<AudioClip> clips;

    public AudioSource audioSource;

    /// <summary>
    /// 播放按钮音效
    /// </summary>
    /// <param name="name"></param>
    public void PlayAudio(string name)
    {
        var clip = clips.Find(v => v.name == name);
        if (clip)
        {
            // 在头盔处播放声音片段
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
    /// <summary>
    /// 播放/停止背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayBackGround(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            audioSource.Stop();
        }
        else
        {
            var clip = clips.Find(v => v.name == name);
            if (clip)
            {
                // 在头盔处播放声音片段
                audioSource.loop = true;
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
