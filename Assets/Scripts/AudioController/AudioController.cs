using AxGrid;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

public class AudioController : MonoBehaviourExtBind
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _noise;

    [SerializeField] public AudioClip[] _musicClips;
    [SerializeField] public AudioClip[] _noiseClips;

    /*[SerializeField] [Range(0f, 1f)] private float _maxMusicVolume;
    [SerializeField] [Range(0f, 1f)] private float _maxNoiseVolume;*/

    [OnStart]
    private void start()
    {
        Settings.Model.EventManager.AddAction("MusicNeedToBeChanged", ChangeBackgroundSounds);
    }

    private void ChangeBackgroundSounds()
    {
        switch(StateMachine.CurrentState())
        {
            case StateMachine.States.Work:
                {
                    /*ChangeSound(_music, _musicClips[0], _maxMusicVolume);
                    ChangeSound(_noise, _noiseClips[0], _maxNoiseVolume);*/
                    _music.clip = _musicClips[0];
                    _noise.clip = _noiseClips[0];
                    _music.Play();
                    _noise.Play();
                    break;
                }
            case StateMachine.States.Shop:
                {
                    /*ChangeSound(_music, _musicClips[1], _maxMusicVolume);
                    ChangeSound(_noise, _noiseClips[1], _maxNoiseVolume);*/
                    _music.clip = _musicClips[1];
                    _noise.clip = _noiseClips[1];
                    _music.Play();
                    _noise.Play();
                    break;
                }
            case StateMachine.States.Home:
                {
                    /*ChangeSound(_music, _musicClips[2], _maxMusicVolume);
                    ChangeSound(_noise, _noiseClips[2], _maxNoiseVolume);*/
                    _music.clip = _musicClips[2];
                    _noise.clip = _noiseClips[2];
                    _music.Play();
                    _noise.Play();
                    break;
                }
        }
    }

    /*private void ChangeSound(AudioSource source, AudioClip clip, float maxVolume)
    {
        Path = new CPath()
            .EasingLinear(1f, 0.1f, 0, (v) => source.volume = v)
            .Wait(1f)
            .Action(() => {
                source.clip = clip;
                source.Play();
            })
            .EasingLinear(1f, 0, 0.1f, (v) => source.volume = v);
    }*/
}
