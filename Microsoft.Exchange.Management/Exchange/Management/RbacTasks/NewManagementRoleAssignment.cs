using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000662 RID: 1634
	[Cmdlet("New", "ManagementRoleAssignment", SupportsShouldProcess = true, DefaultParameterSetName = "User")]
	public sealed class NewManagementRoleAssignment : NewMultitenancySystemConfigurationObjectTask<ExchangeRoleAssignment>
	{
		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x0600394A RID: 14666 RVA: 0x000F0CA0 File Offset: 0x000EEEA0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewManagementRoleAssignment(this.DataObject.Name, this.DataObject.Role.ToString(), this.DataObject.User.ToString(), this.DataObject.RoleAssignmentDelegationType.ToString(), this.DataObject.RecipientWriteScope.ToString(), this.DataObject.ConfigWriteScope.ToString());
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x0600394B RID: 14667 RVA: 0x000F0D1C File Offset: 0x000EEF1C
		// (set) Token: 0x0600394C RID: 14668 RVA: 0x000F0D29 File Offset: 0x000EEF29
		[Parameter(Mandatory = false, Position = 0)]
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

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x000F0D37 File Offset: 0x000EEF37
		// (set) Token: 0x0600394E RID: 14670 RVA: 0x000F0D4E File Offset: 0x000EEF4E
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		public RoleIdParameter Role
		{
			get
			{
				return (RoleIdParameter)base.Fields[RbacCommonParameters.ParameterRole];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterRole] = value;
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000F0D61 File Offset: 0x000EEF61
		// (set) Token: 0x06003950 RID: 14672 RVA: 0x000F0D78 File Offset: 0x000EEF78
		[Parameter(Mandatory = true, ParameterSetName = "User")]
		public UserIdParameter User
		{
			get
			{
				return (UserIdParameter)base.Fields[RbacCommonParameters.ParameterUser];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterUser] = value;
			}
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x000F0D8B File Offset: 0x000EEF8B
		// (set) Token: 0x06003952 RID: 14674 RVA: 0x000F0DA2 File Offset: 0x000EEFA2
		[Parameter(Mandatory = true, ParameterSetName = "SecurityGroup")]
		public SecurityGroupIdParameter SecurityGroup
		{
			get
			{
				return (SecurityGroupIdParameter)base.Fields[RbacCommonParameters.ParameterSecurityGroup];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterSecurityGroup] = value;
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06003953 RID: 14675 RVA: 0x000F0DB5 File Offset: 0x000EEFB5
		// (set) Token: 0x06003954 RID: 14676 RVA: 0x000F0DCC File Offset: 0x000EEFCC
		[Parameter(Mandatory = true, ParameterSetName = "Policy")]
		public MailboxPolicyIdParameter Policy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields[RbacCommonParameters.ParameterPolicy];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterPolicy] = value;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000F0DDF File Offset: 0x000EEFDF
		// (set) Token: 0x06003956 RID: 14678 RVA: 0x000F0DF6 File Offset: 0x000EEFF6
		[Parameter(Mandatory = true, ParameterSetName = "Computer")]
		public ComputerIdParameter Computer
		{
			get
			{
				return (ComputerIdParameter)base.Fields[RbacCommonParameters.ParameterComputer];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterComputer] = value;
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000F0E09 File Offset: 0x000EF009
		// (set) Token: 0x06003958 RID: 14680 RVA: 0x000F0E2F File Offset: 0x000EF02F
		[Parameter(ParameterSetName = "User")]
		[Parameter(ParameterSetName = "SecurityGroup")]
		public SwitchParameter Delegating
		{
			get
			{
				return (SwitchParameter)(base.Fields[RbacCommonParameters.ParameterDelegating] ?? false);
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterDelegating] = value;
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06003959 RID: 14681 RVA: 0x000F0E47 File Offset: 0x000EF047
		// (set) Token: 0x0600395A RID: 14682 RVA: 0x000F0E68 File Offset: 0x000EF068
		[Parameter]
		public RecipientWriteScopeType RecipientRelativeWriteScope
		{
			get
			{
				return (RecipientWriteScopeType)(base.Fields[RbacCommonParameters.ParameterRecipientRelativeWriteScope] ?? -1);
			}
			set
			{
				base.VerifyValues<RecipientWriteScopeType>(RbacRoleAssignmentCommon.AllowedRecipientRelativeWriteScope, value);
				base.Fields[RbacCommonParameters.ParameterRecipientRelativeWriteScope] = value;
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x000F0E8C File Offset: 0x000EF08C
		// (set) Token: 0x0600395C RID: 14684 RVA: 0x000F0EA3 File Offset: 0x000EF0A3
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

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x000F0EB6 File Offset: 0x000EF0B6
		// (set) Token: 0x0600395E RID: 14686 RVA: 0x000F0ECD File Offset: 0x000EF0CD
		[ValidateNotNullOrEmpty]
		[Parameter]
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

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000F0EE0 File Offset: 0x000EF0E0
		// (set) Token: 0x06003960 RID: 14688 RVA: 0x000F0EF7 File Offset: 0x000EF0F7
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

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x000F0F0A File Offset: 0x000EF10A
		// (set) Token: 0x06003962 RID: 14690 RVA: 0x000F0F21 File Offset: 0x000EF121
		[ValidateNotNullOrEmpty]
		[Parameter]
		public ManagementScopeIdParameter ExclusiveRecipientWriteScope
		{
			get
			{
				return (ManagementScopeIdParameter)base.Fields["ExclusiveRecipientWriteScope"];
			}
			set
			{
				base.Fields["ExclusiveRecipientWriteScope"] = value;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06003963 RID: 14691 RVA: 0x000F0F34 File Offset: 0x000EF134
		// (set) Token: 0x06003964 RID: 14692 RVA: 0x000F0F4B File Offset: 0x000EF14B
		[Parameter]
		[ValidateNotNullOrEmpty]
		public ManagementScopeIdParameter ExclusiveConfigWriteScope
		{
			get
			{
				return (ManagementScopeIdParameter)base.Fields["ExclusiveConfigWriteScope"];
			}
			set
			{
				base.Fields["ExclusiveConfigWriteScope"] = value;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06003965 RID: 14693 RVA: 0x000F0F5E File Offset: 0x000EF15E
		// (set) Token: 0x06003966 RID: 14694 RVA: 0x000F0F84 File Offset: 0x000EF184
		[Parameter(Mandatory = false)]
		public SwitchParameter UnScopedTopLevel
		{
			get
			{
				return (SwitchParameter)(base.Fields["UnScopedTopLevel"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UnScopedTopLevel"] = value;
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x000F0F9C File Offset: 0x000EF19C
		// (set) Token: 0x06003968 RID: 14696 RVA: 0x000F0FC2 File Offset: 0x000EF1C2
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

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x000F0FDA File Offset: 0x000EF1DA
		// (set) Token: 0x0600396A RID: 14698 RVA: 0x000F0FE2 File Offset: 0x000EF1E2
		[Parameter(Mandatory = false)]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x0600396B RID: 14699 RVA: 0x000F0FEC File Offset: 0x000EF1EC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ExchangeOrganizationalUnit exchangeOrganizationalUnit = null;
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			this.ConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
			this.DataObject = (ExchangeRoleAssignment)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (!this.IgnoreDehydratedFlag)
			{
				SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			this.role = (ExchangeRole)base.GetDataObject<ExchangeRole>(this.Role, base.DataSession, null, new LocalizedString?(Strings.ErrorRoleNotFound(this.Role.ToString())), new LocalizedString?(Strings.ErrorRoleNotUnique(this.Role.ToString())));
			RoleHelper.VerifyNoScopeForUnScopedRole(base.Fields, this.role, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (this.role != null && this.role.IsDeprecated)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotCreateRoleAssignmentToADeprecatedRole(this.role.ToString())), ErrorCategory.InvalidOperation, null);
			}
			RoleAssigneeType roleAssigneeType;
			ADObject adobject;
			if (this.Policy != null)
			{
				RoleAssignmentPolicy roleAssignmentPolicy = (RoleAssignmentPolicy)base.GetDataObject<RoleAssignmentPolicy>(this.Policy, RecipientTaskHelper.GetTenantLocalConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId), null, new LocalizedString?(Strings.ErrorRBACPolicyNotFound(this.Policy.ToString())), new LocalizedString?(Strings.ErrorRBACPolicyNotUnique(this.Policy.ToString())));
				if (!this.role.IsEndUserRole)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorNonEndUserRoleCannoBeAssignedToPolicy(this.role.Name)), ErrorCategory.InvalidOperation, roleAssignmentPolicy.Id);
				}
				OrganizationId organizationId = OrganizationId.ForestWideOrgId;
				if (this.ConfigurationSession is ITenantConfigurationSession)
				{
					organizationId = TaskHelper.ResolveOrganizationId(this.role.Id, ExchangeRole.RdnContainer, (ITenantConfigurationSession)this.ConfigurationSession);
				}
				ADObjectId adobjectId;
				if (OrganizationId.ForestWideOrgId.Equals(organizationId))
				{
					adobjectId = this.ConfigurationSession.GetOrgContainerId();
				}
				else
				{
					adobjectId = organizationId.ConfigurationUnit;
				}
				if (!roleAssignmentPolicy.Id.IsDescendantOf(adobjectId))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorPolicyOutOfRoleScope(roleAssignmentPolicy.Id.ToString(), adobjectId.Name)), ErrorCategory.InvalidOperation, null);
				}
				roleAssigneeType = RoleAssigneeType.RoleAssignmentPolicy;
				adobject = roleAssignmentPolicy;
			}
			else
			{
				ADRecipient adrecipient = null;
				if (this.User != null)
				{
					adrecipient = (ADUser)base.GetDataObject<ADUser>(this.User, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorAssigneeUserNotFound(this.User.ToString())), new LocalizedString?(Strings.ErrorAssigneeUserNotUnique(this.User.ToString())));
				}
				else if (this.SecurityGroup != null)
				{
					adrecipient = (ADGroup)base.GetDataObject<ADGroup>(this.SecurityGroup, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorAssigneeSecurityGroupNotFound(this.SecurityGroup.ToString())), new LocalizedString?(Strings.ErrorAssigneeSecurityGroupNotUnique(this.SecurityGroup.ToString())));
				}
				else if (this.Computer != null)
				{
					adrecipient = (ADComputerRecipient)base.GetDataObject<ADComputerRecipient>(this.Computer, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorAssigneeComputerNotFound(this.Computer.ToString())), new LocalizedString?(Strings.ErrorAssigneeComputerNotUnique(this.Computer.ToString())));
				}
				RoleHelper.ValidateRoleAssignmentUser(adrecipient, new Task.TaskErrorLoggingDelegate(base.WriteError), false);
				roleAssigneeType = ExchangeRoleAssignment.RoleAssigneeTypeFromADRecipient(adrecipient);
				adobject = adrecipient;
			}
			((IDirectorySession)base.DataSession).LinkResolutionServer = adobject.OriginatingServer;
			RoleHelper.PrepareNewRoleAssignmentWithUniqueNameAndDefaultScopes(this.Name, this.DataObject, this.role, adobject.Id, adobject.OrganizationId, roleAssigneeType, this.Delegating.IsPresent ? RoleAssignmentDelegationType.Delegating : RoleAssignmentDelegationType.Regular, this.ConfigurationSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (this.role.IsUnscopedTopLevel && this.UnScopedTopLevel)
			{
				this.skipHRoleCheck = true;
				if (this.Delegating)
				{
					this.DataObject.RoleAssignmentDelegationType = RoleAssignmentDelegationType.DelegatingOrgWide;
				}
			}
			else
			{
				RoleHelper.AnalyzeAndStampCustomizedWriteScopes(this, this.DataObject, this.role, this.ConfigurationSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ExchangeOrganizationalUnit>), new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ManagementScope>), ref this.skipHRoleCheck, ref exchangeOrganizationalUnit, ref this.customRecipientScope, ref this.customConfigScope);
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000F143C File Offset: 0x000EF63C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			RbacRoleAssignmentCommon.CheckMutuallyExclusiveParameters(this);
			base.CheckExclusiveParameters(new object[]
			{
				RbacCommonParameters.ParameterDelegating,
				"ExclusiveRecipientWriteScope"
			});
			base.CheckExclusiveParameters(new object[]
			{
				RbacCommonParameters.ParameterDelegating,
				"ExclusiveConfigWriteScope"
			});
			TaskLogger.LogExit();
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000F149C File Offset: 0x000EF69C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.UnScopedTopLevel && !this.role.IsUnscopedTopLevel)
			{
				base.WriteError(new InvalidOperationException(Strings.ParameterAllowedOnlyForTopLevelRoleManipulation("UnScopedTopLevel", RoleType.UnScoped.ToString())), ErrorCategory.InvalidArgument, null);
			}
			if (!this.skipHRoleCheck && base.ExchangeRunspaceConfig != null)
			{
				RoleHelper.HierarchicalCheckForRoleAssignmentCreation(this, this.DataObject, this.customRecipientScope, this.customConfigScope, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.customConfigScope != null && ScopeRestrictionType.DatabaseScope == this.customConfigScope.ScopeRestrictionType)
			{
				this.WriteWarning(Strings.WarningRoleAssignmentWithDatabaseScopeApplicableOnlyInSP);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000F1560 File Offset: 0x000EF760
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000F15C4 File Offset: 0x000EF7C4
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			base.WriteResult(this.ConvertDataObjectToPresentationObject(dataObject));
			TaskLogger.LogExit();
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000F1600 File Offset: 0x000EF800
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			ExchangeRoleAssignment exchangeRoleAssignment = (ExchangeRoleAssignment)dataObject;
			ExchangeRoleAssignmentPresentation result = new ExchangeRoleAssignmentPresentation(exchangeRoleAssignment, exchangeRoleAssignment.User, AssignmentMethod.Direct);
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x04002615 RID: 9749
		private ManagementScope customRecipientScope;

		// Token: 0x04002616 RID: 9750
		private ManagementScope customConfigScope;

		// Token: 0x04002617 RID: 9751
		private bool skipHRoleCheck;

		// Token: 0x04002618 RID: 9752
		private ExchangeRole role;
	}
}
