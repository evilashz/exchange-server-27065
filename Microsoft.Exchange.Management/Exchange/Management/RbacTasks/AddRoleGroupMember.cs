using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000670 RID: 1648
	[Cmdlet("Add", "RoleGroupMember", SupportsShouldProcess = true)]
	public sealed class AddRoleGroupMember : RoleGroupMemberTaskBase
	{
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x000F65B3 File Offset: 0x000F47B3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddRoleGroupMember(this.Identity.ToString(), base.Member.ToString());
			}
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000F65D0 File Offset: 0x000F47D0
		protected override void PerformGroupMemberAction()
		{
			TaskLogger.LogEnter();
			if (this.DataObject.RoleGroupType == RoleGroupType.Linked)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorLinkedRoleGroupCannotHaveMembers), (ErrorCategory)1000, null);
			}
			MailboxTaskHelper.ValidateAndAddMember(base.TenantGlobalCatalogSession, this.DataObject, base.Member, false, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
			MailboxTaskHelper.ValidateAddedMembers(base.TenantGlobalCatalogSession, this.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
			TaskLogger.LogExit();
		}
	}
}
