using UnityEngine;

public class ActiveElement : MonoBehaviour
{
    public GameObject Target;
    public GameObject ToDisabledCollision;
    private ParticleSystem.EmissionModule EmHead;
    private ParticleSystem.EmissionModule EmTarget;

    private bool isOpen = false;
    public AudioClip SoundDoor;
    public AudioClip[] SoundSwitch = new AudioClip[2];

    private void Start()
    {
        if (Target)
        {
            if (gameObject.CompareTag("Fan") && Target.CompareTag("Fan"))
            {
                EmHead = gameObject.GetComponent<ParticleSystem>().emission;
                EmTarget = Target.GetComponent<ParticleSystem>().emission;
                EmHead.enabled = false;
                EmTarget.enabled = false;
            }
        }
    }

    public void ToActivate()
    {
        if (gameObject.CompareTag("Fan") && Target.CompareTag("Fan"))
        {
            ForFan();
        }

    }
    private void ForFan()
    {
        EmHead.enabled = true;
        EmTarget.enabled = true;
        ToDisabledCollision.SetActive(false);
    }

    public void ToActivate(bool key)
    {
        if (gameObject.CompareTag("Switch") && Target.CompareTag("Switch"))
        {
            ForSwitch(key);
        }
    }

    private void ForSwitch(bool key)
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if (key == true)
            {
                gameObject.GetComponent<AudioSource>().clip = SoundSwitch[0];

                if (isOpen == false)
                {
                    isOpen = true;
                    Target.GetComponent<Animator>().Play("Open");
                }
                else if (isOpen == true)
                {
                    isOpen = false;
                    Target.GetComponent<Animator>().Play("Close");
                }
            }
            else
            {
                gameObject.GetComponent<AudioSource>().clip = SoundSwitch[1];
            }

            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}

