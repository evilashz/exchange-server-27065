using System;
using System.Security;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000005 RID: 5
	internal interface ICryptoAdapter
	{
		// Token: 0x0600000E RID: 14
		string ClearStringToEncryptedString(string clearString);

		// Token: 0x0600000F RID: 15
		SecureString EncryptedStringToSecureString(string encryptedString);

		// Token: 0x06000010 RID: 16
		bool TryEncryptedStringToSecureString(string encryptedString, out SecureString secureString, out Exception exception);

		// Token: 0x06000011 RID: 17
		bool TrySecureStringToEncryptedString(SecureString secureString, out string encryptedString, out Exception exception);
	}
}
