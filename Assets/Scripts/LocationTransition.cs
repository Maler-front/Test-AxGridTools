using AxGrid;
using AxGrid.Base;
using UnityEngine;
using UnityEngine.UI;
using AxGrid.Path;
using AxGrid.Model;

public class LocationTransition : MonoBehaviourExtBind
{
    public const string BLACKOUT_ALPHA = "BlackoutAlpha";
    [SerializeField]
    private Image _blackoutImage;
    [SerializeField]
    public static float TimeToFade { get; private set; }

    [OnStart]
    private void start()
    {
        TimeToFade = 1f;

        Settings.Model.EventManager.AddAction("CurrentStateChanged", FadeInOut);
        Settings.Model.EventManager.AddAction("GameLose", FadeIn);
        Settings.Model.EventManager.AddAction("GameWin", FadeIn);

        Path = new CPath()
            .Action(() => _blackoutImage.raycastTarget = true)
            .Action(() => Settings.Invoke("BackgroundNeedToBeChanged"))
            .EasingLinear(TimeToFade, 1, 0, (a) => Model.Set(BLACKOUT_ALPHA, a))
            .Action(() => _blackoutImage.raycastTarget = false);
    }

    public void FadeInOut()
    {
        Path = new CPath()
            .Action(() => _blackoutImage.raycastTarget = true)
            .Action(() => Settings.Invoke("FadeAnimatingStarted"))
            .EasingLinear(TimeToFade, 0, 1, (a) => Model.Set(BLACKOUT_ALPHA, a))
            .Action(() => Settings.Invoke("BackgroundNeedToBeChanged"))
            .Action(() => Settings.Invoke("MusicNeedToBeChanged"))
            .Wait(TimeToFade)
            .EasingLinear(TimeToFade, 1, 0, (a) => Model.Set(BLACKOUT_ALPHA, a))
            .Action(() => Settings.Invoke("FadeAnimatingEnded"))
            .Action(() => _blackoutImage.raycastTarget = false);
    }

    public void FadeIn()
    {
        Path = new CPath()
            .Action(() => _blackoutImage.raycastTarget = true)
            .Action(() => Settings.Invoke("FadeAnimatingStarted"))
            .EasingLinear(TimeToFade, 0, 1, (a) => Model.Set(BLACKOUT_ALPHA, a))
            .Action(() => Settings.Invoke("FadeAnimatingEnded"));
    }

    [Bind("On" + BLACKOUT_ALPHA + "Changed")]
    private void BlackoutAlpha()
    {
        Color color = _blackoutImage.color;
        color.a = Model.GetFloat(BLACKOUT_ALPHA);
        _blackoutImage.color = color;
    }
}
