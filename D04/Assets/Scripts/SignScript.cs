using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    private GameObject gamePanel;

    public bool forFinish = false;
    private bool isFinish = false;

    void Start()
    {
        gamePanel = GameObject.Find("GamePanel");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!forFinish)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player") && !isFinish)
        {
            isFinish = true;

            GetComponent<Animator>().Play("Rotate");
            GetComponent<AudioSource>().Play();

            gamePanel.GetComponent<GamePanel>().Finish();
        }
    }
}
