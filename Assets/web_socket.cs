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

	private float value_speed = 0;
	private string data_speed = "0";
	private string data_distance = "0";
	private string data_time = "00:00";
	private Boolean data_resting = false;
	// Use this for initialization
	void Start () {
		//using (var ws = new WebSocket ("ws://10.0.0.22:8080")) {
		var ws = new WebSocket ("ws://10.0.0.165:8080");
			ws.OnOpen += (sender, e) => {
				Debug.Log("Open: ");
			};
			ws.OnMessage += (sender, e) => {
				var message = JSON.Parse(e.Data);
				data_speed = Math.Round(value_speed)  + " KM/H";
				data_distance = message["distance"] + " KM";
				var raw_time = float.Parse(message["time"]);
				var seconds = Math.Round(raw_time / 60).ToString("00");
				var minutes = Math.Round(raw_time % 60).ToString("00");
				data_time = seconds + ":" + minutes;
				data_resting = message["resting"] == "true";
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
			speed.text = data_speed;
		}
		if(distance.text != data_distance) {
			distance.text = data_distance;
		}
		if(time.text != data_time) {
			time.text = data_time;
		}
	}

	public float getSpeed() {
		return Math.Min(value_speed * 0.8f, 80);
	}

	public Boolean getResting() {
		return data_resting;
	}
}
