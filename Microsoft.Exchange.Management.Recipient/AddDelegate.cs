using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200009A RID: 154
	[Cmdlet("Add", "ExchangeAdministrator", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class AddDelegate : DelegateTask
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002BF48 File Offset: 0x0002A148
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddDelegate(this.Identity.ToString(), this.DataObject.Id.ToString(), base.Role.ToString());
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002BF7C File Offset: 0x0002A17C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.Role == DelegateRoleType.ServerAdmin && this.server.IsEdgeServer && !ADSession.IsBoundToAdam)
			{
				base.WriteError(new CannotDelegateEdgeServerAdminException(base.Scope), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = false;
			if (this.DataObject.Guid == this.user.Guid)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorAddGroupToItself), ErrorCategory.InvalidData, this.Identity);
			}
			foreach (ADObjectId adobjectId in this.DataObject.Members)
			{
				if (this.user.Guid == adobjectId.ObjectGuid)
				{
					if (base.Role == DelegateRoleType.ServerAdmin)
					{
						flag = true;
						break;
					}
					base.WriteError(new RecipientTaskException(Strings.ErrorUserAlreadyDelegate((string)this.Identity, this.DataObject.Id.DistinguishedName)), ErrorCategory.InvalidData, this.DataObject.Identity);
				}
			}
			if (base.Role == DelegateRoleType.ServerAdmin)
			{
				this.GrantServerAdminRole();
			}
			else if (!flag)
			{
				this.DataObject.Members.Add(this.user.Id);
				try
				{
					base.InternalProcessRecord();
				}
				catch (ADObjectEntryAlreadyExistsException)
				{
				}
			}
			this.WriteResult(this.user.Id, base.Role, base.Scope);
			TaskLogger.LogExit();
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002C11C File Offset: 0x0002A31C
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 128, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\permission\\AddDelegate.cs");
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002C14C File Offset: 0x0002A34C
		private void WriteResult(ADObjectId member, DelegateRoleType role, string scope)
		{
			string text = Strings.OrganizationWide.ToString();
			DelegateUser sendToPipeline = new DelegateUser(member.ToString(), role, (role != DelegateRoleType.ServerAdmin) ? text : scope);
			base.WriteObject(sendToPipeline);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002C18C File Offset: 0x0002A38C
		private void GrantServerAdminRole()
		{
			try
			{
				ActiveDirectoryAccessRule[] acesToServerAdmin = PermissionTaskHelper.GetAcesToServerAdmin(this.ConfigurationSession, ((IADSecurityPrincipal)this.user).Sid);
				DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), this.server, acesToServerAdmin);
			}
			catch (SecurityDescriptorAccessDeniedException exception)
			{
				base.WriteError(exception, ErrorCategory.PermissionDenied, null);
			}
			this.WriteWarning(Strings.CouldNotFindLocalAdministratorGroup(this.server.Name, this.Identity.ToString()));
		}
	}
}
