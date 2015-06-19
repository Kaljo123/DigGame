using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

	private Touch t;
	public float limitLeft = -1000;
	public float limitRight = 1000;
	
	void Update(){
		
		if (Input.touchCount > 0) {
			t = Input.touches[0];
			
			if (t.phase == TouchPhase.Moved){
				
				gameObject.transform.position -= new Vector3(t.deltaPosition.x,0,0) * Time.deltaTime;
				if (gameObject.transform.position.x > limitRight){
					gameObject.transform.position = new Vector3(limitRight, gameObject.transform.position.y, gameObject.transform.position.z);
				}else if (gameObject.transform.position.x < limitLeft){
					gameObject.transform.position = new Vector3(limitLeft, gameObject.transform.position.y, gameObject.transform.position.z);
				}
			}
		}
	}

}
