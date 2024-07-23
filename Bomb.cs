using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Bomb : DestructibleBase
{
    [SerializeField] private UnityEvent _explodeEvent;

    protected override void Awake()
    {
        base.Awake();
        Assert.IsNotNull(_explodeEvent, $"{_explodeEvent} in {this} is null");
    }

    protected override void OnHit()
    {
        //Not thing to do. Leave it blank here.
    }

    protected override void OnDestruction()
    {
        if (!_isDestroy)
        {
            return;
        }

        EffectManager.Instance.PlayOneShot("BombExplosion", this.transform);
        SEManager.Instance.PlayRandomSound("Explode", this.transform);
        _animator.Play("Destroy");
        _collider.enabled = false;
        _explodeEvent.Invoke();
        Destroy(gameObject, _destroyDelayTime);    
    }
}
