using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007C3 RID: 1987
	internal class DirSyncBasedMergeConfiguration : DirSyncBasedTenantFullSyncConfiguration
	{
		// Token: 0x1700230E RID: 8974
		// (get) Token: 0x0600629F RID: 25247 RVA: 0x00154951 File Offset: 0x00152B51
		private MergePageToken PageToken
		{
			get
			{
				return (MergePageToken)base.FullSyncPageToken;
			}
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x00154960 File Offset: 0x00152B60
		public DirSyncBasedMergeConfiguration(MergePageToken pageToken, ExchangeConfigurationUnit tenantFullSyncOrganizationCU, Guid invocationId, OutputResultDelegate writeResult, ISyncEventLogger eventLogger, IExcludedObjectReporter excludedObjectReporter, PartitionId partitionId) : base(pageToken, tenantFullSyncOrganizationCU, invocationId, writeResult, eventLogger, excludedObjectReporter)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New DirSyncBasedMergeConfiguration");
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "pageToken.MergeState = {0}", pageToken.MergeState.ToString());
			if (pageToken.MergeState == MergeState.Start)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Update merge state");
				this.PageToken.UpdateMergeState(partitionId);
			}
		}

		// Token: 0x1700230F RID: 8975
		// (get) Token: 0x060062A1 RID: 25249 RVA: 0x001549E0 File Offset: 0x00152BE0
		public override bool MoreData
		{
			get
			{
				return !this.PageToken.IsMergeComplete;
			}
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x00154C90 File Offset: 0x00152E90
		public override IEnumerable<ADRawEntry> GetDataPage()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "DirSyncBasedMergeConfiguration.GetDataPage entering");
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "DirSyncBasedMergeConfiguration.GetDataPage this.MoreData = {0}", this.MoreData);
			if (this.MoreData)
			{
				bool stateWasComplete = this.PageToken.State == TenantFullSyncState.Complete;
				ExTraceGlobals.BackSyncTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "DirSyncBasedMergeConfiguration.GetDataPage stateWasComplete = {0}", stateWasComplete);
				foreach (ADRawEntry entry in this.GetDataPageWrapper())
				{
					ExTraceGlobals.BackSyncTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "DirSyncBasedMergeConfiguration.GetDataPage entry {0}", entry.Id);
					yield return entry;
				}
				if (this.MoreData && this.PageToken.State == TenantFullSyncState.Complete && stateWasComplete)
				{
					ExTraceGlobals.MergeTracer.TraceError<Guid>((long)this.PageToken.TenantExternalDirectoryId.GetHashCode(), "Previous TFS token had MoreData=false but watermarks were not satisfied, and after the second call we are still in the same position. Apparently {0} is not receiving timely updates. Fail the cmdlet and suggest resuming incremental sync for now", this.PageToken.InvocationId);
					throw new BackSyncDataSourceReplicationException();
				}
			}
			yield break;
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x00154CB0 File Offset: 0x00152EB0
		protected override void FinishFullSync()
		{
			ExTraceGlobals.MergeTracer.TraceDebug((long)this.PageToken.TenantExternalDirectoryId.GetHashCode(), "DirSyncBasedMergeConfiguration.FinishFullSync entering");
			base.FinishFullSync();
			this.PageToken.UpdateMergeState(base.TenantConfigurationSession.SessionSettings.PartitionId);
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x00154D07 File Offset: 0x00152F07
		private IEnumerable<ADRawEntry> GetDataPageWrapper()
		{
			return base.GetDataPage();
		}
	}
}
