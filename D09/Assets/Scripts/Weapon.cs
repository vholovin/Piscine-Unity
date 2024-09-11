using UnityEngine;

public class Weapon : MonoBehaviour
{
    public uint FireDamage = 10;
    public uint FireDistanceRange = 50;
    public float FireTimer = 1f;

    private bool fireTarget = false;
    private float timer;

    public bool IsAreaDamage = true;
    public int CountFires = 10;
    public Vector2 RangeAngleX = new(-20f, 20f);
    public Vector2 RangeAngleY = new(-10f, 10f);

    private float[] rotateX;
    private float[] rotateY;

    public GameObject ShotEffect;
    public Transform[] ShotEffectPos;

    public GameObject EnemyEffect;
    public GameObject[] FallEffect;

    private GameObject mainCamera;
    private Animator animator;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rotateX = new float[CountFires];
        rotateY = new float[CountFires];

        for (int i = 0; i < CountFires; i++)
        {
            rotateX[i] = Random.Range(RangeAngleX[0], RangeAngleX[1]);
            rotateY[i] = Random.Range(RangeAngleY[0], RangeAngleY[1]);
        }
    }

    private void Update()
    {
        UpdateTarget();
        UpdateTimer();

        DebugRay();
    }

    private void UpdateTarget()
    {
        RaycastHit fireRaycastTarget;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out fireRaycastTarget, FireDistanceRange))
        {
            if (fireRaycastTarget.transform != null)
            {
                fireTarget = (fireRaycastTarget.transform.GetComponent<Enemy>()) ? true : false;
            }
        }
        else
        {
            fireTarget = false;
        }
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    public void ToShot()
    {
        if (timer < FireTimer)
        {
            return;
        }


        timer = 0;
        animator.SetTrigger("TrShot");

        foreach (Transform ShotPos in ShotEffectPos)
        {
            Instantiate(ShotEffect, ShotPos.transform.position, ShotPos.transform.rotation, mainCamera.transform);
        }

        if (IsAreaDamage)
        {
            AreaShots();
        }
        else
        {
            SingleShot();
        }
    }

    private void AreaShots()
    {
        RaycastHit areaRaycastTarget;
        Vector3 position = mainCamera.transform.position;
        Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward);
        Vector3 forwardRandom;


        for (int i = 0; i < CountFires; i++)
        {
            forwardRandom = forward;
            forwardRandom = Quaternion.AngleAxis(rotateX[i], Vector3.up) * forwardRandom;
            forwardRandom = Quaternion.AngleAxis(rotateY[i], Vector3.forward) * forwardRandom;

            Physics.Raycast(position, forwardRandom, out areaRaycastTarget, FireDistanceRange);

            TakeDamageAndEffect(areaRaycastTarget);
        }
    }

    private void SingleShot()
    {
        RaycastHit singleRaycastTarget;
        Vector3 position = mainCamera.transform.position;
        Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward);

        Physics.Raycast(position, forward, out singleRaycastTarget, FireDistanceRange);

        TakeDamageAndEffect(singleRaycastTarget);
    }

    private void TakeDamageAndEffect(RaycastHit raycastTarget)
    {
        if (raycastTarget.transform == null)
        {
            return;
        }

        if (raycastTarget.transform.GetComponent<Enemy>())
        {
            raycastTarget.transform.GetComponent<Enemy>().GetDamage(FireDamage);
            Instantiate(EnemyEffect, raycastTarget.point, Quaternion.LookRotation(raycastTarget.normal), raycastTarget.transform);
        }
        else
        {
            foreach (GameObject effect in FallEffect)
            {
                Instantiate(effect, raycastTarget.point, Quaternion.LookRotation(raycastTarget.normal));
            }
        }
    }

    public bool GetFireTarget()
    {
        return fireTarget;
    }

    private void DebugRay()
    {
        Vector3 position = mainCamera.transform.position;
        Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward) * FireDistanceRange;
        Color color = (fireTarget) ? Color.red : Color.white;

        Debug.DrawRay(position, forward, color);

        if (IsAreaDamage)
        {
            Vector3 forwardRandom;

            for (int i = 0; i < CountFires; i++)
            {
                forwardRandom = forward;
                forwardRandom = Quaternion.AngleAxis(rotateX[i], Vector3.up) * forwardRandom;
                forwardRandom = Quaternion.AngleAxis(rotateY[i], Vector3.forward) * forwardRandom;

                Debug.DrawRay(position, forwardRandom, Color.yellow);
            }
        }
    }
}