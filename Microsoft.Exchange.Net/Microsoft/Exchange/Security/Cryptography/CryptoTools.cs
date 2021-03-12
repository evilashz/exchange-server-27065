using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Security.Cryptography
{
	// Token: 0x02000AA2 RID: 2722
	internal static class CryptoTools
	{
		// Token: 0x06003A88 RID: 14984 RVA: 0x0009581C File Offset: 0x00093A1C
		public static byte[] Decrypt(byte[] cypher, byte[] key)
		{
			if (cypher == null || cypher.Length == 0)
			{
				return null;
			}
			if (key == null || key.Length == 0)
			{
				throw new ArgumentNullException("key");
			}
			byte[] result;
			using (SymmetricAlgorithm symmetricAlgorithm = new AesCryptoServiceProvider())
			{
				if (cypher.Length < symmetricAlgorithm.IV.Length)
				{
					throw new ExchangeDataException(string.Concat(new object[]
					{
						"Data less data (",
						cypher.Length,
						") than the length of the initialization vector (",
						symmetricAlgorithm.IV.Length,
						")"
					}));
				}
				symmetricAlgorithm.Key = key;
				byte[] array = new byte[symmetricAlgorithm.IV.Length];
				Array.Copy(cypher, array, array.Length);
				symmetricAlgorithm.IV = array;
				using (ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateDecryptor())
				{
					result = cryptoTransform.TransformFinalBlock(cypher, symmetricAlgorithm.IV.Length, cypher.Length - symmetricAlgorithm.IV.Length);
				}
			}
			return result;
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x00095924 File Offset: 0x00093B24
		public unsafe static SecureString Decrypt(string data, byte[] key)
		{
			if (string.IsNullOrEmpty(data))
			{
				return null;
			}
			byte[] array = CryptoTools.Decrypt(Convert.FromBase64String(data), key);
			if (array == null || array.Length == 0)
			{
				return new SecureString();
			}
			char[] chars = Encoding.Unicode.GetChars(array);
			SecureString result;
			fixed (char* ptr = chars)
			{
				result = new SecureString(ptr, chars.Length);
			}
			Array.Clear(array, 0, array.Length);
			Array.Clear(chars, 0, chars.Length);
			return result;
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000959A4 File Offset: 0x00093BA4
		public static byte[] Encrypt(byte[] clear, byte[] key)
		{
			if (clear == null || clear.Length == 0)
			{
				return null;
			}
			if (key == null || key.Length == 0)
			{
				throw new ArgumentNullException("key");
			}
			byte[] result;
			using (SymmetricAlgorithm symmetricAlgorithm = new AesCryptoServiceProvider())
			{
				symmetricAlgorithm.Key = key;
				symmetricAlgorithm.GenerateIV();
				using (ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor())
				{
					byte[] array = cryptoTransform.TransformFinalBlock(clear, 0, clear.Length);
					byte[] array2 = new byte[array.Length + symmetricAlgorithm.IV.Length];
					symmetricAlgorithm.IV.CopyTo(array2, 0);
					array.CopyTo(array2, symmetricAlgorithm.IV.Length);
					result = array2;
				}
			}
			return result;
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x00095A5C File Offset: 0x00093C5C
		public static string Encrypt(SecureString data, byte[] key)
		{
			if (data == null || data.Length == 0)
			{
				return null;
			}
			IntPtr intPtr = IntPtr.Zero;
			byte[] array = new byte[data.Length * 2];
			string result;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(data);
				Marshal.Copy(intPtr, array, 0, array.Length);
				result = Convert.ToBase64String(CryptoTools.Encrypt(array, key));
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				Array.Clear(array, 0, array.Length);
			}
			return result;
		}
	}
}
