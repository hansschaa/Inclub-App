using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class ViewController : MonoBehaviour 
{

	//Global Variables
	public static bool _fromChapter;
	
	//Views
	[Header("Views")]
	public GameObject _titleScreenView;
	public GameObject _contactView;
	public GameObject _chapterListView;
	public GameObject _introductionView1;
	public GameObject _introductionView2;
	public GameObject _happyTraveler;
	public GameObject _loadingView;

	//Buttons
	[Header("Buttons")]
	public GameObject _backButton;

	public GameObject _nextButton;
	public GameObject _socialNetworkButton;

	//Variables
	[Header("Variables")]
	public float _delayChapter;
	public Camera mainCamera;

	public void Start()
	{
		if(ViewController._fromChapter)
		{
			ViewController._fromChapter = false;
			this.updateView(_chapterListView, _titleScreenView, true, false);
		}

		if(this._titleScreenView.activeInHierarchy)
		{
			this._backButton.SetActive(false);
			this._nextButton.SetActive(false);
		}
	}

	public void pressHappyTraveler()
	{
		this.updateView(this._happyTraveler,this._titleScreenView,true, false);
	}

	public void pressBackButton()
	{
		if(this._introductionView1.activeInHierarchy)
		{
			this.updateView(this._titleScreenView,this._introductionView1,false, false);
		}

		else if(this._introductionView2.activeInHierarchy)
		{
			this.updateView(this._introductionView1,this._introductionView2,true, true);
		}

		else if(this._happyTraveler.activeInHierarchy)
		{
			this.updateView(this._titleScreenView,this._happyTraveler,false, false);
		}

		else if(this._chapterListView.activeInHierarchy)
		{
			this.updateView(this._introductionView2,this._chapterListView,true, true);
		}
	}

	public void pressNextButton()
	{
		if(this._introductionView1.activeInHierarchy)
		{
			this.updateView(this._introductionView2,this._introductionView1,true, true);
		}

		else if(this._introductionView2.activeInHierarchy)
		{
			this.updateView(this._chapterListView,this._introductionView2,true, false);
		}
	}

	public void pressSocialNetworkButton()
	{
		this._contactView.SetActive(true);
	}	

	public void pressPlayButton()
	{
		// var doorway = new DoorwayTransition()
		// {
		// 	// nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
		// 	duration = 1.0f,
		// 	perspective = 1.5f,
		// 	depth = 3f,
		// 	runEffectInReverse = false
		// };
		// TransitionKit.instance.transitionWithDelegate( doorway );
		this.updateView(this._introductionView1,this._titleScreenView, true, true);
	}

	public void pressChapterButton(int chapterNumber)
	{
		this.updateView(this._loadingView, this._chapterListView, false, false);
		this.mainCamera.backgroundColor = Color.gray;
		StartCoroutine(waitThenCallback(this._delayChapter, () => 
        { 
			 SceneManager.LoadScene(chapterNumber);
		}));
	}

	public void pressExitButton()
	{
		Application.Quit();
	}

	private void updateView(GameObject activeView, GameObject inactiveView, bool backButtonBool, bool nextButtonBool)
	{
		activeView.SetActive(true);
		inactiveView.SetActive(false);

		if(backButtonBool)
			this._backButton.SetActive(true);

		else
			this._backButton.SetActive(false);

		if(nextButtonBool)
			this._nextButton.SetActive(true);

		else
			this._nextButton.SetActive(false);
	}

	private IEnumerator waitThenCallback(float time, Action callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}

	public void pressCloseContact()
	{
		this._contactView.SetActive(false);
	}
}
