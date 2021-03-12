using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MiniSubscriptionInformation : IComparable<MiniSubscriptionInformation>
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x00017EF0 File Offset: 0x000160F0
		internal MiniSubscriptionInformation(Guid externalDirectoryOrgId, Guid databaseGuid, Guid mailboxGuid, Guid subscriptionGuid, AggregationSubscriptionType subscriptionType, ExDateTime nextOwaMailboxPolicyProbeTime)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncUtilities.ThrowIfArgumentNull("nextOwaMailboxPolicyProbeTime", nextOwaMailboxPolicyProbeTime);
			this.externalDirectoryOrgId = externalDirectoryOrgId;
			this.databaseGuid = databaseGuid;
			this.mailboxGuid = mailboxGuid;
			this.subscriptionGuid = subscriptionGuid;
			this.subscriptionType = subscriptionType;
			this.nextOwaMailboxPolicyProbeTime = nextOwaMailboxPolicyProbeTime;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00017F63 File Offset: 0x00016163
		internal Guid ExternalDirectoryOrgId
		{
			get
			{
				return this.externalDirectoryOrgId;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00017F6B File Offset: 0x0001616B
		internal Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00017F73 File Offset: 0x00016173
		internal Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00017F7B File Offset: 0x0001617B
		internal Guid SubscriptionGuid
		{
			get
			{
				return this.subscriptionGuid;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00017F83 File Offset: 0x00016183
		internal AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return this.subscriptionType;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00017F8B File Offset: 0x0001618B
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00017F93 File Offset: 0x00016193
		internal ExDateTime NextOwaMailboxPolicyProbeTime
		{
			get
			{
				return this.nextOwaMailboxPolicyProbeTime;
			}
			set
			{
				this.nextOwaMailboxPolicyProbeTime = value;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00017F9C File Offset: 0x0001619C
		public int CompareTo(MiniSubscriptionInformation miniSubscriptionInformation)
		{
			SyncUtilities.ThrowIfArgumentNull("miniSubscriptionInformation", miniSubscriptionInformation);
			return this.SubscriptionGuid.CompareTo(miniSubscriptionInformation.SubscriptionGuid);
		}

		// Token: 0x0400021C RID: 540
		private readonly Guid externalDirectoryOrgId;

		// Token: 0x0400021D RID: 541
		private readonly Guid databaseGuid;

		// Token: 0x0400021E RID: 542
		private readonly Guid mailboxGuid;

		// Token: 0x0400021F RID: 543
		private readonly Guid subscriptionGuid;

		// Token: 0x04000220 RID: 544
		private readonly AggregationSubscriptionType subscriptionType;

		// Token: 0x04000221 RID: 545
		private ExDateTime nextOwaMailboxPolicyProbeTime;
	}
}
