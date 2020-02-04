using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Windows.Storage;


public class DataStorage : IDataStorage
{
    public bool Save(string key, object value)
    {
        //if (null == value)
        //    return false;
        if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
        {
            if (ApplicationData.Current.LocalSettings.Values[key].ToString() != null)
            {
                // do update
                try
                {
                    ApplicationData.Current.LocalSettings.Values[key] = value;
                }
                catch
                {

                    string jsonValue = Serialize(value);
                    ApplicationData.Current.LocalSettings.Values[key] = jsonValue;
                }

            }
        }

        else
        {
            // do create key and save value, first time only.
            ApplicationData.Current.LocalSettings.CreateContainer(key, ApplicationDataCreateDisposition.Always);

            if (ApplicationData.Current.LocalSettings.Values[key] == null)
            {
                try
                {
                    ApplicationData.Current.LocalSettings.Values.Add(key, value);
                }
                catch (Exception ex)
                {

                    string jsonValue = Serialize(value);
                    ApplicationData.Current.LocalSettings.Values.Add(key, jsonValue);
                }

            }
        }
       
        return true;
    }

    public T Restore<T>(string token)
    {
        var store = ApplicationData.Current.LocalSettings;
        if (!store.Values.ContainsKey(token))
        {
            return default(T);
        }
        try
        {
            return (T)store.Values[token];
        }
        catch (Exception)
        {
            string jsonValue = store.Values[token].ToString();
            return Deserialize<T>(jsonValue);
        }
    }

    public bool ContainsKey(string token)
    {
        var store = Windows.Storage.ApplicationData.Current.LocalSettings;
        if (!store.Values.ContainsKey(token))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RemoveKey(string token)
    {
        var store = Windows.Storage.ApplicationData.Current.LocalSettings;
        if (store.Values.ContainsKey(token))
        {
            store.Values.Remove(token);
        }

    }

    /// <summary>Serializes the specified object as a JSON string</summary>
    /// <param name="objectToSerialize">Specified object to serialize</param>
    /// <returns>JSON string of serialzied object</returns>
    private static string Serialize(object objectToSerialize)
    {
        using (System.IO.MemoryStream _Stream = new System.IO.MemoryStream())
        {
            try
            {
                var _Serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(objectToSerialize.GetType());
                _Serializer.WriteObject(_Stream, objectToSerialize);
                _Stream.Position = 0;
                System.IO.StreamReader _Reader = new System.IO.StreamReader(_Stream);
                return _Reader.ReadToEnd();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Serialize:" + e.Message);
                return string.Empty;
            }
        }
    }

    /// <summary>Deserializes the JSON string as a specified object</summary>
    /// <typeparam name="T">Specified type of target object</typeparam>
    /// <param name="jsonString">JSON string source</param>
    /// <returns>Object of specied type</returns>
    public static T Deserialize<T>(string jsonString)
    {
        using (System.IO.MemoryStream _Stream = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
        {
            try
            {
                var _Serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                return (T)_Serializer.ReadObject(_Stream);
            }
            catch (Exception) { throw; }
        }
    }

}

