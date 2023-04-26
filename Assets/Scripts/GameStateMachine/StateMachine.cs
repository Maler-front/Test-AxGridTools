using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;
using AxGrid.Tools.Binders;
using UnityEngine;

public class StateMachine : MonoBehaviourExt
{
    public const string WORK_BUTTON = "Work";
    public const string SHOP_BUTTON = "Shop";
    public const string HOME_BUTTON = "Home";
    [SerializeField] private UIButtonDataBind _workButton;
    [SerializeField] private UIButtonDataBind _shopButton;
    [SerializeField] private UIButtonDataBind _homeButton;

    [OnAwake]
    private void awake()
    {
        FSM fsm = new FSM();
        fsm.Add(new InShop());
        fsm.Add(new AtWork());
        fsm.Add(new AtHome());
        Settings.Fsm = fsm;
    }

    [OnStart]
    private void start()
    {
        _workButton.buttonName = WORK_BUTTON;
        _shopButton.buttonName = SHOP_BUTTON;
        _homeButton.buttonName = HOME_BUTTON;

        Settings.Fsm.Start("AtHome");
        Settings.Model.Set(_workButton.enableField, true);
        Settings.Model.Set(_shopButton.enableField, true);
        Settings.Model.Set(_homeButton.enableField, false);

        Model.EventManager.AddAction("On" + WORK_BUTTON + "Click", () => ChangeStateTo(States.Work));
        Model.EventManager.AddAction("On" + SHOP_BUTTON + "Click", () => ChangeStateTo(States.Shop));
        Model.EventManager.AddAction("On" + HOME_BUTTON + "Click", () => ChangeStateTo(States.Home));
    }

    [OnUpdate]
    private void UpdateThis()
    {
        Settings.Fsm.Update(Time.deltaTime);
    }

    private void ChangeStateTo(States state)
    {
        Settings.Model.Set(_workButton.enableField, true);
        Settings.Model.Set(_shopButton.enableField, true);
        Settings.Model.Set(_homeButton.enableField, true);

        switch (state)
        {
            case States.Work:
                {
                    Settings.Fsm.Change("AtWork");
                    Settings.Model.Set(_workButton.enableField, false);
                    break;
                }
            case States.Shop:
                {
                    Settings.Fsm.Change("InShop");
                    Settings.Model.Set(_shopButton.enableField, false);
                    break;
                }
            case States.Home:
                {
                    Settings.Fsm.Change("AtHome");
                    Settings.Model.Set(_homeButton.enableField, false);
                    break;
                }
        }
    }

    public static States CurrentState()
    {
        switch(Settings.Fsm.CurrentStateName)
        {
            case "AtWork":
                {
                    return States.Work;
                }
            case "InShop":
                {
                    return States.Shop;
                }
            case "AtHome":
                {
                    return States.Home;
                }
        }

        Debug.LogError("States Error");
        return States.Home;
    }

    public enum States
    {
        Work,
        Shop,
        Home
    }
}