using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour {
	
	public enum MenuState {
		LOGIN = 0,
		TRANSITION = 1,
		MENU = 2
	}

	public GameObject loginForm;
	public GameObject menuForm;
	public Transform mainCamera;
	public float cameraMoveSpeed;
	public float cameraFinalPos;

	public MenuState state = MenuState.LOGIN;

	public void login () {
		state = MenuState.TRANSITION;
		loginForm.SetActive(false);		
	}

	public void Update () {
		if (inTransition()) {
			Vector3 newPos = mainCamera.position;
			newPos.x += cameraMoveSpeed * Time.deltaTime;
			mainCamera.position = newPos;
		}
		if (!isInMenu() && mainCamera.position.x >= cameraFinalPos) {
			state = MenuState.MENU;
			menuForm.SetActive(true);
		}
	}

	private bool inTransition() {
		return state == MenuState.TRANSITION;
	}

	private bool isInMenu() {
		return state == MenuState.MENU;
	}

	public void playFreeRideMode () {
		SceneManager.LoadScene("game");
	}

	public void exit () {
		Application.Quit();
	}
}
