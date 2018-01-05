using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;
using System;

public class SplashScreenController : MonoBehaviour 
{
	[Header("Variables")]
	public GameObject _logoInclub;
	public GameObject _fadePanel;

	public GameObject _logoUtalca;
	public GameObject _logoAulab;

	[Header("Parámetros Animación")]
	public float _delayColorLogoTransition;
	public float _delayColorPanelTransition;
	public float _intervalTimeBetweenFadeInAndOut;
	
	//Colours
	private Color32 _alphaFadeTo255;
	private Color32 _alphaFadeTo0;
	private Color32 _alphaLogoTo255;
	private Color32 _alphaLogoTo0;

	//Sequences
	private Sequence _splashScreen;
	private Sequence _secondLogoView;


	// Use this for initialization
	void Start () 
	{
	
		this._splashScreen = DOTween.Sequence();

		this._alphaFadeTo0 = new Color32(0,0,0,0);
		this._alphaFadeTo255 = new Color32(0,0,0,255);
		this._alphaLogoTo0 = new Color32(255,255,255,0);
		this._alphaLogoTo255 = new Color32(255,255,255,255);

		this._splashScreen.Append(this._fadePanel.GetComponent<Image>().DOColor(this._alphaFadeTo0,this._delayColorPanelTransition)).AppendInterval(this._intervalTimeBetweenFadeInAndOut);
		this._splashScreen.Append(this._logoInclub.GetComponent<Image>().DOColor(this._alphaLogoTo0,this._delayColorLogoTransition));

		this._splashScreen.Append(this._logoUtalca.GetComponent<Image>().DOColor(this._alphaLogoTo255,this._delayColorLogoTransition));
		this._splashScreen.Join(this._logoAulab.GetComponent<Image>().DOColor(this._alphaLogoTo255,this._delayColorLogoTransition)).AppendInterval(this._intervalTimeBetweenFadeInAndOut);

		this._splashScreen.Append(this._fadePanel.GetComponent<Image>().DOColor(this._alphaFadeTo255,this._delayColorPanelTransition/2));
		this._splashScreen.Play();

		this._splashScreen.OnComplete(()=> transicion());
	}

	public void transicion()
	{
		// var doorway = new DoorwayTransition()
		// {
		// 	nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
		// 	duration = 1.0f,
		// 	perspective = 1.5f,
		// 	depth = 3f,
		// 	runEffectInReverse = false
		// };
		// TransitionKit.instance.transitionWithDelegate( doorway );

		var wind = new WindTransition()
		{
			nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
			duration = 1.0f,
			size = 0.3f
		};
		TransitionKit.instance.transitionWithDelegate( wind );


	}
}
