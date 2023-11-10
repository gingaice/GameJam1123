using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPickup : PickUpBase
{
    [SerializeField]
    public int repairVal;
    public override IEnumerator Effect(GameObject target, float time)
    {
        if(target.GetComponent<PlayerBase>() != null)
        {
            target.GetComponent<PlayerBase>().Repair(repairVal);

            yield return new WaitForSeconds(time);
        }
        else
        {

            yield return null;
        }
    }
}
