using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.ShaderGraph.Internal;

namespace SaveLoadSystem
{
    public static class SaveGameManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        public const string SaveDirectory = "/SaveData/";
        public const string FileName = "SaveGame.sav";

        public static readonly string keyWord = "7395815";

        public static bool SaveGame()
        {
            var dir = Application.persistentDataPath + SaveDirectory;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonUtility.ToJson(CurrentSaveData, true);
            var encryptedJson = EncryptDecrypt(json);
            File.WriteAllText(dir + FileName, encryptedJson);

            GUIUtility.systemCopyBuffer = dir;

            return true;
        }

        public static void LoadGame()
        {
            string fullPath = Application.persistentDataPath + SaveDirectory + FileName;
            SaveData tempData = new SaveData();

            if (File.Exists(fullPath))
            {
                var json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(EncryptDecrypt(json));
            }
            else
            {
                Debug.LogError("Save file does not exist");
            }

            CurrentSaveData = tempData;
        }

        private static string EncryptDecrypt(string data)
        {
            string result = "";

            for (int i = 0; i < data.Length; i++)
            {
                result += (char)(data[i] ^ keyWord[i % keyWord.Length]);
            }

            return result;
        }
    }
}
