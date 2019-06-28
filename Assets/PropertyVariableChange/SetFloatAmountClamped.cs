using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetFloatAmountClamped : SetPropertyBlockVariable
{

    [Range(0, 1)]
    [SerializeField] private float _floatAmount = 0;
    public float floatAmount { get { return _floatAmount; } }
   
    // Update is called once per frame
    public override void Tick()
    { 
        if (!base.CanTick())
            return;

        for (int i = 0; i < manager.renderDatas.Count; ++i)
        {
            manager.renderDatas[i].SetFloat(propertyId, floatAmount);
        }
    }

}
