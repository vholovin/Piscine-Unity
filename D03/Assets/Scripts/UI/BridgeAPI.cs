using UnityEngine;

public class BridgeAPI : MonoBehaviour
{
    public GameObject Game;
    private gameManager gm;

    void Start()
    {
        gm = gameManager.gm;
    }

    public int GetHP()
    {
        return (gm.playerHp);
    }

    public int GetEnergy()
    {
        return (gm.playerEnergy);
    }

    public void SetEnergy(int energy)
    {
        gm.playerEnergy += energy;
    }

    public void SetSpeed(float speed)
    {
        gm.changeSpeed(speed);
    }

    public void SetPause(bool status)
    {
        gm.pause(status);
    }

    public bool GetLastWave()
    {
        return (gm.lastWave);
    }

    public void GameOver()
    {
        gm.gameOver();
    }
    public void GameWin()
    {
        gm.gameWin();
    }

    public bool CheckLastEnemy()
    {
        if (gm.lastWave == true)
        {
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
            foreach (GameObject spawner in spawners)
            {
                if (spawner.GetComponent<ennemySpawner>().isEmpty == false || spawner.transform.childCount > 0)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}
