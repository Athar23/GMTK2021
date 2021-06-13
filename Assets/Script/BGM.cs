using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource bgm;
    // Start is called before the first frame update
    void Awake()
    {
        bgm=GetComponent<AudioSource>();
        bgm.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
