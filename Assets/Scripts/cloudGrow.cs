using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudGrow : MonoBehaviour {


	//user input
	public float lifeTime = 1.0f; //time of growth
	public float startTime = 0.0f;//time to start
	public Vector3 myCenter =new Vector3(0.0f, 0.0f, 0.0f);//center to rotate around
	public float sunLightSpeed = 0.01f;
	public float cloudColspeed = 0.02f;
	public float centerOffset = 0.0f;

	//private member variable
	private Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);
	Renderer rend;
	private Color cloudLow  = new Color(150.0f, 150.0f, 150.0f)/255.0f;
	private Color  cloudHigh  = new Color(200.0f, 200.0f, 200.0f)/255.0f;
	private Color sunLow = new Color (50.0f, 50.0f, 50.0f)/255.0f;
	private Color sunHigh = new Color (0.0f, 0.0f, 0.0f)/255.0f;
	private float radius = 1.0f;
	private float centripetalSpeed = 0.05f; 
	private Vector3 diskCenter = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 centerVec = new Vector3(0.0f, 0.0f, 0.0f);


	// Use this for initialization
	void Start () {
		//private member variables;
		if (lifeTime == 0.0f) {
			speed = transform.localScale;
		} else {
			speed = transform.localScale / lifeTime;
		}
		gameObject.transform.localScale =new Vector3 (0.0f, 0.0f, 0.0f);
		//shader
		rend = GetComponent<Renderer>();
		rend.material.shader = Shader.Find("QuantumTheory/QuantumMetaCloud-HardAlphaBlend");
		//movement
		diskCenter = myCenter + new Vector3(0.0f, transform.position.y - myCenter.y, 0.0f);
		centerVec = transform.position - diskCenter;
	}
	
	// Update is called once per frame
	void Update () {
		float currTime = Time.fixedTime;
		if (currTime >= startTime ){ 
			float liveTime = currTime - startTime;
			if (liveTime < lifeTime) {//while growing
				gameObject.transform.localScale += speed*Time.deltaTime;
			}
			float sunLerp = 0.0f;
			if ((currTime - startTime) % 5 < 1.0f) {
				sunLerp = 2* Mathf.Abs (((currTime * sunLightSpeed*10) % 1 )- 0.5f);
			} else {
				sunLerp = 2* Mathf.Abs (((currTime * sunLightSpeed) % 1 )- 0.5f);
			}

			float cloudLerp = 2* Mathf.Abs (((currTime * cloudColspeed) % 1) - 0.5f);

			Color sunCol = Color.Lerp (sunLow, sunHigh, sunLerp);
			Color cloudCol = Color.Lerp (cloudLow, cloudHigh, cloudLerp);
			rend.material.SetColor ("_SunColor", sunCol);
			rend.material.SetColor ("_AmbientColor", cloudCol);

			//for centripetal movement:
			float centerLerp = 0.9f + 0.1f*Mathf.Sin(centerOffset+2*Mathf.PI*currTime*centripetalSpeed);
			transform.position = diskCenter + centerVec * centerLerp;


		}
	}
}
