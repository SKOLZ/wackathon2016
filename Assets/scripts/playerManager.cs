using UnityEngine;
using System.Collections;

public class playerManager : MonoBehaviour {

	public enum PlayerState {
		RIDING = 0,
		RESTING = 1
	}

	public float speed;

	private PlayerState state;
	
	void Start () {
		state = PlayerState.RIDING;
	}

	void Update () {
		if (isRiding()) {
			// get speed
			Vector3 newPos = transform.position;
			newPos.z += speed * Time.deltaTime;
			transform.position = newPos;
		}
	}

	public bool isRiding () {
		return state == PlayerState.RIDING;
	}

	public bool isResting () {
		return state == PlayerState.RESTING;
	}
}
