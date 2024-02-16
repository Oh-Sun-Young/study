using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 사운드를 들을 때 필요한 것
* MP3 Player    → AudioSource
* MP3 음원       → AudioClip
* 관객(귀)        → AudioListener
*/
public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>(); // 캐싱 역활

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i< soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform; // Rect Transform 일 경우 SetParent 사용
            }
            _audioSources[(int)Define.Sound.Bgm].loop = true;
            _audioSources[(int)Define.Sound.Bgm].volume = 0.2f;
            _audioSources[(int)Define.Sound.Effect].spatialBlend = 1;
            _audioSources[(int)Define.Sound.Effect].maxDistance = 10;
            _audioSources[(int)Define.Sound.Effect].rolloffMode = AudioRolloffMode.Linear;
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        AudioSource audioSource;

        switch (type)
        {
            case Define.Sound.Bgm:
                audioSource = _audioSources[(int)Define.Sound.Bgm];

                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.pitch = pitch;
                audioSource.clip = audioClip;
                audioSource.Play();
                break;
            case Define.Sound.Effect:
                audioSource = _audioSources[(int)Define.Sound.Effect];
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
                break;
        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    AudioClip GetOrAddAudioClip(string path)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (!_audioClips.TryGetValue(path, out audioClip))
        {
            audioClip = Managers.resource.Load<AudioClip>(path);
            _audioClips.Add(path, audioClip);
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing : {path}");

        return audioClip;
    }
}
