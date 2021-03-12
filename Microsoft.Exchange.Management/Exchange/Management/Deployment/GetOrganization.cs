using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001D3 RID: 467
	[Cmdlet("Get", "Organization", DefaultParameterSetName = "Identity")]
	public sealed class GetOrganization : GetSystemConfigurationObjectTask<OrganizationIdParameter, ExchangeConfigurationUnit>
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x000483F7 File Offset: 0x000465F7
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x0004840E File Offset: 0x0004660E
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override OrganizationIdParameter Identity
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x00048421 File Offset: 0x00046621
		// (set) Token: 0x06001030 RID: 4144 RVA: 0x00048429 File Offset: 0x00046629
		[Parameter(Mandatory = false)]
		public long UsnForReconciliationSearch
		{
			get
			{
				return this.usnForReconciliationSearch;
			}
			set
			{
				this.usnForReconciliationSearch = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00048432 File Offset: 0x00046632
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x0004844C File Offset: 0x0004664C
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				MonadFilter monadFilter = new MonadFilter(value, this, this.FilterableObjectSchema);
				this.inputFilter = monadFilter.InnerFilter;
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00048484 File Offset: 0x00046684
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x0004848C File Offset: 0x0004668C
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00048495 File Offset: 0x00046695
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x0004849D File Offset: 0x0004669D
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return base.AccountPartition;
			}
			set
			{
				base.AccountPartition = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x000484A6 File Offset: 0x000466A6
		internal ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<ExchangeConfigurationUnitSchema>();
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x000484AD File Offset: 0x000466AD
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000484B0 File Offset: 0x000466B0
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = new ExistsFilter(ExchangeConfigurationUnitSchema.OrganizationalUnitLink);
				QueryFilter queryFilter2;
				if (base.InternalFilter != null)
				{
					queryFilter2 = new AndFilter(new QueryFilter[]
					{
						base.InternalFilter,
						queryFilter
					});
				}
				else
				{
					queryFilter2 = queryFilter;
				}
				if (this.inputFilter != null)
				{
					queryFilter2 = new AndFilter(new QueryFilter[]
					{
						queryFilter2,
						this.inputFilter
					});
				}
				if (this.UsnForReconciliationSearch >= 0L)
				{
					queryFilter2 = new AndFilter(new QueryFilter[]
					{
						queryFilter2,
						new ComparisonFilter(ComparisonOperator.GreaterThan, ExchangeConfigurationUnitSchema.UsnCreated, this.UsnForReconciliationSearch)
					});
				}
				return queryFilter2;
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00048550 File Offset: 0x00046750
		protected override IConfigDataProvider CreateSession()
		{
			ADSessionSettings adsessionSettings = (this.accountPartition != null) ? ADSessionSettings.FromAllTenantsPartitionId(this.accountPartition) : ADSessionSettings.RescopeToAllTenants(base.SessionSettings);
			adsessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
			return DirectorySessionFactory.Default.CreateTenantConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, adsessionSettings, 184, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\GetOrganizationTask.cs");
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000485B8 File Offset: 0x000467B8
		protected override void InternalStateReset()
		{
			this.accountPartition = null;
			if (this.AccountPartition != null)
			{
				this.accountPartition = RecipientTaskHelper.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			PartitionId partitionId;
			if (this.RequestForMultipleOrgs() && this.accountPartition == null && ADAccountPartitionLocator.IsSingleForestTopology(out partitionId))
			{
				this.accountPartition = partitionId;
				this.WriteWarning(Strings.ImplicitPartitionIdSupplied(this.accountPartition.ToString()));
			}
			if (this.Identity != null)
			{
				this.Identity.AccountPartition = this.accountPartition;
			}
			base.InternalStateReset();
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0004864C File Offset: 0x0004684C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (this.UsnForReconciliationSearch >= 0L && base.DomainController == null)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorDomainControllerNotSpecifiedWithUsnForReconciliationSearch), ErrorCategory.InvalidArgument, null);
			}
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00048688 File Offset: 0x00046888
		protected override void WriteResult(IConfigurable dataObject)
		{
			ExchangeConfigurationUnit exchangeConfigurationUnit = (ExchangeConfigurationUnit)dataObject;
			if (exchangeConfigurationUnit.OrganizationStatus != OrganizationStatus.ReadyForRemoval && exchangeConfigurationUnit.OrganizationStatus != OrganizationStatus.SoftDeleted && exchangeConfigurationUnit.OrganizationStatus != OrganizationStatus.Active && exchangeConfigurationUnit.OrganizationStatus != OrganizationStatus.ReadOnly)
			{
				this.WriteWarning(Strings.ErrorNonActiveOrganizationFound(exchangeConfigurationUnit.Identity.ToString()));
			}
			TenantOrganizationPresentationObject tenantOrganizationPresentationObject = new TenantOrganizationPresentationObject(exchangeConfigurationUnit);
			if (exchangeConfigurationUnit.HasSharedConfigurationBL())
			{
				tenantOrganizationPresentationObject.IsSharingConfiguration = true;
				tenantOrganizationPresentationObject.ResetChangeTracking();
			}
			base.WriteResult(tenantOrganizationPresentationObject);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000486FB File Offset: 0x000468FB
		protected override void InternalValidate()
		{
			if (this.RequestForMultipleOrgs() && this.accountPartition == null)
			{
				base.WriteError(new ArgumentException(Strings.PartitionIdRequiredForMultipleOrgSearch), ErrorCategory.InvalidArgument, null);
			}
			base.InternalValidate();
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00048730 File Offset: 0x00046930
		private bool RequestForMultipleOrgs()
		{
			return this.Identity == null;
		}

		// Token: 0x04000769 RID: 1897
		private QueryFilter inputFilter;

		// Token: 0x0400076A RID: 1898
		private long usnForReconciliationSearch = -1L;

		// Token: 0x0400076B RID: 1899
		private PartitionId accountPartition;
	}
}
