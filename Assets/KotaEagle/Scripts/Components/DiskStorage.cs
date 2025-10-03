using Newtonsoft.Json;
using UnityEngine;

public class DiskStorage
{
	private StringCipher cipher;
	public DiskStorage()
	{
		cipher = new StringCipher();
	}


	private string Encrypt(object obj, string key)
	{
		string data = JsonConvert.SerializeObject(obj);
		return cipher.Encrypt(data, key);
	}
	private T Decrypt<T>(string block, string key)
	{
		string data = cipher.Decrypt(block, key);
		return JsonConvert.DeserializeObject<T>(data);
	}


	public void SaveData(object obj, string key)
	{
		string block = Encrypt(obj, key);
		PlayerPrefs.SetString(key, block);
	}
	public T GetSavedData<T>(string key)
	{
		if (!PlayerPrefs.HasKey(key)) return default(T);

		string block = PlayerPrefs.GetString(key);
		return Decrypt<T>(block, key);
	}
	public void DeleteData(string key)
	{
		PlayerPrefs.DeleteKey(key);
	}
	public void DeleteAllData()
	{
		PlayerPrefs.DeleteAll();
	}
}