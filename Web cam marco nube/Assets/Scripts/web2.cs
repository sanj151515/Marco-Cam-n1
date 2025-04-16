using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class web2 : MonoBehaviour
{
    WebCamTexture webcamtexture;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        WebCamDevice my_device = new WebCamDevice();
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < 0; i++)
        {
            Debug.Log(devices[i].name);
            my_device = devices[i];
        }
        webcamtexture = new WebCamTexture(my_device.name);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamtexture;
        webcamtexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
