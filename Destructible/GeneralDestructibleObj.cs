using UnityEngine;
using UnityEngine.Assertions;

public class GeneralDestructibleObj : DestructibleBase
{
    [Header("Drop")]
    [SerializeField] private bool _guaranteedDrop = false;

    [Header("SFX")]
    [SerializeField] private string _soundName;  

    protected override void Awake()
    {
        base.Awake();
        Assert.IsFalse(string.IsNullOrEmpty(_soundName), "Sound name can't be null or empty");
    }

    protected override void OnHit()
    {
        EffectManager.Instance.PlayOneShot("DustUp", this.transform);
        SEManager.Instance.PlaySound("Destruct", _soundName, this.transform);
    }

    protected override void OnDestruction()
    {
        if(!_isDestroy)
        {
            return;
        }

        _animator.Play("Destroy");
        _collider.enabled = false;
        DropItemManager.Instance.DropHealingPickup(transform.position, _guaranteedDrop);
        Destroy(gameObject, _destroyDelayTime);
    }
}
