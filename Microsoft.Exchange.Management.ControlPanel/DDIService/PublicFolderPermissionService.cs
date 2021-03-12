using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000313 RID: 787
	public static class PublicFolderPermissionService
	{
		// Token: 0x06002E93 RID: 11923 RVA: 0x0008E604 File Offset: 0x0008C804
		public static void GetPublicFolderClientPermissionEntryPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			IEnumerable<object> enumerable = store.GetDataObject("MailboxFolderPermissions") as IEnumerable<object>;
			List<PublicFolderPermissionInfo> list = new List<PublicFolderPermissionInfo>();
			foreach (object obj in enumerable)
			{
				MailboxFolderPermission mailboxFolderPermission = obj as MailboxFolderPermission;
				if (mailboxFolderPermission != null && mailboxFolderPermission.User.UserType != MailboxFolderUserId.MailboxFolderUserType.Anonymous && mailboxFolderPermission.User.UserType != MailboxFolderUserId.MailboxFolderUserType.Default)
				{
					list.Add((PublicFolderPermissionInfo)mailboxFolderPermission);
				}
			}
			dataTable.Rows[0]["FolderPermissions"] = list;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x0008E6AC File Offset: 0x0008C8AC
		public static void GetPublicFolderListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.Rows[0]["TargetPublicFolders"] = (store.GetDataObject("PublicFolderList") as IEnumerable<object>);
		}
	}
}
