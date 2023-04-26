using AxGrid.Base;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifAnimator : MonoBehaviourExt
{
    [SerializeField]
    private Texture2D[] _frames;
    [SerializeField]
    private RawImage _image;
    [SerializeField]
    private float _framesPerSecond = 15f;
    public bool playing = false;

    [OnAwake]
    private void awake()
    {
        if(_image == null)
            if (!TryGetComponent(out _image))
                Debug.LogError($"GifAnimator on {gameObject.name} doesnt have raw image component!");
    }

    [OnUpdate]
    private void update()
    {
        if (playing)
        {
            float index = Time.time * _framesPerSecond;
            index %= _frames.Length;
            _image.texture = _frames[(int)index];
        }
    }

    public void ChangeGifAnimation(Texture2D[] newFrames) 
    {
        playing = false;
        _frames = newFrames;
        playing = true;
    }
}
