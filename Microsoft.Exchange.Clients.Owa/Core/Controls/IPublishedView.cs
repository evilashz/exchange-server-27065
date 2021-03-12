using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x02000395 RID: 917
	internal interface IPublishedView
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060022BD RID: 8893
		string DisplayName { get; }

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060022BE RID: 8894
		string PublisherDisplayName { get; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060022BF RID: 8895
		string ICalUrl { get; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060022C0 RID: 8896
		SanitizedHtmlString PublishTimeRange { get; }
	}
}
