using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormController : MonoBehaviour
{
    public void SendForm()
    {
        var name = transform.Find("Name/InputField").GetComponent<InputField>().text;
        var age = transform.Find("Age/InputField").GetComponent<InputField>().text;
        var experience = transform.Find("Experience/InputField").GetComponent<InputField>().text;
        var genreId = transform.Find("Genre/Dropdown").GetComponent<Dropdown>().value;
        var genre = transform.Find("Genre/Dropdown").GetComponent<Dropdown>().options[genreId].text;
        var disciplineId = transform.Find("Discipline/Dropdown").GetComponent<Dropdown>().value;
        var discipline = transform.Find("Discipline/Dropdown").GetComponent<Dropdown>().options[disciplineId].text;

        GameObject.FindWithTag("GameController").TryGetComponent(out ServerManager server);
        server.StartGame();
        server.SaveClientData(name, age, genre, experience, discipline);
    }

     private void EnableContinue()
    {
        transform.Find("Play/Text").GetComponent<Text>().color = new Color(1, 164f / 255, 0);
        transform.Find("Play").GetComponent<Button>().interactable = true;
    }

    private void DisableContinue()
    {
        transform.Find("Play/Text").GetComponent<Text>().color = new Color(0, 0, 0);
        transform.Find("Play").GetComponent<Button>().interactable = false;
    }

    public void VerifyfValidity()
    {
        var t1 = transform.Find("Name/InputField").GetComponent<InputField>().text;
        var t2 = transform.Find("Age/InputField").GetComponent<InputField>().text;
        var t3 = transform.Find("Experience/InputField").GetComponent<InputField>().text;

        var isValid = t1.Length > 0 && t2.Length > 0 && t3.Length > 0;
        if (isValid) EnableContinue();
        else DisableContinue();
    }
}
