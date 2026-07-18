using UnityEngine;

public class Level3AudioManager : MonoBehaviour
{
    public static Level3AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioSource alarmSource;

    [Header("Audio Clips")]
    public AudioClip soundCard;
    public AudioClip soundAlarm;
    public AudioClip soundHospital;
    public AudioClip soundCoins;
    public AudioClip musicStart;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayMusicStart()
    {
        if (musicSource != null && musicStart != null) {
            musicSource.clip = musicStart;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayLockdownMusic()
    {
        if (musicSource != null && soundHospital != null) {
            musicSource.clip = soundHospital;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayCoinSound()
    {
        if (sfxSource != null && soundCoins != null) {
            sfxSource.PlayOneShot(soundCoins);
        }
    }

    public void PlayKeycardSound()
    {
        if (sfxSource != null && soundCard != null) {
            sfxSource.PlayOneShot(soundCard);
        }
    }

    public void PlayAlarm()
    {
        StartCoroutine(PlayAlarmRoutine());
    }

    private System.Collections.IEnumerator PlayAlarmRoutine()
    {
        if (alarmSource != null && soundAlarm != null) {
            alarmSource.clip = soundAlarm;
            alarmSource.loop = true;
            alarmSource.Play();
            yield return new WaitForSeconds(7f);
            alarmSource.Stop();
        }
    }
}