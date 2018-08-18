using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public GameObject obj;
    public Animator ui;

    private float up_speed = 2.0f;
    private Transform visual;
    private Animator animator;

    private Rigidbody2D body;

    private Vector3 _orignalPosition;
    private Quaternion _orignalRotation;

    protected void resetTransform()
    {
        gameObject.transform.position = _orignalPosition;
        gameObject.transform.rotation = _orignalRotation;
    }

    void Awake()
    {
        up_speed = GameParams.UP_SPEED;
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        _orignalPosition = gameObject.transform.position;
        _orignalRotation = gameObject.transform.rotation;
        visual = obj.GetComponent<Transform>();
        animator = obj.GetComponent<Animator>();
        body.Sleep();
    }

    public void Restart()
    {
        resetTransform();
        rotateBird(0.0f, true);
        ui.SetBool("GameOver", false);
        animator.SetBool("IsDead", false);
        body.Sleep();
    }
	
	// Update is called once per frame
	void Update () {

        visual.position = gameObject.transform.position;
        if(!body.IsSleeping())
            rotateBird(ANGLE_EACH_TICK);

        if (GameMgr.State == GameMgr.RunningState.READY)
        {
            if (!body.IsSleeping())
            {
                body.Sleep();
            }
        }

        if (GameMgr.State == GameMgr.RunningState.OVER)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            if (body.IsSleeping())
            {
                body.WakeUp();
                GameMgr.State = GameMgr.RunningState.PLAYING;
            }
            else
            {
                OneClick();
            }
        }
        
    }

    void OneClick()
    {
        body.velocity = transform.up * up_speed;
        rotateBird(ANGLE_EACH_CLICK, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void Die()
    {
        animator.SetBool("IsDead", true);
        GameMgr.GameOver();
        ui.SetBool("GameOver", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Judge" && transform.position.x > collision.transform.position.x)
        {
            GameMgr.Score += 1;
        }
        if (collision.gameObject.name == "Land")
        {
            body.Sleep();
        }
    }


    private static float ANGLE_EACH_CLICK = 30.0f;
    private static float ANGLE_EACH_TICK = -3.0f;

    private void rotateBird(float angle, bool is_set=false)
    {
        Vector3 euler = visual.rotation.eulerAngles;
        if (!is_set)
        {
            euler.z += angle;
        }
        else
        {
            euler.z = angle;
        }
       
        if (angle < 0)
        {
            euler.z = convertAngle(euler.z);
        }
        euler.z = Mathf.Max(-90.0f, euler.z);
        euler.z = Mathf.Min(90.0f, euler.z);
        visual.eulerAngles = euler;
    }

    private float convertAngle(float angle)
    {
        angle -= 180;

        if (angle > 0)
            return angle - 180;

        return angle + 180;
    }
}
