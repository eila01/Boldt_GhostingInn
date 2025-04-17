using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void playSoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        // assign the audioClip
        audioSource.clip = audioClip;
        // assign volume
        audioSource.volume = volume;
        // play sound 
        audioSource.Play();
        // get length of sound FX clip
        float clipLength = audioSource.clip.length;
        // destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }
    
    public void playRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        // assign a random index
        int rand = Random.Range(0, audioClip.Length);
        // spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        // assign the audioClip
        audioSource.clip = audioClip[rand];
        // assign volume
        audioSource.volume = volume;
        // play sound 
        audioSource.Play();
        // get length of sound FX clip
        float clipLength = audioSource.clip.length;
        // destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
