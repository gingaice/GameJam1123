using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBase : BaseObject, IPickUp
{
    [SerializeField]
    public float effectDuration;

    public void CastEffect(GameObject target)
    {
        StartCoroutine(Effect(target, effectDuration));
    }
    public virtual IEnumerator Effect(GameObject target, float time)
    {
        yield return new WaitForSeconds(time);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Int32 layer = LayerMask.NameToLayer("Border");

        if (collision.gameObject.GetComponent<PlayerController>() == true)
        {
            GetComponent<AudioSource>().Play();
            CastEffect(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == layer)
        {
            Destroy(gameObject);
        }
    }
}
