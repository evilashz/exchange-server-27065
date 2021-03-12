using System;
using System.Collections.Generic;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200005C RID: 92
	internal class MailboxDataForFolders : MailboxData
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00013F68 File Offset: 0x00012168
		internal MailboxDataForFolders(ElcUserFolderInformation elcUserFolderInformation, ElcAuditLog elcAuditLog) : base(elcUserFolderInformation)
		{
			this.userPolicies = elcUserFolderInformation.UserAdFolders;
			this.elcAuditLog = elcAuditLog;
			this.folderProcessor = new FolderProcessor(elcUserFolderInformation.MailboxSession, this.userPolicies);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00013F9B File Offset: 0x0001219B
		internal ElcAuditLog ElcAuditLog
		{
			get
			{
				return this.elcAuditLog;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00013FA3 File Offset: 0x000121A3
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00013FAB File Offset: 0x000121AB
		internal List<AdFolderData> UserPolicies
		{
			get
			{
				return this.userPolicies;
			}
			set
			{
				this.userPolicies = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00013FB4 File Offset: 0x000121B4
		internal FolderProcessor FolderProcessor
		{
			get
			{
				return this.folderProcessor;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00013FBC File Offset: 0x000121BC
		internal Guid GetFolderGuidFromObjectGuid(Guid? destinationFolderObjectGuid)
		{
			if (destinationFolderObjectGuid == null)
			{
				return Guid.Empty;
			}
			foreach (AdFolderData adFolderData in this.UserPolicies)
			{
				if (adFolderData.Folder.Id.ObjectGuid == destinationFolderObjectGuid)
				{
					return adFolderData.Folder.Guid;
				}
			}
			return Guid.Empty;
		}

		// Token: 0x040002AF RID: 687
		private ElcAuditLog elcAuditLog;

		// Token: 0x040002B0 RID: 688
		private List<AdFolderData> userPolicies;

		// Token: 0x040002B1 RID: 689
		private FolderProcessor folderProcessor;
	}
}
