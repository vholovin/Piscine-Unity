using System.Collections.Generic;
using UnityEngine;

public class CharacterMovePlayer : MonoBehaviour
{

    [HideInInspector] public static List<Collider2D> Characters = new();
    public float OverlapCircleSize = 0.1f;

    void Update()
    {
        MouseControl();
    }

    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButton();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RightMouseButton();
        }
    }

    private void LeftMouseButton()
    {
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if ((Characters.Count > 0 && Input.GetKey(KeyCode.LeftControl)) || Characters.Count == 0)
        {
            Collider2D[] NewCharacters = Physics2D.OverlapCircleAll(new Vector2(MousePosition.x, MousePosition.y), OverlapCircleSize);

            foreach (Collider2D character in NewCharacters)
            {
                if (character.GetComponent<Character>())
                {
                    if (character.GetComponent<Character>().GetSelect() == false)
                    {
                        character.GetComponent<Character>().SetSelect(true);
                        Characters.Add(character);
                    }
                }
            }
        }

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            foreach (Collider2D character in Characters)
            {
                character.GetComponent<Character>().SetPosition(new Vector2(MousePosition.x, MousePosition.y));
            }
        }
    }

    private void RightMouseButton()
    {
        foreach (Collider2D character in Characters)
        {
            character.GetComponent<Character>().SetSelect(false);
        }

        Characters.Clear();
    }
}