using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200005A RID: 90
	internal sealed class ItemAuditLogData
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00013E08 File Offset: 0x00012008
		internal ItemAuditLogData(object[] row, PropertyIndexHolder propertyIndexHolder, FolderAuditLogData folderAuditLogData)
		{
			if (propertyIndexHolder.MessageSubjectIndex >= 0)
			{
				this.messageSubject = (row[propertyIndexHolder.MessageSubjectIndex] as string);
			}
			this.messageSender = (row[propertyIndexHolder.MessageSenderIndex] as string);
			string text = row[propertyIndexHolder.MessageSentRepresentingIndex] as string;
			if (!string.IsNullOrEmpty(text) && !text.Equals(this.messageSender, StringComparison.InvariantCultureIgnoreCase))
			{
				this.messageSender = this.messageSender + " sent on behalf of " + text;
			}
			this.messageInternetId = (row[propertyIndexHolder.MessageInternetIdIndex] as string);
			this.messageClass = (row[propertyIndexHolder.ItemClassIndex] as string);
			this.folderAuditLogData = folderAuditLogData;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00013EB3 File Offset: 0x000120B3
		internal string MessageSubject
		{
			get
			{
				return this.messageSubject;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00013EBB File Offset: 0x000120BB
		internal string MessageSender
		{
			get
			{
				return this.messageSender;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00013EC3 File Offset: 0x000120C3
		internal string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00013ECB File Offset: 0x000120CB
		internal string MessageInternetId
		{
			get
			{
				return this.messageInternetId;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00013ED3 File Offset: 0x000120D3
		internal FolderAuditLogData FolderAuditLogData
		{
			get
			{
				return this.folderAuditLogData;
			}
		}

		// Token: 0x040002A5 RID: 677
		private string messageSubject;

		// Token: 0x040002A6 RID: 678
		private string messageSender;

		// Token: 0x040002A7 RID: 679
		private string messageClass;

		// Token: 0x040002A8 RID: 680
		private string messageInternetId;

		// Token: 0x040002A9 RID: 681
		private FolderAuditLogData folderAuditLogData;
	}
}
