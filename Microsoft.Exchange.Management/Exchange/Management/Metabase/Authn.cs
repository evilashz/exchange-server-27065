using System;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x0200049D RID: 1181
	internal enum Authn
	{
		// Token: 0x04001E86 RID: 7814
		None,
		// Token: 0x04001E87 RID: 7815
		DcePrivate,
		// Token: 0x04001E88 RID: 7816
		DcePublic,
		// Token: 0x04001E89 RID: 7817
		DecPublic = 4,
		// Token: 0x04001E8A RID: 7818
		GssNegotiate = 9,
		// Token: 0x04001E8B RID: 7819
		Winnt,
		// Token: 0x04001E8C RID: 7820
		GssSchannel = 14,
		// Token: 0x04001E8D RID: 7821
		GssKerberos = 16,
		// Token: 0x04001E8E RID: 7822
		Msn,
		// Token: 0x04001E8F RID: 7823
		Dpa,
		// Token: 0x04001E90 RID: 7824
		Mq = 100,
		// Token: 0x04001E91 RID: 7825
		Default = -1
	}
}
