using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000190 RID: 400
	internal enum PropertyType
	{
		// Token: 0x04000B17 RID: 2839
		ReadOnly = 1,
		// Token: 0x04000B18 RID: 2840
		WriteOnly,
		// Token: 0x04000B19 RID: 2841
		ReadWrite,
		// Token: 0x04000B1A RID: 2842
		ReadAndRequiredForWrite,
		// Token: 0x04000B1B RID: 2843
		ReadOnlyForNonDraft
	}
}
