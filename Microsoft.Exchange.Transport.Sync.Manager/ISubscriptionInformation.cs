using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISubscriptionInformation
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600017F RID: 383
		Guid DatabaseGuid { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000180 RID: 384
		Guid MailboxGuid { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000181 RID: 385
		Guid SubscriptionGuid { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000182 RID: 386
		Guid TenantGuid { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000183 RID: 387
		Guid ExternalDirectoryOrgId { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000184 RID: 388
		AggregationSubscriptionType SubscriptionType { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000185 RID: 389
		AggregationType AggregationType { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000186 RID: 390
		string IncomingServerName { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000187 RID: 391
		SyncPhase SyncPhase { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000188 RID: 392
		bool Disabled { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000189 RID: 393
		ExDateTime? LastSuccessfulDispatchTime { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600018A RID: 394
		ExDateTime? LastSyncCompletedTime { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600018B RID: 395
		string HubServerDispatched { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600018C RID: 396
		bool SupportsSerialization { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600018D RID: 397
		SerializedSubscription SerializedSubscription { get; }
	}
}
