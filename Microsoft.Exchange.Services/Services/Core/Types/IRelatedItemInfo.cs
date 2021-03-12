using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005E6 RID: 1510
	public interface IRelatedItemInfo
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002D73 RID: 11635
		// (set) Token: 0x06002D74 RID: 11636
		SingleRecipientType From { get; set; }

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002D75 RID: 11637
		// (set) Token: 0x06002D76 RID: 11638
		ItemId ItemId { get; set; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002D77 RID: 11639
		// (set) Token: 0x06002D78 RID: 11640
		ItemId ConversationId { get; set; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002D79 RID: 11641
		// (set) Token: 0x06002D7A RID: 11642
		string Preview { get; set; }

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002D7B RID: 11643
		// (set) Token: 0x06002D7C RID: 11644
		string ItemClass { get; set; }
	}
}
