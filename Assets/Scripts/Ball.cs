using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public static Ball holdedBall;
    public static bool isSomeBallHolded;

    public static float multForFingerForce = 200f;
    public static float forceDiffMagnitudeLimiter = 1f;

    private static Vector2 recentTouchPos;

    [SerializeField] FixedJoystick joystick;
    [SerializeField] Camera camera;
    [SerializeField] TrajectoryRenderer tr;

    public static List<Ball> Balls = new List<Ball>();

    static public float G = 0.667430f;

    bool isPosLocked = false;

    [HideInInspector] public Rigidbody2D rb;
    private LineRenderer lr;
    private Transform transform;
    private Collider2D collider;

    private void FixedUpdate() {

        if(Input.touchCount > 0) recentTouchPos = camera.ScreenToWorldPoint(Input.GetTouch(0).position);

        if(isSomeBallHolded && holdedBall == this) {            
            tr.DrawTrajectory(this, recentTouchPos);

            if(Input.touchCount < 1) {
                isSomeBallHolded = false;
                tr.ClearTrajectory();
                if(!isPosLocked) rb.constraints = RigidbodyConstraints2D.None;
                rb.AddForce(calcForceByFinger());
            }
        } else if(!isSomeBallHolded && Input.touchCount >= 1 && !joystick.isHolded) {
            Vector2 touchPos = camera.ScreenToWorldPoint(Input.GetTouch(0).position);
            if(collider.bounds.Contains(touchPos)) {
                isSomeBallHolded = true;
                holdedBall = this;

                rb.constraints = RigidbodyConstraints2D.FreezePosition;
              }
        }

        if(!(isSomeBallHolded && holdedBall == this)) rb.AddForce(CalcAttractionToBalls());
    }

    Vector2 CalcAttractionToBalls() {

        Vector2 force = new Vector2();

        foreach(Ball ball in Balls) {
            if(ball == this) continue;

            force += CalcAttractToBall(ball);
        }

        return force;
    }

    Vector2 CalcAttractToBall(Ball ball) {

        Vector2 diff = ball.transform.position - transform.position;


        if(diff.magnitude < transform.localScale.x*forceDiffMagnitudeLimiter) {
            float ang = Mathf.Atan(diff.y/diff.x);

            diff.x = Convert.ToSingle(Math.Cos(ang)*forceDiffMagnitudeLimiter);
            diff.y = Convert.ToSingle(Math.Sin(ang)*forceDiffMagnitudeLimiter);

        }

        Vector2 force = diff*G*ball.rb.mass;

        force /= diff.sqrMagnitude;

        return force;
    }

    private void OnEnable() {

        rb = this.GetComponent<Rigidbody2D>();
        lr = this.GetComponent<LineRenderer>();
        transform = this.GetComponent<Transform>();
        collider = this.GetComponent<Collider2D>();

        if(Balls == null) Balls = new List<Ball>();

        this.name = "Ball" + Balls.Count;

        Balls.Add(this);
    }

    private void OnDisable() {
        Balls.Remove(this);
    }

    Vector2 calcForceByFinger() {
        Vector2 force = new Vector2(transform.position.x - recentTouchPos.x, transform.position.y - recentTouchPos.y);

        force *= multForFingerForce*rb.mass;

        return force;
    }

    public void SetValues(Camera cam, TrajectoryRenderer tr, FixedJoystick joystick) {
        this.camera = cam;
        this.tr = tr;
        this.joystick = joystick;
    }

    public void SetRandomValues(bool mass, string size, bool pos, bool vel) {
        if(mass) {
            rb.mass = UnityEngine.Mathf.Round(UnityEngine.Random.Range(10f, 1000f));
        }

        if(size == "by mass") {
            Vector3 scale = new Vector3(rb.mass*0.001f, rb.mass*0.001f, rb.mass*0.001f);
            transform.localScale = scale;
        } else if(size == "rand" || size == "random") {
            float randSize = UnityEngine.Random.Range(0.1f, 10f);
            Vector3 scale = new Vector3(randSize, randSize, randSize);
            transform.localScale = scale;
        }

        if(pos) {
            Debug.Log("before" + transform.position);
            Vector3 position = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0);
            float camZoom = camera.GetComponent<CameraController>().zoom;
            position *= camZoom;
            transform.position += position;
            Debug.Log(transform.position);
        }

        if(vel) {
            Vector2 velocity = new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
            rb.velocity = velocity;
        }
    }

    public void LockPos() {
        SetPosLocking(true);
    }

    public void UnlockPos() {
        SetPosLocking(false); 
    }

    public void TogglePosLocking() {
        SetPosLocking(!isPosLocked);
    }

    public void SetPosLocking(bool val) {
        isPosLocked = val;

        if(val) rb.constraints = RigidbodyConstraints2D.FreezePosition;
        else if(!val) rb.constraints = RigidbodyConstraints2D.None;
    }

    public bool IsPosLocked() {
        return isPosLocked;
    }





    static float ToSingle(double x) {
        return (float)x;
    }

}
