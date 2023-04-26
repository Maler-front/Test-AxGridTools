using AxGrid.Base;
using UnityEngine;
using AxGrid;

public class PlayerParameters : MonoBehaviourExt
{
    [Header("Health")]
    [SerializeField] private string HEALTH_NAME_IN_MODEL = "Health";
    [SerializeField] private int MAX_HEALTH = 1000;
    [SerializeField] private int START_HEALTH = 500;
    [SerializeField] private int HEALTH_INC = 2;
    [SerializeField] private int HEALTH_DEC = 1;

    [Header("Food")]
    [SerializeField] private string FOOD_NAME_IN_MODEL = "Food";
    [SerializeField] private int MAX_FOOD = 1000;
    [SerializeField] private int START_FOOD = 500;
    [SerializeField] private int FOOD_INC = 1;
    [SerializeField] private int FOOD_DEC = 1;

    [Header("Money")]
    [SerializeField] private string MONEY_NAME_IN_MODEL = "Money";
    [SerializeField] private int MAX_MONEY = 10000;
    [SerializeField] private int START_MONEY = 500;
    [SerializeField] private int MONEY_INC = 2;
    [SerializeField] private int MONEY_DEC = 1;

    private bool _gameEnded = false;

    [OnAwake]
    private void awake()
    {
        Model.Set(MONEY_NAME_IN_MODEL, START_MONEY);
        Model.Set(HEALTH_NAME_IN_MODEL, START_HEALTH);
        Model.Set(FOOD_NAME_IN_MODEL, START_FOOD);
        Model.Set("MAX_" + HEALTH_NAME_IN_MODEL.ToUpper(), MAX_HEALTH);
        Model.Set("MAX_" + FOOD_NAME_IN_MODEL.ToUpper(), MAX_FOOD);
    }

    [OnStart]
    private void start()
    {
        Settings.Model.EventManager.AddAction("OnInShop", OnInShop);
        Settings.Model.EventManager.AddAction("OnAtWork", OnAtWork);
        Settings.Model.EventManager.AddAction("OnAtHome", OnAtHome);
    }

    private void OnInShop()
    {
        if (_gameEnded)
            return;

        int currentMoney = Model.GetInt(MONEY_NAME_IN_MODEL) - MONEY_DEC;
        int currentFood = Model.GetInt(FOOD_NAME_IN_MODEL) + FOOD_INC;

        if (currentMoney <= 0)
        {
            Settings.Model.EventManager.Invoke("GameLose");
            _gameEnded = true;
            enabled = false;
            return;
        }
            
        if (currentFood < MAX_FOOD)
        {
            Model.Set(FOOD_NAME_IN_MODEL, currentFood);
            Model.Set(MONEY_NAME_IN_MODEL, currentMoney);
        }
    }

    private void OnAtWork()
    {
        if (_gameEnded)
            return;

        int currentMoney = Model.GetInt(MONEY_NAME_IN_MODEL) + MONEY_INC;
        int currentHealth = Model.GetInt(HEALTH_NAME_IN_MODEL) - HEALTH_DEC;

        if (currentMoney > MAX_MONEY)
        {
            Settings.Model.EventManager.Invoke("GameWin");
            _gameEnded = true;
            enabled = false;
            return;
        }

        if (currentHealth <= 0)
        {
            Settings.Model.EventManager.Invoke("GameLose");
            _gameEnded = true;
            enabled = false;
            return;
        }

        Model.Set(MONEY_NAME_IN_MODEL, currentMoney);
        Model.Set(HEALTH_NAME_IN_MODEL, currentHealth);
    }

    private void OnAtHome()
    {
        if (_gameEnded)
            return;

        int currentFood = Model.GetInt(FOOD_NAME_IN_MODEL) - FOOD_DEC;
        int currentHealth = Model.GetInt(HEALTH_NAME_IN_MODEL) + HEALTH_INC;

        if(currentHealth < MAX_HEALTH && currentFood > 0)
        {
            Model.Set(HEALTH_NAME_IN_MODEL, currentHealth);
            Model.Set(FOOD_NAME_IN_MODEL, currentFood);
        }
    }
}
