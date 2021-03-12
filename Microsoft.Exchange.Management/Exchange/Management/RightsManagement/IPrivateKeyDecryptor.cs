using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x0200071A RID: 1818
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPrivateKeyDecryptor
	{
		// Token: 0x060040AC RID: 16556
		byte[] Decrypt(string encryptedData);
	}
}
