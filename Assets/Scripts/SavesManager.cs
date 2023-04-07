using System.IO;
using UnityEngine;

[DisallowMultipleComponent]
public class SavesManager
{
    public static void Save(string fileName, GameState gameState)
    {
        string path = GetSaveFilePath(fileName);
        string dir = Path.GetDirectoryName(path);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        File.WriteAllText(path, JsonUtility.ToJson(gameState));
    }

    public static GameState Load(string fileName)
    {
        string path = GetSaveFilePath(fileName);

        if (!IsSavePresent(path))
            return null;

        return JsonUtility.FromJson<GameState>(File.ReadAllText(path));
    }

    public static bool IsSavePresent(string path) => File.Exists(path);

    public static string GetSaveFilePath(string fileName) => Path.Combine(Application.persistentDataPath, "save", fileName);

    public static void DeleteSaves(string fileName) => File.Delete(GetSaveFilePath(fileName));
}
