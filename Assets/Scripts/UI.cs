using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    public Bird bird;
    public Pipe pipe;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickRestart()
    {
        bird.Restart();
        pipe.Restart();
        GameMgr.GameStart();
    }
}
