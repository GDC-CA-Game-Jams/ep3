using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMOD;
using FMODUnity;

public class UIButtons : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] FMODUnity.StudioEventEmitter hoverSound;
    [SerializeField] FMODUnity.StudioEventEmitter clickSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        hoverSound.Play();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        clickSound.Play();
    }
}
