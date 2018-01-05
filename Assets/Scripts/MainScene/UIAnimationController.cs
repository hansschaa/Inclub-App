using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIAnimationController : MonoBehaviour {

	[Header("Variables")]
	public Transform _viajeroFelizButton;
	public Transform _rightButtons;
	public Transform _leftOptions;
	public Transform _playButton;

	public Transform _airplaneSprite;
	
	public float _globalMargin;

	//Sequence
	private Sequence _buttonsInScene;

	
	void Start () 
	{
		this._buttonsInScene = DOTween.Sequence();
		this._buttonsInScene.Append(this._viajeroFelizButton.DOLocalMoveX(this._viajeroFelizButton.localPosition.x + _globalMargin*3.1f,1.5f,true));
		this._buttonsInScene.Join(this._rightButtons.DOLocalMoveX(this._rightButtons.localPosition.x - _globalMargin*2,1.5f,true));
		this._buttonsInScene.Join(this._leftOptions.DOLocalMoveX(this._leftOptions.localPosition.x + _globalMargin*2f,1.5f,true));
		
		this._buttonsInScene.Join(this._playButton.DOLocalMoveY(this._playButton.localPosition.y + _globalMargin * 2.55f,1.5f,true)).PrependInterval(1f);

		this._buttonsInScene.Play().OnComplete(()=> this._airplaneSprite.DOLocalMoveX(this._playButton.localPosition.x + _globalMargin * 8,4f,true));



		
	}
}
