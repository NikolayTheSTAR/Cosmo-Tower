using System;
using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;
//using TheSTAR.Utility;
using UnityEngine.Serialization;

namespace TheSTAR.Data
{
    public sealed class DataController : MonoBehaviour
    {
        private const string GameDataFileName = "game_data.json";
        private string ProfileDataFileName(int profileIndex)
        {
            return $"profile_{profileIndex.ToString()}.json";
        }

        public GameData gameData = new();

        [SerializeField] private bool clearData = false;

        #region Unity Methodes

        private void Awake()
        {
            if (clearData) LoadDefault();
            else Load();
        }
        
        #endregion

        private string GameDataPath => Path.Combine(Application.persistentDataPath, GameDataFileName);
        public string ProfileDataPath(int profileIndex)
        {
            string value = Path.Combine(Application.persistentDataPath, ProfileDataFileName(profileIndex));
            return value;
        }
        
        [ContextMenu("Save")]
        public void Save()
        {
            SerializationToJson(out GameData newData);

            string jsonString = JsonUtility.ToJson(newData, true);
            File.WriteAllText(GameDataPath, jsonString);
        }

        [ContextMenu("Load")]
        private void Load()
        {
            if (File.Exists(GameDataPath))
            {
                string jsonString = File.ReadAllText(GameDataPath);
                GameData newData = JsonUtility.FromJson<GameData>(jsonString);
                DeserializationFromJson(newData);
            }
            else LoadDefault();
        }

        [ContextMenu("ClearData")]
        private void LoadDefault()
        {        
            gameData = new();
        }

        // Запись данных
        private void SerializationToJson(out GameData newGameData)
        {
            newGameData = gameData;
        }

        // Чтение данных
        private void DeserializationFromJson(GameData fileGameData)
        {
            gameData = fileGameData;
        }
        
        [Serializable]
        public class GameData
        {
            public bool isMusicOn = true;
            public bool isSoundsOn = true;
        }
    }
}