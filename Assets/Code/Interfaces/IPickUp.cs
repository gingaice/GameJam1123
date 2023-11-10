using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUp
{
    void CastEffect(GameObject target);
    IEnumerator Effect(GameObject target, float time);
}
