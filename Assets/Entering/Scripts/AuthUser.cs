using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AuthUser : MonoBehaviour {

	//000webhost return after echo
	//<!-- Hosting24 Analytics Code -->

	public Canvas canvas;
	
#if UNITY_EDITOR
	private new static string name = "nkaljo@gmail.com";
#elif UNITY_ANDROID
	private new static string name = null;
	private AndroidJavaObject email = null;
	private AndroidJavaObject activityContext = null;
#endif
	private string scriptURL = "http://mydb.freeiz.com/scripts/RegLog.php";//"localhost/UnityGrave/RegLog.php";

	// Use this for initialization
	void Start () {
#if !UNITY_EDITOR
		if (email == null) {
			using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				activityContext = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			}

			using(AndroidJavaClass pluginClass = new AndroidJavaClass("com.kaljo.unity.Email")){
				if (pluginClass !=null){
					email = pluginClass.CallStatic<AndroidJavaObject>("instance");
					email.Call ("setContext", activityContext);
					name = email.Call<string>("getEmail");
				}

			}
		}
#endif
		StartCoroutine ("regLog");
	}

	IEnumerator regLog(){
		WWWForm form = new WWWForm ();
		form.AddField ("email", name);

		WWW script = new WWW (scriptURL, form);
		yield return script;

		if (script.error != null) {
			//please check your internet connection or something like that
			Debug.Log (script.error);
		} else {
			//Debug.Log(script.text);
			if (script.text.StartsWith("registered")){ //registered for first time
				canvas.gameObject.SetActive(true);
			}else if (script.text.StartsWith("logined")){ //logined with active username
				//Debug.Log("level loaded login");
				Application.LoadLevel("GameScene");
			}else{ //registered but didn't set username yet
				canvas.gameObject.SetActive(true);
			}
		}

	}

	public static string getEmail(){
		return name;
	}

}


















