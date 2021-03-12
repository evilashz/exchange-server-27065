using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SubscriptionInformation : EventArgs, ISubscriptionInformation
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000AC2E File Offset: 0x00008E2E
		internal SubscriptionInformation()
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000AC36 File Offset: 0x00008E36
		internal SubscriptionInformation(SubscriptionCacheManager cacheManager, SubscriptionCacheEntry cacheEntry)
		{
			SyncUtilities.ThrowIfArgumentNull("cacheManager", cacheManager);
			SyncUtilities.ThrowIfArgumentNull("cacheEntry", cacheEntry);
			this.databaseGuid = cacheManager.DatabaseGuid;
			this.cacheEntry = cacheEntry;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000AC67 File Offset: 0x00008E67
		public virtual Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000AC6F File Offset: 0x00008E6F
		public virtual Guid MailboxGuid
		{
			get
			{
				return this.cacheEntry.MailboxGuid;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000AC7C File Offset: 0x00008E7C
		public virtual Guid SubscriptionGuid
		{
			get
			{
				return this.cacheEntry.SubscriptionGuid;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000AC89 File Offset: 0x00008E89
		public Guid TenantGuid
		{
			get
			{
				return this.cacheEntry.TenantGuid;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000AC96 File Offset: 0x00008E96
		public Guid ExternalDirectoryOrgId
		{
			get
			{
				return this.cacheEntry.ExternalDirectoryOrgId;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000ACA3 File Offset: 0x00008EA3
		public string IncomingServerName
		{
			get
			{
				return this.cacheEntry.IncomingServerName;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return this.cacheEntry.SubscriptionType;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000ACBD File Offset: 0x00008EBD
		public AggregationType AggregationType
		{
			get
			{
				return this.cacheEntry.AggregationType;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000ACCA File Offset: 0x00008ECA
		public virtual bool Disabled
		{
			get
			{
				return this.cacheEntry.Disabled;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000ACD7 File Offset: 0x00008ED7
		public SyncPhase SyncPhase
		{
			get
			{
				return this.cacheEntry.SyncPhase;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		public virtual ExDateTime? LastSuccessfulDispatchTime
		{
			get
			{
				return this.cacheEntry.LastSuccessfulDispatchTime;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000ACF1 File Offset: 0x00008EF1
		public string HubServerDispatched
		{
			get
			{
				return this.cacheEntry.HubServerDispatched;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000ACFE File Offset: 0x00008EFE
		public string LastHubServerDispatched
		{
			get
			{
				return this.cacheEntry.LastHubServerDispatched;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000AD0B File Offset: 0x00008F0B
		public bool SupportsSerialization
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000AD0E File Offset: 0x00008F0E
		public SerializedSubscription SerializedSubscription
		{
			get
			{
				return this.cacheEntry.SerializedSubscription;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000AD1B File Offset: 0x00008F1B
		public ExDateTime? LastSyncCompletedTime
		{
			get
			{
				return this.cacheEntry.LastSyncCompletedTime;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000AD28 File Offset: 0x00008F28
		public virtual bool IsMigration
		{
			get
			{
				return this.cacheEntry.IsMigration;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000AD35 File Offset: 0x00008F35
		internal ExDateTime? FirstOutstandingDispatchTime
		{
			get
			{
				return this.cacheEntry.FirstOutstandingDispatchTime;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000AD42 File Offset: 0x00008F42
		internal bool RecoverySyncEnabled
		{
			get
			{
				return this.cacheEntry.RecoverySyncEnabled;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000AD4F File Offset: 0x00008F4F
		internal StoreObjectId SubscriptionMessageId
		{
			get
			{
				return this.cacheEntry.SubscriptionMessageId;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000AD5C File Offset: 0x00008F5C
		internal string UserLegacyDn
		{
			get
			{
				return this.cacheEntry.UserLegacyDn;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000AD69 File Offset: 0x00008F69
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000AD76 File Offset: 0x00008F76
		internal string Diagnostics
		{
			get
			{
				return this.cacheEntry.Diagnostics;
			}
			set
			{
				this.cacheEntry.Diagnostics = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000AD84 File Offset: 0x00008F84
		internal string SyncWatermark
		{
			get
			{
				return this.cacheEntry.SyncWatermark;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000AD91 File Offset: 0x00008F91
		public override string ToString()
		{
			return this.cacheEntry.ToString();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		internal bool TrySave(SyncLogSession syncLogSession)
		{
			SubscriptionCacheManager cacheManager = DataAccessLayer.GetCacheManager(this.databaseGuid);
			if (cacheManager == null)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)224UL, this.SubscriptionGuid, this.MailboxGuid, "Failed to save cache message as cache manager is not found.", new object[0]);
				return false;
			}
			if (syncLogSession != null)
			{
				this.cacheEntry.Diagnostics = syncLogSession.GetBlackBoxText();
			}
			else
			{
				this.cacheEntry.Diagnostics = null;
			}
			bool result;
			try
			{
				cacheManager.UpdateCacheMessage(this.cacheEntry);
				result = true;
			}
			catch (CacheTransientException)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)225UL, this.SubscriptionGuid, this.MailboxGuid, "Failed to save cache message due to transient exception.", new object[0]);
				result = false;
			}
			catch (CachePermanentException)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)226UL, this.SubscriptionGuid, this.MailboxGuid, "Failed to save cache message due to permanent exception.", new object[0]);
				result = false;
			}
			return result;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000AEAC File Offset: 0x000090AC
		internal void MarkOutstandingDispatch(ExDateTime dispatchTime, string hubServerName)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("hubServerName", hubServerName);
			if (this.cacheEntry.FirstOutstandingDispatchTime == null)
			{
				this.cacheEntry.FirstOutstandingDispatchTime = new ExDateTime?(dispatchTime);
			}
			this.cacheEntry.LastSuccessfulDispatchTime = new ExDateTime?(dispatchTime);
			this.SetLastHubServer(this.cacheEntry.HubServerDispatched);
			this.cacheEntry.HubServerDispatched = hubServerName;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000AF18 File Offset: 0x00009118
		internal void MarkLastSuccessfulDispatch(ExDateTime? dispatchTime)
		{
			if (this.cacheEntry.LastSuccessfulDispatchTime != null)
			{
				this.cacheEntry.LastSuccessfulDispatchTime = dispatchTime;
			}
			this.cacheEntry.FirstOutstandingDispatchTime = null;
			this.cacheEntry.HubServerDispatched = null;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000AF66 File Offset: 0x00009166
		internal void MarkFailedDispatch(ExDateTime? lastSuccessfulDispatchTime, ExDateTime? firstOutstandingDispatchTime, string hubServerDispatched)
		{
			this.cacheEntry.LastSuccessfulDispatchTime = lastSuccessfulDispatchTime;
			this.cacheEntry.FirstOutstandingDispatchTime = firstOutstandingDispatchTime;
			this.cacheEntry.HubServerDispatched = hubServerDispatched;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000AF8C File Offset: 0x0000918C
		internal void MarkSyncCompletion(bool disableSubscription, SyncPhase? syncPhase, SerializedSubscription serializedSubscription, string syncWatermark)
		{
			this.cacheEntry.Disabled = disableSubscription;
			this.cacheEntry.RecoverySyncEnabled = false;
			this.cacheEntry.LastSyncCompletedTime = new ExDateTime?(ExDateTime.UtcNow);
			if (serializedSubscription != null)
			{
				this.cacheEntry.SerializedSubscription = serializedSubscription;
			}
			if (syncWatermark != null)
			{
				this.cacheEntry.SyncWatermark = syncWatermark;
			}
			this.SetLastHubServer(this.cacheEntry.HubServerDispatched);
			this.cacheEntry.HubServerDispatched = null;
			this.cacheEntry.FirstOutstandingDispatchTime = null;
			if (syncPhase != null)
			{
				this.cacheEntry.UpdateSyncPhase(syncPhase.Value);
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B032 File Offset: 0x00009232
		internal void MarkSyncTimeOut()
		{
			this.cacheEntry.RecoverySyncEnabled = true;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B040 File Offset: 0x00009240
		internal void UpdateSyncPhase(SyncPhase syncPhase)
		{
			this.cacheEntry.UpdateSyncPhase(syncPhase);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B04E File Offset: 0x0000924E
		internal bool Validate(AggregationSubscription actualSubscription, Guid actualUserMailboxGuid, bool fix, out string inconsistencyInfo)
		{
			return this.cacheEntry.Validate(actualSubscription, actualUserMailboxGuid, fix, out inconsistencyInfo);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B060 File Offset: 0x00009260
		private void SetLastHubServer(string lastHubServer)
		{
			if (!string.IsNullOrEmpty(lastHubServer))
			{
				this.cacheEntry.LastHubServerDispatched = lastHubServer;
			}
		}

		// Token: 0x04000109 RID: 265
		private readonly SubscriptionCacheEntry cacheEntry;

		// Token: 0x0400010A RID: 266
		private readonly Guid databaseGuid;
	}
}
