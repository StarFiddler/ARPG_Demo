using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;
    private int _itemCollected = 0;
    public int Items
    {
        get {return _itemCollected; }
        set {
            _itemCollected = value;
            Debug.LogFormat("Items: {0}", _itemCollected);
        if(_itemCollected >= maxItems)
        {
            labelText = "You've found all the items!";
            showWinScreen = true;
            Time.timeScale = 0f;
        }
        else
        {
            labelText = "Item found, only " + (maxItems - _itemCollected) + "more to go!";
        }
        }
    }
    private int _playerHP = 10;
    public int HP
    {
        get { return _playerHP; }
        set { _playerHP = value;
        if(_playerHP <= 0)
        {
            labelText = "You want another life with that?";
            showLossScreen = true;
            Time.timeScale = 0;
        }
        else
        {
            labelText = "Oh... that's got hurt.";
        }
        Debug.LogFormat("Lives: {0}", _playerHP);}
    }
    // Start is called before the first frame update 
    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100), "YOU WON!"))
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1.0f;
            }
        }
        if(showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100), "YOU loss!"))
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1.0f;
            }
        }
    }
    
}
