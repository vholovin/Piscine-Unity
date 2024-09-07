using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TankPlayer : MonoBehaviour
{
    private NavMeshAgent Agent;

    public GameObject Body;
    public GameObject Tower;

    public uint SpeedMove = 5;
    public uint SpeedRotateBody = 50;

    public uint MouseSensitivityX = 5;

    public Slider BoostSlider;
    public Image BoostFill;

    public uint BoostMin = 1;
    public uint BoostMax = 2;
    private uint boost = 1;

    public float BoostEnergyUsing = 0.3f;
    public float BoostEnergyRegenerate = 0.1f;
    private float boostEnergy;

    private readonly float fillMinValueUI = 0.0f;
    private Color fillMinColorUI = Color.black;

    private readonly float fillLowValueUI = 33.3f;
    private Color fillLowColorUI = Color.red;

    private readonly float fillMiddleValueUI = 66.6f;
    private Color fillMiddleColorUI = Color.yellow;

    private readonly float fillMaxValueUI = 100.0f;
    private Color fillMaxColorUI = Color.white;

    public Image FireCrossHair;
    public uint FireDistanceRange = 50;
    private RaycastHit fireRaycastTarget;
    private bool fireTarget = false;

    private Color fireQuietlyColor = Color.white;
    private Color fireTargetColor = Color.red;

    public uint[] FireDamage = new uint[2] { 2, 20 };
    public GameObject[] FireParticle = new GameObject[2];

    public GameObject[] FireEffect = new GameObject[2];
    public Transform FireEffectPosition;

    public Text AmmoMissileUI;
    public uint AmmoMissileStart = 100;
    private uint ammoMissile;

    public Text HpUI;
    public int HpStart = 100;
    private int hp;

    public GameObject DestroyEffect;
    public uint ExplosionForce = 1000;
    public uint ExplosionRadius = 10;
    private bool IsDestroyed = false;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        BoostSlider.minValue = fillMinValueUI;
        BoostSlider.maxValue = fillMaxValueUI;
        boostEnergy = BoostSlider.maxValue;

        ammoMissile = AmmoMissileStart;
        hp = HpStart;

        Body.GetComponent<NavMeshObstacle>().enabled = false;
        Tower.GetComponent<NavMeshObstacle>().enabled = false;
    }
    private void Update()
    {
        if (!IsDestroyed)
        {
            Move();
            RotateBody();
            RotateTower();

            BoostKey();
            UpdateBoostUI();

            SearchFireTarget();
            UpdateFireCrossHairUI();

            ToFire();

            UpdateAmmoUI();
            UpdateHpUI();

            Destroy();
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Agent.Move(boost * SpeedMove * Time.deltaTime * Agent.transform.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Agent.Move(-1.0f * boost * SpeedMove * Time.deltaTime * Agent.transform.forward);
        }
    }

    private void RotateBody()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Agent.transform.RotateAround(Agent.transform.position, Vector3.up, SpeedRotateBody * boost * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Agent.transform.RotateAround(Agent.transform.position, Vector3.down, SpeedRotateBody * boost * Time.deltaTime);
        }
    }

    private void RotateTower()
    {
        float x = Tower.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSensitivityX;
        Tower.transform.localEulerAngles = new Vector3(0, x, 0);
    }

    private void BoostKey()
    {
        boost = BoostMin;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (boostEnergy > BoostSlider.minValue
                && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                    Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                boostEnergy -= BoostEnergyUsing;
                boost = BoostMax;
            }
            else
            {
                boost = BoostMin;
            }
        }
        else
        {
            if (boostEnergy < BoostSlider.maxValue)
            {
                boostEnergy += BoostEnergyRegenerate;
            }
        }
    }

    private void UpdateBoostUI()
    {
        BoostSlider.value = boostEnergy;

        if (boostEnergy <= fillLowValueUI)
        {
            BoostFill.color = fillLowColorUI;
        }
        else if (boostEnergy > fillLowValueUI && boostEnergy <= fillMiddleValueUI)
        {
            BoostFill.color = fillMiddleColorUI;
        }
        else if (boostEnergy > fillMiddleValueUI && boostEnergy <= fillMaxValueUI)
        {
            BoostFill.color = fillMaxColorUI;
        }
    }

    private void SearchFireTarget()
    {
        if (Physics.Raycast(Tower.transform.position, Tower.transform.TransformDirection(Vector3.forward), out fireRaycastTarget, FireDistanceRange))
        {
            if (fireRaycastTarget.transform != null)
            {
                Debug.DrawLine(transform.position, fireRaycastTarget.point, Color.red);
            }

            if (fireRaycastTarget.transform.CompareTag(transform.tag))
            {
                fireTarget = true;
            }
            else
            {
                fireTarget = false;
            }
        }
        else
        {
            fireTarget = false;
        }
    }

    private void UpdateFireCrossHairUI()
    {
        if (fireTarget)
        {
            FireCrossHair.color = fireTargetColor;
        }
        else
        {
            FireCrossHair.color = fireQuietlyColor;
        }
    }

    private void ToFire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(FireEffect[0], FireEffectPosition.position, Quaternion.identity);

            if (fireRaycastTarget.transform != null)
            {
                Instantiate(FireParticle[0], fireRaycastTarget.point, Quaternion.identity);

                if (fireRaycastTarget.transform.GetComponent<TankEnemy>())
                {
                    fireRaycastTarget.transform.GetComponent<TankEnemy>().GetDamage(FireDamage[0]);
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1) && ammoMissile > 0)
        {
            Instantiate(FireEffect[1], FireEffectPosition.position, Quaternion.identity);
            ammoMissile--;

            if (fireRaycastTarget.transform != null)
            {
                Instantiate(FireParticle[1], fireRaycastTarget.point, Quaternion.identity);

                if (fireRaycastTarget.transform.GetComponent<TankEnemy>())
                {
                    fireRaycastTarget.transform.GetComponent<TankEnemy>().GetDamage(FireDamage[1]);
                }
            }
        }
    }

    private void UpdateAmmoUI()
    {
        float percentAmmo = (ammoMissile * 100.0f / AmmoMissileStart);

        if (percentAmmo == fillMinValueUI)
        {
            AmmoMissileUI.color = fillMinColorUI;
        }
        else if (percentAmmo <= fillLowValueUI)
        {
            AmmoMissileUI.color = fillLowColorUI;
        }
        else if (percentAmmo <= fillMiddleValueUI)
        {
            AmmoMissileUI.color = fillMiddleColorUI;
        }
        else if (percentAmmo <= fillMaxValueUI)
        {
            AmmoMissileUI.color = fillMaxColorUI;
        }

        AmmoMissileUI.text = ammoMissile.ToString() + " / " + AmmoMissileStart.ToString();
    }

    private void UpdateHpUI()
    {
        float percentHp = (hp * 100.0f / HpStart);

        if (percentHp <= fillMinValueUI)
        {
            HpUI.color = fillMinColorUI;
        }
        else if (percentHp <= fillLowValueUI)
        {
            HpUI.color = fillLowColorUI;
        }
        else if (percentHp <= fillMiddleValueUI)
        {
            HpUI.color = fillMiddleColorUI;
        }
        else if (percentHp <= fillMaxValueUI)
        {
            HpUI.color = fillMaxColorUI;
        }

        HpUI.text = hp.ToString() + " / " + HpStart.ToString();
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
