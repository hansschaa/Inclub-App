using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigController : MonoBehaviour 
{

	public bool _isAudioOff = false;

    private static ConfigController _instance;

    public static ConfigController Instance 
    { 
        get { return _instance; } 
    } 

    private void Awake() 
    { 
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    } 

	
	void Start()
	{
		print(_isAudioOff);
		if(_isAudioOff)
			GameObject.Find("Main Camera").gameObject.GetComponent<AudioSource>().enabled = false;
	}

	public void pressAudioOn()
	{
		this._isAudioOff = !this._isAudioOff;
		print(_isAudioOff);

	}


}
