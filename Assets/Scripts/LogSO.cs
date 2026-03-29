using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(menuName ="ScriptableObject/Log")]
public class LogSO : ScriptableObject
{

    public List<string> logDate;
    public List<string> log;


}



public static class Log
{
    
    private static LogSO log;

    private static LogSO LoadLog()
    {
        if(log == null)
        {
            log = Resources.Load<LogSO>("ScriptableObjects/Log");
        }
        return log;
        
    }

    public static void AddToLog(string _log)
    {
        log = LoadLog();
        DateTime time = DateTime.Now;
        string logTime = "";

        string fixedSecond;
        string fixedMinute;

        if (time.Second < 10) fixedSecond = $"0{time.Second}";
        else fixedSecond = $"{time.Second}";
                    
        if (time.Minute < 10) fixedMinute = $"0{time.Minute}";
        else fixedMinute = $"{time.Minute}";

        logTime = $"{time.Hour}:{fixedMinute}:{fixedSecond}";

        //var log = Resources.Load<LogSO>("ScriptableObjects/Log");
        log.logDate.Add(logTime);
        log.log.Add(_log);
        //log.
    }

    public static void CleanLog()
    {
        var log = Resources.Load<LogSO>("ScriptableObjects/Log");
        log.logDate.Clear();
        log.log.Clear();
    }
    

    public static void ExportLog()
    {

        log = LoadLog();
        DateTime time = DateTime.Now;

        if (!File.Exists("Log/"))
        {
            Directory.CreateDirectory("Log");
        }

        string fixedSecond;
        string fixedMinute;

        if (time.Second < 10) fixedSecond = $"0{time.Second}";
        else fixedSecond = $"{time.Second}";
                    
        if (time.Minute < 10) fixedMinute = $"0{time.Minute}";
        else fixedMinute = $"{time.Minute}";


        string logTime = $"Log_{time.Hour}{fixedMinute}{fixedSecond}";


        //log = Resources.Load<LogSO>("ScriptableObjects/Log");

        string finalLog = "";

        for(int i = 0; i < log.logDate.Count; i++)
        {
            finalLog += $"[{log.logDate[i]}]{log.log[i]}.\n";
        }

        
        
        //string json = JsonUtility.ToJson(finalLog);

        File.WriteAllText("Log/" + logTime+".txt", finalLog);


    }
}

