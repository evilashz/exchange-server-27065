using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000078 RID: 120
	[Flags]
	internal enum ItemBindOption
	{
		// Token: 0x0400023A RID: 570
		None = 0,
		// Token: 0x0400023B RID: 571
		LoadRequiredPropertiesOnly = 1,
		// Token: 0x0400023C RID: 572
		SoftDeletedItem = 2
	}
}
