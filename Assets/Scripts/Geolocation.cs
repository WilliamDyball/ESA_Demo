using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Geolocation : MonoBehaviour {
    public static Geolocation Instance { set; get; }

    public float fLatitude;
    public float fLonitude;
    string strCity;

    public Text txt0;
    public Text txt1;
    public Text txt2;
    public Text txt3;
    public Text txt4;
    public Text txt5;

    private void Start() {
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService() {
        if (!Input.location.isEnabledByUser) {
            Debug.Log("GPS not enabled.");
            txt0.text = "GPS not enabled.";
            yield break;
        }

        Input.location.Start();
        int iMaxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && iMaxWait > 0) {
            txt0.text = "Initializing.";
            yield return new WaitForSeconds(1);
            iMaxWait--;
        }

        if (iMaxWait <= 0) {
            Debug.Log("Timed out.");
            txt0.text = "Timed out.";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed) {
            Debug.Log("Unable to determine device location.");
            txt0.text = "Unable to determine device location.";
            yield break;
        }

        fLatitude = Input.location.lastData.latitude;
        fLonitude = Input.location.lastData.longitude;
        txt0.text = "Lat: " + fLatitude + " Long: " + fLonitude;
        Input.location.Stop();

        //https://nominatim.openstreetmap.org/reverse?format=xml&lat=52.5487429714954&lon=-1.81602098644987&zoom=18&addressdetails=1
        //WWW www = new WWW("https://nominatim.openstreetmap.org/reverse?format=xml&lat=52.5487429714954&lon=-1.81602098644987&zoom=18");
        WWW www = new WWW("https://nominatim.openstreetmap.org/reverse?email=sherberted@hotmail.co.uk&format=xml&lat=" + fLatitude + "&lon=" + fLonitude + "&zoom=18&addressdetails=1");
        //WWW www = new WWW("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + fLatitude + "," + fLonitude + "&sensor=true");
        yield return www;
        if (www.error != null) {
            txt0.text = "www.error: " + www.error;
            yield break;
        }

        XmlDocument reverseGeocodeResult = new XmlDocument();
        reverseGeocodeResult.LoadXml(www.text);

        //txt.text = www.text;
        int i = 0;
        strCity = null;
        //foreach (XmlNode eachAdressComponent in reverseGeocodeResult.GetElementsByTagName("addressparts").Item(0).ChildNodes) {
        //    foreach (XmlNode xmlNode in eachAdressComponent) {
        //        switch (i) {
        //            case 0:
        //                txt0.text = xmlNode.Value;
        //                break;
        //            case 1:
        //                txt1.text = xmlNode.Value;
        //                break;
        //            case 2:
        //                txt2.text = xmlNode.Value;
        //                break;
        //            case 3:
        //                txt3.text = xmlNode.Value;
        //                break;
        //            case 4:
        //                txt4.text = xmlNode.Value;
        //                break;
        //            case 5:
        //                txt5.text = xmlNode.Value;
        //                break;
        //            default:
        //                break;
        //        }
        //        i++;
        //    }
        //}
    }
}
