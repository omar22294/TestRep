using System;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

namespace UniqueKeyGenerator
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class UniqueKeys
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		private const int LEN = 1000000;
		
		[STAThread]
		static void Main(string[] args)
		{
						
			UniqueKeys uk = new UniqueKeys();
			
			string[] keys = new string[LEN];
			
			DateTime start = DateTime.Now;
			Console.WriteLine("Generated Keys using Guid");
			
			for(int loop = 0; loop < LEN ; loop++)
			{
				keys[loop] = uk.UsingGuid();
			}
			
			Console.WriteLine("---------------------------------- in " + (DateTime.Now - start));
			
			Hashtable e = uk.Frequency(keys);
						
			int rep = 0;
			foreach( string key in keys)
			{
				if ((int) e[key] > 0)
				{
					Console.WriteLine("Key : " + key + " Frequency : " + e[key].ToString());
					rep++;
				}
			}
			Console.WriteLine(rep + "----------------------------------");



			keys = new string[LEN];
			start = DateTime.Now;
			
			Console.WriteLine("Generated Keys using RNG Character Mask");
			for(int loop = 0; loop < LEN ; loop++)
			{
				keys[loop] = uk.RNGCharacterMask();
			}
			
			Console.WriteLine("---------------------------------- in " + (DateTime.Now - start));
			e = uk.Frequency(keys);
		
			foreach( string key in keys)
			{
				if ((int) e[key] > 0)
				{
					Console.WriteLine("Key : " + key + " Frequency : " + e[key].ToString());
					rep++;
				}
			}
			Console.WriteLine(rep + "----------------------------------");

//		
//			
			Console.ReadLine();
		}


		private  string UsingGuid()
		{
			string result = Guid.NewGuid().ToString().GetHashCode().ToString("x");
			return result;
		}


		private string UsingTicks()
		{
			string val  = DateTime.Now.Ticks.ToString("x");
			return val;
		}


		private string RNGCharacterMask()
		{
		int maxSize  = 8 ;
		int minSize = 5 ;
		char[] chars = new char[62];
		string a;
		a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
		chars = a.ToCharArray();
		int size  = maxSize ;
		byte[] data = new byte[1];
		RNGCryptoServiceProvider  crypto = new RNGCryptoServiceProvider();
		crypto.GetNonZeroBytes(data) ;
		size =  maxSize ;
		data = new byte[size];
		crypto.GetNonZeroBytes(data);
		StringBuilder result = new StringBuilder(size) ;
		foreach(byte b in data )
		{ result.Append(chars[b % (chars.Length - 1)]); }
			return result.ToString();
		}


		private string UsingDateTime()
		{
			return DateTime.Now.ToString().GetHashCode().ToString("x");
		}
		

		private Hashtable  Frequency(string[] keys)
		{
			Hashtable  freq = new Hashtable(LEN);

			foreach(string key in keys)
			{
				if (freq[key] == null)
				{
					freq.Add(key, 0);
				}
				else
				{
					freq[key] = (int) freq[key] +1;
				}
			}
			return freq;
		}
		

	}
}
