using System.Collections;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public IEnumerator AttackCooldown(int delay, bool isAttackPossible)
    {
        var wait = new WaitForSeconds(delay);
        Debug.Log("ждем " + delay);

        yield return wait;

        isAttackPossible = true;
    }
}
