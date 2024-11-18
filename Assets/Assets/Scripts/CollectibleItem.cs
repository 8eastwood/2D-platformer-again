using UnityEngine;

public abstract class CollectibleItem : MonoBehaviour
{
    public void DestroyAfterCapture()
    {
        Destroy(gameObject);
    }
}
