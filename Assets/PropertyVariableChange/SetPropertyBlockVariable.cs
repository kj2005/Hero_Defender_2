using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PropertyManager))]
public abstract class SetPropertyBlockVariable : MonoBehaviour {


    public string variableName = "";
    private string oldName = "";
    protected int propertyId = -1;

    public bool useProperty = true;

    public abstract void Tick();

    PropertyManager _manager = null;

    public PropertyManager manager { get { return _manager; } }

    private void Awake()
    {
        ReAwake();
    }

    [ContextMenu("ReAwake")]
    public void ReAwake()
    {
        _manager = GetComponent<PropertyManager>();
        manager.AddProperty(this);
    }

    public void Reset()
    {
        for (int i = 0; i < manager.renderDatas.Count; ++i)
        {
            manager.renderDatas[i].ResetProperty();
        }
    }

    private void OnDestroy()
    {
        GetComponent<PropertyManager>().RemoveProperty(this);
    }

    // Update is called once per frame
    protected bool CanTick () {
        if (!useProperty)
        {
            return false;
        }

        if (oldName != variableName)
        {
            propertyId = Shader.PropertyToID(variableName);
            oldName = variableName;
        }

        if (variableName == "")
            return false;


        if (propertyId == -1)
            propertyId = Shader.PropertyToID(variableName);

        return true;
    }

}

[System.Serializable]
public class RendererData
{
    public Renderer renderer = null;
    public MaterialPropertyBlock propertyBlock = null;

    public RendererData(Renderer _renderer)
    {
        renderer = _renderer;
        propertyBlock = new MaterialPropertyBlock();
    }

    public void SetFloat(int propertyId, float amount)
    {
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat(propertyId, amount);
        renderer.SetPropertyBlock(propertyBlock);
    }
    public void SetColor(int propertyId, Color color)
    {
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(propertyId, color);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void ResetProperty()
    {
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.Clear();
        renderer.SetPropertyBlock(propertyBlock);
    }
}