using UnityEngine;
using System.Collections;

public class roadManager : MonoBehaviour {

	public Transform player;
	public int chunk;
	
	private float roadLength;
	private const float EPS = 0.01f;

	void Start () {
		MeshRenderer spriteRenderer = GetComponent<MeshRenderer>();
		roadLength = spriteRenderer.bounds.size.z;
	}
	
	void FixedUpdate () {
		if (player.position.z > chunk * roadLength) {
			chunk += 2;
			Vector3 newPos = transform.position;
			newPos.z += 2.0f * roadLength - EPS;
			transform.position = newPos;
		}
	}
}
