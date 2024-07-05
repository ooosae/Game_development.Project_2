using System;
using System.Collections.Generic;
using System.IO;
using Menu.LeaderBord.Data;
using Newtonsoft.Json;
using UnityEngine;
using File = System.IO.File;

namespace Menu.LeaderBord
{
    public class LeaderBordDataController : MonoBehaviour
    {
        private const string FileName = "leaderbord.json";
        private const string SavesFolderName = "S21Saves";

        private LeaderBordData _leaderBordData;

        public LeaderBordData GetLeaderBordData()
        {
            LoadDataIfNotExists();
            
            return _leaderBordData;
        }

        public void AddLeaderBordResult(int points)
        {
            LoadDataIfNotExists();
            
            _leaderBordData.LeaderBordResults.Add(new LeaderBordResult()
            {
                Date = DateTime.Now.Ticks, 
                Points = points
            });
            
            _leaderBordData.LeaderBordResults.Sort((first, second) =>
            {
                return second.Points.CompareTo(first.Points);
            });

            if (_leaderBordData.LeaderBordResults.Count > 3)
            {
                _leaderBordData.LeaderBordResults.RemoveAt(_leaderBordData.LeaderBordResults.Count - 1);
            }

            SaveData();
        }

        private void LoadDataIfNotExists()
        {
            if (_leaderBordData == null)
            {
                LoadLeaderBordData();
            }

            if (_leaderBordData == null)
            {
                Debug.LogError("huh?");
                return;
            }

            if (_leaderBordData.LeaderBordResults == null)
            {
                _leaderBordData.LeaderBordResults = new List<LeaderBordResult>();
            }
        }

        private void LoadLeaderBordData()
        {
            CreateDataFileIfNotExists();
            
            var json = File.ReadAllText(GetPathToFile());
            _leaderBordData = JsonConvert.DeserializeObject<LeaderBordData>(json);
        }

        private void SaveData()
        {
            SaveData(_leaderBordData);
        }

        private void SaveData(LeaderBordData leaderBordData)
        {
            CreateDataFileIfNotExists();

            var json = JsonConvert.SerializeObject(leaderBordData);
            File.WriteAllText(GetPathToFile(), json);
        }

        private void CreateDataFileIfNotExists()
        {
            var pathToFile = GetPathToFile();
            var pathToSavesDirectory = GetSavesDirectoryPath();
            var fileIsExists = File.Exists(pathToFile);
            var directoryIsExists = Directory.Exists(pathToSavesDirectory);
            if (!directoryIsExists)
            {
                Directory.CreateDirectory(pathToSavesDirectory);
            }
            
            if (!fileIsExists)
            {
                var fileStream = File.Create(pathToFile);
                var streamWriter = new StreamWriter(fileStream);
                streamWriter.Write("{}");
                streamWriter.Close();
            }
        }

        private string GetPathToFile()
        {
            return $"{GetSavesDirectoryPath()}{FileName}";
        }
        
        private string GetSavesDirectoryPath()
        {
            return $"{Application.persistentDataPath}/{SavesFolderName}/";
        }
    }
}