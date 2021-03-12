using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E22 RID: 3618
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncClientOperation
	{
		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x06007D1C RID: 32028
		// (set) Token: 0x06007D1D RID: 32029
		int?[] ChangeTrackingInformation { get; set; }

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x06007D1E RID: 32030
		ChangeType ChangeType { get; }

		// Token: 0x1700217C RID: 8572
		// (get) Token: 0x06007D1F RID: 32031
		string ClientAddId { get; }

		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x06007D20 RID: 32032
		ISyncItemId Id { get; }

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x06007D21 RID: 32033
		ISyncItem Item { get; }

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x06007D22 RID: 32034
		bool SendEnabled { get; }
	}
}
