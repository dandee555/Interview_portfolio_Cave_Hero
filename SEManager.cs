using System.Collections;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private SELibrary  _seLibrary;
    [SerializeField] private GameObject _sourcePrefab;

    public static SEManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound(string groupName, string soundName, Transform playPosition, float volume = 1f, bool isLoop = false)
    {
        AudioClip soundToPlay = _seLibrary.GetAudioClip(groupName, soundName);
        _ = StartCoroutine(Play(soundToPlay, playPosition, volume, isLoop));
    }

    public void PlayRandomSound(string groupName, Transform playPosition, float volume = 1f, bool isLoop = false)
    {
        AudioClip soundToPlay = _seLibrary.GetRandomAudioClip(groupName);
        _ = StartCoroutine(Play(soundToPlay, playPosition, volume, isLoop));
    }

    private IEnumerator Play(AudioClip clipToPlay, Transform playPosition, float volume, bool isLoop)
    {
        var sourceObj = ObjectPoolManager.Instance.SpawnObject(_sourcePrefab, playPosition.position, Quaternion.identity, ObjectPoolManager.PoolType.SFX);

        if(sourceObj.TryGetComponent(out AudioSource source))
        {
            source.clip   = clipToPlay;
            source.volume = volume;
            source.loop   = isLoop;

            source.Play();
        }
        else
        {
            throw new MissingComponentException($"{sourceObj} doesn't have component AudioSource");
        }

        yield return new WaitForSeconds(clipToPlay.length);

        if(!isLoop)
        {
            ObjectPoolManager.Instance.ReturnObjectToPool(sourceObj);
        }
    }
}