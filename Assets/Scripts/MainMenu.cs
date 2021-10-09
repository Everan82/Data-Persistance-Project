using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public static string playerName;
    public static string Best;
    public static int hiScore;
    //public TextMeshPro ;
    public TMPro.TMP_Text BestScore;
        //MeshPro BestScore;
    private void Awake()
    {
        LoadHiScore();
        BestScore.text = "Best Score: " + MainMenu.Best + " " + MainMenu.hiScore;
        /*if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //LoadName();*/
    }
    public void TrovaNome(string s)
    {
        playerName = s;
        
    }
    public void StartNew()
    {
        
        Debug.Log(playerName);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        //MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public int hiScore;
        public string Best;
    }

    public static void SaveHiScore()
    {
        SaveData data = new SaveData();
        data.Best = Best;
        data.hiScore = hiScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); 
    }

    public static void LoadHiScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            hiScore = data.hiScore;
            Best = data.Best;
        }
    }
}
