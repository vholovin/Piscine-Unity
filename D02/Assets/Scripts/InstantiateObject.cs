using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    public GameObject InstantObject;
    public GameObject InstantPosition;
    public float TimeDelay = 10f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeDelay)
        {
            GameObject.Instantiate(InstantObject, InstantPosition.transform.position, Quaternion.identity);
            timer = 0.0f;
        }
    }
}
