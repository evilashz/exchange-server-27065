using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B1E RID: 2846
	[Cmdlet("Get", "ThrottlingPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetThrottlingPolicy : GetMultitenancySystemConfigurationObjectTask<ThrottlingPolicyIdParameter, ThrottlingPolicy>
	{
		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x060064F9 RID: 25849 RVA: 0x001A549E File Offset: 0x001A369E
		// (set) Token: 0x060064FA RID: 25850 RVA: 0x001A54B5 File Offset: 0x001A36B5
		[Parameter(Mandatory = false)]
		public ThrottlingPolicyScopeType ThrottlingPolicyScope
		{
			get
			{
				return (ThrottlingPolicyScopeType)base.Fields[ThrottlingPolicySchema.ThrottlingPolicyScope];
			}
			set
			{
				base.VerifyValues<ThrottlingPolicyScopeType>(GetThrottlingPolicy.AllowedThrottlingPolicyScopeTypes, value);
				base.Fields[ThrottlingPolicySchema.ThrottlingPolicyScope] = value;
				if (value == ThrottlingPolicyScopeType.Global)
				{
					this.getGlobalPolicy = true;
				}
			}
		}

		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x060064FB RID: 25851 RVA: 0x001A54E4 File Offset: 0x001A36E4
		// (set) Token: 0x060064FC RID: 25852 RVA: 0x001A54EC File Offset: 0x001A36EC
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x060064FD RID: 25853 RVA: 0x001A54F5 File Offset: 0x001A36F5
		// (set) Token: 0x060064FE RID: 25854 RVA: 0x001A54FD File Offset: 0x001A36FD
		[Parameter(Mandatory = false)]
		public SwitchParameter Explicit { get; set; }

		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x060064FF RID: 25855 RVA: 0x001A5506 File Offset: 0x001A3706
		// (set) Token: 0x06006500 RID: 25856 RVA: 0x001A550E File Offset: 0x001A370E
		[Parameter(Mandatory = false)]
		public SwitchParameter Diagnostics { get; set; }

		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x06006501 RID: 25857 RVA: 0x001A5517 File Offset: 0x001A3717
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x06006502 RID: 25858 RVA: 0x001A551C File Offset: 0x001A371C
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity == null)
				{
					ADObjectId adobjectId;
					if (this.getGlobalPolicy)
					{
						adobjectId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
					}
					else
					{
						adobjectId = base.CurrentOrgContainerId;
						if (base.SharedConfiguration != null)
						{
							adobjectId = base.SharedConfiguration.SharedConfigurationCU.Id;
						}
					}
					return adobjectId.GetChildId("Global Settings");
				}
				return null;
			}
		}

		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x06006503 RID: 25859 RVA: 0x001A556E File Offset: 0x001A376E
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Static;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x06006504 RID: 25860 RVA: 0x001A5580 File Offset: 0x001A3780
		protected override QueryFilter InternalFilter
		{
			get
			{
				return base.OptionalIdentityData.AdditionalFilter;
			}
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x001A5590 File Offset: 0x001A3790
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.Fields.IsModified(ThrottlingPolicySchema.ThrottlingPolicyScope))
			{
				base.OptionalIdentityData.AdditionalFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					base.OptionalIdentityData.AdditionalFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ThrottlingPolicySchema.ThrottlingPolicyScope, this.ThrottlingPolicyScope)
				});
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x001A55FE File Offset: 0x001A37FE
		protected override IConfigDataProvider CreateSession()
		{
			if (this.getGlobalPolicy)
			{
				return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 187, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\throttling\\GetThrottlingPolicy.cs");
			}
			return base.CreateSession();
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x001A563C File Offset: 0x001A383C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (base.WriteObjectCount == 0U && base.Fields.IsModified(ThrottlingPolicySchema.ThrottlingPolicyScope) && this.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Organization && !this.Explicit)
			{
				DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 215, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\throttling\\GetThrottlingPolicy.cs");
				try
				{
					ThrottlingPolicy defaultOrganizationEffectiveThrottlingPolicy = ThrottlingPolicy.GetDefaultOrganizationEffectiveThrottlingPolicy();
					this.WriteWarning(Strings.WarningReturnDefaultOrganizationThrottlingPolicy);
					base.WriteResult(defaultOrganizationEffectiveThrottlingPolicy);
				}
				catch (GlobalThrottlingPolicyNotFoundException)
				{
					base.WriteError(new ManagementObjectNotFoundException(DirectoryStrings.GlobalThrottlingPolicyNotFoundException), ErrorCategory.ObjectNotFound, null);
				}
				catch (GlobalThrottlingPolicyAmbiguousException)
				{
					base.WriteError(new ManagementObjectAmbiguousException(DirectoryStrings.GlobalThrottlingPolicyAmbiguousException), ErrorCategory.InvalidResult, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x001A5710 File Offset: 0x001A3910
		protected override void WriteResult(IConfigurable dataObject)
		{
			ThrottlingPolicy throttlingPolicy = (ThrottlingPolicy)dataObject;
			if (!this.Explicit)
			{
				try
				{
					throttlingPolicy.ConvertToEffectiveThrottlingPolicy(false);
				}
				catch (GlobalThrottlingPolicyNotFoundException)
				{
					base.WriteError(new ManagementObjectNotFoundException(DirectoryStrings.GlobalThrottlingPolicyNotFoundException), ErrorCategory.ObjectNotFound, null);
				}
				catch (GlobalThrottlingPolicyAmbiguousException)
				{
					base.WriteError(new ManagementObjectAmbiguousException(DirectoryStrings.GlobalThrottlingPolicyAmbiguousException), ErrorCategory.InvalidResult, null);
				}
			}
			if (!this.Diagnostics)
			{
				throttlingPolicy.Diagnostics = null;
			}
			base.WriteResult(throttlingPolicy);
		}

		// Token: 0x0400363C RID: 13884
		private static readonly ThrottlingPolicyScopeType[] AllowedThrottlingPolicyScopeTypes = (ThrottlingPolicyScopeType[])Enum.GetValues(typeof(ThrottlingPolicyScopeType));

		// Token: 0x0400363D RID: 13885
		private bool getGlobalPolicy;
	}
}
