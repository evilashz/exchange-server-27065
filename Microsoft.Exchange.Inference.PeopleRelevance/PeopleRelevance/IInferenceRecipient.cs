using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IInferenceRecipient : IMessageRecipient, IEquatable<IMessageRecipient>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		long TotalSentCount { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		DateTime FirstSentTime { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5
		// (set) Token: 0x06000006 RID: 6
		DateTime LastSentTime { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7
		// (set) Token: 0x06000008 RID: 8
		int RecipientRank { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9
		// (set) Token: 0x0600000A RID: 10
		double RawRecipientWeight { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		bool HasUpdatedData { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13
		int RelevanceCategory { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14
		// (set) Token: 0x0600000F RID: 15
		int RelevanceCategoryAtLastCapture { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16
		// (set) Token: 0x06000011 RID: 17
		long LastUsedInTimeWindow { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18
		// (set) Token: 0x06000013 RID: 19
		int CaptureFlag { get; set; }
	}
}
