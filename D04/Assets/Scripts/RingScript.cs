using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    private GameObject gamePanel;

    public uint TimeDestroy = 1;

    private bool isUsed = false;

    void Start()
    {
        gamePanel = GameObject.Find("GamePanel");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isUsed)
        {
            isUsed = true;

            gamePanel.GetComponent<GamePanel>().AddRing();

            GetComponent<Animator>().Play("RingUp");
            GetComponent<AudioSource>().Play();

            Destroy(gameObject, TimeDestroy);
        }
    }
}
