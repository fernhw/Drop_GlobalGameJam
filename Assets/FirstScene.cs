using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProInputSystem;

public class FirstScene : MonoBehaviour
{
    AnalogueInput a;
    // Start is called before the first frame update
    void Start()
    {
       ProInput.Init();
    }

    // Update is called once per frame
    void Update()
    {
        ProInput.UpdateInput(Time.deltaTime);
        if (ProInput.A) {
            SceneManager.LoadScene("mainScene");
      }
    }
}
