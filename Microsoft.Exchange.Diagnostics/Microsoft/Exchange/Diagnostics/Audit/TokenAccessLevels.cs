using System;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x0200018C RID: 396
	[Flags]
	internal enum TokenAccessLevels
	{
		// Token: 0x040007E3 RID: 2019
		AssignPrimary = 1,
		// Token: 0x040007E4 RID: 2020
		Duplicate = 2,
		// Token: 0x040007E5 RID: 2021
		Impersonate = 4,
		// Token: 0x040007E6 RID: 2022
		Query = 8,
		// Token: 0x040007E7 RID: 2023
		QuerySource = 16,
		// Token: 0x040007E8 RID: 2024
		AdjustPrivileges = 32,
		// Token: 0x040007E9 RID: 2025
		AdjustGroups = 64,
		// Token: 0x040007EA RID: 2026
		AdjustDefault = 128,
		// Token: 0x040007EB RID: 2027
		AdjustSessionId = 256,
		// Token: 0x040007EC RID: 2028
		Read = 131080,
		// Token: 0x040007ED RID: 2029
		Write = 131296,
		// Token: 0x040007EE RID: 2030
		AllAccess = 983551,
		// Token: 0x040007EF RID: 2031
		MaximumAllowed = 33554432
	}
}
