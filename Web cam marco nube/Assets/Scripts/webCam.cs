using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class webCam : MonoBehaviour
{

    public RawImage rawImage;
    public AspectRatioFitter aspectFitter;

    private WebCamTexture webcamTexture;

    void Start()
    {
        StartCoroutine(StartCamera());
    }

    IEnumerator StartCamera()
    {
        // Verificar si hay cámaras disponibles
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogWarning("No se detectó ninguna cámara.");
            yield break;
        }

        string camName = WebCamTexture.devices[0].name; // Usa la primera cámara disponible
        Debug.Log("Usando cámara: " + camName);

        // Iniciar la cámara con una resolución segura
        webcamTexture = new WebCamTexture(camName, 640, 480);
        rawImage.texture = webcamTexture;
        webcamTexture.Play();

        // Esperar hasta que la cámara esté lista
        yield return new WaitUntil(() => webcamTexture.width > 100);

        // Ajustar el aspect ratio
        float ratio = (float)webcamTexture.width / webcamTexture.height;
        aspectFitter.aspectRatio = ratio;

        // Configurar rotación y escala
        rawImage.rectTransform.localEulerAngles = new Vector3(0, 0, -webcamTexture.videoRotationAngle);
        rawImage.rectTransform.localScale = new Vector3(
            1,
            webcamTexture.videoVerticallyMirrored ? -1 : 1,
            1
        );

        Debug.Log("Cámara inicializada correctamente.");
    }

    void Update()
    {
        // Solo actualiza si la cámara está activa y lista
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            // Verifica si hay un frame nuevo disponible
            if (webcamTexture.didUpdateThisFrame)
            {
                rawImage.texture = webcamTexture; // Actualiza el RawImage con la textura de la cámara
            }
        }
    }


    void OnDestroy()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop(); // Detener la cámara al destruir el objeto
        }
    }
}


