using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B13 RID: 2835
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReminder
	{
		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x060066CE RID: 26318
		// (set) Token: 0x060066CF RID: 26319
		Guid Identifier { get; set; }

		// Token: 0x060066D0 RID: 26320
		int GetCurrentVersion();
	}
}
