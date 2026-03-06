using System.Collections;
using UnityEngine;

public class GroundPound : AbilityBase
{
    public bool isGroundPounding;
    public float groundPoundDuration;



    protected override void Ability()
    {
        StartCoroutine(Pound());
    }

    IEnumerator Pound()
    {
        isGroundPounding = true;
       
     yield return new WaitForSeconds(groundPoundDuration);
        isGroundPounding = false;
    }
}
