using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007A7 RID: 1959
	public abstract class SetMailboxFolderPermissionBase : SetTenantXsoObjectWithFolderIdentityTaskBase<MailboxFolder>
	{
		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x0600450E RID: 17678 RVA: 0x0011BCF0 File Offset: 0x00119EF0
		// (set) Token: 0x0600450F RID: 17679 RVA: 0x0011BD07 File Offset: 0x00119F07
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x0011BD1A File Offset: 0x00119F1A
		protected RecipientTypeDetails UserRecipientTypeDetails
		{
			get
			{
				if (this.targetUser == null)
				{
					return RecipientTypeDetails.None;
				}
				return this.targetUser.RecipientTypeDetails;
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06004511 RID: 17681 RVA: 0x0011BD32 File Offset: 0x00119F32
		// (set) Token: 0x06004512 RID: 17682 RVA: 0x0011BD3A File Offset: 0x00119F3A
		private protected MailboxFolderUserId MailboxFolderUserId
		{
			protected get
			{
				return this.mailboxFolderUserId;
			}
			private set
			{
				this.ValidateMailboxFolderUserId(value);
				this.mailboxFolderUserId = value;
			}
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x0011BD4C File Offset: 0x00119F4C
		protected sealed override IConfigDataProvider CreateSession()
		{
			this.targetUser = this.PrepareMailboxUser();
			base.InnerMailboxFolderDataProvider = new MailboxFolderDataProvider(base.OrgWideSessionSettings, this.targetUser, "SetMailboxFolderPermissionBase");
			this.MailboxFolderUserId = this.User.ResolveMailboxFolderUserId(base.InnerMailboxFolderDataProvider.MailboxSession);
			return base.InnerMailboxFolderDataProvider;
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x0011BDA3 File Offset: 0x00119FA3
		protected virtual void ValidateMailboxFolderUserId(MailboxFolderUserId mailboxFolderUserId)
		{
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x0011BDA8 File Offset: 0x00119FA8
		internal Permission GetTargetPermission(PermissionSet permissionSet)
		{
			Permission result = null;
			switch (this.MailboxFolderUserId.UserType)
			{
			case MailboxFolderUserId.MailboxFolderUserType.Default:
				return permissionSet.DefaultPermission;
			case MailboxFolderUserId.MailboxFolderUserType.Anonymous:
				return permissionSet.AnonymousPermission;
			case MailboxFolderUserId.MailboxFolderUserType.Internal:
			case MailboxFolderUserId.MailboxFolderUserType.External:
			{
				PermissionSecurityPrincipal securityPrincipal = this.MailboxFolderUserId.ToSecurityPrincipal();
				return permissionSet.GetEntry(securityPrincipal);
			}
			}
			foreach (Permission permission in permissionSet)
			{
				if (this.MailboxFolderUserId.Equals(permission.Principal))
				{
					result = permission;
					break;
				}
			}
			return result;
		}

		// Token: 0x06004516 RID: 17686
		internal abstract bool InternalProcessPermissions(Folder folder);

		// Token: 0x06004517 RID: 17687 RVA: 0x0011BE58 File Offset: 0x0011A058
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			MailboxFolder dataObject = this.DataObject;
			MailboxSession mailboxSession = base.InnerMailboxFolderDataProvider.MailboxSession;
			VersionedId internalFolderIdentity = dataObject.InternalFolderIdentity;
			using (Folder folder = Folder.Bind(mailboxSession, internalFolderIdentity))
			{
				if (this.InternalProcessPermissions(folder))
				{
					folder.Save();
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002AA4 RID: 10916
		private ADUser targetUser;

		// Token: 0x04002AA5 RID: 10917
		private MailboxFolderUserId mailboxFolderUserId;
	}
}
