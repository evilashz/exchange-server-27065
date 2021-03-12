using System;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregationWorkItem : RetryableWorkItem
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00006140 File Offset: 0x00004340
		internal AggregationWorkItem(SyncLog syncLog, string legacyDN, StoreObjectId subscriptionMessageId, AggregationSubscriptionType subscriptionType, Guid subscriptionId, bool recoverySyncMode, Guid databaseGuid, Guid userMailboxGuid, Guid tenantGuid, AggregationType aggregationType, bool initialSync, string mailboxServer, bool isSyncNow, ISyncWorkerData subscription, string mailboxServerSyncWatermark, Guid mailboxServerGuid, SyncPhase syncPhase) : this(syncLog, legacyDN, subscriptionMessageId, subscriptionType, subscriptionId, recoverySyncMode, databaseGuid, userMailboxGuid, tenantGuid, aggregationType, initialSync, mailboxServer, isSyncNow, subscription, mailboxServerSyncWatermark, mailboxServerGuid, syncPhase, AggregationConfiguration.Instance.InitialRetryInMilliseconds, AggregationConfiguration.Instance.RetryBackoffFactor, AggregationConfiguration.Instance.MaximumNumberOfAttempts)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006190 File Offset: 0x00004390
		protected AggregationWorkItem(SyncLog syncLog, string legacyDN, StoreObjectId subscriptionMessageId, AggregationSubscriptionType subscriptionType, Guid subscriptionId, bool recoverySyncMode, Guid databaseGuid, Guid userMailboxGuid, Guid tenantGuid, AggregationType aggregationType, bool initialSync, string mailboxServer, bool isSyncNow, ISyncWorkerData subscription, string mailboxServerSyncWatermark, Guid mailboxServerGuid, SyncPhase syncPhase, int initialRetryInMilliseconds, int retryBackOffFactor, int maximumRetries) : base(initialRetryInMilliseconds, retryBackOffFactor, maximumRetries)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLog", syncLog);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("legacyDN", legacyDN);
			SyncUtilities.ThrowIfArgumentNull("subscriptionMessageId", subscriptionMessageId);
			if (subscriptionType == AggregationSubscriptionType.All || subscriptionType == AggregationSubscriptionType.Unknown)
			{
				throw new ArgumentOutOfRangeException("subscriptionType", "AggregationWorkItem cannot be created for all/unknown subscription types.");
			}
			SyncUtilities.ThrowIfGuidEmpty("userMailboxGuid", userMailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfArgumentNull("mailboxServer", mailboxServer);
			SyncUtilities.ThrowIfGuidEmpty("mailboxServerGuid", mailboxServerGuid);
			this.subscription = subscription;
			this.mailboxServerSyncWatermark = mailboxServerSyncWatermark;
			this.mailboxServerGuid = mailboxServerGuid;
			this.legacyDN = legacyDN;
			this.subscriptionMessageId = subscriptionMessageId;
			this.subscriptionType = subscriptionType;
			this.subscriptionId = subscriptionId;
			this.recoverySyncMode = recoverySyncMode;
			this.userMailboxGuid = userMailboxGuid;
			this.tenantGuid = tenantGuid;
			this.mailboxServer = mailboxServer;
			this.aggregationType = aggregationType;
			this.syncPhase = syncPhase;
			this.initialSync = initialSync;
			this.isSyncNow = isSyncNow;
			this.syncLogSession = syncLog.OpenSession(this.userMailboxGuid, this.subscriptionType, this.subscriptionId);
			this.subscriptionPoisonStatus = SyncPoisonHandler.GetPoisonStatus(this.subscriptionId, this.syncLogSession, out this.subscriptionPoisonCallstack);
			this.subscriptionPoisonContext = new SyncPoisonContext(this.subscriptionId);
			this.syncHealthData = new SyncHealthData();
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00006319 File Offset: 0x00004519
		public ExDateTime CreationTime
		{
			get
			{
				base.CheckDisposed();
				return this.creationTime;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00006327 File Offset: 0x00004527
		public TimeSpan Lifetime
		{
			get
			{
				base.CheckDisposed();
				return ExDateTime.UtcNow - this.CreationTime;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000633F File Offset: 0x0000453F
		public string LegacyDN
		{
			get
			{
				base.CheckDisposed();
				return this.legacyDN;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000634D File Offset: 0x0000454D
		public StoreObjectId SubscriptionMessageId
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionMessageId;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000635B File Offset: 0x0000455B
		public AggregationSubscriptionType SubscriptionType
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionType;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00006369 File Offset: 0x00004569
		public AggregationType AggregationType
		{
			get
			{
				base.CheckDisposed();
				return this.aggregationType;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00006377 File Offset: 0x00004577
		public bool InitialSync
		{
			get
			{
				base.CheckDisposed();
				return this.initialSync;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00006385 File Offset: 0x00004585
		public SyncPhase SyncPhase
		{
			get
			{
				base.CheckDisposed();
				return this.syncPhase;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00006393 File Offset: 0x00004593
		public bool IsSyncNow
		{
			get
			{
				base.CheckDisposed();
				return this.isSyncNow;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000063A1 File Offset: 0x000045A1
		public Guid SubscriptionId
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionId;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000063AF File Offset: 0x000045AF
		public SyncPoisonContext SubscriptionPoisonContext
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionPoisonContext;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000063BD File Offset: 0x000045BD
		public SyncPoisonStatus SubscriptionPoisonStatus
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionPoisonStatus;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000063CB File Offset: 0x000045CB
		public string SubscriptionPoisonCallstack
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionPoisonCallstack;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000063D9 File Offset: 0x000045D9
		public bool IsRecoverySyncMode
		{
			get
			{
				base.CheckDisposed();
				return this.recoverySyncMode;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000063E7 File Offset: 0x000045E7
		public Guid DatabaseGuid
		{
			get
			{
				base.CheckDisposed();
				return this.databaseGuid;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000063F5 File Offset: 0x000045F5
		public Guid UserMailboxGuid
		{
			get
			{
				base.CheckDisposed();
				return this.userMailboxGuid;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00006403 File Offset: 0x00004603
		public Guid TenantGuid
		{
			get
			{
				base.CheckDisposed();
				return this.tenantGuid;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006411 File Offset: 0x00004611
		public string MailboxServer
		{
			get
			{
				base.CheckDisposed();
				return this.mailboxServer;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000641F File Offset: 0x0000461F
		public ISyncWorkerData Subscription
		{
			get
			{
				base.CheckDisposed();
				return this.subscription;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000642D File Offset: 0x0000462D
		public Guid MailboxServerGuid
		{
			get
			{
				base.CheckDisposed();
				return this.mailboxServerGuid;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000643B File Offset: 0x0000463B
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000644C File Offset: 0x0000464C
		public SyncEngineState SyncEngineState
		{
			get
			{
				base.CheckDisposed();
				return this.syncEngineState;
			}
			set
			{
				base.CheckDisposed();
				lock (this.syncRoot)
				{
					this.syncEngineState = value;
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00006494 File Offset: 0x00004694
		public SyncLogSession SyncLogSession
		{
			get
			{
				base.CheckDisposed();
				return this.syncLogSession;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000064A2 File Offset: 0x000046A2
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000064B0 File Offset: 0x000046B0
		public AsyncOperationResult<SyncEngineResultData> LastWorkItemResultData
		{
			get
			{
				base.CheckDisposed();
				return this.lastWorkItemResultData;
			}
			set
			{
				base.CheckDisposed();
				this.lastWorkItemResultData = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000064BF File Offset: 0x000046BF
		public SyncHealthData SyncHealthData
		{
			get
			{
				base.CheckDisposed();
				return this.syncHealthData;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000064CD File Offset: 0x000046CD
		public bool WasAttemptMadeToOpenMailboxSession
		{
			get
			{
				return this.syncEngineState != null && this.syncEngineState.WasAttemptMadeToOpenMailboxSession;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000064E4 File Offset: 0x000046E4
		public bool IsMailboxServerSyncWatermarkAvailable
		{
			get
			{
				return this.mailboxServerSyncWatermark != null;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000064F2 File Offset: 0x000046F2
		public string MailboxServerSyncWatermark
		{
			get
			{
				return this.mailboxServerSyncWatermark;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000064FA File Offset: 0x000046FA
		public SyncStorageProviderConnectionStatistics ConnectionStatistics
		{
			get
			{
				return this.connectionStatistics;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006504 File Offset: 0x00004704
		public override string ToString()
		{
			base.CheckDisposed();
			if (this.subscription == null)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} : {1}.", new object[]
				{
					this.subscriptionType,
					this.legacyDN
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0} : {1}.", new object[]
			{
				this.subscription,
				this.syncEngineState
			});
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006578 File Offset: 0x00004778
		public override int GetHashCode()
		{
			return this.SubscriptionId.GetHashCode();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000659C File Offset: 0x0000479C
		public override bool Equals(object obj)
		{
			base.CheckDisposed();
			AggregationWorkItem otherWorkItem = obj as AggregationWorkItem;
			return this.Equals(otherWorkItem);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000065BD File Offset: 0x000047BD
		public bool Equals(AggregationWorkItem otherWorkItem)
		{
			base.CheckDisposed();
			return otherWorkItem != null && otherWorkItem.SubscriptionId == this.SubscriptionId;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000065DC File Offset: 0x000047DC
		public void Cancel(IAsyncResult asyncResult)
		{
			lock (this.syncRoot)
			{
				base.CheckDisposed();
				this.GetExecutionEngine().Cancel(asyncResult);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006628 File Offset: 0x00004828
		protected override void InternalDispose(bool disposing)
		{
			lock (this.syncRoot)
			{
				if (disposing && this.syncEngineState != null)
				{
					this.syncEngineState.Dispose();
					this.syncEngineState = null;
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006680 File Offset: 0x00004880
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AggregationWorkItem>(this);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006688 File Offset: 0x00004888
		protected internal virtual IExecutionEngine GetExecutionEngine()
		{
			if (!this.IsProcessedByDeleteEngine())
			{
				return SyncEngine.Instance;
			}
			return DeleteEngine.Instance;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000066AA File Offset: 0x000048AA
		internal bool IsProcessedByDeleteEngine()
		{
			return this.SyncPhase == SyncPhase.Delete;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000066B5 File Offset: 0x000048B5
		internal bool IsProcessedBySyncEngine()
		{
			return this.SyncPhase == SyncPhase.Initial || this.SyncPhase == SyncPhase.Incremental;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000066CC File Offset: 0x000048CC
		internal XElement GetDiagnosticInfo()
		{
			return new XElement("WorkItem", new object[]
			{
				new XElement("subscriptionID", this.SubscriptionId),
				new XElement("type", this.SubscriptionType),
				new XElement("databaseGuid", this.DatabaseGuid),
				new XElement("mailboxGuid", this.UserMailboxGuid),
				new XElement("tenantGuid", this.TenantGuid),
				new XElement("lifetime", this.Lifetime.ToString()),
				new XElement("retryCount", base.CurrentRetryCount)
			});
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000067C4 File Offset: 0x000049C4
		internal virtual void ResetSyncEngineState()
		{
			base.CheckDisposed();
			lock (this.syncRoot)
			{
				if (this.syncEngineState != null)
				{
					this.syncEngineState.Dispose();
					this.syncEngineState = null;
				}
			}
		}

		// Token: 0x04000072 RID: 114
		private readonly object syncRoot = new object();

		// Token: 0x04000073 RID: 115
		private readonly SyncPoisonContext subscriptionPoisonContext;

		// Token: 0x04000074 RID: 116
		private readonly SyncHealthData syncHealthData;

		// Token: 0x04000075 RID: 117
		private readonly SyncPoisonStatus subscriptionPoisonStatus;

		// Token: 0x04000076 RID: 118
		private readonly string subscriptionPoisonCallstack;

		// Token: 0x04000077 RID: 119
		private readonly ExDateTime creationTime = ExDateTime.UtcNow;

		// Token: 0x04000078 RID: 120
		private readonly ISyncWorkerData subscription;

		// Token: 0x04000079 RID: 121
		private readonly SyncStorageProviderConnectionStatistics connectionStatistics = new SyncStorageProviderConnectionStatistics();

		// Token: 0x0400007A RID: 122
		private readonly bool isSyncNow;

		// Token: 0x0400007B RID: 123
		private readonly Guid mailboxServerGuid;

		// Token: 0x0400007C RID: 124
		private readonly string mailboxServerSyncWatermark;

		// Token: 0x0400007D RID: 125
		private SyncEngineState syncEngineState;

		// Token: 0x0400007E RID: 126
		private Guid subscriptionId;

		// Token: 0x0400007F RID: 127
		private string legacyDN;

		// Token: 0x04000080 RID: 128
		private StoreObjectId subscriptionMessageId;

		// Token: 0x04000081 RID: 129
		private AggregationSubscriptionType subscriptionType;

		// Token: 0x04000082 RID: 130
		private bool recoverySyncMode;

		// Token: 0x04000083 RID: 131
		private AggregationType aggregationType;

		// Token: 0x04000084 RID: 132
		private bool initialSync;

		// Token: 0x04000085 RID: 133
		private SyncPhase syncPhase;

		// Token: 0x04000086 RID: 134
		private Guid databaseGuid;

		// Token: 0x04000087 RID: 135
		private Guid userMailboxGuid;

		// Token: 0x04000088 RID: 136
		private string mailboxServer;

		// Token: 0x04000089 RID: 137
		private Guid tenantGuid;

		// Token: 0x0400008A RID: 138
		private SyncLogSession syncLogSession;

		// Token: 0x0400008B RID: 139
		private AsyncOperationResult<SyncEngineResultData> lastWorkItemResultData;
	}
}
