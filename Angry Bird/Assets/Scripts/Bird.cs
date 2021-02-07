﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown, HitSomething }
    // public GameObject parent;
    public Rigidbody2D rigidbody;
    public CircleCollider2D collider;

    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Bird> OnBirdShot = delegate { };

    private BirdState _state;
    private float _minVelocity = 0.05f;
    private bool _flagDestroy;

    private void Start()
    {
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        collider.enabled = false;
        _state = BirdState.Idle;
    }

    private void FixedUpdate()
    {
        if(_state == BirdState.Idle && rigidbody.velocity.sqrMagnitude >= _minVelocity)
        {
            _state = BirdState.Thrown;
        }

        if ((_state == BirdState.Thrown || _state == BirdState.HitSomething)
            && rigidbody.velocity.sqrMagnitude < _minVelocity && !_flagDestroy)
        {
            _flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }

    private IEnumerator DestroyAfter(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        collider.enabled = true;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.velocity = velocity * speed * distance;
        OnBirdShot(this);
    }

    private void OnDestroy()
    {
        if(_state == BirdState.Thrown || _state == BirdState.HitSomething)
            OnBirdDestroyed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _state = BirdState.HitSomething;
        OnHit(collision);
    }

    public virtual void OnTap()
    {

    }
    public virtual void OnHit(Collision2D collision)
    {

    }

    public BirdState State
    {
        get
        {
            return _state;
        }
    }

}
