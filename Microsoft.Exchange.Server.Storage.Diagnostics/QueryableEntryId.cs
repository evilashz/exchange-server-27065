using System;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000044 RID: 68
	public class QueryableEntryId
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x0000DA58 File Offset: 0x0000BC58
		public QueryableEntryId(Guid mailboxInstanceGuid, EntryIdHelpers.EIDType eidType, ExchangeId folderId, ExchangeId messageId)
		{
			this.MailboxInstanceGuid = mailboxInstanceGuid;
			this.EidType = eidType.ToString();
			this.FolderId = folderId.To26ByteArray();
			if (messageId == ExchangeId.Null)
			{
				this.MessageId = null;
				return;
			}
			this.MessageId = messageId.To26ByteArray();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000DAB3 File Offset: 0x0000BCB3
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000DABB File Offset: 0x0000BCBB
		public Guid MailboxInstanceGuid { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000DACC File Offset: 0x0000BCCC
		public string EidType { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000DAD5 File Offset: 0x0000BCD5
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000DADD File Offset: 0x0000BCDD
		public byte[] FolderId { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000DAE6 File Offset: 0x0000BCE6
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000DAEE File Offset: 0x0000BCEE
		public byte[] MessageId { get; private set; }
	}
}
