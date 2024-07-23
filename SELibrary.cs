using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SELibrary : MonoBehaviour
{
    [SerializeField]
    private List<SoundEffect> _soundEffects = new List<SoundEffect>();

    public AudioClip GetAudioClip(string groupName, string soundName)
    {
        var group = GetSoundEffectGroup(groupName);

        var sound = group.Clips.FirstOrDefault(s => s.name == soundName);
        return sound != null ? sound : throw new NullReferenceException($"Can't find sound effect {soundName}");
    }

    public AudioClip GetRandomAudioClip(string groupName)
    {
        var group = GetSoundEffectGroup(groupName);

        if(group.Clips.Length <= 0)
        {
            throw new InvalidOperationException($"Sound effect group {groupName} is empty");
        }

        int randIndex = UnityEngine.Random.Range(0, group.Clips.Length);
        return group.Clips[randIndex];
    }

    private SoundEffect GetSoundEffectGroup(string groupName)
    {
        var group = _soundEffects.Find(g => g.GroupName == groupName);
        return group ?? throw new NullReferenceException($"Can't find sound effect group {groupName}");
    }
}

[Serializable]
public class SoundEffect
{
    [SerializeField] private string      _groupName;
    [SerializeField] private AudioClip[] _clips;

    public string GroupName  => _groupName;

    public AudioClip[] Clips => _clips;
}