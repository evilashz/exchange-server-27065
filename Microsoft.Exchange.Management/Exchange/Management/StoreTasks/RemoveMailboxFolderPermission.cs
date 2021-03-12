using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007B2 RID: 1970
	[Cmdlet("Remove", "MailboxFolderPermission", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class RemoveMailboxFolderPermission : SetMailboxFolderPermissionBase
	{
		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x0011CFED File Offset: 0x0011B1ED
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMailboxFolderPermission(this.Identity.ToString(), base.User.ToString());
			}
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x0011D00A File Offset: 0x0011B20A
		protected override void ValidateMailboxFolderUserId(MailboxFolderUserId mailboxFolderUserId)
		{
			base.ValidateMailboxFolderUserId(mailboxFolderUserId);
			if ((mailboxFolderUserId.UserType == MailboxFolderUserId.MailboxFolderUserType.Default || mailboxFolderUserId.UserType == MailboxFolderUserId.MailboxFolderUserType.Anonymous) && base.UserRecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
			{
				throw new CannotRemoveSpecialUserException();
			}
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x0011D03C File Offset: 0x0011B23C
		internal override bool InternalProcessPermissions(Folder folder)
		{
			PermissionSet permissionSet = folder.GetPermissionSet();
			Permission targetPermission = base.GetTargetPermission(permissionSet);
			if (targetPermission == null)
			{
				throw new UserNotFoundInPermissionEntryException(base.MailboxFolderUserId.ToString());
			}
			if (base.MailboxFolderUserId.UserType == MailboxFolderUserId.MailboxFolderUserType.Default)
			{
				permissionSet.SetDefaultPermission(MemberRights.None);
			}
			else if (base.MailboxFolderUserId.UserType == MailboxFolderUserId.MailboxFolderUserType.Anonymous)
			{
				permissionSet.SetAnonymousPermission(MemberRights.None);
			}
			else
			{
				permissionSet.RemoveEntry(targetPermission.Principal);
			}
			return true;
		}
	}
}
