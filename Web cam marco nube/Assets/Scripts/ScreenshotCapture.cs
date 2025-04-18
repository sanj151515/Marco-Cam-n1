using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ScreenshotCapture : MonoBehaviour
{

    public int frameRate = 24;
    public float captureDuration = 3f;

    public bool isCapture = false;

    private WebCamTexture webcamTexture;
    private List<Texture2D> capturedFrames = new List<Texture2D>();

    public string saveFolder = "CapturasWebcam"; // Carpeta donde se guardarán las imágenes

    public List<Texture2D> GetCapturedFrames()
    {
        return capturedFrames;
    }

    public void TakeScreenshot()
    {
        if (isCapture) return;

        StartCoroutine(CaptureFramesCoroutine());
    }

    private IEnumerator CaptureFramesCoroutine()
    {
        yield return new WaitForEndOfFrame();  // Esperar para capturar bien los efectos

        isCapture = true;

        // Crear carpeta de guardado si no existe
        string folderPath = Path.Combine(Application.dataPath, saveFolder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Inicializa la cámara
        //webcamTexture = new WebCamTexture();
        //webcamTexture.Play();

        // Espera a que la cámara esté lista
        //yield return new WaitUntil(() => webcamTexture.width > 100);

        int totalFrames = Mathf.RoundToInt(frameRate * captureDuration);
        float interval = 1f / frameRate;

        for (int i = 0; i < totalFrames; i++)
        {
            yield return new WaitForEndOfFrame();

            // Captura el frame de la webcam
            Texture2D photo = new Texture2D(Screen.width, Screen.height);
            //Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height);
            photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            //photo.SetPixels(webcamTexture.GetPixels());
            photo.Apply();

            capturedFrames.Add(photo);

            // Guardar como PNG
            byte[] bytes = photo.EncodeToPNG();
            string filePath = Path.Combine(folderPath, $"frame_{i:D3}.png");
            File.WriteAllBytes(filePath, bytes);

            Debug.Log($"Frame {i + 1}/{totalFrames} guardado en: {filePath}");

            yield return new WaitForSeconds(interval);
        }

        //webcamTexture.Stop();

        isCapture = false;

        Debug.Log($"✅ Captura completa: {capturedFrames.Count} imágenes guardadas en {folderPath}");
    }


    /*
    string filePath;

    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();  // Esperar para capturar bien los efectos

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();
        //Application.persistentDataPath
        filePath = Path.Combine(Application.dataPath, "screenshot.png");
        File.WriteAllBytes(filePath, screenshot.EncodeToPNG());

        Debug.Log("Screenshot guardado en: " + filePath);

        // Subir a la nube
        //StartCoroutine(FindObjectOfType<GoogleDriveUploader>().UploadImage(filePath));
    }*/
}
