using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000081 RID: 129
	[DataContract]
	public class UserMailboxFolderPermission : MailboxFolderPermissionRow
	{
		// Token: 0x06001B80 RID: 7040 RVA: 0x000572B6 File Offset: 0x000554B6
		public UserMailboxFolderPermission(MailboxFolderPermission permission) : base(permission)
		{
		}

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x000572BF File Offset: 0x000554BF
		// (set) Token: 0x06001B82 RID: 7042 RVA: 0x000572D6 File Offset: 0x000554D6
		[DataMember]
		public string ChangePermissionsForUser
		{
			get
			{
				return string.Format(OwaOptionStrings.ChangePermissions, base.User);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x000572E0 File Offset: 0x000554E0
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x00057323 File Offset: 0x00055523
		[DataMember]
		public string ReadAccessRights
		{
			get
			{
				if (!base.IsSupported)
				{
					return null;
				}
				return Enum.GetName(typeof(MailboxFolderPermissionRole), base.MailboxFolderPermission.AccessRights.First<MailboxFolderAccessRight>().Permission);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
