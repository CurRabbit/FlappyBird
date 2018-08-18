using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHelper: MonoBehaviour {

    private float speed = 1.0f;
    private static float BACK_WIDTH = 8.0f;

	// Use this for initialization
	void Start () {
        speed = GameParams.MOVE_SPEED;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameMgr.State == GameMgr.RunningState.OVER)
        {
            return;
        }
        foreach( Transform obj in gameObject.GetComponentInChildren<Transform>())
        {
            setBGPos(obj);
        }
    }

    float nextPosX(float px)
    {
        float moveDelta = Time.deltaTime * speed;
        px -= moveDelta;
        if(px < - BACK_WIDTH)
        {
            px += 2 * BACK_WIDTH;
        }
        return px;
    }

    void setBGPos(Transform bg)
    {
        bg.position = new Vector3(nextPosX(bg.position.x) , bg.position.y, 0.0f);
    }
}
 