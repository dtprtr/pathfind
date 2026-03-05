using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DashAbility : AbilityBase
{
    [HideInInspector] public bool isDashing;  
    public float dashPower;//how much force the dash will apply to the player
    public float dashingtime;
    public CharacterController character;
    public TrailRenderer tr;
    public character_movement playerMovement;
    
    public void Awake()
    {
        character = GetComponent<CharacterController>();
        tr = GetComponent<TrailRenderer>();
        playerMovement = GetComponent<character_movement>();
    }

    protected override void Ability()
    {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        isDashing = true;
        playerMovement.enabled = false;
        character.Move(transform.forward * dashPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingtime);
        playerMovement.enabled = true;
        tr.emitting = false;
        playerMovement.velocityY = 0;
        isDashing = false;
    }
}
