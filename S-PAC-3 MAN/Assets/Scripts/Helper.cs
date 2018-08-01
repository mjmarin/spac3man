using System; 							/* Convert class */
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;		/* List class */

public class Helper {

	/* Protection data */
	private static string hash = "c57f431343f100b441e268cc12babc34";
	public static string Encrypt(string toEncrypt){

		byte[] data = UTF8Encoding.UTF8.GetBytes(toEncrypt);

		MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));

		TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider(){Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};

		ICryptoTransform tr = trip.CreateEncryptor();
		byte[] encrypted = tr.TransformFinalBlock(data,0,data.Length);

		return Convert.ToBase64String(encrypted,0,encrypted.Length);
	}

	public static string Decrypt(string toDecrypt){

		byte[] data = Convert.FromBase64String(toDecrypt);

		MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));

		TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider(){Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};

		ICryptoTransform tr = trip.CreateDecryptor();
		byte[] decrypted = tr.TransformFinalBlock(data,0,data.Length);

		return UTF8Encoding.UTF8.GetString(decrypted);
	}

	/* It gives random integers without repetition */
	public static int[] RandomIntValues(int count, int minRange, int maxRange){
		List<int> usedValues = new List<int>();	
		int value;

		value = UnityEngine.Random.Range(minRange, maxRange);
		usedValues.Add(value);

		while(usedValues.Count < count){
			value = UnityEngine.Random.Range(minRange, maxRange);
			while(usedValues.Contains(value)){
				value = UnityEngine.Random.Range(minRange, maxRange);
			}
			usedValues.Add(value);
		}
		return usedValues.ToArray();
	}
}
