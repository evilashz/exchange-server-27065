using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.MessageSecurity
{
	// Token: 0x02000004 RID: 4
	internal interface IDirectTrust
	{
		// Token: 0x0600000C RID: 12
		void Load();

		// Token: 0x0600000D RID: 13
		void Unload();

		// Token: 0x0600000E RID: 14
		SecurityIdentifier MapCertToSecurityIdentifier(X509Certificate2 certificate);

		// Token: 0x0600000F RID: 15
		SecurityIdentifier MapCertToSecurityIdentifier(IX509Certificate2 certificate);
	}
}
