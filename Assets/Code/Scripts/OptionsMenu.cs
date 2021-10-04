using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using SimpleFileBrowser;
using System.IO;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private void SelectFile(System.Action<AudioClip> callback, Text currentFile )
    {
        IEnumerator GetAudioClip(string path)
        {
            var url = "file:///" + path;
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.UNKNOWN))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    currentFile.text = FileBrowserHelpers.GetFilename(path);
                    callback(clip);
                }
            }
        }

        void OnSuccess(string[] paths)
        {
            currentFile.text = "Cargando...";
            var path = paths[0];        
            StartCoroutine(GetAudioClip(path));
            
        }

        void OnCancel()
        {
        }

        var pickMode = FileBrowser.PickMode.Files;
        var buttonText = "Seleccionar";
        var title = "Selecione un archivo de audio";
        var fileFilter = new FileBrowser.Filter("Archivos de audio", ".mp3", ".ogg", ".wav", ".aiff", ".aif", ".mod", ".it", ".s3m", ".xm");

        FileBrowser.SetFilters(false, fileFilter);
        FileBrowser.ShowLoadDialog(OnSuccess, OnCancel, pickMode, false, null, title, buttonText);
    }

    public void SelectFileLenguaje(int index)
    {
        Transform filename = transform.Find("Scrollback/Options/Lenguaje/Filename" + (index + 1));
        SelectFile((clip) => GameStateGlobal.SetLenguajeClip(index, clip), filename.GetComponent<Text>());
    }
    public void SelectFileMatematicas(int index)
    {
        Transform filename = transform.Find("Scrollback/Options/Matematicas/Filename" + (index + 1));
        SelectFile((clip) => GameStateGlobal.SetMatematicasClip(index, clip), filename.GetComponent<Text>());
    }
    public void SelectFileBiologia(int index)
    {
        Transform filename = transform.Find("Scrollback/Options/Biologia/Filename" + (index + 1));
        SelectFile((clip) => GameStateGlobal.SetBiologiaClip(index, clip), filename.GetComponent<Text>());
    }
    public void SelectFileInformar(int index)
    {
        Transform filename = transform.Find("Scrollback/Options/Informar/Filename" + (index + 1));
        SelectFile((clip) => GameStateGlobal.SetInformarClip(index, clip), filename.GetComponent<Text>());
    }
}
