using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShadowMoveTowards : MonoBehaviour
{
    public Transform targetTrans;

    // List of vectors where in the shadow will back off towards
    // Original position of this shadow is already included as default
    public List<Vector3> startingPoints;

    public float speed = 1.0f;

    // If true, shadow moves towards player
    // If false, shadow backs off to a random staring point
    public bool isMovingTowards = true;

    public UnityEvent OnDamaged = new UnityEvent();
    public UnityEvent OnReachPlayer = new UnityEvent();


    Vector3 curBackOffPos;

    void Start()
    {
        startingPoints.Add(transform.position);

        curBackOffPos = transform.position;
    }
    
    void Update()
    {
        if ( isMovingTowards ) {
            Vector3 targetPos = new Vector3(targetTrans.position.x, transform.position.y, targetTrans.position.z);

            transform.position = Vector3.MoveTowards(transform.position,targetPos,Time.deltaTime * speed * Random.Range(0.5f,1.5f));
            transform.LookAt(targetTrans);

            if ( ( targetTrans.position - transform.position ).magnitude <= 0.3f ) {
                ReachedPlayer();
            }
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position,curBackOffPos,Time.deltaTime * speed * Random.Range(1.5f,2.0f));
            transform.LookAt(curBackOffPos);
            if (( curBackOffPos - transform.position).magnitude <= 0.1f ) {
                MoveTowardsAgain();
            }
        }
    }

    void MoveTowardsAgain( ) {
        isMovingTowards = true;
    }

    /// <summary>
    /// Called when the shadow reaches the player
    /// </summary>
    void ReachedPlayer( ) {
        OnReachPlayer.Invoke();
    }

    /// <summary>
    /// Call this when the shadow is damaged. How that is, depends on you. XD
    /// Call ths only once pls.
    /// 
    /// Shadow automatically moves towards player if it reaches its back off position
    /// </summary>
    public void DamageShadow( ) {
        isMovingTowards = false;

        curBackOffPos = startingPoints[Random.Range(0, startingPoints.Count-1)];
    }
}
