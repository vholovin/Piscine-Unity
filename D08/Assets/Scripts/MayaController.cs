using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MayaController : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameObject EnemyMessegeUI;
    public Slider EnemyUI;

    private GameObject enemy;

    private AnimatorScript animator;

    [HideInInspector] public float DistanceAttack = 2f;
    private float distance;

    RaycastHit hit;

    public Slider LifeUI;
    public float LifeRegenerate = 0.01f;

    public Text LevelUI;

    [HideInInspector] public float HP;
	[HideInInspector] public float STR = 10f;
	[HideInInspector] public float AGI = 15f;
	[HideInInspector] public float CON = 12f;
	[HideInInspector] public float Armor = 50f;
	[HideInInspector] public float minDamage = 5f;
	[HideInInspector] public float maxDamage = 10f;
	[HideInInspector] public float Level = 1f;
	[HideInInspector] public float XP = 0f;
	[HideInInspector] public float Money = 0f;
	[HideInInspector] public static float XPToNextLevel = 100f;

	void Start ()
	{
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<AnimatorScript>();

        enemy = null;

        HP = 5 * CON;
		minDamage = STR / 2;
		maxDamage = minDamage + 4;

		LifeUI.maxValue = HP;
	}
	
	void Update ()
	{
		if (HP > 0)
		{
            SetPosition();
            SetAttack();
            RegenerateHP();
            UpdateLevel();
        }
        UpdateMayaUI();
        UpdateEnemyUI();
    }


    private void SetPosition()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
                agent.SetDestination(hit.point);
                agent.isStopped = false;
                animator.SetAttack(false);

                if (hit.transform.gameObject.GetComponent<EnemyController>())
                {
                    if (hit.transform.gameObject.GetComponent<EnemyController>().HP > 0)
                    {
                        enemy = hit.transform.gameObject;
                    }
                    else
                    {
                        enemy = null;
                    }
                }
                else
                {
                    enemy = null;
                }
            }
        }
    }

    private void SetAttack()
	{
		if (enemy == null)
		{
            return;
		}

        distance = Vector3.Distance(enemy.transform.position, transform.position);

        if (distance <= DistanceAttack)
        {
            animator.SetAttack(true);
            agent.isStopped = true;
            agent.transform.LookAt(enemy.transform.position);
        }
        else
        {
            animator.SetAttack(false);
            agent.isStopped = false;
        }

    }

    public void ToDealDamage()
	{
		if (enemy == null)
		{
			return;
		}

		if (Random.Range(0, 100) < (75 + AGI - enemy.GetComponent<EnemyController>().AGI) &&
            distance <= DistanceAttack)
        {
			float damage = Random.Range(minDamage, maxDamage) * (1 - enemy.GetComponent<EnemyController>().Armor / 200);
			enemy.GetComponent<EnemyController>().ToTakeDamage(damage);

			if (enemy.GetComponent<EnemyController>().HP <= 0)
			{
				XP += enemy.GetComponent<EnemyController>().XP;
				Money += enemy.GetComponent<EnemyController>().Money;

				enemy = null;
                animator.SetAttack(false);
            }
		}
		else
		{
            Debug.Log("Maya miss!");
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

    private void RegenerateHP()
    {
        if (HP < LifeUI.maxValue)
            {
                HP += LifeRegenerate;
            }
    }

private void UpdateLevel()
	{
        if (XP > XPToNextLevel)
        {
            XP = 0;
            Level += 1f;
        }
    }

   private void UpdateMayaUI()
	{
		LifeUI.value = HP;
        LevelUI.text = Level.ToString();
	}

	private void UpdateEnemyUI()
	{
		if (enemy == null)
		{
			EnemyMessegeUI.SetActive(false);
		}
		else
		{
            EnemyMessegeUI.SetActive(true);
            EnemyUI.maxValue = enemy.GetComponent<EnemyController>().MaxHP;
            EnemyUI.value = enemy.GetComponent<EnemyController>().HP;
        }
    }

    void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		if (enemy != null)
		{
            Gizmos.DrawLine(gameObject.transform.position, enemy.transform.position);
        }
	}
}