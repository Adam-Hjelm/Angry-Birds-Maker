using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMeuButtonListeners : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int buttons = transform.childCount;
        for (int i = 0; i < buttons; i++)
        {
            switch (i)
            {
                case 0:
                    transform.GetChild(0).GetComponent<Button>().onClick.AddListener(SceneTransitionHandler.Instance.GoToLevelSelectScene);
                    break;

                case 1:
                    transform.GetChild(1).GetComponent<Button>().onClick.AddListener(SceneTransitionHandler.Instance.GoToBuildLevelScene);
                    break;

                case 2:
                    transform.GetChild(2).GetComponent<Button>().onClick.AddListener(SceneTransitionHandler.Instance.QuitGame);
                    break;
            }
        }
    }
}
