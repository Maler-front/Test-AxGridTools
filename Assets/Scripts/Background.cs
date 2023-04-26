using AxGrid;
using AxGrid.Base;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviourExt
{
    [SerializeField] private Texture2D[] _workBackground;
    [SerializeField] private Texture2D[] _shopBackground;
    [SerializeField] private Texture2D[] _homeBackground;

    [SerializeField] private GifAnimator _gifAnimator;

    [OnStart]
    private void start()
    {
        Settings.Model.EventManager.AddAction("BackgroundNeedToBeChanged", ChangeBackground);
    }

    public void ChangeBackground()
    {
        switch (StateMachine.CurrentState())
        {
            case StateMachine.States.Work:
                {
                    _gifAnimator.ChangeGifAnimation(_workBackground);
                    break;
                }
            case StateMachine.States.Shop:
                {
                    _gifAnimator.ChangeGifAnimation(_shopBackground);
                    break;
                }
            case StateMachine.States.Home:
                {
                    _gifAnimator.ChangeGifAnimation(_homeBackground);
                    break;
                }
        }
    }
}
