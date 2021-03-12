using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Exchange.Security.Cryptography;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000723 RID: 1827
	internal class PasswordDerivedKey
	{
		// Token: 0x060040CF RID: 16591 RVA: 0x001098E8 File Offset: 0x00107AE8
		public PasswordDerivedKey(SecureString password) : this(PasswordDerivedKey.RmsPasswordDeriveBytes.GetBytes(password, 16))
		{
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x001098F8 File Offset: 0x00107AF8
		public PasswordDerivedKey(SecureString password, bool newHashAlgorithm) : this(PasswordDerivedKey.GetPasswordDeriveBytes(password, newHashAlgorithm))
		{
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x00109908 File Offset: 0x00107B08
		private static byte[] GetPasswordDeriveBytes(SecureString password, bool newHashAlgorithm)
		{
			if (newHashAlgorithm)
			{
				return PasswordDerivedKey.RmsPasswordDeriveBytes.GetBytes(password, 16);
			}
			byte[] bytes;
			using (PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(PasswordDerivedKey.ConvertFromSecureString(password), null))
			{
				bytes = passwordDeriveBytes.GetBytes(16);
			}
			return bytes;
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x00109954 File Offset: 0x00107B54
		public PasswordDerivedKey(byte[] key) : this(new AesCryptoServiceProvider(), key)
		{
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x00109962 File Offset: 0x00107B62
		public PasswordDerivedKey(SymmetricAlgorithm algorithm, byte[] key)
		{
			this.m_symmetricAlgorithm = algorithm;
			this.m_symmetricAlgorithm.Key = key;
			this.m_symmetricAlgorithm.IV = PasswordDerivedKey.IV;
			this.m_symmetricAlgorithm.Padding = PaddingMode.PKCS7;
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x0010999C File Offset: 0x00107B9C
		private static string ConvertFromSecureString(SecureString secureString)
		{
			if (secureString == null)
			{
				throw new ArgumentNullException("secureString");
			}
			IntPtr intPtr = Marshal.SecureStringToBSTR(secureString);
			string result;
			try
			{
				string text = Marshal.PtrToStringUni(intPtr);
				result = text;
			}
			finally
			{
				Marshal.ZeroFreeBSTR(intPtr);
			}
			return result;
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x001099E4 File Offset: 0x00107BE4
		private static byte[] ConvertFromSecureStringToByteArray(SecureString secureString)
		{
			if (secureString == null)
			{
				throw new ArgumentNullException("secureString");
			}
			IntPtr intPtr = Marshal.SecureStringToCoTaskMemUnicode(secureString);
			byte[] result;
			try
			{
				int num = secureString.Length * Marshal.SizeOf(typeof(char)) * 2;
				byte[] array = new byte[num];
				Marshal.Copy(intPtr, array, 0, num);
				result = array;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x00109A58 File Offset: 0x00107C58
		internal void Clear()
		{
			this.m_symmetricAlgorithm.Clear();
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x00109A68 File Offset: 0x00107C68
		internal byte[] Decrypt(byte[] cipherText)
		{
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			byte[] result;
			try
			{
				memoryStream = new MemoryStream();
				cryptoStream = new CryptoStream(memoryStream, this.m_symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write);
				cryptoStream.Write(cipherText, 0, cipherText.Length);
				cryptoStream.FlushFinalBlock();
				result = memoryStream.ToArray();
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
				if (cryptoStream != null)
				{
					cryptoStream.Close();
				}
			}
			return result;
		}

		// Token: 0x04002914 RID: 10516
		private const int KEYSIZE = 128;

		// Token: 0x04002915 RID: 10517
		private static byte[] IV = new byte[]
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16
		};

		// Token: 0x04002916 RID: 10518
		private SymmetricAlgorithm m_symmetricAlgorithm;

		// Token: 0x02000724 RID: 1828
		private class RmsPasswordDeriveBytes
		{
			// Token: 0x060040D9 RID: 16601 RVA: 0x00109AFC File Offset: 0x00107CFC
			public static byte[] GetBytes(SecureString password, int numberOfBytes)
			{
				if (password == null)
				{
					throw new ArgumentException("password");
				}
				if (numberOfBytes > 32)
				{
					throw new ArgumentOutOfRangeException("numberOfBytes");
				}
				byte[] array = null;
				using (SHA256CryptoServiceProvider sha256CryptoServiceProvider = new SHA256CryptoServiceProvider())
				{
					array = sha256CryptoServiceProvider.ComputeHash(PasswordDerivedKey.ConvertFromSecureStringToByteArray(password));
				}
				byte[] array2 = new byte[numberOfBytes];
				for (int i = 0; i < numberOfBytes; i++)
				{
					array2[i] = array[i];
				}
				return array2;
			}

			// Token: 0x04002917 RID: 10519
			private const int SHA256HashSize = 32;
		}
	}
}
