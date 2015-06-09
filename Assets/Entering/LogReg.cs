using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogReg : MonoBehaviour {

	private string scriptURL = "http://mydb.freeiz.com/scripts/updateGameNick.php";
	private string email;

	public InputField nick;
	public Button done;


	public void onClickDone(){
		email = AuthUser.getEmail();
		StartCoroutine ("updateNick");
		//Application.LoadLevel ("SinglePGame");
		Debug.Log ("level loaded");
	}

	IEnumerator updateNick(){
		WWWForm form = new WWWForm ();
		form.AddField ("nick", nick.text);
		form.AddField ("email", email);

		WWW script = new WWW (scriptURL, form);
		yield return script;
		
		
		if (script.error != null) {
			//please check your internet connection or something like that
			Debug.Log (script.error);
		} else {
			Debug.Log(script.text);
		}
		
	}

}
