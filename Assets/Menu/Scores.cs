//using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{

    private const string filename = "Scores.txt";

    public Text textNickname;
    public Text textLevel;
    public Text textDate;
    public Text textScore;

    [Serializable]
    public class Score
    {
        public string NickName;
        public DateTime Time;
        public float Value;
        public int Level;
    }

    [Serializable]
    public class ScoreList
    {
        [SerializeField]
        public List<Score> Scores;
    }

    public void GetScores()
    {
        List<Score> result = new List<Score>();
        if (!File.Exists(filename))
        {
            var stream = File.CreateText(filename);
            stream.Close();
        }

        using (StreamReader sr = new StreamReader(filename))
        {
            string line = sr.ReadToEnd();
            result = (ScoreList)(JsonUtility.FromJson<ScoreList>(line)) == null ? new List<Score>() : JsonUtility.FromJson<ScoreList>(line).Scores;
        }

        // result.Add(new Score() { NickName = "Anna", Level = 1, Value = 1001, Time = DateTime.Now });
        // result.Add(new Score() { NickName = "Stas", Level = 2, Value = 2500, Time = DateTime.Now });
        string nicknames = "",
               levels = "",
               dates = "",
               values = "";
        foreach (Score sc in result)
        {
            nicknames += sc.NickName + "\r\n";
            levels += sc.Level + "\r\n";
            dates += sc.Time.ToString("dd.MM.yyyy HH:mm") + "\r\n";
            values += sc.Value.ToString("f") + "\r\n";
        }

        this.textNickname.text = nicknames;
        this.textLevel.text = levels;
        this.textDate.text = dates;
        this.textScore.text = values;
    }


    public void AddNewScore(string nickName, float value, int level)
    {
        ScoreList scores = new ScoreList()
        {
            Scores = new List<Score>()
        };

        if (!File.Exists(filename))
        {
            var stream = File.CreateText(filename);
            stream.Close();
        }
        using (StreamReader sr = new StreamReader(filename))
        {
            string line = sr.ReadToEnd();
            scores = JsonUtility.FromJson<ScoreList>(line);
        }

        if (scores == null)
        {
            scores = new ScoreList()
            {
                Scores = new List<Score>()
            };
        }

        var scope = new Score()
        {
            NickName = nickName,
            Time = DateTime.Now,
            Value = value,
            Level = level
        };

        scores.Scores.Add(scope);
        scores.Scores.Sort((a, b) => { return b.Value.CompareTo(a.Value); });

        using (StreamWriter sw = new StreamWriter(filename, false))
        {
            string line = JsonUtility.ToJson(scores);
            sw.WriteLine(line);
        }

        Debug.Log("ADD SCORE");
        Debug.Log(JsonUtility.ToJson(scores));
    }
}
