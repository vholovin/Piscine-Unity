using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float TimeDestroy = 3.0f;

    void Update()
    {
        Destroy(gameObject, TimeDestroy);
    }
}
