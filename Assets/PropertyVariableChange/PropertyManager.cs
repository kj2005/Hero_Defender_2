using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PropertyManager : MonoBehaviour
{
    public List<SetPropertyBlockVariable> properties = new List<SetPropertyBlockVariable>();
    public List<RendererData> renderDatas = new List<RendererData>();

    // Update is called once per frame
    void Update () {
        for (int j = 0; j < renderDatas.Count; ++j)
        {
            renderDatas[j].ResetProperty();
        }

        for (int i = 0; i < properties.Count; ++i)
        {
            properties[i].Tick();
        }
	}

    public void AddProperty(SetPropertyBlockVariable property)
    {
        if (properties.Contains(property))
            return;
        properties.Add(property);
    }

    public void RemoveProperty(SetPropertyBlockVariable property)
    {
        properties.Remove(property);
    }

    [ContextMenu("Retreive Renderers")]
    public void RetreiveRenderers()
    {
        renderDatas.Clear();
        //find all renderers in all children recursively and add them to renderer datas.
        FindRenderersRecursive(transform);
    }

    //find all the renderers and add them to a list of renderer datas.
    private void FindRenderersRecursive(Transform current)
    {
        //check if this child has a renderer.
        Renderer renderer = current.GetComponent<Renderer>();
        if (renderer)
        {
            //create a new renderer data and add it to renderer datas.
            renderDatas.Add(new RendererData(renderer));
        }

        //loop through all children and look for renderers.
        for (int i = 0; i < current.childCount; ++i)
        {
            FindRenderersRecursive(current.GetChild(i));
        }
    }
}
