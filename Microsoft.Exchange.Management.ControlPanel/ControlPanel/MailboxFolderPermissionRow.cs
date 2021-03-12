using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200007F RID: 127
	[DataContract]
	public class MailboxFolderPermissionRow : BaseRow
	{
		// Token: 0x06001B6D RID: 7021 RVA: 0x000570B6 File Offset: 0x000552B6
		public MailboxFolderPermissionRow(MailboxFolderPermission permission) : base(new MailboxFolderPermissionIdentity(permission), permission)
		{
			this.MailboxFolderPermission = permission;
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x000570CC File Offset: 0x000552CC
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x000570D4 File Offset: 0x000552D4
		public MailboxFolderPermission MailboxFolderPermission { get; private set; }

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x000570DD File Offset: 0x000552DD
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x000570EF File Offset: 0x000552EF
		public Identity MailboxFolderId
		{
			get
			{
				return ((MailboxFolderPermissionIdentity)base.Identity).MailboxFolderId;
			}
			set
			{
				((MailboxFolderPermissionIdentity)base.Identity).MailboxFolderId = value;
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x00057102 File Offset: 0x00055302
		// (set) Token: 0x06001B73 RID: 7027 RVA: 0x0005710F File Offset: 0x0005530F
		[DataMember]
		public string User
		{
			get
			{
				return base.Identity.DisplayName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x00057118 File Offset: 0x00055318
		// (set) Token: 0x06001B75 RID: 7029 RVA: 0x0005719E File Offset: 0x0005539E
		[DataMember]
		public bool IsSupported
		{
			get
			{
				if (this.MailboxFolderPermission.AccessRights.Count != 1)
				{
					return false;
				}
				MailboxFolderAccessRight[] array = this.MailboxFolderPermission.AccessRights.ToArray<MailboxFolderAccessRight>();
				int i = 0;
				while (i < array.Length)
				{
					MailboxFolderAccessRight mailboxFolderAccessRight = array[i];
					bool result;
					if (!mailboxFolderAccessRight.IsRole)
					{
						result = false;
					}
					else
					{
						MailboxFolderPermissionRole permission = (MailboxFolderPermissionRole)mailboxFolderAccessRight.Permission;
						if (permission == MailboxFolderPermissionRole.Reviewer || permission == MailboxFolderPermissionRole.AvailabilityOnly || permission == MailboxFolderPermissionRole.LimitedDetails)
						{
							i++;
							continue;
						}
						result = false;
					}
					return result;
				}
				return true;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x000571F4 File Offset: 0x000553F4
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x0005723F File Offset: 0x0005543F
		[DataMember]
		public string AccessRights
		{
			get
			{
				string[] value = Array.ConvertAll<MailboxFolderAccessRight, string>(this.MailboxFolderPermission.AccessRights.ToArray<MailboxFolderAccessRight>(), delegate(MailboxFolderAccessRight x)
				{
					if (!x.IsRole)
					{
						return (x.Permission == 0) ? OwaOptionStrings.NoneAccessRightRole : OwaOptionStrings.CustomAccessRightRole;
					}
					return LocalizedDescriptionAttribute.FromEnumForOwaOption(typeof(MailboxFolderPermissionRole), x.Permission);
				});
				return string.Join(",", value);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x00057248 File Offset: 0x00055448
		// (set) Token: 0x06001B79 RID: 7033 RVA: 0x00057279 File Offset: 0x00055479
		public bool IsAnonymousOrDefault
		{
			get
			{
				switch (this.MailboxFolderPermission.User.UserType)
				{
				case MailboxFolderUserId.MailboxFolderUserType.Default:
				case MailboxFolderUserId.MailboxFolderUserType.Anonymous:
					return true;
				default:
					return false;
				}
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
