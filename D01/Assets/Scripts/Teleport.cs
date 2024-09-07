using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject OutTeleport;

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.transform.position = OutTeleport.transform.position;
    }
}