﻿using UnityEngine;
using System.Collections;

public class playerManager : MonoBehaviour {
	public web_socket ws;

	public enum PlayerState {
		RIDING = 0,
		RESTING = 1,
		STOPPED = 2
	}
	public Animator playerAnimator;
	private float speed;

	private PlayerState state;
	
	void Start () {
		state = PlayerState.STOPPED;
		speed = 0;
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

	void Update () {
		playerAnimator.SetBool("isResting", this.isResting());
		playerAnimator.SetBool("isRiding", isRiding());
		playerAnimator.SetBool("hasStopped", isStopped());
		speed = ws.getSpeed ();
		bool restStatus = ws.getResting ();
		if (!isStopped()) {
			Vector3 newPos = transform.position;
			newPos.z += speed * Time.deltaTime;
			transform.position = newPos;
			if (speed == 0) {
				state = PlayerState.STOPPED;
			} else if (restStatus) {
				Debug.Log ("resting!");
				state = PlayerState.RESTING;
			}
		} else {
			if (speed != 0) {
				state = PlayerState.RIDING;
			}
		}
	}

}
