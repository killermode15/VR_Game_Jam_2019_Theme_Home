using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShadowAI : MonoBehaviour {

    public UnityEvent OnDestinationReached;

	public enum State { WALK, ATTACK, STAGGER, DEATH };
	public State currentState;
	public int hp = 3;

	public Transform targetDestination;
	public Transform behindPlayer;
	public float speed;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

	private void FixedUpdate()
	{
		UpdateFSM();
	}

	private void UpdateFSM()
	{
		switch(currentState)		
		{
			case State.WALK: UpdateWalk(); break;
			case State.ATTACK: UpdateAttack(); break;
			case State.STAGGER: UpdateStagger(); break;
			case State.DEATH: UpdateDeath(); break;
			default: UpdateWalk(); break;
		}	
	}

	private void UpdateWalk()
	{
        Vector3 targetPos = targetDestination.position;
        targetPos.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime * Random.Range(0.5f, 1.0f));
		Debug.Log(Vector3.Distance(transform.position, targetDestination.position));
		if(Vector3.Distance(transform.position, targetPos) < 0.2f)
        {
            Debug.Log("TEST");

            currentState = State.ATTACK;
        }
	}

	private void UpdateAttack()
	{
        //moves behind or above player's head and bows to player's face slowly
        OnDestinationReached.Invoke();
    }

	private void UpdateStagger()
	{
		//gets moved back
		//play stagger anim
		//subtract this object's hp
		 
	}

	private void UpdateDeath()
	{
		//if hp = 0 play dissolving shader
	}

    public void ResetPosition()
    {
        transform.position = startPos;
    }
}
