using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource audioSourceRef;
    

    public AudioClip[] audios;

    void Awake()
    {
        //audioSourceRef.GetComponent<AudioSource>();
        //UiContN.FindObjectOfType<UiContN>().soundEvent.AddListener(ChoosePlaySound);
        UiContN uicontnTempLink = this.gameObject.GetComponentInParent<UiContN>();

        uicontnTempLink.soundEvent.AddListener(ChoosePlaySound);
    }


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChoosePlaySound(int index)
    {

        audioSourceRef.clip = audios[index];
        audioSourceRef.Play();

    }

    void RandomizePitch()
    {

    }
}
