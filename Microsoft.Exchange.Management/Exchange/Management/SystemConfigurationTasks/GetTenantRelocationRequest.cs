using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000691 RID: 1681
	[Cmdlet("Get", "TenantRelocationRequest", DefaultParameterSetName = "PartitionWide")]
	public sealed class GetTenantRelocationRequest : GetSystemConfigurationObjectTask<TenantRelocationRequestIdParameter, TenantRelocationRequest>
	{
		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000FD1E4 File Offset: 0x000FB3E4
		// (set) Token: 0x06003B79 RID: 15225 RVA: 0x000FD1FB File Offset: 0x000FB3FB
		[Parameter(Mandatory = true, ParameterSetName = "PartitionWide")]
		public new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return (AccountPartitionIdParameter)base.Fields["AccountPartitionParam"];
			}
			set
			{
				base.Fields["AccountPartitionParam"] = value;
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000FD20E File Offset: 0x000FB40E
		// (set) Token: 0x06003B7B RID: 15227 RVA: 0x000FD234 File Offset: 0x000FB434
		[Parameter(ParameterSetName = "PartitionWide")]
		public SwitchParameter SourceStateOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["SourceStateOnlyParam"] ?? false);
			}
			set
			{
				base.Fields["SourceStateOnlyParam"] = value;
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06003B7C RID: 15228 RVA: 0x000FD24C File Offset: 0x000FB44C
		// (set) Token: 0x06003B7D RID: 15229 RVA: 0x000FD263 File Offset: 0x000FB463
		[Parameter(ParameterSetName = "PartitionWide")]
		public RelocationStateRequested RelocationStateRequested
		{
			get
			{
				return (RelocationStateRequested)base.Fields["RelocationStateRequestedParam"];
			}
			set
			{
				base.Fields["RelocationStateRequestedParam"] = value;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06003B7E RID: 15230 RVA: 0x000FD27B File Offset: 0x000FB47B
		// (set) Token: 0x06003B7F RID: 15231 RVA: 0x000FD292 File Offset: 0x000FB492
		[Parameter(ParameterSetName = "PartitionWide")]
		public RelocationStatusDetailsSource RelocationStatusDetailsSource
		{
			get
			{
				return (RelocationStatusDetailsSource)base.Fields["RelocationStatusDetailsSourceParam"];
			}
			set
			{
				base.Fields["RelocationStatusDetailsSourceParam"] = value;
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06003B80 RID: 15232 RVA: 0x000FD2AA File Offset: 0x000FB4AA
		// (set) Token: 0x06003B81 RID: 15233 RVA: 0x000FD2C1 File Offset: 0x000FB4C1
		[Parameter(ParameterSetName = "PartitionWide")]
		public RelocationError RelocationLastError
		{
			get
			{
				return (RelocationError)base.Fields["RelocationLastErrorParam"];
			}
			set
			{
				base.Fields["RelocationLastErrorParam"] = value;
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x000FD2D9 File Offset: 0x000FB4D9
		// (set) Token: 0x06003B83 RID: 15235 RVA: 0x000FD2FA File Offset: 0x000FB4FA
		[Parameter(ParameterSetName = "PartitionWide")]
		public SwitchParameter Suspended
		{
			get
			{
				return (SwitchParameter)(base.Fields["SuspendedParam"] ?? false);
			}
			set
			{
				base.Fields["SuspendedParam"] = value;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06003B84 RID: 15236 RVA: 0x000FD312 File Offset: 0x000FB512
		// (set) Token: 0x06003B85 RID: 15237 RVA: 0x000FD333 File Offset: 0x000FB533
		[Parameter(ParameterSetName = "PartitionWide")]
		public SwitchParameter Lockdown
		{
			get
			{
				return (SwitchParameter)(base.Fields["LockdownParam"] ?? false);
			}
			set
			{
				base.Fields["LockdownParam"] = value;
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x000FD34B File Offset: 0x000FB54B
		// (set) Token: 0x06003B87 RID: 15239 RVA: 0x000FD36C File Offset: 0x000FB56C
		[Parameter(ParameterSetName = "PartitionWide")]
		public SwitchParameter StaleLockdown
		{
			get
			{
				return (SwitchParameter)(base.Fields["StaleLockdownParam"] ?? false);
			}
			set
			{
				base.Fields["StaleLockdownParam"] = value;
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x000FD384 File Offset: 0x000FB584
		// (set) Token: 0x06003B89 RID: 15241 RVA: 0x000FD3A5 File Offset: 0x000FB5A5
		[Parameter(ParameterSetName = "PartitionWide")]
		public SwitchParameter HasPermanentError
		{
			get
			{
				return (SwitchParameter)(base.Fields["HasPermanentErrorParam"] ?? false);
			}
			set
			{
				base.Fields["HasPermanentErrorParam"] = value;
			}
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06003B8B RID: 15243 RVA: 0x000FD3C8 File Offset: 0x000FB5C8
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = base.Fields.IsModified("RelocationStateRequestedParam") ? new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.RelocationStateRequested, (int)this.RelocationStateRequested) : null;
				QueryFilter queryFilter2 = base.Fields.IsModified("RelocationStatusDetailsSourceParam") ? new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.RelocationStatusDetailsRaw, (byte)this.RelocationStatusDetailsSource) : null;
				QueryFilter queryFilter3 = base.Fields.IsModified("RelocationLastErrorParam") ? new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.RelocationLastError, (int)this.RelocationLastError) : null;
				QueryFilter queryFilter4 = base.Fields.IsModified("SuspendedParam") ? new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.Suspended, this.Suspended) : null;
				QueryFilter queryFilter5 = base.Fields.IsModified("LockdownParam") ? (this.Lockdown ? TenantRelocationRequest.LockedRelocationRequestsFilter : new NotFilter(TenantRelocationRequest.LockedRelocationRequestsFilter)) : null;
				QueryFilter queryFilter6 = null;
				if (base.Fields.IsModified("StaleLockdownParam"))
				{
					int config = TenantRelocationConfigImpl.GetConfig<int>("MaxTenantLockDownTimeInMinutes");
					ExDateTime olderThan = ExDateTime.Now.AddMinutes((double)(-(double)config));
					queryFilter6 = TenantRelocationRequest.GetStaleLockedRelocationRequestsFilter(olderThan, false);
					if (!this.StaleLockdown)
					{
						queryFilter6 = new NotFilter(queryFilter6);
					}
				}
				QueryFilter queryFilter7 = base.Fields.IsModified("HasPermanentErrorParam") ? new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.HasPermanentError, this.HasPermanentError) : null;
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					TenantRelocationRequest.TenantRelocationRequestFilter,
					queryFilter,
					queryFilter2,
					queryFilter3,
					queryFilter4,
					queryFilter5,
					queryFilter6,
					queryFilter7
				});
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06003B8C RID: 15244 RVA: 0x000FD584 File Offset: 0x000FB784
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000FD588 File Offset: 0x000FB788
		protected override IConfigDataProvider CreateSession()
		{
			if (this.AccountPartition == null && this.Identity == null)
			{
				base.WriteError(new NotSupportedException(Strings.ErrorUnknownPartition), ErrorCategory.InvalidData, null);
			}
			PartitionId partitionId;
			if (this.Identity != null)
			{
				if (this.Identity.RawIdentity.Contains("*"))
				{
					base.WriteError(new ArgumentException(Strings.ErrorWildcardNotSupportedInRelocationIdentity(this.Identity.RawIdentity)), ErrorCategory.InvalidOperation, this.Identity);
				}
				OrganizationId organizationId = this.Identity.ResolveOrganizationId();
				partitionId = organizationId.PartitionId;
			}
			else
			{
				partitionId = RecipientTaskHelper.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			this.sourceForestRIDMaster = ForestTenantRelocationsCache.GetRidMasterName(partitionId);
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsPartitionId(partitionId), 223, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Relocation\\GetTenantRelocationRequest.cs");
			if (base.DomainController != null && !this.sourceForestRIDMaster.StartsWith(base.DomainController, StringComparison.OrdinalIgnoreCase))
			{
				ForestTenantRelocationsCache.Reset();
			}
			tenantConfigurationSession.SessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
			return tenantConfigurationSession;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000FD6A0 File Offset: 0x000FB8A0
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity
			});
			TenantRelocationRequest tenantRelocationRequest = (TenantRelocationRequest)dataObject;
			if (!this.SourceStateOnly)
			{
				if (tenantRelocationRequest.TargetForest != null)
				{
					this.targetForestRIDMaster = ForestTenantRelocationsCache.GetRidMasterName(new PartitionId(tenantRelocationRequest.TargetForest));
				}
				Exception ex;
				TenantRelocationRequest.PopulatePresentationObject(tenantRelocationRequest, this.targetForestRIDMaster, out ex);
				if (ex != null)
				{
					if (ex is CannotFindTargetTenantException)
					{
						base.WriteWarning(ex.Message);
					}
					else
					{
						base.WriteError(ex, ErrorCategory.InvalidOperation, tenantRelocationRequest.Identity);
					}
				}
				GetTenantRelocationRequest.PopulateGlsProperty(tenantRelocationRequest, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				GetTenantRelocationRequest.PopulateRidMasterProperties(tenantRelocationRequest, this.sourceForestRIDMaster, this.targetForestRIDMaster, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				if (tenantRelocationRequest.OriginatingServer != this.sourceForestRIDMaster)
				{
					this.warning = Strings.WarningShouldReadFromRidMaster(tenantRelocationRequest.OriginatingServer, this.sourceForestRIDMaster);
				}
			}
			base.WriteResult(tenantRelocationRequest);
			TaskLogger.LogExit();
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000FD793 File Offset: 0x000FB993
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			if (!string.IsNullOrEmpty(this.warning.ToString()))
			{
				this.WriteWarning(this.warning);
			}
			base.InternalEndProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000FD7C9 File Offset: 0x000FB9C9
		internal static void PopulateRidMasterProperties(TenantRelocationRequest presentationObject, string sourceForestRIDMaster, string targetForestRIDMaster, Task.TaskVerboseLoggingDelegate writeVerbose)
		{
			presentationObject.SourceForestRIDMaster = sourceForestRIDMaster;
			presentationObject.TargetForestRIDMaster = targetForestRIDMaster;
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000FD7DC File Offset: 0x000FB9DC
		internal static bool TryGlsLookupByExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN, out Exception exception)
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			resourceForestFqdn = null;
			accountForestFqdn = null;
			tenantContainerCN = null;
			bool result = false;
			try
			{
				result = glsDirectorySession.TryGetTenantForestsByOrgGuid(externalDirectoryOrganizationId, out resourceForestFqdn, out accountForestFqdn, out tenantContainerCN);
				exception = null;
			}
			catch (GlsTransientException ex)
			{
				exception = ex;
			}
			catch (GlsTenantNotFoundException ex2)
			{
				exception = ex2;
			}
			catch (GlsPermanentException ex3)
			{
				exception = ex3;
			}
			return result;
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000FD850 File Offset: 0x000FBA50
		internal static void PopulateGlsProperty(TenantRelocationRequest presentationObject, Task.TaskVerboseLoggingDelegate writeVerbose)
		{
			if (ADSessionSettings.IsGlsDisabled)
			{
				presentationObject.GLSResolvedForest = GetTenantRelocationRequest.GlsDisabled;
				return;
			}
			Guid externalDirectoryOrganizationId = new Guid(presentationObject.ExternalDirectoryOrganizationId);
			string text;
			string glsresolvedForest;
			string text2;
			Exception ex;
			if (GetTenantRelocationRequest.TryGlsLookupByExternalDirectoryOrganizationId(externalDirectoryOrganizationId, out text, out glsresolvedForest, out text2, out ex))
			{
				presentationObject.GLSResolvedForest = glsresolvedForest;
			}
			else
			{
				presentationObject.GLSResolvedForest = GetTenantRelocationRequest.GlsLookupFailed;
			}
			if (ex != null)
			{
				presentationObject.GLSResolvedForest = "<" + ex.GetType().Name + ">";
				if (writeVerbose != null)
				{
					writeVerbose(Strings.ErrorInGlsLookup(ex.ToString()));
				}
			}
		}

		// Token: 0x040026C3 RID: 9923
		internal const string PartitionWide = "PartitionWide";

		// Token: 0x040026C4 RID: 9924
		internal const string AccountPartitionParam = "AccountPartitionParam";

		// Token: 0x040026C5 RID: 9925
		internal const string SourceStateOnlyParam = "SourceStateOnlyParam";

		// Token: 0x040026C6 RID: 9926
		internal const string RelocationStateRequestedParam = "RelocationStateRequestedParam";

		// Token: 0x040026C7 RID: 9927
		internal const string RelocationStatusDetailsSourceParam = "RelocationStatusDetailsSourceParam";

		// Token: 0x040026C8 RID: 9928
		internal const string RelocationLastErrorParam = "RelocationLastErrorParam";

		// Token: 0x040026C9 RID: 9929
		internal const string SuspendedParam = "SuspendedParam";

		// Token: 0x040026CA RID: 9930
		internal const string LockdownParam = "LockdownParam";

		// Token: 0x040026CB RID: 9931
		internal const string StaleLockdownParam = "StaleLockdownParam";

		// Token: 0x040026CC RID: 9932
		internal const string HasPermanentErrorParam = "HasPermanentErrorParam";

		// Token: 0x040026CD RID: 9933
		private string sourceForestRIDMaster;

		// Token: 0x040026CE RID: 9934
		private string targetForestRIDMaster;

		// Token: 0x040026CF RID: 9935
		private LocalizedString warning;

		// Token: 0x040026D0 RID: 9936
		internal static readonly string GlsDisabled = "<GLS disabled>";

		// Token: 0x040026D1 RID: 9937
		internal static readonly string GlsLookupFailed = "<GLS lookup failed - tenant not found>";
	}
}
