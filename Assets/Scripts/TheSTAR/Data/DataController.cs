using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

namespace TheSTAR.Data
{
    public sealed class DataController : MonoBehaviour
    {
        private const string GameDataFileName = "game_data.json";

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
        
        [ContextMenu("Save")]
        public void Save()
        {
            JsonSerializerSettings settings = new() { TypeNameHandling = TypeNameHandling.Objects };
            string jsonString = JsonConvert.SerializeObject(gameData, Formatting.Indented, settings);
            File.WriteAllText(GameDataPath, jsonString);
        }

        [ContextMenu("Load")]
        private void Load()
        {
            if (File.Exists(GameDataPath))
            {
                string jsonString = File.ReadAllText(GameDataPath);
                gameData = JsonConvert.DeserializeObject<GameData>(jsonString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
            }
            else LoadDefault();
        }

        [ContextMenu("ClearData")]
        private void LoadDefault()
        {        
            gameData = new();
        }
        
        [Serializable]
        public class GameData
        {
            public bool isMusicOn = true;
            public bool isSoundsOn = true;

            public BattleData battleData = new();
        }

        [Serializable]
        public class BattleData
        {
            public Dictionary<CurrencyType, int> currencyData = new();

            public void AddCurrency(CurrencyType currencyType, int count, out int result)
            {
                if (currencyData.ContainsKey(currencyType)) currencyData[currencyType] += count;
                else currencyData.Add(currencyType, count);

                result = currencyData[currencyType];
            }

            public int GetCurrencyCount(CurrencyType currencyType)
            {
                if (currencyData.ContainsKey(currencyType)) return currencyData[currencyType];
                else return 0;
            }
        }
    }
}