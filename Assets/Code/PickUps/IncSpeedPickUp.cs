using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncSpeedPickUp : PickUpBase
{
    [SerializeField]
    public int speedIncrease;
    public override IEnumerator Effect(GameObject target, float time)
    {
        if(target.GetComponent<PlayerController>() != null)
        {
            target.GetComponent<PlayerController>().AdjustMoveSpeed(speedIncrease);
            yield return new WaitForSeconds(time);
            target.GetComponent<PlayerController>().ResetMoveSpeed();
        }

        yield return null;
    }
}
