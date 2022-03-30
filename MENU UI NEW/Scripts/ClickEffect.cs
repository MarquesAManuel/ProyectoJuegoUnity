using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickEffect : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compresClip, _unCompresClip;
    [SerializeField] private AudioSource _source;

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = _pressed;
        _source.PlayOneShot(_compresClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.sprite = _default;
        _source.PlayOneShot(_unCompresClip);
    }

    public void IWasClicking()
    {
        Debug.Log("Clickie");
    }

}
