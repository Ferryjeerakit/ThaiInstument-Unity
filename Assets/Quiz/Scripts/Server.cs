using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
	private Read read;

	//เก็บค่าต่างๆ
	[SerializeField] GameObject loginPanel;
	[SerializeField] GameObject welcomePanel;
	[SerializeField] Text user;
	
	[Space]
	[SerializeField] InputField username;
	[SerializeField] InputField password;

	[SerializeField] Text errorMessages;
	[SerializeField] GameObject progressCircle;


	[SerializeField] Button loginButton;

	
	[SerializeField] string url;
	[Space]
	[SerializeField] GameObject topPanel;
	
	
    

	WWWForm form;

	//ปุ่มlogin
	public void OnLoginBtnClicked ()
	{
		loginPanel.SetActive(true);
		topPanel.SetActive(false);
	}

	public void OnLoginButtonClicked ()
	{
		loginButton.interactable = false;
		progressCircle.SetActive (true);
		StartCoroutine (Login ());
		
	}

	//กด done หน้าต่าง panel ปิด
	public void OnDoneBtnClicked ()
	{
		
		welcomePanel.SetActive (false);
		loginPanel.SetActive(false);
		topPanel.SetActive(false);

		
	}

  //เชื่อม server
	IEnumerator Login () {
    form = new WWWForm ();

    form.AddField ("username", username.text);
    form.AddField ("password", password.text);

    WWW w = new WWW (url, form);
    yield return w;

    if (w.error != null) {
        errorMessages.text = "404 not found!";
        Debug.Log("<color=red>"+w.text+"</color>");//error
    } else {
         if (w.isDone)
        {
            if (w.text.Contains("error"))
            {
                errorMessages.text = "invalid username or password!";
                Debug.Log("<color=red>" + w.text + "</color>");//error
            }
            else
            {
                //open welcome panel
                welcomePanel.SetActive(true);
                user.text = "" + username.text;
				

                //update score
                yield return StartCoroutine(UpdateScore(username.text, password.text, QuizManager.scoreCount));

				
            }
        }

    }

    loginButton.interactable = true;
    progressCircle.SetActive (false);

    w.Dispose ();
	}

	// IEnumerator Login () {
	// 	form = new WWWForm ();

	// 	form.AddField ("username", username.text);
	// 	form.AddField ("password", password.text);

	// 	WWW w = new WWW (url, form);
	// 	yield return w;

	// 	if (w.error != null) {
	// 		errorMessages.text = "404 not found!";
	// 		Debug.Log("<color=red>"+w.text+"</color>");//error
	// 	} else {
	// 		 if (w.isDone)
	// 		{
	// 			if (w.text.Contains("error"))
	// 			{
	// 				errorMessages.text = "invalid username or password!";
	// 				Debug.Log("<color=red>" + w.text + "</color>");//error
	// 			}
	// 			else
	// 			{
	// 				//open welcome panel
	// 				welcomePanel.SetActive(true);
	// 				user.text = username.text;

	// 				//update score
	// 				yield return StartCoroutine(UpdateScore(username.text, password.text, 150)); // Replace 150 with the actual score
	// 			}
	// 		}
	// 		// if (w.isDone) {
	// 		// 	if (w.text.Contains ("error")) {
	// 		// 		errorMessages.text = "invalid username or password!";
	// 		// 		Debug.Log("<color=red>"+w.text+"</color>");//error
	// 		// 	} else {
	// 		// 		//open welcome panel
	// 		// 		welcomePanel.SetActive (true);
	// 		// 		user.text = username.text;

	// 		// 		// Insert data into MySQL table
	// 		// 		yield return StartCoroutine(UpdateScore());
	// 		// 		// WWWForm insertForm = new WWWForm();
	// 		// 		// insertForm.AddField("username", username.text);
	// 		// 		// insertForm.AddField("score", 150); // Change the value as per your requirement
	// 		// 		// WWW insertRequest = new WWW("http://localhost/unity/api/savescore.php", insertForm);
	// 		// 		// yield return insertRequest;
	// 		// 		// if (insertRequest.error != null) {
	// 		// 		// 	Debug.Log("<color=red>"+insertRequest.error+"</color>");//error
	// 		// 		// } else {
	// 		// 		// 	Debug.Log("<color=green>"+insertRequest.text+"</color>");//success
	// 		// 		// }
	// 		// 	}
	// 		// }
	// 	}

	// 	loginButton.interactable = true;
	// 	progressCircle.SetActive (false);

	// 	w.Dispose ();
	// }

	private IEnumerator UpdateScore(string username, string password, int score)
	{
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		form.AddField("password", password);
		form.AddField("score", score.ToString());

		using (WWW www = new WWW("http://localhost/unity/api/savescore.php", form))
		{
			yield return www;

			if (www.error != null)
			{
				Debug.Log("<color=red>" + www.error + "</color>");
			}
			else
			{
				Debug.Log("<color=green>" + www.text + "</color>");
				Debug.Log("Updating score for user: " + username + ", password: " + password);

			}
		}
	}
	
	// IEnumerator SendScore(int score) {
	// 	WWWForm form = new WWWForm();
	// 	form.AddField("score", 100);

	// 	UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity/api/savescore.php", form);
	// 	yield return www.SendWebRequest();

	// 	if (www.result == UnityWebRequest.Result.Success) {
	// 		Debug.Log("Score sent successfully.");
	// 	} else {
	// 		Debug.LogError("Error sending score: " + www.error);
	// 	}
	// }
}
