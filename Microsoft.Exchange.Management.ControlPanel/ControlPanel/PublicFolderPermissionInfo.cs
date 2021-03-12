using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000314 RID: 788
	[DataContract]
	public class PublicFolderPermissionInfo
	{
		// Token: 0x06002E95 RID: 11925 RVA: 0x0008E6D4 File Offset: 0x0008C8D4
		public PublicFolderPermissionInfo(MailboxFolderPermission permissionEntry)
		{
			if (permissionEntry.User.ADRecipient == null)
			{
				this.User = new Identity(permissionEntry.User.ToString());
				this.Sid = "Unknown";
			}
			else
			{
				this.User = permissionEntry.User.ADRecipient.ToIdentity();
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)permissionEntry.User.ADRecipient.propertyBag[IADSecurityPrincipalSchema.Sid];
				this.Sid = ((securityIdentifier == null) ? "Unknown" : securityIdentifier.Value);
			}
			List<string> list = new List<string>();
			if (permissionEntry.AccessRights.Count == 1 && permissionEntry.AccessRights[0].IsRole)
			{
				using (IEnumerator enumerator = Enum.GetValues(typeof(PublicFolderPermission)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						PublicFolderPermission publicFolderPermission = (PublicFolderPermission)obj;
						if (PublicFolderPermissionInfo.ContainsRight((PublicFolderPermission)permissionEntry.AccessRights[0].Permission, publicFolderPermission))
						{
							List<string> list2 = list;
							int num = (int)publicFolderPermission;
							list2.Add(num.ToString());
						}
					}
					goto IL_16C;
				}
			}
			foreach (MailboxFolderAccessRight mailboxFolderAccessRight in permissionEntry.AccessRights)
			{
				list.Add(mailboxFolderAccessRight.Permission.ToString());
			}
			IL_16C:
			this.AccessRights = list.ToArray();
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x0008E878 File Offset: 0x0008CA78
		// (set) Token: 0x06002E97 RID: 11927 RVA: 0x0008E880 File Offset: 0x0008CA80
		[DataMember]
		public Identity User { get; set; }

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x0008E889 File Offset: 0x0008CA89
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x0008E891 File Offset: 0x0008CA91
		[DataMember]
		public string Sid { get; set; }

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x0008E89A File Offset: 0x0008CA9A
		// (set) Token: 0x06002E9B RID: 11931 RVA: 0x0008E8A2 File Offset: 0x0008CAA2
		[DataMember]
		public string[] AccessRights { get; set; }

		// Token: 0x06002E9C RID: 11932 RVA: 0x0008E8AB File Offset: 0x0008CAAB
		public static explicit operator PublicFolderPermissionInfo(MailboxFolderPermission folderPermission)
		{
			if (folderPermission != null)
			{
				return new PublicFolderPermissionInfo(folderPermission);
			}
			return null;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x0008E8B8 File Offset: 0x0008CAB8
		public static bool ContainsRight(PublicFolderPermission accessRights, PublicFolderPermission accessRight)
		{
			return (accessRights & accessRight) == accessRight;
		}
	}
}
