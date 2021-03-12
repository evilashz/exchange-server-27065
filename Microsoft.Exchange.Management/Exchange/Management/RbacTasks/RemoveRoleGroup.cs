using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200066E RID: 1646
	[Cmdlet("Remove", "RoleGroup", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRoleGroup : RemoveRecipientObjectTask<RoleGroupIdParameter, ADGroup>
	{
		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x000F5F30 File Offset: 0x000F4130
		// (set) Token: 0x06003A35 RID: 14901 RVA: 0x000F5F38 File Offset: 0x000F4138
		[Parameter(Mandatory = false)]
		public new SwitchParameter ForReconciliation
		{
			get
			{
				return base.ForReconciliation;
			}
			set
			{
				base.ForReconciliation = value;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000F5F41 File Offset: 0x000F4141
		private ADObjectId RootOrgUSGContainerId
		{
			get
			{
				if (this.rootOrgUSGContainerId == null)
				{
					this.rootOrgUSGContainerId = RoleGroupCommon.GetRootOrgUsgContainerId(this.ConfigurationSession, base.ServerSettings, base.PartitionOrRootOrgGlobalCatalogSession, base.CurrentOrganizationId);
				}
				return this.rootOrgUSGContainerId;
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000F5F74 File Offset: 0x000F4174
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string managedBy = RoleGroupCommon.NamesFromObjects(this.roleGroup.ManagedBy);
				string roles = RoleGroupCommon.NamesFromObjects(this.roleGroup.Roles);
				string roleAssignments = RoleGroupCommon.NamesFromObjects(this.roleGroup.RoleAssignments);
				return Strings.ConfirmationMessageRemoveRoleGroup(this.Identity.ToString(), roles, managedBy, roleAssignments);
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000F5FC7 File Offset: 0x000F41C7
		internal new SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return new SwitchParameter(false);
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x000F5FCF File Offset: 0x000F41CF
		// (set) Token: 0x06003A3A RID: 14906 RVA: 0x000F6003 File Offset: 0x000F4203
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

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06003A3B RID: 14907 RVA: 0x000F601B File Offset: 0x000F421B
		// (set) Token: 0x06003A3C RID: 14908 RVA: 0x000F6041 File Offset: 0x000F4241
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

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x000F6059 File Offset: 0x000F4259
		// (set) Token: 0x06003A3E RID: 14910 RVA: 0x000F607F File Offset: 0x000F427F
		[Parameter(Mandatory = false)]
		public SwitchParameter RemoveWellKnownObjectGuid
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveWellKnownObjectGuid"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RemoveWellKnownObjectGuid"] = value;
			}
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000F6098 File Offset: 0x000F4298
		protected override IConfigDataProvider CreateSession()
		{
			this.writableConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 146, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleGroup\\RemoveRoleGroup.cs");
			return base.CreateSession();
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x000F60D4 File Offset: 0x000F42D4
		protected override void InternalValidate()
		{
			base.OptionalIdentityData.RootOrgDomainContainerId = this.RootOrgUSGContainerId;
			base.InternalValidate();
			if (!this.BypassSecurityGroupManagerCheck)
			{
				ADObjectId user;
				base.TryGetExecutingUserId(out user);
				RoleGroupCommon.ValidateExecutingUserHasGroupManagementRights(user, base.DataObject, base.ExchangeRunspaceConfig, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (RoleGroupCommon.IsPrecannedRoleGroup(base.DataObject, this.ConfigurationSession, new Guid[0]))
			{
				base.WriteError(new TaskInvalidOperationException(Strings.ErrorCannotDeletePrecannedRoleGroup(base.DataObject.Name)), ExchangeErrorCategory.Client, null);
			}
			RoleAssignmentsGlobalConstraints roleAssignmentsGlobalConstraints = new RoleAssignmentsGlobalConstraints(this.ConfigurationSession, base.TenantGlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError));
			roleAssignmentsGlobalConstraints.ValidateIsSafeToRemoveRoleGroup(base.DataObject, this.roleAssignmentResults, this);
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000F6198 File Offset: 0x000F4398
		protected override IConfigurable ResolveDataObject()
		{
			ADGroup adgroup = (ADGroup)base.ResolveDataObject();
			this.writableConfigSession = (IConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(this.writableConfigSession, adgroup.OrganizationId, base.OrgWideSessionSettings, true);
			Result<ExchangeRoleAssignment>[] array = this.writableConfigSession.FindRoleAssignmentsByUserIds(new ADObjectId[]
			{
				adgroup.Id
			}, false);
			this.roleAssignmentResults = array;
			this.roleGroup = new RoleGroup(adgroup, array);
			return adgroup;
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000F6208 File Offset: 0x000F4408
		protected override void InternalProcessRecord()
		{
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.roleAssignmentResults.Length != 0)
			{
				((IRecipientSession)base.DataSession).VerifyIsWithinScopes(base.DataObject, true);
			}
			bool flag = true;
			foreach (Result<ExchangeRoleAssignment> result in this.roleAssignmentResults)
			{
				string id = result.Data.Id.ToString();
				try
				{
					base.WriteVerbose(Strings.VerboseRemovingRoleAssignment(id));
					result.Data.Session.Delete(result.Data);
					base.WriteVerbose(Strings.VerboseRemovedRoleAssignment(id));
				}
				catch (Exception ex)
				{
					flag = false;
					if (!base.IsKnownException(ex))
					{
						throw;
					}
					this.WriteWarning(Strings.WarningCouldNotRemoveRoleAssignment(id, ex.Message));
				}
			}
			if (!flag)
			{
				base.WriteError(new TaskException(Strings.ErrorCouldNotRemoveRoleAssignments(base.DataObject.Id.ToString())), ExchangeErrorCategory.ServerOperation, base.DataObject);
			}
			if (this.RemoveWellKnownObjectGuid)
			{
				ExchangeConfigurationUnit exchangeConfigurationUnit = this.writableConfigSession.Read<ExchangeConfigurationUnit>(base.DataObject.OrganizationId.ConfigurationUnit);
				foreach (DNWithBinary dnwithBinary in exchangeConfigurationUnit.OtherWellKnownObjects)
				{
					if (dnwithBinary.DistinguishedName.Equals(base.DataObject.DistinguishedName, StringComparison.OrdinalIgnoreCase))
					{
						exchangeConfigurationUnit.OtherWellKnownObjects.Remove(dnwithBinary);
						this.writableConfigSession.Save(exchangeConfigurationUnit);
						break;
					}
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x04002649 RID: 9801
		private ADObjectId rootOrgUSGContainerId;

		// Token: 0x0400264A RID: 9802
		private IConfigurationSession writableConfigSession;

		// Token: 0x0400264B RID: 9803
		private Result<ExchangeRoleAssignment>[] roleAssignmentResults;

		// Token: 0x0400264C RID: 9804
		private RoleGroup roleGroup;
	}
}
