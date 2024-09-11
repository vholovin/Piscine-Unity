using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;

    private GameObject player;

    private AnimatorScript animator;

    [HideInInspector] public float DistanceView = 20f;
    [HideInInspector] public float DistanceAttack = 2f;
    private float distance;

    [HideInInspector] public float STR = 10f;
    [HideInInspector] public float AGI = 15f;
    [HideInInspector] public float CON = 12f;
    [HideInInspector] public float Armor = 50f;
    [HideInInspector] public float HP = 50f;
    [HideInInspector] public float MaxHP = 50f;
    [HideInInspector] public float minDamage = 5f;
    [HideInInspector] public float maxDamage = 10f;
    [HideInInspector] public float Level = 1f;
    [HideInInspector] public float XP = 50f;
    [HideInInspector] public float Money = 0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<AnimatorScript>();

        GameObject[] players;
        if (player == null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            player = players[0];
        }
    }

    void Update()
    {
        if (HP > 0f)
        {
            SearchPlayer();
        }
    }

    private void SearchPlayer()
    {
        if (player == null)
        {
            animator.SetAttack(false);
            return;
        }

        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= DistanceView)
        {

            if (distance <= DistanceAttack)
            {
                animator.SetAttack(true);
                agent.isStopped = true;
                agent.transform.LookAt(player.transform.position);
            }
            else
            {
                animator.SetAttack(false);
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }
        }
    }

    public void ToDealDamage()
    {
        if (player == null)
        {
            return;
        }

        if (Random.Range(0, 100) < (75 + AGI - player.GetComponent<MayaController>().AGI) &&
            distance <= DistanceAttack)
        {
            float damage = Random.Range(minDamage, maxDamage) * (1 - player.GetComponent<MayaController>().Armor / 200);
            player.GetComponent<MayaController>().ToTakeDamage(damage);

            if (player.GetComponent<MayaController>().HP <= 0f)
            {
                player = null;
                animator.SetAttack(false);
            }
        }
        else
        {
            Debug.Log("Enemy miss!");
        }
    }

    public void ToTakeDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            agent.isStopped = true;
            animator.SetDead(true);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, DistanceView / 2f);
    }
}