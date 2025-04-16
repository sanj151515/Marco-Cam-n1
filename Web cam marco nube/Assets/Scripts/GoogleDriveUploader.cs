using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
public class GoogleDriveUploader : MonoBehaviour
{
    string uploadURL = "https://script.google.com/macros/library/d/1Mp9V9txcbEWTXQ__MYMR8aUE-ekJ3nV860VRBOtCh9VeilZYdk_qpi7V/1";  // Reemplaza con la URL de tu API en Google Apps Script

    public IEnumerator UploadImage(string filePath)
    {
        byte[] imageData = System.IO.File.ReadAllBytes(filePath);
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageData, "screenshot.png", "image/png");

        using (UnityWebRequest request = UnityWebRequest.Post(uploadURL, form))
        {
            Debug.Log("Enviando solicitud a: " + uploadURL);  // Para verificar la URL
            yield return request.SendWebRequest();

            Debug.Log("Respuesta recibida: " + request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                string driveLink = request.downloadHandler.text;
                FindObjectOfType<QRGenerator>().GenerateQRCode(driveLink);
            }
            else
            {
                Debug.LogError("Error al subir la imagen: " + request.error);
            }
        }
    }
}
