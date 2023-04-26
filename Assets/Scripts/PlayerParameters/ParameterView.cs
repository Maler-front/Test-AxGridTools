using AxGrid;
using AxGrid.Base;
using UnityEngine;
using UnityEngine.UI;

public class ParameterView : MonoBehaviourExt
{
    [SerializeField] private string _parameterName;
    [SerializeField] private Slider _slider;

    [OnStart]
    private void start()
    {
        Settings.Model.EventManager.AddAction("On" + _parameterName + "Changed", ChangeView);
        _slider.value = Model.GetFloat(_parameterName) / Model.GetFloat("MAX_" + _parameterName.ToUpper());
    }

    private void ChangeView()
    {
        _slider.value = Model.GetFloat(_parameterName) / Model.GetFloat("MAX_" + _parameterName.ToUpper());
    }
}
