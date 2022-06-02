using Newtonsoft.Json;

namespace FunctionTestRunner.Utils;

public class ScenarioPropertyBag
{
    private readonly Dictionary<string, Tuple<bool, string>> _bag;

    public static ScenarioPropertyBag Create()
    {
        return new ScenarioPropertyBag();
    }

    private ScenarioPropertyBag()
    {
        _bag = new Dictionary<string, Tuple<bool, string>>();
    }

    public void Require(params string[] keys)
    {
        var missingKeys = new List<string>();
        foreach (var key in keys)
        {
            if (!_bag.ContainsKey(key))
            {
                missingKeys.Add(key);
            }
        }

        if (missingKeys.Count > 0)
        {
            throw new Exception($"The following data item(s) are not set in data bag: {string.Join(", ", missingKeys)}. Scenario is aborted.");
        }
    }

    public bool ContainsKeys(params string[] keys)
    {
        var missingKeys = new List<string>();
        foreach (var key in keys)
        {
            if (!_bag.ContainsKey(key))
            {
                missingKeys.Add(key);
            }
        }

        return missingKeys.Count == 0;
    }

    public bool ContainsKeys(params DataBagKey[] keys)
    {
        return ContainsKeys(keys.Select(x => x.ToString()).ToArray());
    }

    public T Get<T>(string key)
    {
        if (!_bag.ContainsKey(key) || !_bag[key].Item1)
        {
            return default(T);
        }

        return JsonConvert.DeserializeObject<T>(_bag[key].Item2);
    }

    public void Set(string key, object value)
    {
        var insert = new Tuple<bool, string>(value != null, value == null ? string.Empty : JsonConvert.SerializeObject(value));
        if (_bag.ContainsKey(key))
        {
            _bag[key] = insert;
        }
        else
        {
            _bag.Add(key, insert);
        }
    }

    public void RemoveKey(string key)
    {
        if (_bag.ContainsKey(key))
        {
            _bag.Remove(key);
        }
    }

    internal object Get<T>(object labelId)
    {
        throw new NotImplementedException();
    }
}
