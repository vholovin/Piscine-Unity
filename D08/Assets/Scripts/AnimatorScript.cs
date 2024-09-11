using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorScript : MonoBehaviour
{
    private Animator animator;

    [HideInInspector] public float MagnitudeForRun = 0.1f;
    private float magnitude = 0f;

    private bool isAttack = false;

    [HideInInspector] public float DeadTimer = 5f;
    private bool isDead = false;
    private bool isCoroutine = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateMagnitude();
        UpdateAnimator();
    }

    private void UpdateMagnitude()
    {
        magnitude = gameObject.GetComponent<NavMeshAgent>().velocity.magnitude;
    }

    private void UpdateAnimator()
    {
        if (isDead)
        {
            if (!isCoroutine)
            {
                isCoroutine = true;
                StartCoroutine(nameof(ToDestroyBody));
            }
        }
        else
        {

            if (magnitude > MagnitudeForRun)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);

                if (isAttack)
                {
                    animator.SetBool("Attack", true);
                }
                else
                {
                    animator.SetBool("Attack", false);
                }
            }
        }
    }

    IEnumerator ToDestroyBody()
    {
        animator.SetBool("Dead", true);
        Destroy(gameObject, DeadTimer);
        yield return new WaitForSeconds(DeadTimer);
    }

    public void SetAttack(bool value)
    {
        isAttack = value;
    }

    public void SetDead(bool value)
    {
        isDead = value;
    }
}