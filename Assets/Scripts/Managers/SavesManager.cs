using System.IO;
using UnityEngine;

[DisallowMultipleComponent]
public class SavesManager
{
    public static void SaveState(string fileName, GameState gameState)
    {
        string path = GetSaveFilePath(fileName);
        string dir = Path.GetDirectoryName(path);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        File.WriteAllText(path, JsonUtility.ToJson(gameState));
    }

    public static void SaveBlackboardTexture(string fileName, Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();
        string path = GetSaveFilePath(fileName);
        File.WriteAllBytes(path, bytes);
    }

    public static void LoadBlackboardTexture(string fileName, ref Texture2D texture)
    {
        string path = GetSaveFilePath(fileName);
        if (File.Exists(path))
        {
            byte[] fileData = File.ReadAllBytes(path);
            texture.LoadImage(fileData);
        }
    }

    public static GameState LoadState(string fileName)
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
