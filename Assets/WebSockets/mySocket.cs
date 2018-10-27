using UnityEngine;
using WebSocketSharp;
using System.Collections;
using System.Threading;

public class mySocket : MonoBehaviour {
	private WebSocket ws;
	//System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

	public HandAnimController[] fingers;


	changePosition cp;
	public float f1 = 0, f2 = 0, f3 = 0, f4 = 0;

	public float threshValue = 0.1f;

	float time=0;
	float timenew=0;
	float delay=0;

	float tf1, tf2, tf3, tf4;



	void Start() {
		ws = new WebSocket("ws://192.168.43.19:10000/");
		time = System.DateTime.Now.Millisecond;

		//ws.OnOpen += OnOpenHandler;
		ws.OnMessage += OnMessageHandler;
		ws.OnClose += OnCloseHandler;

		ws.ConnectAsync(); 
	}

	private void OnOpenHandler(object sender, System.EventArgs e) {
		Debug.Log("WebSocket connected!");
		Thread.Sleep(3000);

		ws.SendAsync("connected!", OnSendComplete);
	}

	private void OnMessageHandler(object sender, MessageEventArgs e) {
	//	Debug.Log("WebSocket server said: " + delay);
		timenew = System.DateTime.Now.Millisecond;
		delay = timenew - time;
		time = System.DateTime.Now.Millisecond;
		string[] vec3 = e.Data.Split (','); 
		tf1 = float.Parse (vec3 [0]);
		tf2 = float.Parse (vec3 [1]);
		tf3 = float.Parse (vec3 [2]);
		tf4 = float.Parse (vec3 [3]);

		//float x = float.Parse (e.Data);
		//cp.changepostion (x);
		//Thread.Sleep(3000);
		//ws.CloseAsync();
	}

	private void OnCloseHandler(object sender, CloseEventArgs e) {
		Debug.Log("WebSocket closed with reason: " + e.Reason);
	}

	private void OnSendComplete(bool success) {
		Debug.Log("Message sent successfully? " + success);
	}

	public void Update()
	{

		fingers [0].animationRange = process(f1, tf1);
		fingers [1].animationRange = process(f2, tf2);
		fingers [2].animationRange = process(f3, tf3);
		fingers [3].animationRange = process(f4, tf4);

		Debug.Log (fingers [0].animationRange + " " + fingers [1].animationRange + " " + fingers [2].animationRange + " " + fingers [3].animationRange);
 
		//gameObject.transform.position = new Vector3 (x-20, 0, y-20);
	}
		

	float process(float a, float b){
		if (Mathf.Abs (a - b) > threshValue) {
			return b;
		} else {
			return a;
		}
	}


}