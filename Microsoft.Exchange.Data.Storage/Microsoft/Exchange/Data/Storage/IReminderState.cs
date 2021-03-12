using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B15 RID: 2837
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReminderState
	{
		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x060066E5 RID: 26341
		// (set) Token: 0x060066E6 RID: 26342
		Guid Identifier { get; set; }

		// Token: 0x060066E7 RID: 26343
		int GetCurrentVersion();
	}
}
