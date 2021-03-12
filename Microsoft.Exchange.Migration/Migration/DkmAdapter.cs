using System;
using System.Security;
using Microsoft.Exchange.Security.Dkm;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000007 RID: 7
	internal class DkmAdapter : ICryptoAdapter
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002658 File Offset: 0x00000858
		string ICryptoAdapter.ClearStringToEncryptedString(string clearString)
		{
			return new ExchangeGroupKey(null, "Microsoft Exchange DKM").ClearStringToEncryptedString(clearString);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000266B File Offset: 0x0000086B
		SecureString ICryptoAdapter.EncryptedStringToSecureString(string encryptedString)
		{
			return new ExchangeGroupKey(null, "Microsoft Exchange DKM").EncryptedStringToSecureString(encryptedString);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000267E File Offset: 0x0000087E
		bool ICryptoAdapter.TryEncryptedStringToSecureString(string encryptedString, out SecureString secureString, out Exception exception)
		{
			return new ExchangeGroupKey(null, "Microsoft Exchange DKM").TryEncryptedStringToSecureString(encryptedString, out secureString, out exception);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002693 File Offset: 0x00000893
		bool ICryptoAdapter.TrySecureStringToEncryptedString(SecureString secureString, out string encryptedString, out Exception exception)
		{
			return new ExchangeGroupKey(null, "Microsoft Exchange DKM").TrySecureStringToEncryptedString(secureString, out encryptedString, out exception);
		}
	}
}
