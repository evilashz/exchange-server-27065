using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200022C RID: 556
	[Serializable]
	public class MailboxAuditLogSourceFolder
	{
		// Token: 0x06001349 RID: 4937 RVA: 0x0003B1C0 File Offset: 0x000393C0
		private MailboxAuditLogSourceFolder()
		{
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0003B1C8 File Offset: 0x000393C8
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x0003B1D0 File Offset: 0x000393D0
		public string SourceFolderId { get; private set; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0003B1D9 File Offset: 0x000393D9
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x0003B1E1 File Offset: 0x000393E1
		public string SourceFolderPathName { get; private set; }

		// Token: 0x0600134E RID: 4942 RVA: 0x0003B1EC File Offset: 0x000393EC
		public static MailboxAuditLogSourceFolder Parse(string folderId, string folderPathName)
		{
			return new MailboxAuditLogSourceFolder
			{
				SourceFolderId = folderId,
				SourceFolderPathName = folderPathName
			};
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0003B20E File Offset: 0x0003940E
		public override int GetHashCode()
		{
			if (this.SourceFolderId != null)
			{
				return this.SourceFolderId.ToUpperInvariant().GetHashCode();
			}
			return string.Empty.GetHashCode();
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0003B233 File Offset: 0x00039433
		public override string ToString()
		{
			return this.SourceFolderId;
		}
	}
}
