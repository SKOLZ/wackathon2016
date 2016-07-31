using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using WebSocketSharp;
using SimpleJSON;

public class web_socket : MonoBehaviour {
	public Text speed;
	public Text distance;
	public Text time;

	private string data_speed = "0";
	private string data_distance = "0";
	private string data_time = "0";
	// Use this for initialization
	void Start () {
		//using (var ws = new WebSocket ("ws://10.0.0.22:8080")) {
		var ws = new WebSocket ("ws://10.0.0.165:8080");
			ws.OnOpen += (sender, e) => {
				Debug.Log("Open: ");
			};
			ws.OnMessage += (sender, e) => {
				var message = JSON.Parse(e.Data);
				data_speed = message["speed"];
				data_distance = message["distance"];
				data_time = message["time"];
				// Debug.Log("Speed: " + message["speed"]);
				// Debug.Log("Speed: " + message["speed"]);
				// Debug.Log("Speed: " + message["speed"]);
			};
			ws.OnClose += (sender, e) => {
				Debug.Log("Close:" + e.Code);
			};
			ws.OnError += (sender, e) => {
				Debug.Log("Error: " + e.Message);
			};
		ws.ConnectAsync();
		//}
	}
	
	// Update is called once per frame
	void Update () {
		if(speed.text != data_speed) {
			speed.text = data_speed + " KM/H";
		}
		if(distance.text != data_distance) {
			distance.text = data_distance + " KM";
		}
		if(time.text != data_time) {
			time.text = data_time;
		}
	}

	public float getSpeed() {
		return float.Parse (data_speed);
	}
}
