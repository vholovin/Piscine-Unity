using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    public uint TimeDestroy = 3;

    void Update()
    {
        Destroy(gameObject, TimeDestroy);
    }
}
