using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007AC RID: 1964
	[Serializable]
	public sealed class MailboxFolderPermission : ConfigurableObject
	{
		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x0600453F RID: 17727 RVA: 0x0011CA01 File Offset: 0x0011AC01
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxFolderPermission.schema;
			}
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06004540 RID: 17728 RVA: 0x0011CA08 File Offset: 0x0011AC08
		// (set) Token: 0x06004541 RID: 17729 RVA: 0x0011CA1A File Offset: 0x0011AC1A
		public new ObjectId Identity
		{
			get
			{
				return (ObjectId)this[MailboxFolderPermissionSchema.Identity];
			}
			private set
			{
				this[MailboxFolderPermissionSchema.Identity] = value;
			}
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x0011CA28 File Offset: 0x0011AC28
		// (set) Token: 0x06004543 RID: 17731 RVA: 0x0011CA3A File Offset: 0x0011AC3A
		public string FolderName
		{
			get
			{
				return (string)this[MailboxFolderPermissionSchema.FolderName];
			}
			private set
			{
				this[MailboxFolderPermissionSchema.FolderName] = value;
			}
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x06004544 RID: 17732 RVA: 0x0011CA48 File Offset: 0x0011AC48
		// (set) Token: 0x06004545 RID: 17733 RVA: 0x0011CA93 File Offset: 0x0011AC93
		public MailboxFolderUserId User
		{
			get
			{
				MailboxFolderUserId mailboxFolderUserId = (MailboxFolderUserId)this[MailboxFolderPermissionSchema.User];
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					switch (mailboxFolderUserId.UserType)
					{
					case MailboxFolderUserId.MailboxFolderUserType.Internal:
					case MailboxFolderUserId.MailboxFolderUserType.External:
					case MailboxFolderUserId.MailboxFolderUserType.Unknown:
						mailboxFolderUserId = null;
						break;
					}
				}
				return mailboxFolderUserId;
			}
			private set
			{
				this[MailboxFolderPermissionSchema.User] = value;
			}
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06004546 RID: 17734 RVA: 0x0011CAA1 File Offset: 0x0011ACA1
		// (set) Token: 0x06004547 RID: 17735 RVA: 0x0011CAB3 File Offset: 0x0011ACB3
		public Collection<MailboxFolderAccessRight> AccessRights
		{
			get
			{
				return (Collection<MailboxFolderAccessRight>)this[MailboxFolderPermissionSchema.AccessRights];
			}
			set
			{
				MailboxFolderPermission.ValidateAccessRights(value);
				this[MailboxFolderPermissionSchema.AccessRights] = value;
			}
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x0011CAC7 File Offset: 0x0011ACC7
		public MailboxFolderPermission() : base(new SimplePropertyBag(MailboxFolderPermissionSchema.Identity, MailboxFolderPermissionSchema.ObjectState, MailboxFolderPermissionSchema.ExchangeVersion))
		{
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06004549 RID: 17737 RVA: 0x0011CAE3 File Offset: 0x0011ACE3
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x0011CAEC File Offset: 0x0011ACEC
		internal static void ValidateAccessRights(IEnumerable<MailboxFolderAccessRight> accessRights)
		{
			if (accessRights != null)
			{
				bool flag = false;
				bool flag2 = false;
				foreach (MailboxFolderAccessRight mailboxFolderAccessRight in accessRights)
				{
					if (mailboxFolderAccessRight.IsRole)
					{
						if (flag2)
						{
							throw new ArgumentException(Strings.ErrorPrecannedRoleAndSpecificMailboxFolderPermission);
						}
						flag = true;
					}
					else
					{
						if (flag)
						{
							throw new ArgumentException(Strings.ErrorPrecannedRoleAndSpecificMailboxFolderPermission);
						}
						flag2 = true;
					}
				}
			}
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x0011CB6C File Offset: 0x0011AD6C
		internal static MailboxFolderPermission FromXsoPermission(string folderName, Permission permission, ObjectId mailboxFolderId)
		{
			Collection<MailboxFolderAccessRight> accessRights = MailboxFolderAccessRight.CreateMailboxFolderAccessRightCollection((int)permission.MemberRights);
			return new MailboxFolderPermission
			{
				FolderName = folderName,
				Identity = mailboxFolderId,
				User = MailboxFolderUserId.CreateFromSecurityPrincipal(permission.Principal),
				AccessRights = accessRights
			};
		}

		// Token: 0x04002AB0 RID: 10928
		private static MailboxFolderPermissionSchema schema = ObjectSchema.GetInstance<MailboxFolderPermissionSchema>();
	}
}
