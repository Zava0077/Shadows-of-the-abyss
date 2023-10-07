using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoricImpact : MonoBehaviour
{

    bool _isActive;
    bool _isStart = true;
    void Update()
    {
        if (_isActive)
            AbilityUpdate();
    }
    void AbilityUpdate()
    {
        if (_isStart)
            AbilityStart();

    }
    void AbilityStart()
    {


        _isStart = false;
    }
}
