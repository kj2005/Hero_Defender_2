using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShaderReplacementManager", menuName = "ShaderReplacementManager")]
public class ShaderReplacementManager : ScriptableObject{

    [SerializeField]
    public List<Shader> shaders = new List<Shader>();
    //[SerializeField]
    //List<ShaderData> colors = new List<ShaderData>();
    //[SerializeField][HideInInspector]
    //int numShaders = 0;
	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    //private void OnValidate()
    //{
    //    while (colors.Count > shaders.Count)
    //        colors.RemoveAt(colors.Count - 1);
        
    //    for (int i = 0; i < colors.Count; ++i)
    //    {
    //        Shader.SetGlobalColor(shaders[i].name, colors[i].color);
    //    }
    //}
}
