using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ScreenshotCapture : MonoBehaviour
{
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
    }
}
