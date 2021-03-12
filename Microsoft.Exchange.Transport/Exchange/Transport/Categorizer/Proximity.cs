using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200024E RID: 590
	internal enum Proximity : short
	{
		// Token: 0x04000C38 RID: 3128
		LocalServer,
		// Token: 0x04000C39 RID: 3129
		LocalADSite,
		// Token: 0x04000C3A RID: 3130
		RemoteADSite,
		// Token: 0x04000C3B RID: 3131
		RemoteRoutingGroup,
		// Token: 0x04000C3C RID: 3132
		None = 32767
	}
}
