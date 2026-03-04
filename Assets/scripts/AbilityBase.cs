using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbilityBase : MonoBehaviour
{
    [Header("Ability Info]")]
    public string abilityName;
    public Sprite abilityIcon;
    public float cooldownTime;
    public bool canUse = true;

    public void TriggerAbility()
    {
        if (canUse)
        {
            StartCoolDown();
            Ability();
        }
    }

    protected abstract void Ability();

    void StartCoolDown()
    {
        StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        canUse = false;
        yield return new WaitForSeconds(cooldownTime);
        canUse = true;
    }
}
