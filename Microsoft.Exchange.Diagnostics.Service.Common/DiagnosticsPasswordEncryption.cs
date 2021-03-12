using System;
using System.IO;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000008 RID: 8
	public class DiagnosticsPasswordEncryption
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00003E87 File Offset: 0x00002087
		public DiagnosticsPasswordEncryption(string keyString, string initialVectorString)
		{
			this.key = Convert.FromBase64String(keyString);
			this.initialVector = Convert.FromBase64String(initialVectorString);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00003EA8 File Offset: 0x000020A8
		public string EncryptString(string password)
		{
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			StreamWriter streamWriter = null;
			AesCryptoServiceProvider aesCryptoServiceProvider = null;
			try
			{
				aesCryptoServiceProvider = new AesCryptoServiceProvider();
				ICryptoTransform transform = aesCryptoServiceProvider.CreateEncryptor(this.key, this.initialVector);
				memoryStream = new MemoryStream();
				cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				streamWriter = new StreamWriter(cryptoStream);
				streamWriter.Write(password);
			}
			finally
			{
				if (streamWriter != null)
				{
					streamWriter.Close();
					streamWriter = null;
				}
				if (cryptoStream != null)
				{
					cryptoStream.Close();
					cryptoStream = null;
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
				if (aesCryptoServiceProvider != null)
				{
					aesCryptoServiceProvider.Dispose();
				}
			}
			return Convert.ToBase64String(memoryStream.ToArray());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00003F3C File Offset: 0x0000213C
		public string DecryptString(string encryptedPasswordString)
		{
			byte[] buffer = Convert.FromBase64String(encryptedPasswordString);
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			StreamReader streamReader = null;
			AesCryptoServiceProvider aesCryptoServiceProvider = null;
			string result;
			try
			{
				aesCryptoServiceProvider = new AesCryptoServiceProvider();
				ICryptoTransform transform = aesCryptoServiceProvider.CreateDecryptor(this.key, this.initialVector);
				memoryStream = new MemoryStream(buffer);
				cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
				streamReader = new StreamReader(cryptoStream);
				result = streamReader.ReadToEnd();
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
					streamReader = null;
				}
				if (cryptoStream != null)
				{
					cryptoStream.Close();
					cryptoStream = null;
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
					memoryStream = null;
				}
				if (aesCryptoServiceProvider != null)
				{
					aesCryptoServiceProvider.Dispose();
				}
			}
			return result;
		}

		// Token: 0x040002BE RID: 702
		private readonly byte[] key;

		// Token: 0x040002BF RID: 703
		private readonly byte[] initialVector;
	}
}
