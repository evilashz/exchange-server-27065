using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F58 RID: 3928
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRecommendedGroupInfo
	{
		// Token: 0x170023AA RID: 9130
		// (get) Token: 0x0600869F RID: 34463
		Guid ID { get; }

		// Token: 0x170023AB RID: 9131
		// (get) Token: 0x060086A0 RID: 34464
		List<string> Members { get; }

		// Token: 0x170023AC RID: 9132
		// (get) Token: 0x060086A1 RID: 34465
		List<string> Words { get; }
	}
}
