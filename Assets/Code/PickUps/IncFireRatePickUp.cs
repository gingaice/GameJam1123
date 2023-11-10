using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncFireRatePickUp : PickUpBase
{
    public override IEnumerator Effect(GameObject target, float time)
    {
        if (target.GetComponent<PlayerBase>() != null)
        {
            target.GetComponent<PlayerBase>().BoostFireRate();
            yield return new WaitForSeconds(time);
            target.GetComponent<PlayerBase>().ResetFireRate();
        }

        yield return null;
    }
}
