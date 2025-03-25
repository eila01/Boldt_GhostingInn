using UnityEngine;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorVar : MonoBehaviour
{
    public SpriteRenderer mySpriteRend;
    public Color newColor;
    void Start()
    {
        mySpriteRend = GetComponent<SpriteRenderer>();
        mySpriteRend.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
