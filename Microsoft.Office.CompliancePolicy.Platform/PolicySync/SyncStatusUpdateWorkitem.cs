using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	public sealed class SyncStatusUpdateWorkitem : WorkItemBase
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x0001FBA0 File Offset: 0x0001DDA0
		public SyncStatusUpdateWorkitem(string externalIdentity, bool processNow, TenantContext tenantContext, IList<UnifiedPolicyStatus> statusUpdates, string statusUpdateSvcUrl, int maxBatchSize = 0) : this(externalIdentity, default(DateTime), processNow, tenantContext, statusUpdates, statusUpdateSvcUrl, maxBatchSize)
		{
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
		internal SyncStatusUpdateWorkitem(string externalIdentity, DateTime executeTimeUtc, bool processNow, TenantContext tenantContext, IList<UnifiedPolicyStatus> statusUpdates, string statusUpdateSvcUrl, int maxBatchSize = 0) : base(externalIdentity, executeTimeUtc, processNow, tenantContext, false)
		{
			ArgumentValidator.ThrowIfCollectionNullOrEmpty<UnifiedPolicyStatus>("statusUpdates", statusUpdates);
			ArgumentValidator.ThrowIfNullOrEmpty("statusUpdateSvcUrl", statusUpdateSvcUrl);
			Guid tenantId = statusUpdates.First<UnifiedPolicyStatus>().TenantId;
			if (statusUpdates.Any((UnifiedPolicyStatus s) => s.TenantId != tenantId))
			{
				throw new ArgumentException("The collection must contain statuses for a single tenant", "statusUpdates");
			}
			this.StatusUpdateSvcUrl = statusUpdateSvcUrl;
			base.TryCount = 0;
			this.MaxBatchSize = maxBatchSize;
			this.statusUpdates = statusUpdates.ToList<UnifiedPolicyStatus>();
			SyncStatusUpdateWorkitem.SetStatusUpdateTenantIds(this.statusUpdates, tenantContext);
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0001FC7F File Offset: 0x0001DE7F
		public IEnumerable<UnifiedPolicyStatus> StatusUpdates
		{
			get
			{
				return this.statusUpdates;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001FC87 File Offset: 0x0001DE87
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x0001FC8F File Offset: 0x0001DE8F
		public string StatusUpdateSvcUrl { get; private set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001FC98 File Offset: 0x0001DE98
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		public int MaxBatchSize { get; private set; }

		// Token: 0x06000966 RID: 2406 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public override bool Merge(WorkItemBase newWorkItem)
		{
			ArgumentValidator.ThrowIfNull("newWorkItem", newWorkItem);
			ArgumentValidator.ThrowIfWrongType("newWorkItem", newWorkItem, typeof(SyncStatusUpdateWorkitem));
			SyncStatusUpdateWorkitem syncStatusUpdateWorkitem = (SyncStatusUpdateWorkitem)newWorkItem;
			foreach (UnifiedPolicyStatus unifiedPolicyStatus in syncStatusUpdateWorkitem.StatusUpdates)
			{
				int indexOfMatchingStatus = SyncStatusUpdateWorkitem.GetIndexOfMatchingStatus(this.statusUpdates, unifiedPolicyStatus);
				if (indexOfMatchingStatus >= 0)
				{
					if (this.statusUpdates[indexOfMatchingStatus].Version.CompareTo(unifiedPolicyStatus.Version) < 0)
					{
						this.statusUpdates[indexOfMatchingStatus] = unifiedPolicyStatus;
					}
				}
				else
				{
					this.statusUpdates.Add(unifiedPolicyStatus);
				}
			}
			if (base.ExecuteTimeUTC < newWorkItem.ExecuteTimeUTC)
			{
				base.ExecuteTimeUTC = newWorkItem.ExecuteTimeUTC;
			}
			return true;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001FD84 File Offset: 0x0001DF84
		public override bool IsEqual(WorkItemBase newWorkItem)
		{
			return this == newWorkItem;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001FD8A File Offset: 0x0001DF8A
		public override Guid GetPrimaryKey()
		{
			return this.StatusUpdates.First<UnifiedPolicyStatus>().TenantId;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		internal static int GetIndexOfMatchingStatus(IList<UnifiedPolicyStatus> statusCollection, UnifiedPolicyStatus newStatus)
		{
			for (int i = 0; i < statusCollection.Count; i++)
			{
				if (statusCollection[i].ObjectId == newStatus.ObjectId && statusCollection[i].ObjectType == newStatus.ObjectType)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001FDFC File Offset: 0x0001DFFC
		internal static void SetStatusUpdateTenantIds(IList<UnifiedPolicyStatus> statusUpdates, TenantContext tenantContext)
		{
			foreach (UnifiedPolicyStatus unifiedPolicyStatus in from status in statusUpdates
			where status.TenantId == Guid.Empty
			select status)
			{
				unifiedPolicyStatus.TenantId = tenantContext.TenantId;
			}
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001FE6C File Offset: 0x0001E06C
		internal bool IsOverLimit()
		{
			return this.MaxBatchSize != 0 && this.StatusUpdates.Count<UnifiedPolicyStatus>() > this.MaxBatchSize;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001FE8C File Offset: 0x0001E08C
		internal override WorkItemBase Split()
		{
			if (!this.IsOverLimit())
			{
				return null;
			}
			SyncStatusUpdateWorkitem result = new SyncStatusUpdateWorkitem(base.ExternalIdentity, base.ExecuteTimeUTC, base.ProcessNow, base.TenantContext, this.statusUpdates.GetRange(this.MaxBatchSize, this.statusUpdates.Count - this.MaxBatchSize), this.StatusUpdateSvcUrl, this.MaxBatchSize);
			this.statusUpdates = this.statusUpdates.GetRange(0, this.MaxBatchSize);
			return result;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001FF0C File Offset: 0x0001E10C
		internal override bool RoughCompare(object other)
		{
			SyncStatusUpdateWorkitem syncStatusUpdateWorkitem = other as SyncStatusUpdateWorkitem;
			if (syncStatusUpdateWorkitem == null)
			{
				return false;
			}
			if (this.statusUpdates.Count == syncStatusUpdateWorkitem.statusUpdates.Count && this.StatusUpdateSvcUrl.Equals(syncStatusUpdateWorkitem.StatusUpdateSvcUrl, StringComparison.OrdinalIgnoreCase))
			{
				Dictionary<Guid, UnifiedPolicyStatus> dictionary = new Dictionary<Guid, UnifiedPolicyStatus>();
				foreach (UnifiedPolicyStatus unifiedPolicyStatus in this.statusUpdates)
				{
					dictionary[unifiedPolicyStatus.ObjectId] = unifiedPolicyStatus;
				}
				foreach (UnifiedPolicyStatus unifiedPolicyStatus2 in syncStatusUpdateWorkitem.statusUpdates)
				{
					if (!dictionary.ContainsKey(unifiedPolicyStatus2.ObjectId))
					{
						return false;
					}
					UnifiedPolicyStatus unifiedPolicyStatus3 = dictionary[unifiedPolicyStatus2.ObjectId];
					if (unifiedPolicyStatus3.ErrorCode != unifiedPolicyStatus2.ErrorCode || unifiedPolicyStatus3.ObjectType != unifiedPolicyStatus2.ObjectType || !unifiedPolicyStatus3.Version.Equals(unifiedPolicyStatus2.Version) || unifiedPolicyStatus3.Workload != unifiedPolicyStatus2.Workload || !(unifiedPolicyStatus3.TenantId == unifiedPolicyStatus2.TenantId))
					{
						return false;
					}
				}
				return base.RoughCompare(syncStatusUpdateWorkitem);
			}
			return false;
		}

		// Token: 0x040004D5 RID: 1237
		public const int MaxBatchSizeNoLimit = 0;

		// Token: 0x040004D6 RID: 1238
		private List<UnifiedPolicyStatus> statusUpdates;
	}
}
