using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007B3 RID: 1971
	[Cmdlet("Set", "MailboxFolderPermission", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxFolderPermission : SetMailboxFolderPermissionBase
	{
		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x0011D0AE File Offset: 0x0011B2AE
		// (set) Token: 0x06004564 RID: 17764 RVA: 0x0011D0C5 File Offset: 0x0011B2C5
		[Parameter(Mandatory = true)]
		public MailboxFolderAccessRight[] AccessRights
		{
			get
			{
				return (MailboxFolderAccessRight[])base.Fields["AccessRights"];
			}
			set
			{
				MailboxFolderPermission.ValidateAccessRights(value);
				base.Fields["AccessRights"] = value;
			}
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x0011D0DE File Offset: 0x0011B2DE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMailboxFolderPermission(this.Identity.ToString(), base.User.ToString(), base.FormatMultiValuedProperty(this.AccessRights));
			}
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x0011D108 File Offset: 0x0011B308
		internal override bool InternalProcessPermissions(Folder folder)
		{
			PermissionSet permissionSet = folder.GetPermissionSet();
			Permission targetPermission = base.GetTargetPermission(permissionSet);
			if (targetPermission == null)
			{
				throw new UserNotFoundInPermissionEntryException(base.MailboxFolderUserId.ToString());
			}
			MemberRights memberRights = (MemberRights)MailboxFolderAccessRight.CalculateMemberRights(this.AccessRights, folder.ClassName == "IPF.Appointment");
			if (targetPermission.MemberRights == memberRights)
			{
				this.WriteWarning(Strings.WarningMailboxFolderPermissionUnchanged(this.DataObject.Identity.ToString()));
				return false;
			}
			try
			{
				targetPermission.MemberRights = memberRights;
			}
			catch (ArgumentOutOfRangeException exception)
			{
				base.WriteError(exception, (ErrorCategory)1003, this.Identity);
			}
			return true;
		}
	}
}
