using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000BBB RID: 3003
	internal enum AuthenticationMechanism
	{
		// Token: 0x040037EC RID: 14316
		None,
		// Token: 0x040037ED RID: 14317
		Login,
		// Token: 0x040037EE RID: 14318
		Negotiate,
		// Token: 0x040037EF RID: 14319
		Ntlm,
		// Token: 0x040037F0 RID: 14320
		Kerberos,
		// Token: 0x040037F1 RID: 14321
		Certificate,
		// Token: 0x040037F2 RID: 14322
		Gssapi,
		// Token: 0x040037F3 RID: 14323
		Plain
	}
}
