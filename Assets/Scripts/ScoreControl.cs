using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScoreControl : MonoBehaviour
{
    public Text bestScoreText;
    public string bestName;
    public int bestScore;
    public int currentScore;
    public string currentName;
    public MainManager mainManager;

    // Start is called before the first frame update
    void Awake()
    {
        currentName = DataManager.Instance.playerName;
        LoadScore();
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = mainManager.m_Points;
        bestScoreText.text = "Best Score:" + bestName + ":" + bestScore;
        if(bestScore < currentScore)
        {
            bestScore = currentScore;
            bestName = currentName;
            SaveScore();
        }
    }
    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string bestName;
    }
    public void SaveScore ()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.bestName = bestName;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            bestName = data.bestName;
        }
    }

}
