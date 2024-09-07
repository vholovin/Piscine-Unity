using UnityEngine;

public class GolfScript : MonoBehaviour
{
    private bool inWater = false;

    public bool GetInWater()
    {
        return inWater;
    }

    public void SetInWater(bool newStatus)
    {
        inWater = newStatus;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log("in water");
            inWater = true;
        }
    }
}
