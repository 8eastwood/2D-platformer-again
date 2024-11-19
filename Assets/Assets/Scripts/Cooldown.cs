using System.Collections;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public IEnumerator AttackCooldown(int delay)
    {
        var wait = new WaitForSeconds(delay);

        Debug.Log("delay is working");

        yield return wait;
    }
}
