using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip towerBreak;
    public AudioClip extinguish;
    public static AudioPlayer audioPlayer;
    private AudioSource _audioSource;

    private void Start()
    {
        if (audioPlayer != null)
        {
            Destroy(audioPlayer.gameObject);
        }

        audioPlayer = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlayTowerBreak()
    {
        audioPlayer._audioSource.PlayOneShot(audioPlayer.towerBreak);
    }

    public static void PlayExtinguish()
    {
        audioPlayer._audioSource.PlayOneShot(audioPlayer.extinguish);
    }
}
