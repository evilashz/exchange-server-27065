using System;
using System.Security;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security.Cryptography;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000006 RID: 6
	internal class CryptoAdapter : ICryptoAdapter
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002554 File Offset: 0x00000754
		string ICryptoAdapter.ClearStringToEncryptedString(string clearString)
		{
			if (string.IsNullOrEmpty(clearString))
			{
				return null;
			}
			string result;
			using (SecureString secureString = clearString.ConvertToSecureString())
			{
				result = CryptoTools.Encrypt(secureString, CryptoAdapter.EncryptionKey);
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000259C File Offset: 0x0000079C
		bool ICryptoAdapter.TrySecureStringToEncryptedString(SecureString secureInput, out string encryptedString, out Exception failure)
		{
			bool result = true;
			failure = null;
			try
			{
				encryptedString = CryptoTools.Encrypt(secureInput, CryptoAdapter.EncryptionKey);
			}
			catch (Exception ex)
			{
				encryptedString = null;
				result = false;
				failure = ex;
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025DC File Offset: 0x000007DC
		SecureString ICryptoAdapter.EncryptedStringToSecureString(string encryptedInput)
		{
			return CryptoTools.Decrypt(encryptedInput, CryptoAdapter.EncryptionKey);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025EC File Offset: 0x000007EC
		bool ICryptoAdapter.TryEncryptedStringToSecureString(string encryptedInput, out SecureString decryptedOutput, out Exception failure)
		{
			bool result = true;
			failure = null;
			try
			{
				decryptedOutput = CryptoTools.Decrypt(encryptedInput, CryptoAdapter.EncryptionKey);
			}
			catch (Exception ex)
			{
				decryptedOutput = null;
				result = false;
				failure = ex;
			}
			return result;
		}

		// Token: 0x04000009 RID: 9
		private static readonly byte[] EncryptionKey = new Guid("6E1822F4-6997-4A24-870D-D2D671898FD4").ToByteArray();
	}
}
