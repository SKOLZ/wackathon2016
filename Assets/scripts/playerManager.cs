using UnityEngine;
using System.Collections;

public class playerManager : MonoBehaviour {
	public web_socket ws;

	public enum PlayerState {
		RIDING = 0,
		RESTING = 1,
		STOPPED = 2
	}

	private float speed;

	private PlayerState state;
	
	void Start () {
		state = PlayerState.STOPPED;
		speed = 0;
	}

	void Update () {
		speed = ws.getSpeed ();
		var isResting = ws.getResting ();
	
		if (!isStopped()) {
			Vector3 newPos = transform.position;
			newPos.z += speed * Time.deltaTime;
			transform.position = newPos;
			if (speed == 0) {
				state = PlayerState.STOPPED;
			} else if (isResting) {
				Debug.Log ("resting!");
				state = PlayerState.RESTING;
			}
		} else {
			if (speed != 0) {
				state = PlayerState.RIDING;
			}
		}
	}

	public bool isRiding () {
		return state == PlayerState.RIDING;
	}

	public bool isResting () {
		return state == PlayerState.RESTING;
	}

	public bool isStopped () {
		return state == PlayerState.STOPPED;
	}
}
