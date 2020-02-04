using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IDataStorage
{
    bool Save(string token, object value);
    T Restore<T>(string token);

    bool ContainsKey(string token);

    void RemoveKey(string token);
}

