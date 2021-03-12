using System;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.Dkm
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExchangeGroupKey
	{
		// Token: 0x06000016 RID: 22
		string ClearStringToEncryptedString(string clearString);

		// Token: 0x06000017 RID: 23
		string SecureStringToEncryptedString(SecureString secureString);

		// Token: 0x06000018 RID: 24
		SecureString EncryptedStringToSecureString(string encryptedString);

		// Token: 0x06000019 RID: 25
		bool TrySecureStringToEncryptedString(SecureString secureString, out string encryptedString, out Exception exception);

		// Token: 0x0600001A RID: 26
		bool TryEncryptedStringToSecureString(string encryptedString, out SecureString secureString, out Exception exception);

		// Token: 0x0600001B RID: 27
		bool IsDkmException(Exception e);
	}
}
