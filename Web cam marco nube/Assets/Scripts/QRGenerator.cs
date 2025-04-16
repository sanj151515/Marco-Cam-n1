using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class QRGenerator : MonoBehaviour
{
    public Image qrImage;
    public string qrApi = "https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=";

    public void GenerateQRCode(string imageUrl)
    {
        StartCoroutine(LoadQR(qrApi + imageUrl));
    }

    IEnumerator LoadQR(string qrUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(qrUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            qrImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        }
    }
}
