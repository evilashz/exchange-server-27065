using System;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security.Dkm;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200017E RID: 382
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NullExchangeGroupKey : IExchangeGroupKey
	{
		// Token: 0x06000F12 RID: 3858 RVA: 0x0003D038 File Offset: 0x0003B238
		public string ClearStringToEncryptedString(string clearString)
		{
			return clearString;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003D03B File Offset: 0x0003B23B
		public string SecureStringToEncryptedString(SecureString secureString)
		{
			if (secureString == null || secureString.Length == 0)
			{
				return null;
			}
			return secureString.ConvertToUnsecureString();
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003D050 File Offset: 0x0003B250
		public SecureString EncryptedStringToSecureString(string encryptedString)
		{
			if (string.IsNullOrEmpty(encryptedString))
			{
				return null;
			}
			SecureString secureString = new SecureString();
			SecureString result;
			using (DisposeGuard disposeGuard = secureString.Guard())
			{
				foreach (char c in encryptedString)
				{
					secureString.AppendChar(c);
				}
				disposeGuard.Success();
				result = secureString;
			}
			return result;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0003D0C8 File Offset: 0x0003B2C8
		public bool TrySecureStringToEncryptedString(SecureString secureString, out string encryptedString, out Exception exception)
		{
			exception = null;
			encryptedString = this.SecureStringToEncryptedString(secureString);
			return true;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003D0D7 File Offset: 0x0003B2D7
		public bool TryEncryptedStringToSecureString(string encryptedString, out SecureString secureString, out Exception exception)
		{
			exception = null;
			secureString = this.EncryptedStringToSecureString(encryptedString);
			return true;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003D0E6 File Offset: 0x0003B2E6
		public bool IsDkmException(Exception e)
		{
			return false;
		}
	}
}
