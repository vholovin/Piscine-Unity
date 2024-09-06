using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private static readonly int ARRAY_SIZE = 3;

    public float TimeDelay = 0.5f;
    public GameObject[] Object = new GameObject[ARRAY_SIZE];
    [HideInInspector] public static int[] ObjectCount = new int[ARRAY_SIZE];

    private float timer;

    void Start()
    {
        timer = 0.0f;

        for (int i = 0; i < ARRAY_SIZE; i++)
        {
            ObjectCount[i] = 0;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeDelay + Random.Range(-TimeDelay / 2.0f, TimeDelay / 2.0f))
        {
            int iter = Random.Range(0, ARRAY_SIZE);
            if (ObjectCount[iter] == 0)
            {
                ObjectCount[iter]++;
                Instantiate(Object[iter]);
            }
            timer = 0.0f;
        }
    }
}
