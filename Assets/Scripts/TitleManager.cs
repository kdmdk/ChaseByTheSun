using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        if (RunawayManManager.isClear)
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(RunawayManManager.score);
        }
    }

    private void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Main");
        }
        */
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("Main");
    }
}
