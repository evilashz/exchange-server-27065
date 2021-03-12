using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.RbacTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200044F RID: 1103
	[Cmdlet("New", "RoleAssignmentPolicy", SupportsShouldProcess = true)]
	public sealed class NewRoleAssignmentPolicy : NewMailboxPolicyBase<RoleAssignmentPolicy>
	{
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0009A6B2 File Offset: 0x000988B2
		// (set) Token: 0x06002703 RID: 9987 RVA: 0x0009A6C9 File Offset: 0x000988C9
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

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x0009A6DC File Offset: 0x000988DC
		// (set) Token: 0x06002705 RID: 9989 RVA: 0x0009A6EE File Offset: 0x000988EE
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefault
		{
			get
			{
				return this.DataObject.IsDefault;
			}
			set
			{
				this.DataObject.IsDefault = value;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x0009A701 File Offset: 0x00098901
		// (set) Token: 0x06002707 RID: 9991 RVA: 0x0009A718 File Offset: 0x00098918
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

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x0009A72C File Offset: 0x0009892C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string text = RoleGroupCommon.NamesFromObjects(this.roles);
				return Strings.ConfirmationMessageNewRBACDefaultPolicy(base.Name, text);
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x0009A751 File Offset: 0x00098951
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x0009A754 File Offset: 0x00098954
		private bool UpdateExistingDefaultPolicies
		{
			get
			{
				return this.existingDefaultPolicies != null && this.existingDefaultPolicies.Count > 0;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x0009A76E File Offset: 0x0009896E
		// (set) Token: 0x0600270C RID: 9996 RVA: 0x0009A776 File Offset: 0x00098976
		[Parameter(Mandatory = false)]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x0600270E RID: 9998 RVA: 0x0009A788 File Offset: 0x00098988
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (RoleAssignmentPolicy)base.PrepareDataObject();
			this.DataObject.Description = this.Description;
			if (!base.HasErrors)
			{
				ADObjectId orgContainerId = ((IConfigurationSession)base.DataSession).GetOrgContainerId();
				ADObjectId descendantId = orgContainerId.GetDescendantId(RoleAssignmentPolicy.RdnContainer);
				this.DataObject.SetId(descendantId.GetChildId(base.Name));
				this.PrepareRolesAndRoleAssignments();
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0009A80C File Offset: 0x00098A0C
		private void PrepareRolesAndRoleAssignments()
		{
			RoleAssigneeType assigneeType = RoleAssigneeType.RoleAssignmentPolicy;
			if (base.Fields.IsChanged("Roles") && this.Roles != null)
			{
				this.roles = new MultiValuedProperty<ExchangeRole>();
				this.roleAssignments = new List<ExchangeRoleAssignment>();
				this.PrepareRoles();
				this.PrepareRoleAssignments(assigneeType);
			}
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x0009A858 File Offset: 0x00098A58
		private void PrepareRoles()
		{
			bool flag = false;
			foreach (RoleIdParameter roleIdParameter in this.Roles)
			{
				ExchangeRole exchangeRole = (ExchangeRole)base.GetDataObject<ExchangeRole>(roleIdParameter, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorRoleNotFound(roleIdParameter.ToString())), new LocalizedString?(Strings.ErrorRoleNotUnique(roleIdParameter.ToString())));
				if (exchangeRole.RoleType == RoleType.MyBaseOptions)
				{
					flag = true;
				}
				this.roles.Add(exchangeRole);
			}
			if (!flag)
			{
				this.WriteWarning(Strings.WarningNoMyBaseOptionsRole(RoleType.MyBaseOptions.ToString()));
			}
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x0009A8EC File Offset: 0x00098AEC
		private void PrepareRoleAssignments(RoleAssigneeType assigneeType)
		{
			foreach (ExchangeRole role in this.roles)
			{
				bool flag = false;
				ExchangeRoleAssignment exchangeRoleAssignment = new ExchangeRoleAssignment();
				RoleHelper.PrepareNewRoleAssignmentWithUniqueNameAndDefaultScopes(null, exchangeRoleAssignment, role, this.DataObject.Id, this.DataObject.OrganizationId, assigneeType, RoleAssignmentDelegationType.Regular, this.ConfigurationSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
				RoleHelper.AnalyzeAndStampCustomizedWriteScopes(this, exchangeRoleAssignment, role, this.ConfigurationSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ExchangeOrganizationalUnit>), new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ManagementScope>), ref flag, ref this.ou, ref this.customRecipientScope, ref this.customConfigScope);
				if (!flag && base.ExchangeRunspaceConfig != null)
				{
					RoleHelper.HierarchicalCheckForRoleAssignmentCreation(this, exchangeRoleAssignment, this.customRecipientScope, this.customConfigScope, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				this.roleAssignments.Add(exchangeRoleAssignment);
			}
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0009AA00 File Offset: 0x00098C00
		protected override void InternalValidate()
		{
			base.InternalValidate();
			IList<RoleAssignmentPolicy> policies = RoleAssignmentPolicyHelper.GetPolicies((IConfigurationSession)base.DataSession, null);
			this.CheckFirstPolicyIsDefault(policies);
			this.CheckForExistingDefaultPolicies(policies);
			this.CheckForAdminRoles();
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0009AA3C File Offset: 0x00098C3C
		private void CheckForAdminRoles()
		{
			if (this.roles == null)
			{
				return;
			}
			foreach (ExchangeRole exchangeRole in this.roles)
			{
				if (!exchangeRole.IsEndUserRole)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorNonEndUserRoleCannoBeAssignedToPolicy(exchangeRole.Name)), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0009AABC File Offset: 0x00098CBC
		private void CheckForExistingDefaultPolicies(IList<RoleAssignmentPolicy> allPolicies)
		{
			if (this.DataObject.IsDefault)
			{
				this.existingDefaultPolicies = new List<RoleAssignmentPolicy>();
				foreach (RoleAssignmentPolicy roleAssignmentPolicy in allPolicies)
				{
					if (roleAssignmentPolicy.IsDefault)
					{
						this.existingDefaultPolicies.Add(roleAssignmentPolicy);
					}
				}
			}
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0009AB2C File Offset: 0x00098D2C
		private void CheckFirstPolicyIsDefault(IList<RoleAssignmentPolicy> allPolicies)
		{
			if (allPolicies.Count == 0)
			{
				if (!this.DataObject.IsDefault && this.DataObject.IsModified(RoleAssignmentPolicySchema.IsDefault))
				{
					this.WriteWarning(Strings.FirstRoleAssignmentPolicyMustBeDefault);
				}
				this.DataObject.IsDefault = true;
			}
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x0009AB6C File Offset: 0x00098D6C
		protected override void InternalProcessRecord()
		{
			if (this.UpdateExistingDefaultPolicies && !base.ShouldContinue(Strings.ConfirmationMessageSwitchMailboxPolicy("RoleAssignmentPolicy", this.DataObject.Name)))
			{
				return;
			}
			base.InternalProcessRecord();
			if (this.UpdateExistingDefaultPolicies)
			{
				try
				{
					RoleAssignmentPolicyHelper.ClearIsDefaultOnPolicies((IConfigurationSession)base.DataSession, this.existingDefaultPolicies);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
			this.ProcessRoleAssignments();
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0009ABE8 File Offset: 0x00098DE8
		private void ProcessRoleAssignments()
		{
			if (this.roleAssignments == null)
			{
				return;
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 359, "ProcessRoleAssignments", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxPolicies\\RoleAssignmentPolicyTasks.cs");
			tenantOrTopologyConfigurationSession.LinkResolutionServer = this.DataObject.OriginatingServer;
			List<ExchangeRoleAssignment> createdRoleAssignments = new List<ExchangeRoleAssignment>();
			string empty = string.Empty;
			try
			{
				this.WriteRoleAssignments(tenantOrTopologyConfigurationSession, createdRoleAssignments, ref empty);
			}
			catch (Exception)
			{
				this.WriteWarning(Strings.WarningCouldNotCreateRoleAssignment(empty, base.Name));
				this.RollbackChanges(tenantOrTopologyConfigurationSession, createdRoleAssignments);
				throw;
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x0009AC78 File Offset: 0x00098E78
		private void WriteRoleAssignments(IConfigurationSession writableConfigSession, List<ExchangeRoleAssignment> createdRoleAssignments, ref string currentRoleAssignmentName)
		{
			foreach (ExchangeRoleAssignment exchangeRoleAssignment in this.roleAssignments)
			{
				currentRoleAssignmentName = exchangeRoleAssignment.Id.Name;
				writableConfigSession.Save(exchangeRoleAssignment);
				createdRoleAssignments.Add(exchangeRoleAssignment);
			}
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x0009ACE0 File Offset: 0x00098EE0
		private void RollbackChanges(IConfigurationSession writableConfigSession, List<ExchangeRoleAssignment> createdRoleAssignments)
		{
			foreach (ExchangeRoleAssignment exchangeRoleAssignment in createdRoleAssignments)
			{
				base.WriteVerbose(Strings.VerboseRemovingRoleAssignment(exchangeRoleAssignment.Id.ToString()));
				writableConfigSession.Delete(exchangeRoleAssignment);
				base.WriteVerbose(Strings.VerboseRemovedRoleAssignment(exchangeRoleAssignment.Id.ToString()));
			}
			base.WriteVerbose(Strings.VerboseRemovingRoleAssignmentPolicy(this.DataObject.Id.ToString()));
			base.DataSession.Delete(this.DataObject);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0009AD88 File Offset: 0x00098F88
		protected override void WriteResult(IConfigurable dataObject)
		{
			RoleAssignmentPolicy roleAssignmentPolicy = (RoleAssignmentPolicy)dataObject;
			roleAssignmentPolicy.PopulateRoles(this.FetchRoleAssignments());
			base.WriteResult(roleAssignmentPolicy);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0009ADB0 File Offset: 0x00098FB0
		private Result<ExchangeRoleAssignment>[] FetchRoleAssignments()
		{
			Result<ExchangeRoleAssignment>[] array = null;
			if (this.roleAssignments != null)
			{
				array = new Result<ExchangeRoleAssignment>[this.roleAssignments.Count];
				for (int i = 0; i < this.roleAssignments.Count; i++)
				{
					array[i] = new Result<ExchangeRoleAssignment>(this.roleAssignments[i], null);
				}
			}
			return array;
		}

		// Token: 0x04001D92 RID: 7570
		private MultiValuedProperty<ExchangeRole> roles;

		// Token: 0x04001D93 RID: 7571
		private List<ExchangeRoleAssignment> roleAssignments;

		// Token: 0x04001D94 RID: 7572
		private ExchangeOrganizationalUnit ou;

		// Token: 0x04001D95 RID: 7573
		private ManagementScope customRecipientScope;

		// Token: 0x04001D96 RID: 7574
		private ManagementScope customConfigScope;
	}
}
