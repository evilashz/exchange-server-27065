using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007A9 RID: 1961
	[Cmdlet("Get", "MailboxFolderPermission", DefaultParameterSetName = "Identity")]
	public class GetMailboxFolderPermission : GetTenantXsoObjectWithFolderIdentityTaskBase<MailboxFolder>
	{
		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06004522 RID: 17698 RVA: 0x0011C0DC File Offset: 0x0011A2DC
		// (set) Token: 0x06004523 RID: 17699 RVA: 0x0011C0F3 File Offset: 0x0011A2F3
		[Parameter(Mandatory = false)]
		public MailboxFolderUserIdParameter User
		{
			get
			{
				return (MailboxFolderUserIdParameter)base.Fields["User"];
			}
			set
			{
				base.Fields["User"] = value;
			}
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x0011C106 File Offset: 0x0011A306
		protected virtual ObjectId ResolvedObjectId
		{
			get
			{
				return this.Identity.InternalMailboxFolderId;
			}
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x0011C114 File Offset: 0x0011A314
		protected sealed override IConfigDataProvider CreateSession()
		{
			ADUser mailboxOwner = this.PrepareMailboxUser();
			base.InnerMailboxFolderDataProvider = new MailboxFolderDataProvider(base.OrgWideSessionSettings, mailboxOwner, "Get-MailboxFolderPermission");
			if (this.User != null)
			{
				this.mailboxUserId = this.User.ResolveMailboxFolderUserId(base.InnerMailboxFolderDataProvider.MailboxSession);
			}
			return base.InnerMailboxFolderDataProvider;
		}

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x0011C169 File Offset: 0x0011A369
		protected override ObjectId RootId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x0011C16C File Offset: 0x0011A36C
		protected override bool DeepSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x0011C170 File Offset: 0x0011A370
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			MailboxFolder mailboxFolder = (MailboxFolder)dataObject;
			MailboxSession mailboxSession = base.InnerMailboxFolderDataProvider.MailboxSession;
			VersionedId internalFolderIdentity = mailboxFolder.InternalFolderIdentity;
			int num = 0;
			using (Folder folder = Folder.Bind(mailboxSession, internalFolderIdentity))
			{
				PermissionSet permissionSet = folder.GetPermissionSet();
				if (permissionSet.DefaultPermission != null && (this.mailboxUserId == null || this.mailboxUserId.Equals(permissionSet.DefaultPermission.Principal)))
				{
					base.WriteResult(MailboxFolderPermission.FromXsoPermission(folder.DisplayName, permissionSet.DefaultPermission, this.ResolvedObjectId));
					num++;
				}
				if (permissionSet.AnonymousPermission != null && (this.mailboxUserId == null || this.mailboxUserId.Equals(permissionSet.AnonymousPermission.Principal)))
				{
					base.WriteResult(MailboxFolderPermission.FromXsoPermission(folder.DisplayName, permissionSet.AnonymousPermission, this.ResolvedObjectId));
					num++;
				}
				foreach (Permission permission in permissionSet)
				{
					if (this.mailboxUserId == null || this.mailboxUserId.Equals(permission.Principal))
					{
						base.WriteResult(MailboxFolderPermission.FromXsoPermission(folder.DisplayName, permission, this.ResolvedObjectId));
						num++;
					}
				}
			}
			if (this.mailboxUserId != null && num == 0)
			{
				throw new UserNotFoundInPermissionEntryException(this.mailboxUserId.ToString());
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002AA6 RID: 10918
		private MailboxFolderUserId mailboxUserId;
	}
}
