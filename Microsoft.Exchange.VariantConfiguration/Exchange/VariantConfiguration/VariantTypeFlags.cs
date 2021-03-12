using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200014C RID: 332
	[Flags]
	public enum VariantTypeFlags
	{
		// Token: 0x04000534 RID: 1332
		None = 0,
		// Token: 0x04000535 RID: 1333
		Public = 1,
		// Token: 0x04000536 RID: 1334
		Prefix = 2,
		// Token: 0x04000537 RID: 1335
		AllowedInSettings = 4,
		// Token: 0x04000538 RID: 1336
		AllowedInFlights = 8,
		// Token: 0x04000539 RID: 1337
		AllowedInTeams = 16
	}
}
