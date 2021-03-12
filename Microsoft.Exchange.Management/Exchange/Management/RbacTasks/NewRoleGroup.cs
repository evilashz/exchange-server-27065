using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200066B RID: 1643
	[Cmdlet("New", "RoleGroup", SupportsShouldProcess = true, DefaultParameterSetName = "default")]
	public sealed class NewRoleGroup : NewGeneralRecipientObjectTask<ADGroup>
	{
		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x000F44C0 File Offset: 0x000F26C0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string managedBy = RoleGroupCommon.NamesFromObjects(this.managedByRecipients);
				string text = RoleGroupCommon.NamesFromObjects(this.roles);
				string recipientWriteScope = string.Empty;
				string configWriteScope = string.Empty;
				if (this.ou != null)
				{
					recipientWriteScope = this.ou.Name;
				}
				else if (this.customRecipientScope != null)
				{
					recipientWriteScope = this.customRecipientScope.Name;
				}
				if (this.customConfigScope != null)
				{
					configWriteScope = this.customConfigScope.Name;
				}
				return Strings.ConfirmationMessageNewRoleGroup(this.Name, text, managedBy, recipientWriteScope, configWriteScope);
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000F453E File Offset: 0x000F273E
		public new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000F4541 File Offset: 0x000F2741
		// (set) Token: 0x060039D1 RID: 14801 RVA: 0x000F454E File Offset: 0x000F274E
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public new string Name
		{
			get
			{
				return this.DataObject.Name;
			}
			set
			{
				this.DataObject.Name = value;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x000F455C File Offset: 0x000F275C
		// (set) Token: 0x060039D3 RID: 14803 RVA: 0x000F4569 File Offset: 0x000F2769
		[ValidateNotNullOrEmpty]
		[Parameter]
		public string SamAccountName
		{
			get
			{
				return this.DataObject.SamAccountName;
			}
			set
			{
				this.DataObject.SamAccountName = value;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000F4577 File Offset: 0x000F2777
		// (set) Token: 0x060039D5 RID: 14805 RVA: 0x000F458E File Offset: 0x000F278E
		[Parameter]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<SecurityPrincipalIdParameter> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<SecurityPrincipalIdParameter>)base.Fields[DistributionGroupSchema.ManagedBy];
			}
			set
			{
				base.Fields[DistributionGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000F45A1 File Offset: 0x000F27A1
		// (set) Token: 0x060039D7 RID: 14807 RVA: 0x000F45B8 File Offset: 0x000F27B8
		[ValidateNotNullOrEmpty]
		[Parameter]
		public MultiValuedProperty<SecurityPrincipalIdParameter> Members
		{
			get
			{
				return (MultiValuedProperty<SecurityPrincipalIdParameter>)base.Fields[RoleGroupParameters.ParameterMembers];
			}
			set
			{
				base.Fields[RoleGroupParameters.ParameterMembers] = value;
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000F45CB File Offset: 0x000F27CB
		// (set) Token: 0x060039D9 RID: 14809 RVA: 0x000F45E2 File Offset: 0x000F27E2
		[ValidateNotNullOrEmpty]
		[Parameter]
		public RoleIdParameter[] Roles
		{
			get
			{
				return (RoleIdParameter[])base.Fields["Roles"];
			}
			set
			{
				base.Fields["Roles"] = value;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x000F45F5 File Offset: 0x000F27F5
		// (set) Token: 0x060039DB RID: 14811 RVA: 0x000F460C File Offset: 0x000F280C
		[Parameter]
		[ValidateNotNullOrEmpty]
		public OrganizationalUnitIdParameter RecipientOrganizationalUnitScope
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields[RbacCommonParameters.ParameterRecipientOrganizationalUnitScope];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterRecipientOrganizationalUnitScope] = value;
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000F461F File Offset: 0x000F281F
		// (set) Token: 0x060039DD RID: 14813 RVA: 0x000F4636 File Offset: 0x000F2836
		[Parameter]
		[ValidateNotNullOrEmpty]
		public ManagementScopeIdParameter CustomRecipientWriteScope
		{
			get
			{
				return (ManagementScopeIdParameter)base.Fields[RbacCommonParameters.ParameterCustomRecipientWriteScope];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterCustomRecipientWriteScope] = value;
			}
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x060039DE RID: 14814 RVA: 0x000F4649 File Offset: 0x000F2849
		// (set) Token: 0x060039DF RID: 14815 RVA: 0x000F4660 File Offset: 0x000F2860
		[ValidateNotNullOrEmpty]
		[Parameter]
		public ManagementScopeIdParameter CustomConfigWriteScope
		{
			get
			{
				return (ManagementScopeIdParameter)base.Fields[RbacCommonParameters.ParameterCustomConfigWriteScope];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterCustomConfigWriteScope] = value;
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060039E0 RID: 14816 RVA: 0x000F4673 File Offset: 0x000F2873
		// (set) Token: 0x060039E1 RID: 14817 RVA: 0x000F468A File Offset: 0x000F288A
		[Parameter(Mandatory = true, ParameterSetName = "linkedpartnergroup")]
		[ValidateNotNullOrEmpty]
		public string LinkedPartnerGroupId
		{
			get
			{
				return (string)base.Fields[RoleGroupParameters.ParameterLinkedPartnerGroupId];
			}
			set
			{
				base.Fields[RoleGroupParameters.ParameterLinkedPartnerGroupId] = value;
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000F469D File Offset: 0x000F289D
		// (set) Token: 0x060039E3 RID: 14819 RVA: 0x000F46B4 File Offset: 0x000F28B4
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "linkedpartnergroup")]
		public string LinkedPartnerOrganizationId
		{
			get
			{
				return (string)base.Fields[RoleGroupParameters.PartnerLinkedPartnerOrganizationId];
			}
			set
			{
				base.Fields[RoleGroupParameters.PartnerLinkedPartnerOrganizationId] = value;
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000F46C7 File Offset: 0x000F28C7
		// (set) Token: 0x060039E5 RID: 14821 RVA: 0x000F46ED File Offset: 0x000F28ED
		[Parameter]
		public SwitchParameter PartnerManaged
		{
			get
			{
				return (SwitchParameter)(base.Fields[RoleGroupParameters.PartnerLinkedPartnerManaged] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields[RoleGroupParameters.PartnerLinkedPartnerManaged] = value;
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060039E6 RID: 14822 RVA: 0x000F4705 File Offset: 0x000F2905
		// (set) Token: 0x060039E7 RID: 14823 RVA: 0x000F471C File Offset: 0x000F291C
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "crossforest")]
		public UniversalSecurityGroupIdParameter LinkedForeignGroup
		{
			get
			{
				return (UniversalSecurityGroupIdParameter)base.Fields[RoleGroupParameters.ParameterLinkedForeignGroup];
			}
			set
			{
				base.Fields[RoleGroupParameters.ParameterLinkedForeignGroup] = value;
			}
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x060039E8 RID: 14824 RVA: 0x000F472F File Offset: 0x000F292F
		// (set) Token: 0x060039E9 RID: 14825 RVA: 0x000F4746 File Offset: 0x000F2946
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "crossforest")]
		public string LinkedDomainController
		{
			get
			{
				return (string)base.Fields[RoleGroupParameters.ParameterLinkedDomainController];
			}
			set
			{
				base.Fields[RoleGroupParameters.ParameterLinkedDomainController] = value;
			}
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x060039EA RID: 14826 RVA: 0x000F4759 File Offset: 0x000F2959
		// (set) Token: 0x060039EB RID: 14827 RVA: 0x000F4770 File Offset: 0x000F2970
		[Parameter(Mandatory = false, ParameterSetName = "crossforest")]
		public PSCredential LinkedCredential
		{
			get
			{
				return (PSCredential)base.Fields[RoleGroupParameters.ParameterLinkedCredential];
			}
			set
			{
				base.Fields[RoleGroupParameters.ParameterLinkedCredential] = value;
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000F4783 File Offset: 0x000F2983
		// (set) Token: 0x060039ED RID: 14829 RVA: 0x000F479A File Offset: 0x000F299A
		[Parameter]
		public string Description
		{
			get
			{
				return (string)base.Fields["Description"];
			}
			set
			{
				base.Fields["Description"] = value;
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000F47AD File Offset: 0x000F29AD
		// (set) Token: 0x060039EF RID: 14831 RVA: 0x000F47D3 File Offset: 0x000F29D3
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000F47EB File Offset: 0x000F29EB
		// (set) Token: 0x060039F1 RID: 14833 RVA: 0x000F4802 File Offset: 0x000F2A02
		[Parameter(Mandatory = false)]
		public string ValidationOrganization
		{
			get
			{
				return (string)base.Fields["ValidationOrganization"];
			}
			set
			{
				base.Fields["ValidationOrganization"] = value;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x000F4815 File Offset: 0x000F2A15
		// (set) Token: 0x060039F3 RID: 14835 RVA: 0x000F4844 File Offset: 0x000F2A44
		[Parameter(Mandatory = false)]
		public Guid WellKnownObjectGuid
		{
			get
			{
				if (base.Fields["WellKnownObjectGuid"] == null)
				{
					return Guid.Empty;
				}
				return (Guid)base.Fields["WellKnownObjectGuid"];
			}
			set
			{
				base.Fields["WellKnownObjectGuid"] = value;
			}
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000F485C File Offset: 0x000F2A5C
		protected override void InternalBeginProcessing()
		{
			bool flag = false;
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.ValidationOrganization != null && !string.Equals(this.ValidationOrganization, base.CurrentOrganizationId.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				base.ThrowTerminatingError(new ValidationOrgCurrentOrgNotMatchException(this.ValidationOrganization, base.CurrentOrganizationId.ToString()), ExchangeErrorCategory.Client, null);
			}
			if ("crossforest" == base.ParameterSetName)
			{
				try
				{
					NetworkCredential userForestCredential = (this.LinkedCredential == null) ? null : this.LinkedCredential.GetNetworkCredential();
					this.linkedGroupSid = MailboxTaskHelper.GetSidFromAnotherForest<ADGroup>(this.LinkedForeignGroup, this.LinkedDomainController, userForestCredential, base.GlobalConfigSession, new MailboxTaskHelper.GetUniqueObject(base.GetDataObject<ADGroup>), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new MailboxTaskHelper.OneStringErrorDelegate(Strings.ErrorLinkedGroupInTheCurrentForest), new MailboxTaskHelper.TwoStringErrorDelegate(Strings.ErrorGroupNotFoundOnGlobalCatalog), new MailboxTaskHelper.TwoStringErrorDelegate(Strings.ErrorGroupNotFoundOnDomainController), new MailboxTaskHelper.TwoStringErrorDelegate(Strings.ErrorGroupNotUniqueOnGlobalCatalog), new MailboxTaskHelper.TwoStringErrorDelegate(Strings.ErrorGroupNotUniqueOnDomainController), new MailboxTaskHelper.OneStringErrorDelegate(Strings.ErrorVerifyLinkedGroupForest));
				}
				catch (PSArgumentException exception)
				{
					base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, this.LinkedCredential);
				}
			}
			if (!base.Fields.IsModified(ADGroupSchema.ManagedBy) && base.CurrentOrganizationId.Equals(base.ExecutingUserOrganizationId))
			{
				List<SecurityPrincipalIdParameter> list = new List<SecurityPrincipalIdParameter>(2);
				flag = true;
				bool useGlobalCatalog = base.TenantGlobalCatalogSession.UseGlobalCatalog;
				bool useConfigNC = base.TenantGlobalCatalogSession.UseConfigNC;
				bool skipRangedAttributes = base.TenantGlobalCatalogSession.SkipRangedAttributes;
				try
				{
					base.TenantGlobalCatalogSession.UseGlobalCatalog = true;
					base.TenantGlobalCatalogSession.UseConfigNC = false;
					base.TenantGlobalCatalogSession.SkipRangedAttributes = true;
					ADGroup adgroup = base.TenantGlobalCatalogSession.ResolveWellKnownGuid<ADGroup>(RoleGroup.OrganizationManagement_InitInfo.WellKnownGuid, OrganizationId.ForestWideOrgId.Equals(base.CurrentOrganizationId) ? this.ConfigurationSession.ConfigurationNamingContext : base.TenantGlobalCatalogSession.SessionSettings.CurrentOrganizationId.ConfigurationUnit);
					if (adgroup == null)
					{
						base.ThrowTerminatingError(new ManagementObjectNotFoundException(DirectoryStrings.ExceptionADTopologyCannotFindWellKnownExchangeGroup), (ErrorCategory)1001, RoleGroup.OrganizationManagement_InitInfo.WellKnownGuid);
					}
					list.Add(new SecurityPrincipalIdParameter(adgroup.DistinguishedName));
				}
				finally
				{
					base.TenantGlobalCatalogSession.UseGlobalCatalog = useGlobalCatalog;
					base.TenantGlobalCatalogSession.UseConfigNC = useConfigNC;
					base.TenantGlobalCatalogSession.SkipRangedAttributes = skipRangedAttributes;
				}
				ADObjectId adObjectId;
				if (base.TryGetExecutingUserId(out adObjectId))
				{
					list.Add(new SecurityPrincipalIdParameter(adObjectId));
				}
				this.ManagedBy = new MultiValuedProperty<SecurityPrincipalIdParameter>(list);
			}
			if (this.ManagedBy == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorManagedByCannotBeEmpty), (ErrorCategory)1000, null);
			}
			if (!flag || !DatacenterRegistry.IsForefrontForOffice())
			{
				MailboxTaskHelper.CheckAndResolveManagedBy<ADGroup>(this, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), ExchangeErrorCategory.Client, this.ManagedBy.ToArray(), out this.managedByRecipients);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000F4B40 File Offset: 0x000F2D40
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			base.CheckExclusiveParameters(NewRoleGroup.mutuallyExclusiveParameters);
			base.CheckExclusiveParameters(new object[]
			{
				RoleGroupParameters.ParameterMembers,
				RoleGroupParameters.ParameterLinkedForeignGroup
			});
			base.CheckExclusiveParameters(new object[]
			{
				RoleGroupParameters.ParameterMembers,
				RoleGroupParameters.ParameterLinkedPartnerGroupId
			});
			TaskLogger.LogExit();
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000F4BA4 File Offset: 0x000F2DA4
		protected override void PrepareRecipientObject(ADGroup group)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(group);
			group.GroupType = (GroupTypeFlags.Universal | GroupTypeFlags.SecurityEnabled);
			group[ADRecipientSchema.Description] = new MultiValuedProperty<string>(this.Description);
			if (string.Equals(this.Description, CoreStrings.MsoManagedTenantAdminGroupDescription, StringComparison.Ordinal))
			{
				group[ADGroupSchema.RoleGroupTypeId] = 23;
			}
			else if (string.Equals(this.Description, CoreStrings.MsoMailTenantAdminGroupDescription, StringComparison.Ordinal))
			{
				group[ADGroupSchema.RoleGroupTypeId] = 24;
			}
			else if (string.Equals(this.Description, CoreStrings.MsoManagedTenantHelpdeskGroupDescription, StringComparison.Ordinal))
			{
				group[ADGroupSchema.RoleGroupTypeId] = 25;
			}
			if (base.CurrentOrganizationId == OrganizationId.ForestWideOrgId)
			{
				ADObjectId adobjectId = RoleGroupCommon.RoleGroupContainerId(base.TenantGlobalCatalogSession, this.ConfigurationSession);
				group.SetId(adobjectId.GetChildId(this.Name));
			}
			MailboxTaskHelper.StampOnManagedBy(this.DataObject, this.managedByRecipients, new Task.ErrorLoggerDelegate(base.WriteError));
			this.DataObject.RecipientTypeDetails = RecipientTypeDetails.RoleGroup;
			MailboxTaskHelper.ValidateGroupManagedBy(base.TenantGlobalCatalogSession, group, this.managedByRecipients, RoleGroupCommon.OwnerRecipientTypeDetails, true, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError));
			if (string.IsNullOrEmpty(group.SamAccountName))
			{
				IRecipientSession[] recipientSessions = new IRecipientSession[]
				{
					base.RootOrgGlobalCatalogSession
				};
				if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ServiceAccountForest.Enabled && base.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
				{
					recipientSessions = new IRecipientSession[]
					{
						base.RootOrgGlobalCatalogSession,
						base.PartitionOrRootOrgGlobalCatalogSession
					};
				}
				group.SamAccountName = RecipientTaskHelper.GenerateUniqueSamAccountName(recipientSessions, group.Id.DomainId, group.Name, true, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), false);
			}
			else
			{
				RecipientTaskHelper.IsSamAccountNameUnique(group, group.SamAccountName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if ("crossforest" == base.ParameterSetName)
			{
				group.ForeignGroupSid = this.linkedGroupSid;
			}
			if ("linkedpartnergroup" == base.ParameterSetName)
			{
				group.LinkedPartnerGroupId = this.LinkedPartnerGroupId;
				group.LinkedPartnerOrganizationId = this.LinkedPartnerOrganizationId;
			}
			if (this.PartnerManaged.IsPresent)
			{
				group.RawCapabilities.Add(Capability.Partner_Managed);
			}
			if (base.Fields.IsChanged(RoleGroupParameters.ParameterMembers) && this.Members != null)
			{
				foreach (SecurityPrincipalIdParameter member in this.Members)
				{
					MailboxTaskHelper.ValidateAndAddMember(base.TenantGlobalCatalogSession, group, member, false, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
				}
			}
			MailboxTaskHelper.ValidateAddedMembers(base.TenantGlobalCatalogSession, group, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
			TaskLogger.LogExit();
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000F4EC8 File Offset: 0x000F30C8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADGroup result = (ADGroup)base.PrepareDataObject();
			if (!this.PartnerManaged.IsPresent)
			{
				SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			RoleAssigneeType roleAssigneeType = RoleAssigneeType.RoleGroup;
			if ("crossforest" == base.ParameterSetName)
			{
				roleAssigneeType = RoleAssigneeType.LinkedRoleGroup;
			}
			if (base.Fields.IsChanged("Roles") && this.Roles != null)
			{
				this.roles = new MultiValuedProperty<ExchangeRole>();
				this.roleAssignments = new List<ExchangeRoleAssignment>();
				foreach (RoleIdParameter roleIdParameter in this.Roles)
				{
					ExchangeRole item = (ExchangeRole)base.GetDataObject<ExchangeRole>(roleIdParameter, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorRoleNotFound(roleIdParameter.ToString())), new LocalizedString?(Strings.ErrorRoleNotUnique(roleIdParameter.ToString())));
					this.roles.Add(item);
				}
				this.ConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
				foreach (ExchangeRole role in this.roles)
				{
					bool flag = false;
					ExchangeRoleAssignment exchangeRoleAssignment = new ExchangeRoleAssignment();
					RoleHelper.PrepareNewRoleAssignmentWithUniqueNameAndDefaultScopes(null, exchangeRoleAssignment, role, this.DataObject.Id, this.DataObject.OrganizationId, roleAssigneeType, RoleAssignmentDelegationType.Regular, this.ConfigurationSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
					RoleHelper.AnalyzeAndStampCustomizedWriteScopes(this, exchangeRoleAssignment, role, this.ConfigurationSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ExchangeOrganizationalUnit>), new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ManagementScope>), ref flag, ref this.ou, ref this.customRecipientScope, ref this.customConfigScope);
					if (!flag && base.ExchangeRunspaceConfig != null)
					{
						RoleHelper.HierarchicalCheckForRoleAssignmentCreation(this, exchangeRoleAssignment, this.customRecipientScope, this.customConfigScope, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
					}
					this.roleAssignments.Add(exchangeRoleAssignment);
				}
			}
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000F50E8 File Offset: 0x000F32E8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			IConfigurationSession configurationSession = null;
			base.InternalProcessRecord();
			if (this.WellKnownObjectGuid != Guid.Empty || this.roleAssignments != null)
			{
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 676, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleGroup\\NewRoleGroup.cs");
				configurationSession.LinkResolutionServer = this.DataObject.OriginatingServer;
			}
			if (this.WellKnownObjectGuid != Guid.Empty)
			{
				try
				{
					RoleGroupCommon.StampWellKnownObjectGuid(configurationSession, this.DataObject.OrganizationId, this.DataObject.DistinguishedName, this.WellKnownObjectGuid);
				}
				catch (Exception)
				{
					this.DataObject.ExternalDirectoryObjectId = null;
					base.DataSession.Save(this.DataObject);
					base.DataSession.Delete(this.DataObject);
					throw;
				}
			}
			if (this.roleAssignments != null)
			{
				List<ExchangeRoleAssignment> list = new List<ExchangeRoleAssignment>();
				string id = string.Empty;
				try
				{
					foreach (ExchangeRoleAssignment exchangeRoleAssignment in this.roleAssignments)
					{
						exchangeRoleAssignment.User = this.DataObject.Id;
						id = exchangeRoleAssignment.Id.Name;
						configurationSession.Save(exchangeRoleAssignment);
						list.Add(exchangeRoleAssignment);
					}
				}
				catch (Exception)
				{
					this.WriteWarning(Strings.WarningCouldNotCreateRoleAssignment(id, this.Name));
					foreach (ExchangeRoleAssignment exchangeRoleAssignment2 in list)
					{
						base.WriteVerbose(Strings.VerboseRemovingRoleAssignment(exchangeRoleAssignment2.Id.ToString()));
						configurationSession.Delete(exchangeRoleAssignment2);
						base.WriteVerbose(Strings.VerboseRemovedRoleAssignment(exchangeRoleAssignment2.Id.ToString()));
					}
					base.WriteVerbose(Strings.VerboseRemovingRoleGroup(this.DataObject.Id.ToString()));
					base.DataSession.Delete(this.DataObject);
					throw;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000F5354 File Offset: 0x000F3554
		protected override void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			base.WriteVerbose(TaskVerboseStringHelper.GetReadObjectVerboseString(this.DataObject.Identity, base.DataSession, typeof(RoleGroup)));
			ADGroup adgroup = null;
			try
			{
				adgroup = (ADGroup)base.DataSession.Read<ADGroup>(this.DataObject.Identity);
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			if (adgroup == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.ResolveIdentityString(this.DataObject.Identity), typeof(RoleGroup).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, this.DataObject.Identity);
			}
			Result<ExchangeRoleAssignment>[] array = null;
			if (this.roleAssignments != null)
			{
				array = new Result<ExchangeRoleAssignment>[this.roleAssignments.Count];
				for (int i = 0; i < this.roleAssignments.Count; i++)
				{
					array[i] = new Result<ExchangeRoleAssignment>(this.roleAssignments[i], null);
				}
			}
			if (null != adgroup.ForeignGroupSid)
			{
				adgroup.LinkedGroup = SecurityPrincipalIdParameter.GetFriendlyUserName(adgroup.ForeignGroupSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				adgroup.ResetChangeTracking();
			}
			RoleGroup result = new RoleGroup(adgroup, array);
			base.WriteResult(result);
			TaskLogger.LogExit();
		}

		// Token: 0x04002639 RID: 9785
		private MultiValuedProperty<ADRecipient> managedByRecipients;

		// Token: 0x0400263A RID: 9786
		private MultiValuedProperty<ExchangeRole> roles;

		// Token: 0x0400263B RID: 9787
		private List<ExchangeRoleAssignment> roleAssignments;

		// Token: 0x0400263C RID: 9788
		private ExchangeOrganizationalUnit ou;

		// Token: 0x0400263D RID: 9789
		private ManagementScope customRecipientScope;

		// Token: 0x0400263E RID: 9790
		private ManagementScope customConfigScope;

		// Token: 0x0400263F RID: 9791
		private SecurityIdentifier linkedGroupSid;

		// Token: 0x04002640 RID: 9792
		private static string[] mutuallyExclusiveParameters = new string[]
		{
			RbacCommonParameters.ParameterCustomRecipientWriteScope,
			RbacCommonParameters.ParameterRecipientOrganizationalUnitScope
		};
	}
}
