using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006B4 RID: 1716
	public class CryptoHelper
	{
		// Token: 0x06001FEA RID: 8170 RVA: 0x0003D5AC File Offset: 0x0003B7AC
		internal CryptoHelper(string keyContainer, string password)
		{
			this.cspParams = new CspParameters
			{
				KeyContainerName = keyContainer,
				ProviderType = 1,
				KeyNumber = 1,
				Flags = CspProviderFlags.UseDefaultKeyContainer
			};
			this.pdb = new PasswordDeriveBytes(password, null, this.cspParams);
			this.tdes = new TripleDESCryptoServiceProvider
			{
				Key = this.pdb.CryptDeriveKey("TripleDES", "MD5", 0, new byte[8])
			};
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0003D62A File Offset: 0x0003B82A
		protected CryptoHelper()
		{
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0003D632 File Offset: 0x0003B832
		public static CryptoHelper GetInstanceFromGlobalObject(string password)
		{
			return CryptoHelper.GetInstanceFromGlobalObject("CryptoHelper", password);
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0003D63F File Offset: 0x0003B83F
		public static CryptoHelper GetInstance(string password)
		{
			return CryptoHelper.GetInstance("CryptoHelper", password);
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0003D64C File Offset: 0x0003B84C
		public static CryptoHelper GetInstance(string keyContainer, string password)
		{
			if (keyContainer == null)
			{
				throw new ArgumentNullException("keyContainer");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			CryptoHelper result;
			lock (CryptoHelper.cryptoHelperTableLock)
			{
				IDictionary<string, CryptoHelper> dictionary;
				if (CryptoHelper.cryptoHelperTable.ContainsKey(keyContainer))
				{
					dictionary = CryptoHelper.cryptoHelperTable[keyContainer];
				}
				else
				{
					dictionary = new Dictionary<string, CryptoHelper>();
					CryptoHelper.cryptoHelperTable.Add(keyContainer, dictionary);
				}
				CryptoHelper cryptoHelper;
				if (dictionary.ContainsKey(password))
				{
					cryptoHelper = dictionary[password];
				}
				else
				{
					cryptoHelper = new CryptoHelper(keyContainer, password);
					dictionary.Add(password, cryptoHelper);
				}
				result = cryptoHelper;
			}
			return result;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		public static CryptoHelper GetInstanceFromGlobalObject(string keyContainer, string password)
		{
			if (keyContainer == null)
			{
				throw new ArgumentNullException("keyContainer");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			IDictionary<string, CryptoHelper> dictionary = null;
			CryptoHelper result = null;
			if (CryptoHelper.cryptoHelperTable.ContainsKey(keyContainer))
			{
				dictionary = CryptoHelper.cryptoHelperTable[keyContainer];
			}
			if (dictionary != null && dictionary.ContainsKey(password))
			{
				result = dictionary[password];
			}
			return result;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0003D757 File Offset: 0x0003B957
		public static void BuildCryptoHelperTable(string password)
		{
			CryptoHelper.BuildCryptoHelperTable("CryptoHelper", password);
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0003D764 File Offset: 0x0003B964
		public static void BuildCryptoHelperTable(string keyContainer, string password)
		{
			if (keyContainer == null)
			{
				throw new ArgumentNullException("keyContainer");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			IDictionary<string, CryptoHelper> dictionary;
			if (CryptoHelper.cryptoHelperTable.ContainsKey(keyContainer))
			{
				dictionary = CryptoHelper.cryptoHelperTable[keyContainer];
			}
			else
			{
				dictionary = new Dictionary<string, CryptoHelper>();
				CryptoHelper.cryptoHelperTable.Add(keyContainer, dictionary);
			}
			CryptoHelper value;
			if (dictionary.ContainsKey(password))
			{
				value = dictionary[password];
				return;
			}
			value = new CryptoHelper(keyContainer, password);
			dictionary.Add(password, value);
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0003D7E0 File Offset: 0x0003B9E0
		public virtual string Encrypt(string inputText)
		{
			byte[] inArray = this.DoEncrypt(inputText);
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0003D7FC File Offset: 0x0003B9FC
		public virtual string Decrypt(string inputText)
		{
			if (inputText == null)
			{
				throw new ArgumentNullException("inputText");
			}
			byte[] decodedBytes = Convert.FromBase64String(inputText);
			return this.DoDecrypt(decodedBytes);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0003D828 File Offset: 0x0003BA28
		internal string GenerateHint(string hintMaterial)
		{
			if (hintMaterial == null)
			{
				throw new ArgumentNullException("hintMaterial");
			}
			int num = 5381;
			for (int i = 0; i < hintMaterial.Length; i++)
			{
				num = num * 33 + (int)hintMaterial[i];
			}
			byte[] array = new byte[4];
			for (int j = 3; j >= 0; j--)
			{
				array[j] = (byte)(num % 256);
				num >>= 8;
			}
			string text = Convert.ToBase64String(array);
			if (text.Length < 5)
			{
				text += "EEEEE";
			}
			return text.Substring(0, 5);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0003D8B4 File Offset: 0x0003BAB4
		internal string EncryptWithHint(string inputText, string hint, int hintOffset)
		{
			if (hint == null)
			{
				throw new ArgumentNullException("hint");
			}
			if (hintOffset < 0)
			{
				throw new ArgumentOutOfRangeException("hintOffset");
			}
			byte[] inArray = this.DoEncrypt(inputText);
			string text = Convert.ToBase64String(inArray);
			if (text.Length < hintOffset)
			{
				throw new InvalidOperationException("hintOffset is too big");
			}
			return text.Substring(0, hintOffset) + hint + text.Substring(hintOffset);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0003D918 File Offset: 0x0003BB18
		internal string DecryptWithHint(string inputText, string hint, int hintOffset)
		{
			if (inputText == null)
			{
				throw new ArgumentNullException("inputText");
			}
			if (hint == null)
			{
				throw new ArgumentNullException("hint");
			}
			if (hintOffset < 0)
			{
				throw new ArgumentOutOfRangeException("hintOffset");
			}
			if (inputText.Length < hintOffset + 5)
			{
				throw new ArgumentException("inputText is too short to contain hint.");
			}
			if (!hint.Equals(inputText.Substring(hintOffset, 5), StringComparison.InvariantCulture))
			{
				return null;
			}
			string s = inputText.Substring(0, hintOffset) + inputText.Substring(hintOffset + 5);
			byte[] decodedBytes = Convert.FromBase64String(s);
			return this.DoDecrypt(decodedBytes);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x0003D9A0 File Offset: 0x0003BBA0
		private byte[] DoEncrypt(string inputText)
		{
			if (inputText == null)
			{
				throw new ArgumentNullException("inputText");
			}
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			ICryptoTransform cryptoTransform = null;
			byte[] result;
			try
			{
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				memoryStream = new MemoryStream();
				cryptoTransform = this.tdes.CreateEncryptor(this.tdes.Key, new byte[8]);
				cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
				byte[] bytes = asciiencoding.GetBytes(inputText);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				result = memoryStream.ToArray();
			}
			finally
			{
				if (cryptoStream != null)
				{
					cryptoStream.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
				if (cryptoTransform != null)
				{
					cryptoTransform.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0003DA48 File Offset: 0x0003BC48
		private string DoDecrypt(byte[] decodedBytes)
		{
			if (decodedBytes == null)
			{
				throw new ArgumentNullException("decodedBytes");
			}
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			ICryptoTransform cryptoTransform = null;
			string @string;
			try
			{
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				memoryStream = new MemoryStream(decodedBytes);
				cryptoTransform = this.tdes.CreateDecryptor(this.tdes.Key, new byte[8]);
				cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
				byte[] array = new byte[decodedBytes.Length];
				int count = cryptoStream.Read(array, 0, array.Length);
				@string = asciiencoding.GetString(array, 0, count);
			}
			finally
			{
				if (cryptoStream != null)
				{
					cryptoStream.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
				if (cryptoTransform != null)
				{
					cryptoTransform.Dispose();
				}
			}
			return @string;
		}

		// Token: 0x04001EED RID: 7917
		public const string DefaultKeyContainerName = "CryptoHelper";

		// Token: 0x04001EEE RID: 7918
		protected const string EncryptionAlgorithmName = "TripleDES";

		// Token: 0x04001EEF RID: 7919
		protected const string HashAlgorithmName = "MD5";

		// Token: 0x04001EF0 RID: 7920
		protected const string HintFiller = "EEEEE";

		// Token: 0x04001EF1 RID: 7921
		protected const int HintLength = 5;

		// Token: 0x04001EF2 RID: 7922
		private static IDictionary<string, IDictionary<string, CryptoHelper>> cryptoHelperTable = new Dictionary<string, IDictionary<string, CryptoHelper>>();

		// Token: 0x04001EF3 RID: 7923
		private static object cryptoHelperTableLock = new object();

		// Token: 0x04001EF4 RID: 7924
		private CspParameters cspParams;

		// Token: 0x04001EF5 RID: 7925
		private PasswordDeriveBytes pdb;

		// Token: 0x04001EF6 RID: 7926
		private TripleDESCryptoServiceProvider tdes;
	}
}
