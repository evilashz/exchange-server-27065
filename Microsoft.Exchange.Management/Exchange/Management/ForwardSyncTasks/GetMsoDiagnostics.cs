using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.Sync.CookieManager;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200034B RID: 843
	[OutputType(new Type[]
	{
		typeof(DeltaSyncSummary)
	}, ParameterSetName = new string[]
	{
		"GetChanges"
	})]
	[OutputType(new Type[]
	{
		typeof(BacklogSummary)
	}, ParameterSetName = new string[]
	{
		"EstimateBacklog"
	})]
	[OutputType(new Type[]
	{
		typeof(TenantSyncSummary)
	}, ParameterSetName = new string[]
	{
		"GetContext"
	})]
	[Cmdlet("Get", "MsoDiagnostics", DefaultParameterSetName = "GetChanges")]
	public class GetMsoDiagnostics : Task
	{
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x00080778 File Offset: 0x0007E978
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x00080790 File Offset: 0x0007E990
		[AllowNull]
		[Parameter(ParameterSetName = "GetChanges", ValueFromPipelineByPropertyName = true)]
		[Parameter(ParameterSetName = "GetContext", ValueFromPipelineByPropertyName = true)]
		[Alias(new string[]
		{
			"NextCookie"
		})]
		public byte[] Cookie
		{
			get
			{
				return (byte[])base.Fields[GetMsoDiagnostics.ParameterNames.Cookie];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.Cookie] = value;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x000807A4 File Offset: 0x0007E9A4
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x000807BC File Offset: 0x0007E9BC
		[Parameter(Mandatory = true, ParameterSetName = "GetChanges")]
		public SwitchParameter DeltaSync
		{
			get
			{
				return (SwitchParameter)base.Fields[GetMsoDiagnostics.ParameterNames.DeltaSync];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.DeltaSync] = value;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x000807D5 File Offset: 0x0007E9D5
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x000807ED File Offset: 0x0007E9ED
		[Parameter(Mandatory = true, ParameterSetName = "EstimateBacklog")]
		public SwitchParameter EstimateBacklog
		{
			get
			{
				return (SwitchParameter)base.Fields[GetMsoDiagnostics.ParameterNames.EstimateBacklog];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.EstimateBacklog] = value;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00080806 File Offset: 0x0007EA06
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x0008081E File Offset: 0x0007EA1E
		[Parameter(ValueFromPipelineByPropertyName = true)]
		[ValidateRange(1, 10)]
		public int MaxNumberOfBatches
		{
			get
			{
				return (int)base.Fields[GetMsoDiagnostics.ParameterNames.MaxNumberOfBatches];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.MaxNumberOfBatches] = value;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00080837 File Offset: 0x0007EA37
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x0008084F File Offset: 0x0007EA4F
		[Parameter(ParameterSetName = "GetContext", Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Alias(new string[]
		{
			"ContextId"
		})]
		public string ExternalDirectoryOrganizationId
		{
			get
			{
				return (string)base.Fields[GetMsoDiagnostics.ParameterNames.OrganizationId];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.OrganizationId] = value;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00080863 File Offset: 0x0007EA63
		// (set) Token: 0x06001D1D RID: 7453 RVA: 0x0008087B File Offset: 0x0007EA7B
		[Parameter(ParameterSetName = "EstimateBacklog", ValueFromPipelineByPropertyName = true)]
		[Parameter(ParameterSetName = "GetContext", ValueFromPipelineByPropertyName = true)]
		[AllowNull]
		public byte[] PageToken
		{
			get
			{
				return (byte[])base.Fields[GetMsoDiagnostics.ParameterNames.PageToken];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.PageToken] = value;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0008088F File Offset: 0x0007EA8F
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x000808A7 File Offset: 0x0007EAA7
		[ValidateRange(1, 10)]
		[Parameter(ParameterSetName = "GetContext", ValueFromPipelineByPropertyName = true)]
		[Parameter(ParameterSetName = "GetChanges", ValueFromPipelineByPropertyName = true)]
		public int SampleCountForStatistics
		{
			get
			{
				return (int)base.Fields[GetMsoDiagnostics.ParameterNames.SampleCountForStatistics];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.SampleCountForStatistics] = value;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x000808C0 File Offset: 0x0007EAC0
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x000808D8 File Offset: 0x0007EAD8
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public string ServiceInstanceId
		{
			get
			{
				return (string)base.Fields[GetMsoDiagnostics.ParameterNames.ServiceInstanceId];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.ServiceInstanceId] = value;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x000808EC File Offset: 0x0007EAEC
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x00080904 File Offset: 0x0007EB04
		[Parameter(ParameterSetName = "GetContext", Mandatory = true)]
		public SwitchParameter TenantSync
		{
			get
			{
				return (SwitchParameter)base.Fields[GetMsoDiagnostics.ParameterNames.TenantSync];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.TenantSync] = value;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x0008091D File Offset: 0x0007EB1D
		// (set) Token: 0x06001D25 RID: 7461 RVA: 0x00080936 File Offset: 0x0007EB36
		[Parameter(ParameterSetName = "GetChanges", ValueFromPipelineByPropertyName = true)]
		[Parameter(ParameterSetName = "GetContext", ValueFromPipelineByPropertyName = true)]
		public SwitchParameter UseLastCommittedCookie
		{
			get
			{
				return (SwitchParameter)base.Fields[GetMsoDiagnostics.ParameterNames.UseLastCommittedCookie];
			}
			set
			{
				base.Fields[GetMsoDiagnostics.ParameterNames.UseLastCommittedCookie] = value;
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x00080950 File Offset: 0x0007EB50
		public GetMsoDiagnostics()
		{
			this.Cookie = null;
			this.DeltaSync = false;
			this.EstimateBacklog = false;
			this.MaxNumberOfBatches = 3;
			this.ExternalDirectoryOrganizationId = null;
			this.PageToken = null;
			this.SampleCountForStatistics = 3;
			this.ServiceInstanceId = null;
			this.TenantSync = false;
			this.UseLastCommittedCookie = false;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000809BD File Offset: 0x0007EBBD
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetTaskBaseModuleFactory();
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000809C4 File Offset: 0x0007EBC4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.Cookie != null && this.UseLastCommittedCookie.IsPresent)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.GetMsoDiagnosticsExclusiveParameters(GetMsoDiagnostics.ParameterNames.Cookie.ToString(), GetMsoDiagnostics.ParameterNames.UseLastCommittedCookie.ToString())), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00080A1D File Offset: 0x0007EC1D
		protected override void InternalBeginProcessing()
		{
			this.MsoSyncService = new MsoSyncService();
			if (base.ParameterSetName == "EstimateBacklog")
			{
				this.UseLastCommittedCookie = SwitchParameter.Present;
			}
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00080A47 File Offset: 0x0007EC47
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.MsoSyncService.Dispose();
			}
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x00080A60 File Offset: 0x0007EC60
		protected override void InternalProcessRecord()
		{
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (parameterSetName == "GetChanges")
				{
					this.ProcessGetChanges();
					return;
				}
				if (parameterSetName == "GetContext")
				{
					this.ProcessGetContext();
					return;
				}
				if (!(parameterSetName == "EstimateBacklog"))
				{
					return;
				}
				this.ProcessEstimateBacklog();
			}
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x00080AB4 File Offset: 0x0007ECB4
		private void FindTheRightCookie()
		{
			if (this.UseLastCommittedCookie.IsPresent)
			{
				CookieManager cookieManager = CookieManagerFactory.Default.GetCookieManager(ForwardSyncCookieType.CompanyIncremental, this.ServiceInstanceId, 1, TimeSpan.FromMinutes(30.0));
				this.Cookie = cookieManager.ReadCookie();
				base.WriteVerbose(Strings.GetMsoDiagnosticsLastCommittedCookieBeingUsed(cookieManager.GetMostRecentCookieTimestamp()));
				return;
			}
			if (this.Cookie == null)
			{
				this.Cookie = this.MsoSyncService.GetNewCookieForAllObjectsTypes(this.ServiceInstanceId);
				this.WriteWarning(Strings.GetMsoDiagnosticsNewCookieIsBeingUsed);
			}
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00080B3C File Offset: 0x0007ED3C
		private void ProcessGetChanges()
		{
			DeltaSyncSummary deltaSyncSummary = new DeltaSyncSummary();
			List<IEnumerable<DeltaSyncBatchResults>> list = deltaSyncSummary.Samples as List<IEnumerable<DeltaSyncBatchResults>>;
			this.FindTheRightCookie();
			ExProgressRecord exProgressRecord = new ExProgressRecord(1, new LocalizedString("Delta Sync"), new LocalizedString("Sync Call"));
			ExProgressRecord exProgressRecord2 = new ExProgressRecord(2, new LocalizedString("Sync Call"), new LocalizedString("GetChanges"));
			exProgressRecord2.ParentActivityId = exProgressRecord.ActivityId;
			for (int i = 0; i < this.SampleCountForStatistics; i++)
			{
				exProgressRecord.CurrentOperation = Strings.GetMsoDiagnosticsProgressIteration(i + 1, this.SampleCountForStatistics);
				exProgressRecord.PercentComplete = i * 100 / this.SampleCountForStatistics;
				exProgressRecord.RecordType = ProgressRecordType.Processing;
				base.WriteProgress(exProgressRecord);
				byte[] lastCookie = this.Cookie;
				List<DeltaSyncBatchResults> list2 = new List<DeltaSyncBatchResults>();
				list.Add(list2);
				for (int j = 0; j < this.MaxNumberOfBatches; j++)
				{
					exProgressRecord2.CurrentOperation = Strings.GetMsoDiagnosticsProgressBatch(j + 1, this.MaxNumberOfBatches);
					exProgressRecord2.PercentComplete = j * 100 / this.MaxNumberOfBatches;
					exProgressRecord2.RecordType = ProgressRecordType.Processing;
					base.WriteProgress(exProgressRecord2);
					ExDateTime now = ExDateTime.Now;
					GetChangesResponse changes = this.MsoSyncService.SyncProxy.GetChanges(new GetChangesRequest(lastCookie));
					TimeSpan responseTime = ExDateTime.Now - now;
					DirectoryChanges getChangesResult = changes.GetChangesResult;
					if (getChangesResult != null)
					{
						DeltaSyncBatchResults deltaSyncBatchResults = new DeltaSyncBatchResults(getChangesResult);
						deltaSyncBatchResults.Stats.ResponseTime = responseTime;
						deltaSyncBatchResults.LastCookie = lastCookie;
						deltaSyncBatchResults.RawResponse = this.MsoSyncService.RawResponse;
						deltaSyncBatchResults.CalculateStats();
						list2.Add(deltaSyncBatchResults);
						lastCookie = getChangesResult.NextCookie;
					}
					if (getChangesResult == null || !getChangesResult.More)
					{
						break;
					}
				}
				if (list2.Last<DeltaSyncBatchResults>().More)
				{
					this.WriteWarning(Strings.GetMsoDiagnosticsMoreDataIsAvailable);
				}
				exProgressRecord2.RecordType = ProgressRecordType.Completed;
				base.WriteProgress(exProgressRecord2);
			}
			exProgressRecord.RecordType = ProgressRecordType.Completed;
			base.WriteProgress(exProgressRecord);
			deltaSyncSummary.CalculateStats();
			base.WriteObject(deltaSyncSummary);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00080D3C File Offset: 0x0007EF3C
		private void ProcessGetContext()
		{
			TenantSyncSummary tenantSyncSummary = new TenantSyncSummary();
			List<IEnumerable<TenantSyncBatchResults>> list = tenantSyncSummary.Samples as List<IEnumerable<TenantSyncBatchResults>>;
			this.FindTheRightCookie();
			ExProgressRecord exProgressRecord = new ExProgressRecord(1, new LocalizedString("Tenant Sync"), new LocalizedString("Sync Call"));
			ExProgressRecord exProgressRecord2 = new ExProgressRecord(2, new LocalizedString("Sync Call"), new LocalizedString("GetContext"));
			exProgressRecord2.ParentActivityId = exProgressRecord.ActivityId;
			for (int i = 0; i < this.SampleCountForStatistics; i++)
			{
				exProgressRecord.CurrentOperation = Strings.GetMsoDiagnosticsProgressIteration(i + 1, this.SampleCountForStatistics);
				exProgressRecord.PercentComplete = i * 100 / this.SampleCountForStatistics;
				exProgressRecord.RecordType = ProgressRecordType.Processing;
				base.WriteProgress(exProgressRecord);
				byte[] lastCookie = this.Cookie;
				byte[] lastPageToken = this.PageToken;
				string contextId = this.ExternalDirectoryOrganizationId;
				List<TenantSyncBatchResults> list2 = new List<TenantSyncBatchResults>();
				list.Add(list2);
				for (int j = 0; j < this.MaxNumberOfBatches; j++)
				{
					exProgressRecord2.CurrentOperation = Strings.GetMsoDiagnosticsProgressBatch(j + 1, this.MaxNumberOfBatches);
					exProgressRecord2.PercentComplete = j * 100 / this.MaxNumberOfBatches;
					exProgressRecord2.RecordType = ProgressRecordType.Processing;
					base.WriteProgress(exProgressRecord2);
					ExDateTime now = ExDateTime.Now;
					GetContextResponse context = this.MsoSyncService.SyncProxy.GetContext(new GetContextRequest(lastCookie, contextId, lastPageToken));
					TimeSpan responseTime = ExDateTime.Now - now;
					DirectoryObjectsAndLinks getContextResult = context.GetContextResult;
					if (getContextResult != null)
					{
						TenantSyncBatchResults tenantSyncBatchResults = new TenantSyncBatchResults(getContextResult);
						tenantSyncBatchResults.Stats.ResponseTime = responseTime;
						tenantSyncBatchResults.RawResponse = this.MsoSyncService.RawResponse;
						tenantSyncBatchResults.LastPageToken = lastPageToken;
						tenantSyncBatchResults.CalculateStats();
						list2.Add(tenantSyncBatchResults);
						contextId = null;
						lastCookie = null;
						lastPageToken = getContextResult.NextPageToken;
					}
					if (getContextResult == null || !getContextResult.More)
					{
						break;
					}
				}
				if (list2.Last<TenantSyncBatchResults>().More)
				{
					this.WriteWarning(Strings.GetMsoDiagnosticsMoreDataIsAvailable);
				}
				exProgressRecord2.RecordType = ProgressRecordType.Completed;
				base.WriteProgress(exProgressRecord2);
			}
			exProgressRecord.RecordType = ProgressRecordType.Completed;
			base.WriteProgress(exProgressRecord);
			tenantSyncSummary.CalculateStats();
			base.WriteObject(tenantSyncSummary);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x00080F54 File Offset: 0x0007F154
		private void ProcessEstimateBacklog()
		{
			BacklogSummary backlogSummary = new BacklogSummary();
			List<BacklogEstimateResults> list = backlogSummary.Batches as List<BacklogEstimateResults>;
			this.FindTheRightCookie();
			ExProgressRecord exProgressRecord = new ExProgressRecord(1, new LocalizedString("Estimate Backlog"), new LocalizedString("Calling EstimateBacklog API"));
			byte[] latestGetChangesCookie = this.Cookie;
			byte[] lastPageToken = this.PageToken;
			for (int i = 0; i < this.MaxNumberOfBatches; i++)
			{
				exProgressRecord.CurrentOperation = Strings.GetMsoDiagnosticsProgressBatch(i + 1, this.MaxNumberOfBatches);
				exProgressRecord.PercentComplete = i * 100 / this.MaxNumberOfBatches;
				exProgressRecord.RecordType = ProgressRecordType.Processing;
				base.WriteProgress(exProgressRecord);
				ExDateTime now = ExDateTime.Now;
				EstimateBacklogResponse estimateBacklogResponse = this.MsoSyncService.SyncProxy.EstimateBacklog(new EstimateBacklogRequest(latestGetChangesCookie, lastPageToken));
				TimeSpan responseTime = ExDateTime.Now - now;
				BacklogEstimateBatch estimateBacklogResult = estimateBacklogResponse.EstimateBacklogResult;
				if (estimateBacklogResult != null)
				{
					BacklogEstimateResults backlogEstimateResults = new BacklogEstimateResults(estimateBacklogResult);
					list.Add(backlogEstimateResults);
					backlogEstimateResults.ResponseTime = responseTime;
					backlogEstimateResults.RawResponse = this.MsoSyncService.RawResponse;
					latestGetChangesCookie = null;
					lastPageToken = estimateBacklogResult.NextPageToken;
				}
				if (estimateBacklogResult == null || estimateBacklogResult.StatusCode != 1)
				{
					break;
				}
			}
			if (list.Last<BacklogEstimateResults>().StatusCode == 1)
			{
				this.WriteWarning(Strings.GetMsoDiagnosticsMoreDataIsAvailable);
			}
			exProgressRecord.RecordType = ProgressRecordType.Completed;
			base.WriteProgress(exProgressRecord);
			backlogSummary.CalculateStats();
			base.WriteObject(backlogSummary);
		}

		// Token: 0x04001873 RID: 6259
		private const string GetChangesParameterSet = "GetChanges";

		// Token: 0x04001874 RID: 6260
		private const string GetContextParameterSet = "GetContext";

		// Token: 0x04001875 RID: 6261
		private const string EstimateBacklogParameterSet = "EstimateBacklog";

		// Token: 0x04001876 RID: 6262
		private const int DefaultSampleCountForStatistics = 3;

		// Token: 0x04001877 RID: 6263
		private const int MinimumSampleCountForStatistics = 1;

		// Token: 0x04001878 RID: 6264
		private const int MaximumSampleCountForStatistics = 10;

		// Token: 0x04001879 RID: 6265
		private const int DefaultMaxNumberOfBatches = 3;

		// Token: 0x0400187A RID: 6266
		private const int MinimumMaxNumberOfBatches = 1;

		// Token: 0x0400187B RID: 6267
		private const int MaximumMaxNumberOfBatches = 10;

		// Token: 0x0400187C RID: 6268
		private SyncService MsoSyncService;

		// Token: 0x0200034C RID: 844
		private enum ParameterNames
		{
			// Token: 0x0400187E RID: 6270
			Cookie,
			// Token: 0x0400187F RID: 6271
			DeltaSync,
			// Token: 0x04001880 RID: 6272
			EstimateBacklog,
			// Token: 0x04001881 RID: 6273
			MaxNumberOfBatches,
			// Token: 0x04001882 RID: 6274
			OrganizationId,
			// Token: 0x04001883 RID: 6275
			PageToken,
			// Token: 0x04001884 RID: 6276
			SampleCountForStatistics,
			// Token: 0x04001885 RID: 6277
			ServiceInstanceId,
			// Token: 0x04001886 RID: 6278
			TenantSync,
			// Token: 0x04001887 RID: 6279
			UseLastCommittedCookie
		}
	}
}
