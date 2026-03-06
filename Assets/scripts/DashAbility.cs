using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DashAbility : AbilityBase
{
    [HideInInspector] public bool isDashing;  
    public float dashPower;//how much force the dash will apply to the player
    public float dashingDuration;
    public CharacterController character;
    public TrailRenderer tr;
    public character_movement playerMovement;
    public AnimationCurve dashCurve;
    public SphereCollider wallCheckCollider;

    public void Awake()
    {
        character = GetComponent<CharacterController>();
        tr = GetComponent<TrailRenderer>();
        playerMovement = GetComponent<character_movement>();
        wallCheckCollider = GetComponent<SphereCollider>();
    }

    protected override void Ability()
    {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        isDashing = true;
        playerMovement.enabled = false;
        tr.emitting = true;
        
        Vector3 oldPos = character.transform.position;
        Vector3 newPos = oldPos + transform.forward * dashPower;
        
        for (float T = 0; T< dashingDuration; T += Time.deltaTime)
        {
            character.transform.position = Vector3.Lerp(oldPos, newPos,dashCurve.Evaluate( T / dashingDuration));
            yield return new WaitForEndOfFrame();
        }
        
        tr.emitting = false;
        playerMovement.enabled = true;
        isDashing = false;
        
        playerMovement.velocityY = 0;
    }
}
