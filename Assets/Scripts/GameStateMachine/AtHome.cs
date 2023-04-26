using AxGrid;
using AxGrid.FSM;
using UnityEngine;

[State("AtHome")]
public class AtHome : FSMState
{
    private bool _needToUpdate = false;

    [Enter]
    public void EnterState()
    {
        Settings.Model.EventManager.AddAction("FadeAnimatingStarted", () => _needToUpdate = false);
        Settings.Model.EventManager.AddAction("FadeAnimatingEnded", () => _needToUpdate = true);
    }

    [Loop(1 / 60f)]
    public void UpdateState()
    {
        if (_needToUpdate)
            Settings.Model.EventManager.Invoke("OnAtHome");
    }

    [Exit]
    public void ExitState()
    {
        Settings.Model.EventManager.RemoveAction("FadeAnimatingStarted", () => _needToUpdate = false);
        Settings.Model.EventManager.RemoveAction("FadeAnimatingEnded", () => _needToUpdate = true);
        Settings.Model.EventManager?.Invoke("CurrentStateChanged");
    }
}