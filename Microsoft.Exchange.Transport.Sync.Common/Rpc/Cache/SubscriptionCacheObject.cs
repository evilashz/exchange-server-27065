using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	public class SubscriptionCacheObject
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x00015F6C File Offset: 0x0001416C
		internal SubscriptionCacheObject(Guid subscriptionGuid, StoreObjectId subscriptionMessageId, string userLegacyDn, AggregationSubscriptionType subscriptionType, AggregationType aggregationType, SyncPhase syncPhase, ExDateTime? lastSyncCompletedTime, string incomingServerName, Guid userMailboxGuid, long? serializedSubscriptionVersion, Guid tenantGuid, string hubServerDispatched, string lastHubServerDispatched, ExDateTime? firstOutstandingDispatchTime, ExDateTime? lastSuccessfulDispatchTime, bool recoverySyncEnabled, bool disabled, string diagnostics, SubscriptionCacheObjectState state, string reasonForTheState)
		{
			this.subscriptionGuid = subscriptionGuid;
			if (subscriptionMessageId != null)
			{
				this.subscriptionMessageId = subscriptionMessageId.ToString();
			}
			this.userLegacyDn = userLegacyDn;
			this.subscriptionType = subscriptionType;
			this.aggregationType = aggregationType;
			this.syncPhase = syncPhase;
			this.lastSyncCompletedTime = (DateTime?)lastSyncCompletedTime;
			this.incomingServerName = incomingServerName;
			this.userMailboxGuid = userMailboxGuid;
			this.serializedSubscriptionVersion = serializedSubscriptionVersion;
			this.tenantGuid = tenantGuid;
			this.hubServerDispatched = hubServerDispatched;
			this.lastHubServerDispatched = lastHubServerDispatched;
			this.firstOutstandingDispatchTime = (DateTime?)firstOutstandingDispatchTime;
			this.lastSuccessfulDispatchTime = (DateTime?)lastSuccessfulDispatchTime;
			this.recoverySyncEnabled = recoverySyncEnabled;
			this.disabled = disabled;
			this.diagnostics = diagnostics;
			this.state = state;
			this.reasonForTheState = reasonForTheState;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00016033 File Offset: 0x00014233
		public Guid SubscriptionGuid
		{
			get
			{
				return this.subscriptionGuid;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001603B File Offset: 0x0001423B
		public string SubscriptionMessageId
		{
			get
			{
				return this.subscriptionMessageId;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00016043 File Offset: 0x00014243
		public string UserLegacyDn
		{
			get
			{
				return this.userLegacyDn;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001604B File Offset: 0x0001424B
		public AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return this.subscriptionType;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00016053 File Offset: 0x00014253
		public AggregationType AggregationType
		{
			get
			{
				return this.aggregationType;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0001605B File Offset: 0x0001425B
		public SyncPhase SyncPhase
		{
			get
			{
				return this.syncPhase;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00016063 File Offset: 0x00014263
		public DateTime? LastSyncCompletionTime
		{
			get
			{
				return this.lastSyncCompletedTime;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001606B File Offset: 0x0001426B
		public string IncomingServerName
		{
			get
			{
				return this.incomingServerName;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00016073 File Offset: 0x00014273
		public Guid UserMailboxGuid
		{
			get
			{
				return this.userMailboxGuid;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0001607B File Offset: 0x0001427B
		public long? SerializedSubscriptionVersion
		{
			get
			{
				return this.serializedSubscriptionVersion;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00016083 File Offset: 0x00014283
		public Guid TenantGuid
		{
			get
			{
				return this.tenantGuid;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001608B File Offset: 0x0001428B
		public string HubServerDispatched
		{
			get
			{
				return this.hubServerDispatched;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00016093 File Offset: 0x00014293
		public string LastHubServerDispatched
		{
			get
			{
				return this.lastHubServerDispatched;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0001609B File Offset: 0x0001429B
		public DateTime? FirstOutstandingDispatchTime
		{
			get
			{
				return this.firstOutstandingDispatchTime;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000160A3 File Offset: 0x000142A3
		public DateTime? LastSuccessfulDispatchTime
		{
			get
			{
				return this.lastSuccessfulDispatchTime;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000160AB File Offset: 0x000142AB
		public bool RecoverySyncEnabled
		{
			get
			{
				return this.recoverySyncEnabled;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000160B3 File Offset: 0x000142B3
		public bool Disabled
		{
			get
			{
				return this.disabled;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000160BB File Offset: 0x000142BB
		public string Diagnostics
		{
			get
			{
				return this.diagnostics;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000160C4 File Offset: 0x000142C4
		public string State
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}; {1}", new object[]
				{
					this.state,
					this.reasonForTheState
				});
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000160FF File Offset: 0x000142FF
		internal SubscriptionCacheObjectState ObjectState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x040001F7 RID: 503
		internal static readonly int ApproximateCacheObjectSizeInBytes = 1024;

		// Token: 0x040001F8 RID: 504
		private readonly Guid subscriptionGuid;

		// Token: 0x040001F9 RID: 505
		private readonly string subscriptionMessageId;

		// Token: 0x040001FA RID: 506
		private readonly string userLegacyDn;

		// Token: 0x040001FB RID: 507
		private readonly AggregationSubscriptionType subscriptionType;

		// Token: 0x040001FC RID: 508
		private readonly AggregationType aggregationType;

		// Token: 0x040001FD RID: 509
		private readonly SyncPhase syncPhase;

		// Token: 0x040001FE RID: 510
		private readonly DateTime? lastSyncCompletedTime;

		// Token: 0x040001FF RID: 511
		private readonly string incomingServerName;

		// Token: 0x04000200 RID: 512
		private readonly Guid userMailboxGuid;

		// Token: 0x04000201 RID: 513
		private readonly long? serializedSubscriptionVersion;

		// Token: 0x04000202 RID: 514
		private readonly Guid tenantGuid;

		// Token: 0x04000203 RID: 515
		private readonly string hubServerDispatched;

		// Token: 0x04000204 RID: 516
		private readonly string lastHubServerDispatched;

		// Token: 0x04000205 RID: 517
		private readonly DateTime? firstOutstandingDispatchTime;

		// Token: 0x04000206 RID: 518
		private readonly DateTime? lastSuccessfulDispatchTime;

		// Token: 0x04000207 RID: 519
		private readonly bool recoverySyncEnabled;

		// Token: 0x04000208 RID: 520
		private readonly bool disabled;

		// Token: 0x04000209 RID: 521
		private readonly string diagnostics;

		// Token: 0x0400020A RID: 522
		private readonly SubscriptionCacheObjectState state;

		// Token: 0x0400020B RID: 523
		private readonly string reasonForTheState;
	}
}
