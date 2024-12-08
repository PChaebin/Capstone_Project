using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Mixer 설정")]
    public AudioMixer audioMixer;
    public AudioMixerGroup bgmMixer;
    public AudioMixerGroup sfxMixer;

    [Header("BGM 설정")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    private AudioSource bgmPlayer;

    [Header("SFX 설정")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int sfxChannels;
    private AudioSource[] sfxPlayers;
    private int sfxChannelIndex;

    [Header("Vol UI")]
    [SerializeField] private Slider masterVolSlider;
    [SerializeField] private Slider bgmVolSlider;
    [SerializeField] private Slider sfxVolSlider;

    [Header("Option UI")]
    [SerializeField] private GameObject audioOptionUI;
    private Transform originalParent;

    /// <summary>
    /// 현재 효과음 종류
    /// </summary>
    /// <returns></returns>
    public enum SFX
    {
        // MiniGame_SFX
        A_Cup,
        Failed,
        Finish
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    void Start()
    {
        Init();
        InitSliders();

        // 슬라이더 이벤트 등록
        masterVolSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxVolSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    /// <summary>
    /// 씬 이름에 따라 적절한 BGM 설정
    /// </summary>
    /// <param name="sceneName">현재 씬 이름</param>
    private void SetBgmForScene(string sceneName)
    {
        AudioClip selectedBgm = null;

        // 씬 이름에 따라 BGM 선택
        if (sceneName == "Title" || sceneName == "InGame")
        {
            selectedBgm = bgmClips[0]; // Title 씬용 BGM
        }
        else if (sceneName == "MiniGame")
        {
            selectedBgm = bgmClips[2]; // MiniGame 씬용 BGM
        }
        else
        {
            Debug.LogWarning($"'{sceneName}'에 대한 BGM 설정이 없습니다. 기본값을 사용합니다.");
        }

        // 선택된 BGM을 재생
        if (selectedBgm != null && bgmPlayer.clip != selectedBgm)
        {
            bgmPlayer.clip = selectedBgm;
            PlayBgm(true); // BGM 재생
            Debug.Log($"'{sceneName}' 씬에 맞는 BGM을 재생합니다: {selectedBgm.name}");
        }
    }

    void InitSliders()
    {
        float volume;

        if (audioMixer.GetFloat("MasterVol", out volume))
        {
            masterVolSlider.value = Mathf.Pow(10, volume / 20);
        }

        if (audioMixer.GetFloat("BGMVol", out volume))
        {
            bgmVolSlider.value = Mathf.Pow(10, volume / 20);
        }

        if (audioMixer.GetFloat("SFXVol", out volume))
        {
            sfxVolSlider.value = Mathf.Pow(10, volume / 20);
        }
    }

    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.outputAudioMixerGroup = bgmMixer;

        PlayBgm(true);

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[sfxChannels];
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
            sfxPlayers[index].outputAudioMixerGroup = sfxMixer;
        }
    }

    public void PlayBgm(bool isBgmPlay)
    {
        if (isBgmPlay)
        {
            if (!bgmPlayer.isPlaying)
                bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(SFX sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + sfxChannelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            sfxChannelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void SetMasterVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1f);
        audioMixer.SetFloat("MasterVol", Mathf.Log10(volume) * 20);
    }

    public void SetBgmVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1f);
        audioMixer.SetFloat("BGMVol", Mathf.Log10(volume) * 20);
    }

    public void SetSfxVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1f);
        audioMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
    }
}
