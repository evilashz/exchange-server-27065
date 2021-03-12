using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007C2 RID: 1986
	internal class DirSyncBasedTenantFullSyncConfiguration : IncrementalSyncConfiguration
	{
		// Token: 0x1700230C RID: 8972
		// (get) Token: 0x06006293 RID: 25235 RVA: 0x00154793 File Offset: 0x00152993
		// (set) Token: 0x06006294 RID: 25236 RVA: 0x0015479B File Offset: 0x0015299B
		public TenantFullSyncPageToken FullSyncPageToken { get; private set; }

		// Token: 0x1700230D RID: 8973
		// (get) Token: 0x06006295 RID: 25237 RVA: 0x001547A4 File Offset: 0x001529A4
		// (set) Token: 0x06006296 RID: 25238 RVA: 0x001547AC File Offset: 0x001529AC
		public ExchangeConfigurationUnit OrganizationCU { get; private set; }

		// Token: 0x06006297 RID: 25239 RVA: 0x001547B8 File Offset: 0x001529B8
		public DirSyncBasedTenantFullSyncConfiguration(TenantFullSyncPageToken pageToken, ExchangeConfigurationUnit tenantFullSyncOrganizationCU, Guid invocationId, OutputResultDelegate writeResult, ISyncEventLogger eventLogger, IExcludedObjectReporter excludedObjectReporter) : base(pageToken.TenantScopedBackSyncCookie, invocationId, writeResult, eventLogger, excludedObjectReporter)
		{
			if (pageToken == null)
			{
				throw new ArgumentNullException("pageToken");
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New DirSyncBasedTenantFullSyncConfiguration");
			this.FullSyncPageToken = pageToken;
			this.OrganizationCU = tenantFullSyncOrganizationCU;
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x0015480C File Offset: 0x00152A0C
		protected override QueryFilter GetDirSyncQueryFilter()
		{
			QueryFilter dirSyncQueryFilter = base.GetDirSyncQueryFilter();
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "DirSyncBasedTenantFullSyncConfiguration.GetDirSyncQueryFilter entering");
			return new AndFilter(new QueryFilter[]
			{
				dirSyncQueryFilter,
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ConfigurationUnit, this.OrganizationCU.ConfigurationUnit)
			});
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x00154861 File Offset: 0x00152A61
		protected override void CheckForFullSyncFallback()
		{
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x00154864 File Offset: 0x00152A64
		public override byte[] GetResultCookie()
		{
			byte[] resultCookie = base.GetResultCookie();
			if (resultCookie != null)
			{
				this.FullSyncPageToken.TenantScopedBackSyncCookie = BackSyncCookie.Parse(resultCookie);
			}
			return this.FullSyncPageToken.ToByteArray();
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x00154897 File Offset: 0x00152A97
		protected override void PrepareCookieForFailure()
		{
			base.PrepareCookieForFailure();
			this.FullSyncPageToken.TenantScopedBackSyncCookie = base.NewCookie;
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x001548B0 File Offset: 0x00152AB0
		protected override void ReturnErrorCookie()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "DirSyncBasedTenantFullSyncConfiguration.ReturnErrorCookie entering");
			base.WriteResult(this.FullSyncPageToken.ToByteArray(), SyncObject.CreateGetChangesResponse(new List<SyncObject>(), this.MoreData, this.FullSyncPageToken.ToByteArray(), this.FullSyncPageToken.ServiceInstanceId));
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x00154910 File Offset: 0x00152B10
		public override IEnumerable<ADRawEntry> GetDataPage()
		{
			IEnumerable<ADRawEntry> dataPage = base.GetDataPage();
			this.FullSyncPageToken.TenantScopedBackSyncCookie = base.NewCookie;
			if (!this.MoreData)
			{
				this.FinishFullSync();
			}
			return dataPage;
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x00154944 File Offset: 0x00152B44
		protected virtual void FinishFullSync()
		{
			this.FullSyncPageToken.FinishFullSync();
		}
	}
}
