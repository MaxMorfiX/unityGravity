using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour {

    LineRenderer lr;

    public void DrawTrajectory(Ball ball, Vector2 recentTouchPos) {
        Vector2 force = new Vector2(ball.transform.position.x - recentTouchPos.x, ball.transform.position.y - recentTouchPos.y);
        force *= Ball.multForFingerForce;

        Vector2[] trajectory = Plot(ball.transform.position, force, 2);

        lr.positionCount = trajectory.Length;

        Vector3[] positions = new Vector3[trajectory.Length];

        for(int i = 0; i < positions.Length; i++) {
            positions[i] = trajectory[i];
        }

        lr.SetPositions(positions); 
    }

    public void ClearTrajectory() {
        lr.positionCount = 0;
    }

    private void OnEnable() {
        lr = this.GetComponent<LineRenderer>();
    }

    Vector2[] Plot(Vector2 pos, Vector2 velocity, int steps) {
        Vector2[] results = new Vector2[steps];

        for(int i = 0; i < steps; i++) {
            pos += velocity*i*0.01f;

            // Debug.Log(pos);
            // Debug.Log(velocity*rb.mass);

            results[i] = pos;
        }

        return results;
    }

}
