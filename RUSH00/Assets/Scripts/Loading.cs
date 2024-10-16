﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

/*    void Start ()
    {
        StartCoroutine(loadAsync("level1"));
    }
*/
    private IEnumerator loadAsync(string levelName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        if(!operation.isDone)
        {
            while (!operation.isDone)
            {
                Debug.Log(operation.progress);
                yield return operation.isDone;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
    }
}