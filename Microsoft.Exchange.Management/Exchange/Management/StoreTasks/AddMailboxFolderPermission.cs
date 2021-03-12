using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007A8 RID: 1960
	[Cmdlet("Add", "MailboxFolderPermission", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class AddMailboxFolderPermission : SetMailboxFolderPermissionBase
	{
		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x0011BEC8 File Offset: 0x0011A0C8
		// (set) Token: 0x0600451A RID: 17690 RVA: 0x0011BEDF File Offset: 0x0011A0DF
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

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x0011BEF8 File Offset: 0x0011A0F8
		protected virtual ObjectId ResolvedObjectId
		{
			get
			{
				return this.Identity.InternalMailboxFolderId;
			}
		}

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x0600451C RID: 17692 RVA: 0x0011BF05 File Offset: 0x0011A105
		protected virtual bool IsPublicFolderIdentity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x0011BF08 File Offset: 0x0011A108
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddMailboxFolderPermission(this.Identity.ToString(), base.User.ToString(), base.FormatMultiValuedProperty(this.AccessRights));
			}
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x0011BF34 File Offset: 0x0011A134
		protected override void ValidateMailboxFolderUserId(MailboxFolderUserId mailboxFolderUserId)
		{
			base.ValidateMailboxFolderUserId(mailboxFolderUserId);
			if (mailboxFolderUserId.UserType == MailboxFolderUserId.MailboxFolderUserType.Internal && !ADRecipient.IsValidRecipient(mailboxFolderUserId.ADRecipient, !this.IsPublicFolderIdentity))
			{
				throw new InvalidInternalUserIdException(base.User.ToString());
			}
			if (mailboxFolderUserId.UserType == MailboxFolderUserId.MailboxFolderUserType.Unknown)
			{
				throw new InvalidExternalUserIdException(base.User.ToString());
			}
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x0011BF94 File Offset: 0x0011A194
		internal override bool InternalProcessPermissions(Folder folder)
		{
			MemberRights memberRights = (MemberRights)MailboxFolderAccessRight.CalculateMemberRights(this.AccessRights, folder.ClassName == "IPF.Appointment");
			PermissionSet permissionSet = folder.GetPermissionSet();
			Permission permission = null;
			try
			{
				if (base.MailboxFolderUserId.UserType == MailboxFolderUserId.MailboxFolderUserType.Default)
				{
					if (permissionSet.DefaultPermission != null && permissionSet.DefaultPermission.MemberRights != MemberRights.None)
					{
						throw new UserAlreadyExistsInPermissionEntryException(base.MailboxFolderUserId.ToString());
					}
					permissionSet.SetDefaultPermission(memberRights);
					permission = permissionSet.DefaultPermission;
				}
				else if (base.MailboxFolderUserId.UserType == MailboxFolderUserId.MailboxFolderUserType.Anonymous)
				{
					if (permissionSet.AnonymousPermission != null && permissionSet.AnonymousPermission.MemberRights != MemberRights.None)
					{
						throw new UserAlreadyExistsInPermissionEntryException(base.MailboxFolderUserId.ToString());
					}
					permissionSet.SetAnonymousPermission(memberRights);
					permission = permissionSet.AnonymousPermission;
				}
				else
				{
					PermissionSecurityPrincipal securityPrincipal = base.MailboxFolderUserId.ToSecurityPrincipal();
					Permission entry = permissionSet.GetEntry(securityPrincipal);
					if (entry != null)
					{
						throw new UserAlreadyExistsInPermissionEntryException(base.MailboxFolderUserId.ToString());
					}
					permission = permissionSet.AddEntry(securityPrincipal, memberRights);
				}
			}
			catch (ArgumentOutOfRangeException exception)
			{
				base.WriteError(exception, (ErrorCategory)1003, this.Identity);
				return false;
			}
			base.WriteObject(MailboxFolderPermission.FromXsoPermission(folder.DisplayName, permission, this.ResolvedObjectId));
			return true;
		}
	}
}
