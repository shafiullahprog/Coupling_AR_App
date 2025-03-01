using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivationController : MonoBehaviour
{
    [SerializeField] List<GameObject> transparentObjects;
    [SerializeField] List<GameObject> opeaqueObjects;

    [SerializeField] Material mat;

    [SerializeField] bool isTransparent;
    public bool IsTransparent => isTransparent;

    public void SelectType(bool value)
    {
        isTransparent = value;
    }
    public void HandleObjectActicvation()
    {
        if(transparentObjects.Count >0 && opeaqueObjects.Count > 0)
        {
            if (isTransparent)
            {
                Activate(transparentObjects, true);
                Activate(opeaqueObjects, false);
            }
            else
            {
                Activate(transparentObjects, false);
                Activate(opeaqueObjects, true);
            }
        }
    }
    private void Activate(List<GameObject> objs, bool value)
    {
        foreach (GameObject obj in objs)
        {
            if (obj != null)
            {
                obj.SetActive(value);
            }
        }
    }

    public void HandleTranspancy(float value)
    {
        var obj = transparentObjects[0].transform.GetChild(0).GetComponent<MeshRenderer>().material;
        mat = obj;

        float alpha = Mathf.InverseLerp(-0.5f, 0f, value);

        //Color newColor = obj.color;
        //newColor.a = alpha;
        //obj.color = newColor;
    
        //mat.shader.su
    }
}
