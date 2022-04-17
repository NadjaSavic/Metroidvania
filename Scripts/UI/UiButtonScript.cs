using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiButtonScript : MonoBehaviour
{

    public void ClickStart()
    {
        GameManager.Instance.LoadNextScene(1);
    }
}
