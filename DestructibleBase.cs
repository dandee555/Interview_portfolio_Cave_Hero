using UnityEngine;
using UnityEngine.Assertions;

public abstract class DestructibleBase : MonoBehaviour
{
    [Header("Destruct Info")]
    [SerializeField] protected float _destroyDelayTime;
    [SerializeField] protected int   _destroyCount;

    protected bool _isDestroy = false;

    [Header("Others")]
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected Animator   _animator;

    protected virtual void Awake()
    {
        Assert.IsNotNull(_collider, $"{_collider} in {this} is null");
        Assert.IsNotNull(_animator, $"{_animator} in {this} is null");
    }

    public virtual void GetHitOneTime()
    {
        _destroyCount--;

        OnHit();

        if (_destroyCount <= 0)
        {
            _isDestroy = true;
            OnDestruction();
        }
    }

    protected abstract void OnHit();

    protected abstract void OnDestruction();
}
