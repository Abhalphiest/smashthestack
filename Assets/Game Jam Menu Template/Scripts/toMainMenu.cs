using UnityEngine;
using System.Collections;

public class toMainMenu : MonoBehaviour {

    public void loadMainMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("UI"));
        Application.LoadLevel(0);
    }
}
