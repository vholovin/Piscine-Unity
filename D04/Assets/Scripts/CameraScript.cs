using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject activePlayer;
    public Vector3 CameraPosComps = new(0.0f, 1.5f, -10.0f);

    void Start()
    {
        PutCamera(activePlayer);

    }

    //Camera follow player
    void PutCamera(GameObject player)
    {
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = CameraPosComps;
    }
}
