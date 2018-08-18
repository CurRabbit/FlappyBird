using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

    public GameObject pipeline;


	// Use this for initialization
	void Start () {
        //InitializeCreate();	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameMgr.State != GameMgr.RunningState.PLAYING)
        {
            return;
        }
        Checking();
	}

    public void Restart()
    {
        foreach (Transform obj in gameObject.GetComponentInChildren<Transform>()) 
        {
            Destroy(obj.gameObject);
        }
    }


    void InitializeCreate()
    {
        for(int i = 0; i <= 2; i++)
        {
            CreatePipeline(i * GameParams.PIP_INTERNAL, i!=0);
        }
    }

   public  void CreatePipeline(float posX=-1.0f, bool rd_height=true)
    {
        posX = posX == -1.0f ? GameParams.PIP_INIT_X : posX;
        GameObject newPipe = Instantiate(pipeline, gameObject.transform);
        int rd = rd_height ? Random.Range(-2, 8):0;
        newPipe.transform.position = new Vector3(posX, rd * GameParams.PIP_HEIGHT_INTERNAL, 0.0f);
    }

    void Checking()
    {
        float maxX = 0.0f;
        foreach(Transform obj in gameObject.GetComponentInChildren<Transform>())
        {
            maxX = Mathf.Max(maxX, obj.transform.position.x);
            if (obj.position.x <= -GameParams.PIP_INIT_X)
            {
                Destroy(obj.gameObject);
            }
        }

        if (maxX < GameParams.PIP_COUNT * GameParams.PIP_INTERNAL)
        {
            CreatePipeline(maxX + GameParams.PIP_INTERNAL);
        }
    }
}
