using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBird : Bird
{
    [SerializeField]
    public float _explodeForce = 200;
    public float _explodeRadius = 100;
    public bool _hasExplode = false;

    private void Explode(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
        {
            return;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            // jangan meledak lagi kalau udah kena enemy
            _hasExplode = true;
            return;
        }

        if (!_hasExplode)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _explodeRadius);
            Debug.Log(hitColliders.Length);
            foreach (var hitCollider in hitColliders)
            {
                Debug.Log(hitCollider.gameObject.name);
                Rigidbody2D other = hitCollider.gameObject.GetComponent<Rigidbody2D>();
                if(other != null)
                    other.AddForce((other.gameObject.transform.position - transform.position) * _explodeForce);
            }
            _hasExplode = true;
        }
    }
    public override void OnHit(Collision2D collision)
    {
        Explode(collision);
    }
}
