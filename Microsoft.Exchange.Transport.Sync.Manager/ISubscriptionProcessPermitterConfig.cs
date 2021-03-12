using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISubscriptionProcessPermitterConfig
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600018E RID: 398
		bool AggregationSubscriptionsEnabled { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600018F RID: 399
		bool MigrationSubscriptionsEnabled { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000190 RID: 400
		bool PeopleConnectionSubscriptionsEnabled { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000191 RID: 401
		bool PopAggregationEnabled { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000192 RID: 402
		bool DeltaSyncAggregationEnabled { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000193 RID: 403
		bool ImapAggregationEnabled { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000194 RID: 404
		bool FacebookAggregationEnabled { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000195 RID: 405
		bool LinkedInAggregationEnabled { get; }
	}
}
