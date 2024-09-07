using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class TankEnemy : MonoBehaviour
{
    private NavMeshAgent Agent;
    public float AgentMinVisibleRange = 5.0f;
    public float AgentMaxVisibleRange = 100.0f;

	private LayerMask agentMaskTarget;

    private GameObject agentLessTank;
    float agentLessDistance;

    public GameObject Body;
	public GameObject Tower;

    public uint SpeedRotateTower = 50;
    public float DeadZoneRoteteTower = 0.5f;

    public uint FireDistanceRange = 50;
    private RaycastHit fireRaycastTarget;
    private bool fireTarget = false;

    public uint[] FireDamage = new uint[2] { 2, 20 };
    public GameObject[] FireParticle = new GameObject[2];

    public GameObject[] FireEffect = new GameObject[2];
    public Transform FireEffectPosition;

    public float FireDelay = 3f;
    private bool isFire = true;
    private float timer = 0f;

    public int HpStart = 100;
    private int hp;

    public GameObject DestroyEffect;
    public uint ExplosionForce = 1000;
    public uint ExplosionRadius = 10;
    private bool IsDestroyed = false;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        string agentMaskName = LayerMask.LayerToName(layer: gameObject.layer);
		agentMaskTarget = LayerMask.GetMask(agentMaskName);

        hp = HpStart;

        Agent.stoppingDistance = AgentMinVisibleRange;

        Body.GetComponent<NavMeshObstacle>().enabled = false;
        Tower.GetComponent<NavMeshObstacle>().enabled = false;
    }

    private void Update()
	{
        if (!IsDestroyed)
		{
            SearchTarget();

            if (agentLessTank != null)
			{
                Move();
                RotateTower();
				ToFire();
            }

            UpdateTime();
            Destroy();
        }
    }


    private void SearchTarget()
    {
        bool targetIsDestroyed = false;
        agentLessDistance = AgentMaxVisibleRange;

        agentLessTank = null;

        Collider[] agentTarget = Physics.OverlapSphere(gameObject.transform.position, AgentMaxVisibleRange, agentMaskTarget);

        foreach (Collider tank in agentTarget)
        {
            if (tank.gameObject.GetHashCode() != gameObject.GetHashCode())
            {
                if (tank.gameObject.GetComponent<TankEnemy>())
                {
                    targetIsDestroyed = tank.gameObject.GetComponent<TankEnemy>().GetIsDestroyed();
                }

                if (tank.gameObject.GetComponent<TankPlayer>())
                {
                    targetIsDestroyed = tank.gameObject.GetComponent<TankPlayer>().GetIsDestroyed();
                }

                if (targetIsDestroyed == false)
                {
                    float distance = Vector3.Distance(transform.position, tank.gameObject.transform.position);

                    if (distance > AgentMinVisibleRange && distance < agentLessDistance)
                    {
						agentLessTank = tank.gameObject;
                        agentLessDistance = distance;
                    }
                }
            }

        }
    }

    private void Move()
    {
        Agent.destination = agentLessTank.transform.position;
    }

    private void RotateTower()
    {
        Vector3 targetDir = agentLessTank.transform.position - transform.position;
        float angleY = Vector3.SignedAngle(targetDir, Tower.transform.forward, Vector3.up);

        fireTarget = false;

        if (angleY < -1.0f * DeadZoneRoteteTower)
        {
            Tower.transform.RotateAround(Tower.transform.position, Tower.transform.up, SpeedRotateTower * Time.deltaTime);
        }
        else if (angleY > DeadZoneRoteteTower)
        {
            Tower.transform.RotateAround(Tower.transform.position, Tower.transform.up, -1.0f * SpeedRotateTower * Time.deltaTime);
        }
        else
        {
            fireTarget = true;
        }
    }

    private void ToFire()
    {
        if (fireTarget && timer > Random.Range(FireDelay - FireDelay / 2.0f, FireDelay + FireDelay / 2.0f))
        {
            if (Physics.Raycast(Tower.transform.position, Tower.transform.TransformDirection(Vector3.forward), out fireRaycastTarget, FireDistanceRange))
            {
                if (fireRaycastTarget.transform != null)
                {
                    Debug.DrawLine(transform.position, fireRaycastTarget.point, Color.red);

                    if (Random.Range(0, 5) == 2)
                    {
                        return;
                    }

                    int iter = Random.Range(0, FireParticle.Length);

                    Instantiate(FireEffect[iter], FireEffectPosition.position, Quaternion.identity);
                    Instantiate(FireParticle[iter], fireRaycastTarget.point, Quaternion.identity);

                    if (fireRaycastTarget.transform.GetComponent<TankEnemy>())
                    {
                        fireRaycastTarget.transform.GetComponent<TankEnemy>().GetDamage(FireDamage[iter]);
                    }

                    if (fireRaycastTarget.transform.GetComponent<TankPlayer>())
                    {
                        fireRaycastTarget.transform.GetComponent<TankPlayer>().GetDamage(FireDamage[iter]);
                    }

                    isFire = true;
                }
            }
        }
    }

    private void UpdateTime()
	{
		timer += Time.deltaTime;

		if (isFire == true)
		{
			isFire = false;
			timer = 0;
		}
	}

	private void Destroy()
	{
		if (hp <= 0)
		{
            IsDestroyed = true;

            Body.transform.parent = null;
            Body.GetComponent<NavMeshObstacle>().enabled = true;
            Body.AddComponent<Rigidbody>().AddExplosionForce(ExplosionForce, Tower.transform.position, ExplosionRadius);
            Instantiate(DestroyEffect, Body.transform);

            Tower.transform.parent = null;
            Tower.GetComponent<NavMeshObstacle>().enabled = true;
            Tower.AddComponent<Rigidbody>().AddExplosionForce(ExplosionForce, Body.transform.position, ExplosionRadius);

            Destroy(gameObject);
        }
    }

	public void GetDamage(uint damage)
	{
		hp -= (int)damage;
	}

    public bool GetIsDestroyed()
    {
        return IsDestroyed;
    }
}
