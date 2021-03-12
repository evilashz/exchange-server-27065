using System;
using System.Management.Automation;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200066D RID: 1645
	[Cmdlet("Set", "RoleGroup", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRoleGroup : SetRecipientObjectTask<RoleGroupIdParameter, RoleGroup, ADGroup>
	{
		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x000F57D0 File Offset: 0x000F39D0
		private ADObjectId RootOrgUSGContainerId
		{
			get
			{
				if (this.rootOrgUSGContainerId == null)
				{
					IRecipientSession partitionOrRootOrgGlobalCatalogSession = base.PartitionOrRootOrgGlobalCatalogSession;
					this.rootOrgUSGContainerId = RoleGroupCommon.RoleGroupContainerId(DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, partitionOrRootOrgGlobalCatalogSession.ConsistencyMode, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionOrRootOrgGlobalCatalogSession.SessionSettings.PartitionId), 53, "RootOrgUSGContainerId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleGroup\\SetRoleGroup.cs"), this.ConfigurationSession);
				}
				return this.rootOrgUSGContainerId;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06003A13 RID: 14867 RVA: 0x000F5830 File Offset: 0x000F3A30
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string managedBy = RoleGroupCommon.NamesFromObjects(base.DynamicParametersInstance.ManagedBy);
				string roles = RoleGroupCommon.NamesFromObjects(this.roleGroup.Roles);
				return Strings.ConfirmationMessageSetRoleGroup(this.Identity.ToString(), roles, managedBy);
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x000F5871 File Offset: 0x000F3A71
		// (set) Token: 0x06003A15 RID: 14869 RVA: 0x000F5888 File Offset: 0x000F3A88
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override RoleGroupIdParameter Identity
		{
			get
			{
				return (RoleGroupIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000F589B File Offset: 0x000F3A9B
		// (set) Token: 0x06003A17 RID: 14871 RVA: 0x000F58B2 File Offset: 0x000F3AB2
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<SecurityPrincipalIdParameter> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<SecurityPrincipalIdParameter>)base.Fields[WindowsGroupSchema.ManagedBy];
			}
			set
			{
				base.Fields[WindowsGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06003A18 RID: 14872 RVA: 0x000F58C5 File Offset: 0x000F3AC5
		// (set) Token: 0x06003A19 RID: 14873 RVA: 0x000F58DC File Offset: 0x000F3ADC
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)base.Fields["DisplayName"];
			}
			set
			{
				base.Fields["DisplayName"] = value;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06003A1A RID: 14874 RVA: 0x000F58EF File Offset: 0x000F3AEF
		// (set) Token: 0x06003A1B RID: 14875 RVA: 0x000F5923 File Offset: 0x000F3B23
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassSecurityGroupManagerCheck
		{
			get
			{
				if (DatacenterRegistry.IsForefrontForOffice())
				{
					return true;
				}
				return (SwitchParameter)(base.Fields["BypassSecurityGroupManagerCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassSecurityGroupManagerCheck"] = value;
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000F593B File Offset: 0x000F3B3B
		// (set) Token: 0x06003A1D RID: 14877 RVA: 0x000F5952 File Offset: 0x000F3B52
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeDatacenterCrossForestParameterSet")]
		[ValidateNotNull]
		public SecurityIdentifier LinkedForeignGroupSid
		{
			get
			{
				return (SecurityIdentifier)base.Fields[RoleGroupParameters.ParameterLinkedForeignGroupSid];
			}
			set
			{
				base.Fields[RoleGroupParameters.ParameterLinkedForeignGroupSid] = value;
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000F5965 File Offset: 0x000F3B65
		// (set) Token: 0x06003A1F RID: 14879 RVA: 0x000F597C File Offset: 0x000F3B7C
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

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06003A20 RID: 14880 RVA: 0x000F598F File Offset: 0x000F3B8F
		// (set) Token: 0x06003A21 RID: 14881 RVA: 0x000F59A6 File Offset: 0x000F3BA6
		[Parameter(Mandatory = true, ParameterSetName = "crossforest")]
		[ValidateNotNull]
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

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06003A22 RID: 14882 RVA: 0x000F59B9 File Offset: 0x000F3BB9
		// (set) Token: 0x06003A23 RID: 14883 RVA: 0x000F59D0 File Offset: 0x000F3BD0
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

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x000F59E3 File Offset: 0x000F3BE3
		// (set) Token: 0x06003A25 RID: 14885 RVA: 0x000F59FA File Offset: 0x000F3BFA
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

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000F5A0D File Offset: 0x000F3C0D
		// (set) Token: 0x06003A27 RID: 14887 RVA: 0x000F5A33 File Offset: 0x000F3C33
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

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000F5A4B File Offset: 0x000F3C4B
		// (set) Token: 0x06003A29 RID: 14889 RVA: 0x000F5A7A File Offset: 0x000F3C7A
		[Parameter(Mandatory = false)]
		public Guid ExternalDirectoryObjectId
		{
			get
			{
				if (base.Fields["ExternalDirectoryObjectId"] == null)
				{
					return Guid.Empty;
				}
				return (Guid)base.Fields["ExternalDirectoryObjectId"];
			}
			set
			{
				base.Fields["ExternalDirectoryObjectId"] = value;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000F5A92 File Offset: 0x000F3C92
		// (set) Token: 0x06003A2B RID: 14891 RVA: 0x000F5AC1 File Offset: 0x000F3CC1
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

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000F5AD9 File Offset: 0x000F3CD9
		internal new SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return new SwitchParameter(false);
			}
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000F5AE4 File Offset: 0x000F3CE4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
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
			TaskLogger.LogExit();
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000F5BD0 File Offset: 0x000F3DD0
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			if (base.Fields.IsModified(WindowsGroupSchema.ManagedBy))
			{
				base.DynamicParametersInstance.ManagedBy = base.ResolveIdParameterCollection<SecurityPrincipalIdParameter, ADRecipient, ADObjectId>(this.ManagedBy, base.TenantGlobalCatalogSession, null, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.ErrorRecipientNotFound), new Func<IIdentityParameter, LocalizedString>(Strings.ErrorRecipientNotUnique), null, null);
			}
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000F5C30 File Offset: 0x000F3E30
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADGroup adgroup = (ADGroup)base.PrepareDataObject();
			if (!this.BypassSecurityGroupManagerCheck)
			{
				ADObjectId user;
				base.TryGetExecutingUserId(out user);
				RoleGroupCommon.ValidateExecutingUserHasGroupManagementRights(user, adgroup, base.ExchangeRunspaceConfig, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if ("crossforest" == base.ParameterSetName && adgroup.RoleGroupType == RoleGroupType.Standard)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorCannotChangeRoleGroupType), (ErrorCategory)1000, null);
			}
			if ("ExchangeDatacenterCrossForestParameterSet" == base.ParameterSetName)
			{
				if (Datacenter.ExchangeSku.ExchangeDatacenter != Datacenter.GetExchangeSku() && Datacenter.ExchangeSku.DatacenterDedicated != Datacenter.GetExchangeSku())
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorLinkedSidParameterNotAllowed(RoleGroupParameters.ParameterLinkedForeignGroupSid)), (ErrorCategory)1000, null);
				}
				this.linkedGroupSid = this.LinkedForeignGroupSid;
			}
			if ("crossforest" == base.ParameterSetName || "ExchangeDatacenterCrossForestParameterSet" == base.ParameterSetName)
			{
				adgroup.ForeignGroupSid = this.linkedGroupSid;
				if (adgroup.Members.Count > 0)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorLinkedRoleGroupCannotHaveMembers), (ErrorCategory)1000, null);
				}
			}
			if (base.Fields.IsModified("DisplayName"))
			{
				adgroup[RoleGroupSchema.DisplayName] = this.DisplayName;
			}
			this.roleGroup = RoleGroupCommon.PopulateRoleAssignmentsAndConvert(adgroup, this.ConfigurationSession);
			if (base.Fields.IsModified("Description"))
			{
				adgroup[ADGroupSchema.RoleGroupDescription] = (string.IsNullOrEmpty(this.Description) ? null : this.Description);
			}
			if (this.ExternalDirectoryObjectId != Guid.Empty)
			{
				adgroup.ExternalDirectoryObjectId = this.ExternalDirectoryObjectId.ToString();
			}
			TaskLogger.LogExit();
			return adgroup;
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000F5DEC File Offset: 0x000F3FEC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.OptionalIdentityData.RootOrgDomainContainerId = this.RootOrgUSGContainerId;
			base.InternalValidate();
			MailboxTaskHelper.ValidateGroupManagedBy(base.TenantGlobalCatalogSession, this.DataObject, null, RoleGroupCommon.OwnerRecipientTypeDetails, true, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000F5E4C File Offset: 0x000F404C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			if (this.WellKnownObjectGuid != Guid.Empty)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 424, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleGroup\\SetRoleGroup.cs");
				RoleGroupCommon.StampWellKnownObjectGuid(tenantOrTopologyConfigurationSession, this.DataObject.OrganizationId, this.DataObject.DistinguishedName, this.WellKnownObjectGuid);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000F5F07 File Offset: 0x000F4107
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject != this.roleGroup.DataObject)
			{
				throw new ArgumentException("dataObject");
			}
			return this.roleGroup;
		}

		// Token: 0x04002646 RID: 9798
		private ADObjectId rootOrgUSGContainerId;

		// Token: 0x04002647 RID: 9799
		private RoleGroup roleGroup;

		// Token: 0x04002648 RID: 9800
		private SecurityIdentifier linkedGroupSid;
	}
}
