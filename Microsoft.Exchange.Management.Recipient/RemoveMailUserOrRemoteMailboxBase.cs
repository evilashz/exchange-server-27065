using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RbacTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200008B RID: 139
	public abstract class RemoveMailUserOrRemoteMailboxBase<TIdentity> : RemoveMailUserBase<TIdentity> where TIdentity : MailUserIdParameterBase, new()
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x000287E4 File Offset: 0x000269E4
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.orgAdminHelper = new RoleAssignmentsGlobalConstraints(this.ConfigurationSession, base.TenantGlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError));
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00028810 File Offset: 0x00026A10
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.DataObject != null)
			{
				RemoveMailbox.CheckManagedGroups(base.DataObject, base.TenantGlobalCatalogSession, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				if (this.orgAdminHelper.ShouldPreventLastAdminRemoval(this, base.DataObject.OrganizationId) && this.orgAdminHelper.IsLastAdmin(base.DataObject))
				{
					base.WriteError(new CannotRemoveLastOrgAdminException(Strings.ErrorCannotRemoveLastOrgAdmin(base.DataObject.Identity.ToString())), ExchangeErrorCategory.Client, base.DataObject.Identity);
				}
			}
		}

		// Token: 0x040001F3 RID: 499
		private RoleAssignmentsGlobalConstraints orgAdminHelper;
	}
}
