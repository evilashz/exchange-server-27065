using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000669 RID: 1641
	[Cmdlet("Move", "ManagementRoleAssignment", SupportsShouldProcess = true)]
	public sealed class MoveManagementRoleAssignment : SystemConfigurationObjectActionTask<RoleAssignmentIdParameter, ExchangeRoleAssignment>
	{
		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x000F3B98 File Offset: 0x000F1D98
		// (set) Token: 0x060039BD RID: 14781 RVA: 0x000F3BA0 File Offset: 0x000F1DA0
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override RoleAssignmentIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x000F3BA9 File Offset: 0x000F1DA9
		// (set) Token: 0x060039BF RID: 14783 RVA: 0x000F3BC0 File Offset: 0x000F1DC0
		[Parameter(Mandatory = true)]
		public SecurityPrincipalIdParameter User
		{
			get
			{
				return (SecurityPrincipalIdParameter)base.Fields[RbacCommonParameters.ParameterUser];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterUser] = value;
			}
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000F3BD4 File Offset: 0x000F1DD4
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (ExchangeRoleAssignment)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.User, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorUserOrSecurityGroupNotFound(this.User.ToString())), new LocalizedString?(Strings.ErrorUserOrSecurityGroupNotUnique(this.User.ToString())));
			RoleHelper.ValidateRoleAssignmentUser(adrecipient, new Task.TaskErrorLoggingDelegate(base.WriteError), false);
			this.originalUserId = this.DataObject.User;
			this.DataObject.User = adrecipient.Id;
			this.DataObject.RoleAssigneeType = ExchangeRoleAssignment.RoleAssigneeTypeFromADRecipient(adrecipient);
			((IDirectorySession)base.DataSession).LinkResolutionServer = adrecipient.OriginatingServer;
			if (!adrecipient.OrganizationId.Equals(OrganizationId.ForestWideOrgId) && !adrecipient.OrganizationId.Equals(this.DataObject.OrganizationId) && (OrganizationId.ForestWideOrgId.Equals(this.DataObject.OrganizationId) || !this.DataObject.OrganizationId.OrganizationalUnit.IsDescendantOf(adrecipient.OrganizationId.OrganizationalUnit)))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorOrgUserBeAssignedToParentOrg(this.User.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Id);
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000F3D3A File Offset: 0x000F1F3A
		protected override void InternalProcessRecord()
		{
			if (this.IsNonDeprecatedRole())
			{
				this.CloneRoleAssignment(this.DataObject);
				base.InternalProcessRecord();
			}
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000F3D58 File Offset: 0x000F1F58
		private void CloneRoleAssignment(ExchangeRoleAssignment templateAssignment)
		{
			ExchangeRoleAssignment exchangeRoleAssignment = new ExchangeRoleAssignment();
			exchangeRoleAssignment.ProvisionalClone(templateAssignment);
			exchangeRoleAssignment.User = this.originalUserId;
			string unescapedCommonName = templateAssignment.Name.Substring(0, Math.Min(templateAssignment.Name.Length, 55)) + "_" + (Environment.TickCount % 1000).ToString("0000");
			exchangeRoleAssignment.SetId(templateAssignment.Id.Parent.GetChildId(unescapedCommonName));
			base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(exchangeRoleAssignment, templateAssignment.Session, typeof(ExchangeRoleAssignment)));
			templateAssignment.Session.Save(exchangeRoleAssignment);
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x000F3E00 File Offset: 0x000F2000
		private bool IsNonDeprecatedRole()
		{
			ExchangeRole exchangeRole = (ExchangeRole)base.GetDataObject<ExchangeRole>(new RoleIdParameter(this.DataObject.Role), base.DataSession, null, new LocalizedString?(Strings.ErrorRoleNotFound(this.DataObject.Role.ToString())), new LocalizedString?(Strings.ErrorRoleNotUnique(this.DataObject.Role.ToString())));
			if (exchangeRole != null && exchangeRole.IsDeprecated)
			{
				this.WriteWarning(Strings.ErrorCannotMoveRoleAssignmentOfDeprecatedRole(exchangeRole.ToString()));
				return false;
			}
			return true;
		}

		// Token: 0x04002636 RID: 9782
		private ADObjectId originalUserId;
	}
}
