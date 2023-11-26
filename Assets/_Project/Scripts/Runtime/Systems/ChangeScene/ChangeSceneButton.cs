using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneTarget;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => { SceneController.instance.ChangeSceneAsync(sceneTarget); });
    }

}
