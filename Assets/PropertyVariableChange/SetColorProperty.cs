using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorProperty : SetPropertyBlockVariable
{
    [SerializeField] private Color _color = Color.white;
    public Color color { get { return _color; } }

	// Update is called once per frame
	public override void Tick ()
    {
        if (!base.CanTick())
            return;

        for (int i = 0; i < manager.renderDatas.Count; ++i)
        {
            manager.renderDatas[i].SetColor(propertyId, color);
        }
    }

}
