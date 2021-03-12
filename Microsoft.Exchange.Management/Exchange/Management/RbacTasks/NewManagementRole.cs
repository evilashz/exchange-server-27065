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
	// Token: 0x02000675 RID: 1653
	[Cmdlet("New", "ManagementRole", SupportsShouldProcess = true, DefaultParameterSetName = "NewDerivedRoleParameterSet")]
	public sealed class NewManagementRole : NewMultitenancySystemConfigurationObjectTask<ExchangeRole>
	{
		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06003A85 RID: 14981 RVA: 0x000F7127 File Offset: 0x000F5327
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.DataObject.IsUnscopedTopLevel)
				{
					return Strings.ConfirmationMessageNewTopLevelManagementRole(this.DataObject.Name);
				}
				return Strings.ConfirmationMessageNewManagementRole(this.DataObject.Name, this.Parent.ToString());
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x000F7162 File Offset: 0x000F5362
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x000F7179 File Offset: 0x000F5379
		[Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "NewDerivedRoleParameterSet")]
		public RoleIdParameter Parent
		{
			get
			{
				return (RoleIdParameter)base.Fields["Parent"];
			}
			set
			{
				base.Fields["Parent"] = value;
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x000F718C File Offset: 0x000F538C
		// (set) Token: 0x06003A89 RID: 14985 RVA: 0x000F71B2 File Offset: 0x000F53B2
		[Parameter(Mandatory = true, ParameterSetName = "UnScopedTopLevelRoleParameterSet")]
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

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06003A8A RID: 14986 RVA: 0x000F71CA File Offset: 0x000F53CA
		// (set) Token: 0x06003A8B RID: 14987 RVA: 0x000F71E1 File Offset: 0x000F53E1
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

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000F71F4 File Offset: 0x000F53F4
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000F721A File Offset: 0x000F541A
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

		// Token: 0x06003A8E RID: 14990 RVA: 0x000F7234 File Offset: 0x000F5434
		protected override void InternalValidate()
		{
			base.InternalValidate();
			ExchangeRole[] array = this.ConfigurationSession.Find<ExchangeRole>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, base.Name), null, 1);
			if (array.Length > 0)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorRoleNameMustBeUnique(base.Name)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x000F728C File Offset: 0x000F548C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (ExchangeRole)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			this.DataObject.SetId(this.ConfigurationSession, ExchangeRole.RdnContainer, this.DataObject.Name);
			this.DataObject.Description = this.Description;
			if (base.ParameterSetName.Equals("UnScopedTopLevelRoleParameterSet"))
			{
				if (this.UnScopedTopLevel)
				{
					this.DataObject.RoleType = RoleType.UnScoped;
					this.DataObject.StampImplicitScopes();
					this.DataObject.StampIsEndUserRole();
				}
				else
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorNewRoleInvalidValueUnscopedParameter), ErrorCategory.InvalidOperation, null);
				}
			}
			else
			{
				ExchangeRole exchangeRole = (ExchangeRole)base.GetDataObject<ExchangeRole>(this.Parent, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorRoleNotFound(this.Parent.ToString())), new LocalizedString?(Strings.ErrorRoleNotUnique(this.Parent.ToString())));
				if (exchangeRole != null && exchangeRole.IsDeprecated)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCannotCreateARoleFromADeprecatedRole(exchangeRole.ToString())), ErrorCategory.InvalidOperation, null);
				}
				if (!this.DataObject.OrganizationId.Equals(exchangeRole.OrganizationId))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCannotCreateRoleAcrossOrganizations(base.CurrentOrganizationId.ToString(), exchangeRole.ToString(), exchangeRole.OrganizationId.ToString())), ErrorCategory.InvalidOperation, null);
				}
				this.DataObject.SetId(exchangeRole.Id.GetChildId(this.DataObject.Name));
				this.DataObject.RoleType = exchangeRole.RoleType;
				this.DataObject[ExchangeRoleSchema.RoleFlags] = exchangeRole[ExchangeRoleSchema.RoleFlags];
				this.DataObject.RoleEntries = exchangeRole.RoleEntries;
				MultiValuedProperty<RoleEntry> multiValuedProperty = (MultiValuedProperty<RoleEntry>)exchangeRole[ExchangeRoleSchema.InternalDownlevelRoleEntries];
				if (multiValuedProperty.Count > 0)
				{
					this.DataObject[ExchangeRoleSchema.InternalDownlevelRoleEntries] = multiValuedProperty;
					this.DataObject[ExchangeRoleSchema.RoleFlags] = exchangeRole[ExchangeRoleSchema.RoleFlags];
				}
				if (!base.CurrentTaskContext.CanBypassRBACScope && !RoleHelper.HasDelegatingHierarchicalRoleAssignmentWithoutScopeRestriction(base.ExecutingUserOrganizationId, base.ExchangeRunspaceConfig.RoleAssignments, exchangeRole.Id))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorNewRoleNeedHierarchicalRoleAssignmentWithoutScopeRestriction(exchangeRole.ToString())), ErrorCategory.InvalidOperation, null);
				}
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x000F7518 File Offset: 0x000F5718
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			if (this.UnScopedTopLevel)
			{
				try
				{
					if (base.ExecutingUserOrganizationId.Equals(this.DataObject.OrganizationId))
					{
						ADObjectId id;
						RoleAssigneeType roleAssigneeType;
						if (base.TryGetExecutingUserId(out id))
						{
							roleAssigneeType = RoleAssigneeType.User;
						}
						else
						{
							roleAssigneeType = RoleAssigneeType.RoleGroup;
							bool useGlobalCatalog = base.TenantGlobalCatalogSession.UseGlobalCatalog;
							bool useConfigNC = base.TenantGlobalCatalogSession.UseConfigNC;
							bool skipRangedAttributes = base.TenantGlobalCatalogSession.SkipRangedAttributes;
							ADGroup adgroup;
							try
							{
								base.TenantGlobalCatalogSession.UseGlobalCatalog = true;
								base.TenantGlobalCatalogSession.UseConfigNC = false;
								base.TenantGlobalCatalogSession.SkipRangedAttributes = true;
								adgroup = base.TenantGlobalCatalogSession.ResolveWellKnownGuid<ADGroup>(RoleGroup.OrganizationManagement_InitInfo.WellKnownGuid, OrganizationId.ForestWideOrgId.Equals(base.CurrentOrganizationId) ? this.ConfigurationSession.ConfigurationNamingContext : base.TenantGlobalCatalogSession.SessionSettings.CurrentOrganizationId.ConfigurationUnit);
							}
							finally
							{
								base.TenantGlobalCatalogSession.UseGlobalCatalog = useGlobalCatalog;
								base.TenantGlobalCatalogSession.UseConfigNC = useConfigNC;
								base.TenantGlobalCatalogSession.SkipRangedAttributes = skipRangedAttributes;
							}
							if (adgroup != null)
							{
								id = adgroup.Id;
							}
							else
							{
								base.ThrowTerminatingError(new ManagementObjectNotFoundException(DirectoryStrings.ExceptionADTopologyCannotFindWellKnownExchangeGroup), (ErrorCategory)1001, RoleGroup.OrganizationManagement_InitInfo.WellKnownGuid);
							}
						}
						RoleHelper.CreateRoleAssignment(this.DataObject, id, base.ExecutingUserOrganizationId, roleAssigneeType, this.DataObject.OriginatingServer, RoleAssignmentDelegationType.DelegatingOrgWide, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, base.DataSession as IConfigurationSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
					}
				}
				catch (Exception)
				{
					this.WriteWarning(Strings.WarningFailedToCreateAssignmentForNewRole(this.DataObject.Id.ToString()));
					base.DataSession.Delete(this.DataObject);
					throw;
				}
				if (base.ExchangeRunspaceConfig != null)
				{
					base.ExchangeRunspaceConfig.LoadRoleCmdletInfo();
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002661 RID: 9825
		private const string NewDerivedRoleParameterSet = "NewDerivedRoleParameterSet";

		// Token: 0x04002662 RID: 9826
		private const string UnScopedTopLevelRoleParameterSet = "UnScopedTopLevelRoleParameterSet";
	}
}
