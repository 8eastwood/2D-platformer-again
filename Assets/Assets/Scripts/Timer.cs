using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public static IEnumerator Cooldown(WaitForSeconds wait, Action callBack)
    {
        yield return wait;

        callBack.Invoke();
    }
}
