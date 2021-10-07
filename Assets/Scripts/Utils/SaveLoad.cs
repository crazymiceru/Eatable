
using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SaveLoad<T>
{
    private static XmlSerializer _formatter;

    public SaveLoad() => _formatter = new XmlSerializer(typeof(T));

    public void Save(T data, string name = null)
    {
        var path = MakeFullName(name);
        if (data == null && !String.IsNullOrEmpty(path)) return;
        if (!typeof(T).IsSerializable)
        {
            Debug.LogWarning($"Structure {typeof(T)} is not serializable");
            return;
        }
        using (var fs = new FileStream(path, FileMode.Create))
            _formatter.Serialize(fs, data);
    }

    public T Load(string name)
    {
        T result;
        var path = MakeFullName($"{name}");
        if (!File.Exists(path))
        {
            Debug.LogWarning($"Dont find file '{path}' for load");
            return default(T);
        }
        using (var fs = new FileStream(path, FileMode.Open))
        {
            result = (T)_formatter.Deserialize(fs);
        }
        return result;
    }

    private string MakeFullName(string name)
    {
        var path = Path.Combine(Application.dataPath, "Cfg");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        var fullName = Path.Combine(path, name);
        return fullName;
    }
}
