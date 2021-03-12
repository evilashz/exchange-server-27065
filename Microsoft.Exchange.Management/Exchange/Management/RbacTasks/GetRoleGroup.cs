using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200066C RID: 1644
	[Cmdlet("Get", "RoleGroup", DefaultParameterSetName = "Identity")]
	public sealed class GetRoleGroup : GetRecipientBase<RoleGroupIdParameter, ADGroup>
	{
		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x000F550A File Offset: 0x000F370A
		// (set) Token: 0x060039FD RID: 14845 RVA: 0x000F5530 File Offset: 0x000F3730
		[Parameter]
		public SwitchParameter ShowPartnerLinked
		{
			get
			{
				return (SwitchParameter)(base.Fields["ShowPartnerLinked"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ShowPartnerLinked"] = value;
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000F5548 File Offset: 0x000F3748
		// (set) Token: 0x060039FF RID: 14847 RVA: 0x000F5550 File Offset: 0x000F3750
		[Parameter(Mandatory = false)]
		public new long UsnForReconciliationSearch
		{
			get
			{
				return base.UsnForReconciliationSearch;
			}
			set
			{
				base.UsnForReconciliationSearch = value;
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x000F5559 File Offset: 0x000F3759
		// (set) Token: 0x06003A01 RID: 14849 RVA: 0x000F5561 File Offset: 0x000F3761
		[Parameter(ParameterSetName = "AnrSet")]
		[Parameter(ParameterSetName = "Identity")]
		public new SwitchParameter ReadFromDomainController
		{
			get
			{
				return base.ReadFromDomainController;
			}
			set
			{
				base.ReadFromDomainController = value;
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06003A02 RID: 14850 RVA: 0x000F556A File Offset: 0x000F376A
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x000F5574 File Offset: 0x000F3774
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = RoleGroupIdParameter.GetRoleGroupFilter(base.InternalFilter);
				if (!this.ShowPartnerLinked)
				{
					queryFilter = QueryFilter.AndTogether(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.NotEqual, ADGroupSchema.RoleGroupType, RoleGroupType.PartnerLinked),
						queryFilter
					});
				}
				return queryFilter;
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x06003A04 RID: 14852 RVA: 0x000F55C1 File Offset: 0x000F37C1
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetRoleGroup.SortPropertiesArray;
			}
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000F55C8 File Offset: 0x000F37C8
		protected override RecipientType[] RecipientTypes
		{
			get
			{
				return GetRoleGroup.RecipientTypesArray;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x000F55CF File Offset: 0x000F37CF
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return GetRoleGroup.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000F55D6 File Offset: 0x000F37D6
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<RoleGroupSchema>();
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000F55DD File Offset: 0x000F37DD
		internal new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000F55E0 File Offset: 0x000F37E0
		internal new string Anr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06003A0A RID: 14858 RVA: 0x000F55E3 File Offset: 0x000F37E3
		internal new SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return new SwitchParameter(false);
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000F55EB File Offset: 0x000F37EB
		internal new PSCredential Credential
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000F55F0 File Offset: 0x000F37F0
		protected override void InternalValidate()
		{
			if (this.Identity == null)
			{
				if (base.CurrentOrganizationId == OrganizationId.ForestWideOrgId)
				{
					this.rootId = RoleGroupCommon.RoleGroupContainerId(base.TenantGlobalCatalogSession, this.ConfigurationSession);
				}
			}
			else
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.ServerSettings.PreferredGlobalCatalog(base.TenantGlobalCatalogSession.SessionSettings.PartitionId.ForestFQDN), true, ConsistencyMode.PartiallyConsistent, base.NetCredential, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(base.TenantGlobalCatalogSession.SessionSettings.PartitionId), 203, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleGroup\\GetRoleGroup.cs");
				base.OptionalIdentityData.RootOrgDomainContainerId = RoleGroupCommon.RoleGroupContainerId(tenantOrRootOrgRecipientSession, this.ConfigurationSession);
			}
			base.InternalValidate();
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x000F56AC File Offset: 0x000F38AC
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADGroup adgroup = (ADGroup)dataObject;
			if (null != adgroup.ForeignGroupSid)
			{
				adgroup.LinkedGroup = SecurityPrincipalIdParameter.GetFriendlyUserName(adgroup.ForeignGroupSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				adgroup.ResetChangeTracking();
			}
			RoleGroup roleGroup = RoleGroupCommon.PopulateRoleAssignmentsAndConvert(adgroup, this.ConfigurationSession);
			roleGroup.PopulateCapabilitiesProperty();
			return roleGroup;
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000F5718 File Offset: 0x000F3918
		protected override bool ShouldSkipPresentationObject(IConfigurable presentationObject)
		{
			if (base.ExchangeRunspaceConfig != null && base.ExchangeRunspaceConfig.IsDedicatedTenantAdmin)
			{
				RoleGroup roleGroup = presentationObject as RoleGroup;
				if (roleGroup != null)
				{
					return !roleGroup.Roles.Any((ADObjectId adObject) => adObject.Name.StartsWith("SSA_", StringComparison.OrdinalIgnoreCase));
				}
			}
			return false;
		}

		// Token: 0x04002641 RID: 9793
		private ADObjectId rootId;

		// Token: 0x04002642 RID: 9794
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.RoleGroup
		};

		// Token: 0x04002643 RID: 9795
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			ADRecipientSchema.DisplayName
		};

		// Token: 0x04002644 RID: 9796
		private static readonly RecipientType[] RecipientTypesArray = new RecipientType[]
		{
			RecipientType.Group
		};
	}
}
