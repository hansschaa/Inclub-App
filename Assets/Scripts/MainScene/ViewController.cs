using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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

	public void pressBackButton()
	{
		if(this._introductionView1.activeInHierarchy)
		{
			this.updateView(_titleScreenView,_introductionView1,false, false);
		}

		else if(this._introductionView2.activeInHierarchy)
		{
			this.updateView(_introductionView1,_introductionView2,true, true);
		}

		else if(this._chapterListView.activeInHierarchy)
		{
			this.updateView(_introductionView2,_chapterListView,true, true);
		}
	}

	public void pressNextButton()
	{
		if(this._introductionView1.activeInHierarchy)
		{
			this.updateView(_introductionView2,_introductionView1,true, true);
		}

		else if(this._introductionView2.activeInHierarchy)
		{
			this.updateView(_chapterListView,_introductionView2,true, false);
		}
	}

	public void pressSocialNetworkButton()
	{
		_contactView.SetActive(true);
	}	

	public void pressPlayButton()
	{
		this.updateView(_introductionView1, _titleScreenView, true, true);
	}

	public void pressChapterButton(int chapterNumber)
	{
		this.updateView(_loadingView, _chapterListView, false, false);
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
		_contactView.SetActive(false);
	}
}
