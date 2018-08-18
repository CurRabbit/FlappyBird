using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMgr : MonoBehaviour {

    public enum RunningState
    {
        READY,
        PLAYING,
        OVER
    };
    public static RunningState State = RunningState.READY;

    public static int Score = 0;
    public static bool Creating = false;

    public Text ScoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ScoreText.text =  GameMgr.Score.ToString();
	}

    public static void GameStart()
    {
        State = RunningState.READY;
        Score = 0;
    }

    public static void GameOver()
    {
        State = RunningState.OVER;
    }



}
