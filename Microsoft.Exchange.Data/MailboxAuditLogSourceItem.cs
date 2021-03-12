using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200022D RID: 557
	[Serializable]
	public class MailboxAuditLogSourceItem
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x0003B23B File Offset: 0x0003943B
		private MailboxAuditLogSourceItem()
		{
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0003B243 File Offset: 0x00039443
		// (set) Token: 0x06001353 RID: 4947 RVA: 0x0003B24B File Offset: 0x0003944B
		public string SourceItemId { get; private set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0003B254 File Offset: 0x00039454
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x0003B25C File Offset: 0x0003945C
		public string SourceItemSubject { get; private set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0003B265 File Offset: 0x00039465
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x0003B26D File Offset: 0x0003946D
		public string SourceItemFolderPathName { get; private set; }

		// Token: 0x06001358 RID: 4952 RVA: 0x0003B278 File Offset: 0x00039478
		public static MailboxAuditLogSourceItem Parse(string itemId, string itemSubject, string itemFolderPathName)
		{
			return new MailboxAuditLogSourceItem
			{
				SourceItemId = itemId,
				SourceItemSubject = itemSubject,
				SourceItemFolderPathName = itemFolderPathName
			};
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0003B2A1 File Offset: 0x000394A1
		public override int GetHashCode()
		{
			if (this.SourceItemId != null)
			{
				return this.SourceItemId.ToUpperInvariant().GetHashCode();
			}
			return string.Empty.GetHashCode();
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0003B2C6 File Offset: 0x000394C6
		public override string ToString()
		{
			return this.SourceItemId;
		}
	}
}
