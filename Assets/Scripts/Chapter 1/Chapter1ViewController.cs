using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1ViewController : MonoBehaviour {

	public GameObject canvasGame;
	public GameObject canvasOptions;

	public void pressOptionsButton()
	{
		this.canvasGame.SetActive(false);
		this.canvasOptions.SetActive(true);
	}

	public void pressResumeButton()
	{
		this.canvasGame.SetActive(true);
		this.canvasOptions.SetActive(false);
	}

	public void pressReturnToChapterButton()
	{
		ViewController._fromChapter = true;
		SceneManager.LoadScene(1);
	}
}
