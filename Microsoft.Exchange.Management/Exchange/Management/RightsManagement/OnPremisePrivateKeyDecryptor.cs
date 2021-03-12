using System;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000722 RID: 1826
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OnPremisePrivateKeyDecryptor : IPrivateKeyDecryptor
	{
		// Token: 0x060040CC RID: 16588 RVA: 0x00109828 File Offset: 0x00107A28
		public OnPremisePrivateKeyDecryptor(SecureString password)
		{
			this.password = password;
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x00109838 File Offset: 0x00107A38
		public byte[] Decrypt(string encryptedData)
		{
			byte[] result;
			try
			{
				result = OnPremisePrivateKeyDecryptor.DecryptWithPassword(this.password, encryptedData);
			}
			catch (CryptographicException innerException)
			{
				throw new PrivateKeyDecryptionFailedException("Unable to decrypt TPD private key", innerException);
			}
			return result;
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x00109874 File Offset: 0x00107A74
		private static byte[] DecryptWithPassword(SecureString password, string toDecrypt)
		{
			PasswordDerivedKey passwordDerivedKey = new PasswordDerivedKey(password);
			byte[] result;
			try
			{
				result = passwordDerivedKey.Decrypt(Convert.FromBase64String(toDecrypt));
			}
			catch (CryptographicException)
			{
				PasswordDerivedKey passwordDerivedKey2 = new PasswordDerivedKey(password, false);
				try
				{
					result = passwordDerivedKey2.Decrypt(Convert.FromBase64String(toDecrypt));
				}
				finally
				{
					passwordDerivedKey2.Clear();
				}
			}
			finally
			{
				passwordDerivedKey.Clear();
			}
			return result;
		}

		// Token: 0x04002913 RID: 10515
		private SecureString password;
	}
}
