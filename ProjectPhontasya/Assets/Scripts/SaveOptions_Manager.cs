using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class SaveOptions_Manager : MonoBehaviour
{
    private static string _pathOptionXml;
    private static string _fileName = "Options.xml";

    private void Awake()
    {
        //pathOptionXml = "Assets/";
        _pathOptionXml = Application.persistentDataPath;
    }

    public static bool ReturnFileExist()
    {
        if (File.Exists($"{_pathOptionXml}{_fileName}"))
            return true;

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateSaveOption();

        XmlTextReader XmlTR = new XmlTextReader($"{_pathOptionXml}{_fileName}");
        while (XmlTR.Read())
        {
            if (XmlTR.MoveToAttribute("Volume"))
                FMOD_Sound_Manager.SetVolume(XmlTR.ReadContentAsFloat());

            if (XmlTR.MoveToAttribute("VelocityCamera"))
                GameplayCamera.rotationSpeed = XmlTR.ReadContentAsFloat();

            if (XmlTR.MoveToAttribute("InvertY"))
                GameplayCamera.invertYTouch = XmlTR.ReadContentAsBoolean();

            if (XmlTR.MoveToAttribute("Resolution"))
                Options_Manager.currentGraphicSet = XmlTR.ReadContentAsInt();
        }
    }

    /*public static string ReturnLanguage()
    {
        XmlTextReader XmlTR = new XmlTextReader($"{pathOptionXml}{fileName}");
        while (XmlTR.Read())
        {
            if (XmlTR.MoveToAttribute("Language"))
            {
                return XmlTR.ReadContentAsString();
            }
        }

        return null;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateSaveOption()
    {
        if (!File.Exists($"{_pathOptionXml}{_fileName}"))
        {
            //VIENE CREATO
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("data");
            doc.AppendChild(root);

            //----------------------CREATE APPEND AUDIO-------------------------------------------
            XmlElement tileVolume = doc.CreateElement("audio");
            root.AppendChild(tileVolume);
            //----------------------MASTER VOLUME-------------------------------------------
            XmlAttribute attributeVolume = doc.CreateAttribute("Volume");

            attributeVolume.Value = FMOD_Sound_Manager.volume.ToString().Replace(',', '.');
            tileVolume.Attributes.Append(attributeVolume);

            //---------------------------CREATE APPEND CAMERA------------------------------------
            XmlElement tileVelocityCamera = doc.CreateElement("camera");
            root.AppendChild(tileVelocityCamera);
            //----------------------------VELOCITY CAMERA--------------------------------------
            XmlAttribute attributeVelCamera = doc.CreateAttribute("VelocityCamera");

            attributeVelCamera.Value = GameplayCamera.rotationSpeed.ToString().Replace(',', '.');
            tileVelocityCamera.Attributes.Append(attributeVelCamera);
            //----------------------------INVERT Y---------------------------------------------
            XmlAttribute attributeInvertY = doc.CreateAttribute("InvertY");

            attributeInvertY.Value = SaveStringLikeBoolean(GameplayCamera.invertYTouch.ToString());
            tileVelocityCamera.Attributes.Append(attributeInvertY);

            //---------------------------CREATE APPEND GRAPHIC------------------------------------
            XmlElement tileGraphic = doc.CreateElement("graphic");
            root.AppendChild(tileGraphic);
            //----------------------------RESOLUTION---------------------------------------------
            XmlAttribute attributeResolution = doc.CreateAttribute("Resolution");

            attributeResolution.Value = Options_Manager.currentGraphicSet.ToString();
            tileGraphic.Attributes.Append(attributeResolution);

            doc.Save($"{_pathOptionXml}{_fileName}");
        }
    }

    public static void OverrideSaveOptions()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load($"{_pathOptionXml}{_fileName}");

        XmlAttribute volumeAttribute = (XmlAttribute)doc.SelectSingleNode("data/audio/@Volume");
        volumeAttribute.Value = FMOD_Sound_Manager.volume.ToString().Replace(',', '.');
        
        XmlAttribute velocityCameraAttribute = (XmlAttribute)doc.SelectSingleNode("data/camera/@VelocityCamera");
        velocityCameraAttribute.Value = GameplayCamera.rotationSpeed.ToString().Replace(',', '.');
        
        XmlAttribute invertYAttribute = (XmlAttribute)doc.SelectSingleNode("data/camera/@InvertY");
        invertYAttribute.Value = SaveStringLikeBoolean(GameplayCamera.invertYTouch.ToString());

        XmlAttribute ResolutionAttribute = (XmlAttribute)doc.SelectSingleNode("data/graphic/@Resolution");
        ResolutionAttribute.Value = Options_Manager.currentGraphicSet.ToString();

        doc.Save($"{_pathOptionXml}{_fileName}");
    }

    static string SaveStringLikeBoolean(string nameVariable)
    {
        if (nameVariable == "True")
            return "true";
        else if (nameVariable == "False")
            return "false";

        return null;
    }
}
