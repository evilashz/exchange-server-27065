using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002CF RID: 719
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRecipientBase
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001EC6 RID: 7878
		RecipientId Id { get; }

		// Token: 0x06001EC7 RID: 7879
		bool? IsDistributionList();

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001EC8 RID: 7880
		Participant Participant { get; }
	}
}
