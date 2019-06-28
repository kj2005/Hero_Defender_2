using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ShaderOverwrite : MonoBehaviour {

    [SerializeField]
    ShaderReplacementManager manager;

    [SerializeField]
    List<ReplacementData> replacements = new List<ReplacementData>();
    [SerializeField]
    [HideInInspector]
    List<ReplacementData> _replacements = new List<ReplacementData>();
    string replacementTag = "";

    [SerializeField]
    [HideInInspector]
    List<Camera> cameras = new List<Camera>();
    [SerializeField][HideInInspector]
    Camera _camera = null;
    [SerializeField][HideInInspector]
    Camera camera { get { if (_camera == null) { _camera = GetComponent<Camera>(); } return _camera; } }

    [SerializeField][HideInInspector]
    Camera backCam;
    //[ContextMenu("Switch")]
    //public void SwitchOn()
    //{
    //    CreateCameras();
    //}
    [ContextMenu("UpdateColors")]
    public void UpdateColors()
    {
        for (int i = 0; i < replacements.Count; ++i)
        {
            if (replacements[i].camera != null)
                replacements[i].camera.SetReplacementShader(manager.shaders[i], replacementTag);
            else
            {
                Debug.Log("Camera is null!");
            }
        }
    }
    private void Update()
    {
        for (int i = 0; i < replacements.Count; ++i)
        {
            if (replacements[i].camera != null)
                replacements[i].camera.fieldOfView = camera.fieldOfView;
           
        }
    }
    [ContextMenu("Switch")]
    private void CreateCameras()
    {
        if (manager == null)
            return;
        //for (int i = 0; i < replacements.Count; ++i)
        //{
        //    if (replacements[i].camera != null)
        //        DestroyImmediate(replacements[i].camera.gameObject);
        //}
        camera.depth = -1;
        for (int i = 0; i < replacements.Count; ++i)
        {
            if (replacements[i].camera != null)
            {
                Shader.SetGlobalColor(manager.shaders[i].name, replacements[i].color);
                replacements[i].camera.SetReplacementShader(manager.shaders[i], replacementTag);
            }
        }
        if (backCam != null)
            return;
        //DestroyImmediate(backCam.gameObject);
        //cameras.Clear();
        //OnValidate();
        GameObject backMask = new GameObject("back");
        backCam = backMask.AddComponent<Camera>();
        backCam.clearFlags = CameraClearFlags.SolidColor;
        backCam.backgroundColor = Color.black;
        backCam.cullingMask = 0;
        backCam.fieldOfView = camera.fieldOfView;
        backCam.depth = 0;
        backCam.transform.SetParent(transform);
        backCam.transform.position = Vector3.zero;
        backCam.transform.rotation = Quaternion.identity;
        backCam.transform.localScale = Vector3.one;
    }
    public Camera CreateCamera(int i)
    {
        GameObject blueMask = new GameObject("maskCamera");
        Camera newCamera = blueMask.AddComponent<Camera>();
        cameras.Add(newCamera);
        newCamera.clearFlags = CameraClearFlags.Nothing;
        newCamera.cullingMask = replacements[i].layer;
        newCamera.fieldOfView = camera.fieldOfView;
        newCamera.depth = 1;
        newCamera.transform.parent = transform;
        newCamera.transform.localPosition = Vector3.zero;
        newCamera.transform.localRotation = Quaternion.identity;
        newCamera.transform.localScale = Vector3.one;
        newCamera.SetReplacementShader(manager.shaders[i], replacementTag);
        return newCamera;
    }
    private void OnValidate()
    {
        for (int i = 0; i < replacements.Count; ++i)
            CheckReplacement(i);
        while (_replacements.Count > replacements.Count)
        {
            _replacements.RemoveAt(_replacements.Count - 1);
        }
        while (cameras.Count > replacements.Count)
        {
            int i = cameras.Count - 1;
            if(cameras[i] != null)
                DestroyImmediate(cameras[i].gameObject);
            cameras.RemoveAt(i);
        }
    }
    private void CheckReplacement(int i)
    {
        if (i >= cameras.Count)
        {
            CreateCamera(i);
        }
        else if (replacements[i].camera == null)
        {
            replacements[i].camera = cameras[i];
            if (replacements[i].camera == null)
                replacements[i].camera = CreateCamera(i);
        }
        if (i >= _replacements.Count)
        {
            _replacements.Add(new ReplacementData());
            replacements[i].camera = _replacements[i].camera = cameras[i];
            _replacements[i].layer = replacements[i].layer;
            _replacements[i].color = replacements[i].color;
            Shader.SetGlobalColor(manager.shaders[i].name, replacements[i].color);
        }
        
        if (_replacements[i].color != replacements[i].color)
        {
            _replacements[i].color = replacements[i].color;
            Shader.SetGlobalColor(manager.shaders[i].name, replacements[i].color);
        }
        if (_replacements[i].layer != replacements[i].layer)
        {
            replacements[i].camera.cullingMask = _replacements[i].layer = replacements[i].layer;
        }
    }

    private void OnEnable()
    {
        CreateCameras();
        for (int i = 0; i < replacements.Count; ++i)
        {
            replacements[i].camera.gameObject.SetActive(true);
            replacements[i].camera.SetReplacementShader(manager.shaders[i], replacementTag);
            
        }
        backCam.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        for (int i = 0; i < replacements.Count; ++i)
        {
            if (replacements[i] != null)
            {
                replacements[i].camera.ResetReplacementShader();
                replacements[i].camera.gameObject.SetActive(false);
            }
        }
        backCam.gameObject.SetActive(false);
    }

}

[System.Serializable]
public class ReplacementData
{
    [SerializeField]
    public LayerMask layer;
    [SerializeField]
    public Color color;
    [SerializeField]
    [HideInInspector]
    public Camera camera;
}