using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Attribution : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<string, string> urlValues = new Dictionary<string, string>();

    private void Start()
    {
        urlValues.Add("barricade", "https://sketchfab.com/3d-models/wooden-barricades-2838fd388f7b484b8039a0ef977fc866");
        urlValues.Add("cannon", "https://sketchfab.com/3d-models/cannon-low-poly-4cceb1de55974882b524e69135c15366");
        urlValues.Add("surrenderFlag", "https://sketchfab.com/3d-models/flag-swing-9d0965e9e71241baa89583ea02090af6");
        urlValues.Add("rocks", "https://sketchfab.com/3d-models/low-poly-rock-set-48f8612d1db1429d976f8d4f08ed0f0c");
        urlValues.Add("shovel", "https://sketchfab.com/3d-models/low-poly-pirate-shovel-fbcef55c06c24cc2920b024af9f69b40");
        urlValues.Add("swedishFlag", "https://sketchfab.com/3d-models/flag-alembic2-47b6b12926704946a1f7d21d8744a510");
        urlValues.Add("torch", "https://sketchfab.com/3d-models/torch-3e1203d8410348c8900c0ace23d9d40d");
        urlValues.Add("mGlass", "https://sketchfab.com/3d-models/magnifying-glass-model-a4bf7706c6ad42ee979b47331d684e22");
        urlValues.Add("email", "https://sketchfab.com/3d-models/magnifying-glass-model-a4bf7706c6ad42ee979b47331d684e22");


    }

    public void OpenURL(string key)
    {
        Application.OpenURL(urlValues[key]);
    }
    public void Email()
    {

    }

   public void SendEmail(string toEmail)
   {
        string emailSubject = System.Uri.EscapeUriString("Sovinec AR support");
        string emailBody = System.Uri.EscapeUriString("");
        Application.OpenURL("mailto:" + toEmail + "?subject=" + emailSubject + "&body=" + emailBody);
    }
}
