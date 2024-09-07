using UnityEngine;

public class StickScript : MonoBehaviour
{
    private bool inStick = false;

    public bool GetInStick()
    {
        return inStick;
    }
    public void SetInStick(bool newStatus)
    {
        inStick = newStatus;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Golf"))
        {
            gameObject.GetComponent<AudioSource>().Play();
            inStick = true;
        }
    }
}
