using System; 							/* Convert class */
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;       /* List class */
using System.Globalization;				/* CultureInfo class */

public class Helper {

	/* String con el que formar el hash */
	private static string hash = "c57f431343f100b441e268cc12babc34";
	
	/* Encriptación base */
	public static string EncryptString(string toEncrypt){
		if(toEncrypt.Length == 0){ // Si no hay algo que encriptar
			return "-1";
		}else{
			byte[] data = UTF8Encoding.UTF8.GetBytes(toEncrypt);

			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash)); //Hacer el hash

			TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() //Encriptador ECB
			{Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};

			ICryptoTransform tr = trip.CreateEncryptor();
			byte[] encrypted = tr.TransformFinalBlock(data,0,data.Length); //Encriptar en ECB

			return Convert.ToBase64String(encrypted,0,encrypted.Length); //Devolver string encriptada
		}
	}

	public static string EncryptInt(int toEncrypt){
		string str = toEncrypt.ToString();
		return EncryptString(str);
	}

	public static string EncryptFloat(float toEncrypt){
		string str = toEncrypt.ToString();
		return EncryptString(str);
	}

	public static string EncryptBool(bool toEncrypt){
		int boolInt;
		if(toEncrypt){
			boolInt = 1;
		}else{
			boolInt = 0;
		}
		return EncryptInt(boolInt);
	}

	/* Desencriptación base */
	public static string DecryptString(string toDecrypt){
		if(toDecrypt.Length == 0){ // Si no hay algo que encriptar
			return "-1";
		}else{
			byte[] data = Convert.FromBase64String(toDecrypt);

			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash)); // Hacer el hash

			TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() 
			{Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7}; //Desencriptador ECB

			ICryptoTransform tr = trip.CreateDecryptor();
			byte[] decrypted = tr.TransformFinalBlock(data,0,data.Length); // Desencriptado

			return UTF8Encoding.UTF8.GetString(decrypted); // Devolver string desencriptada
		}
	}

	public static int DecryptInt(string toDecrypt){
		string result = DecryptString(toDecrypt);
		return Int32.Parse(result);
	}

	public static float DecryptFloat(string toDecrypt){
		string result = DecryptString(toDecrypt);
		return float.Parse(result, CultureInfo.InvariantCulture.NumberFormat);
	}

	public static bool DecryptBool(string toDecrypt){
		int intBool = DecryptInt(toDecrypt);
		if(intBool > 0){
			return true;
		}else{
			return false;
		}
	}

	/* Devuelve un array de enteros entre minRange(incluido) y maxRange(excluido) sin repetición */
	public static int[] RandomIntValues(int count, int minRange, int maxRange){
		List<int> usedValues = new List<int>();	
		if(count <= maxRange - minRange ){
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
		}
		return usedValues.ToArray();
	}

	/* Funciónes de comprobación de overflow */
	public static bool CheckUlongOverflow(float number){
		if(ulong.MaxValue < number){
			return true;
		}else{
			return false;
		}
	}
	public static bool CheckLongOverflow(float number){
		if(long.MaxValue < number){
			return true;
		}else{
			return false;
		}
	}
}
