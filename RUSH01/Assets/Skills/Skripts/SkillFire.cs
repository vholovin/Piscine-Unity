using UnityEngine;

public class SkillFire : MonoBehaviour
{
	public uint Damage = 10;
	public uint TimeAction = 3;
	public uint SpeedMove = 20;

	public GameObject Explosion;
    private bool exp = false;

    public GameObject Light;
	private readonly float lightAction = 0.1f;

	private ParticleSystem ps;

	void Start()
	{
		ps = gameObject.GetComponent<ParticleSystem>();
		ps.Stop();

		var main = ps.main;
		main.duration = TimeAction;

        ps.Play();
	}

	private void Update()
	{
		if (!exp)
		{
            gameObject.transform.Translate(SpeedMove * Time.deltaTime * Vector3.forward);
        }
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!exp)
		{
            exp = true;
            Instantiate(Explosion, gameObject.transform);

            Destroy(Light, lightAction);

			if (other.gameObject.GetComponent<EnemyLogic>())
			{
                other.gameObject.GetComponent<EnemyLogic>().TakeDamage(Damage);
            }
        }	
	}

}
