using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject boss1;
    public GameObject canvas;
    public Text infoText;

    public void OnQuit()
    {
        Application.Quit();
    }

    public void Reload()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene( 0 );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( boss1 == null )
        {
            infoText.text = "Victory!";
            canvas.SetActive( true );
        }
    }

    public void SetDefeatInfo()
    {
        infoText.text = "Defeat";
        canvas.SetActive( true );
    }
}
