using UnityEngine;

public class Balloon : MonoBehaviour
{
    public uint MinSize = 1;
    public uint MaxSize = 25;

    public uint SpeedIncrement = 275;
    public uint SpeedDecrement = 2;

    private float curTime = 0.0f;
    private float lastTime = 0.0f;

    public uint CurSpaceNumber = 5;
    public uint MaxSpaceNumber = 10;
    public float TimeSpaceRegenerate = 0.2f;

    private void Update()
    {
        UpdateRegenerateSpace();
        IfKeyDown();
        CheckSize();
    }

    private void UpdateRegenerateSpace()
    {
        curTime += Time.deltaTime;
        if (CurSpaceNumber < MaxSpaceNumber)
        {
            if ((curTime - lastTime) > TimeSpaceRegenerate)
            {
                CurSpaceNumber++;
                lastTime = curTime;
            }
        }
    }

    private void IfKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && CurSpaceNumber > 0)
        {
            CurSpaceNumber--;
            UpdateSize(SpeedDecrement, SpeedIncrement);
        }
        else
        {
            UpdateSize(SpeedDecrement, 0);
        }

    }
    private void UpdateSize(uint decrement, uint increment)
    {
        Vector2 size = new Vector2(decrement, decrement) - new Vector2(increment, increment);
        Vector2 curSize = gameObject.transform.localScale;

        gameObject.transform.localScale = curSize + size * Time.deltaTime;
    }

    private void CheckSize()
    {
        float size = (gameObject.transform.localScale.x + gameObject.transform.localScale.y) / 2;

        if (size < MinSize || size > MaxSize)
        {
            Debug.Log("Balloon life time: " + Mathf.RoundToInt(curTime) + "s");
            Destroy(gameObject);
        }
    }
}