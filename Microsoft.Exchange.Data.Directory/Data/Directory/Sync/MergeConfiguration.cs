using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007BA RID: 1978
	internal class MergeConfiguration : TenantFullSyncConfiguration
	{
		// Token: 0x17002308 RID: 8968
		// (get) Token: 0x06006274 RID: 25204 RVA: 0x001533FE File Offset: 0x001515FE
		private MergePageToken PageToken
		{
			get
			{
				return (MergePageToken)base.FullSyncPageToken;
			}
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x0015340C File Offset: 0x0015160C
		public MergeConfiguration(MergePageToken pageToken, Guid invocationId, OutputResultDelegate writeResult, ISyncEventLogger eventLogger, IExcludedObjectReporter excludedObjectReporter, PartitionId partitionId) : base(pageToken, invocationId, writeResult, eventLogger, excludedObjectReporter, MergePageToken.Parse(pageToken.ToByteArray()))
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New MergeConfiguration");
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "pageToken.MergeState = {0}", pageToken.MergeState.ToString());
			if (pageToken.MergeState == MergeState.Start)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Update merge state");
				this.PageToken.UpdateMergeState(partitionId);
			}
		}

		// Token: 0x17002309 RID: 8969
		// (get) Token: 0x06006276 RID: 25206 RVA: 0x00153495 File Offset: 0x00151695
		public override bool MoreData
		{
			get
			{
				return !this.PageToken.IsMergeComplete;
			}
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x00153748 File Offset: 0x00151948
		public override IEnumerable<ADRawEntry> GetDataPage()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "MergeConfiguration.GetDataPage entering");
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "MergeConfiguration.GetDataPage this.MoreData = {0}", this.MoreData);
			if (this.MoreData)
			{
				bool stateWasComplete = this.PageToken.State == TenantFullSyncState.Complete;
				ExTraceGlobals.BackSyncTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "MergeConfiguration.GetDataPage stateWasComplete = {0}", stateWasComplete);
				foreach (ADRawEntry entry in this.GetDataPageWrapper())
				{
					ExTraceGlobals.BackSyncTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "MergeConfiguration.GetDataPage entry {0}", entry.Id);
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

		// Token: 0x06006278 RID: 25208 RVA: 0x00153768 File Offset: 0x00151968
		protected override void FinishFullSync()
		{
			ExTraceGlobals.MergeTracer.TraceDebug((long)this.PageToken.TenantExternalDirectoryId.GetHashCode(), "MergeConfiguration.FinishFullSync entering");
			base.FinishFullSync();
			this.PageToken.UpdateMergeState(base.TenantConfigurationSession.SessionSettings.PartitionId);
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x001537BF File Offset: 0x001519BF
		private IEnumerable<ADRawEntry> GetDataPageWrapper()
		{
			return base.GetDataPage();
		}
	}
}
