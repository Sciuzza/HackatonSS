using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSourceRef;

    public bool infoPanelActive = false;

    public AudioClip[] audios;

    void Awake()
    {
        //audioSourceRef.GetComponent<AudioSource>();
        //UiContN.FindObjectOfType<UiContN>().soundEvent.AddListener(ChoosePlaySound);
        UiContN uicontnTempLink = this.gameObject.GetComponentInParent<UiContN>();

        uicontnTempLink.soundEvent.AddListener(ChoosePlaySound);
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChoosePlaySound(int index)
    {
        if (index == 20)
        {
            StartCoroutine(RandomizePitchTypeWriter(index));
        }
        else if (index == 19)
        {
            audioSourceRef.clip = audios[index];
            audioSourceRef.Play();
        }
        else
        {
            audioSourceRef.clip = audios[index];
            audioSourceRef.Play();
        }
    }

    public IEnumerator RandomizePitchTypeWriter(int index)
    {
        audioSourceRef.clip = audios[index];
        
        while (infoPanelActive)
        {
            audioSourceRef.pitch = Random.Range(1, 1.5f);
            audioSourceRef.Play();
            yield return new WaitForSeconds(0.2f);
        }
        
    }
}
