using System;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x02000012 RID: 18
	public class FullTextIndexRow
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00005477 File Offset: 0x00003677
		private FullTextIndexRow(long fastDocumentId)
		{
			this.fastDocumentId = fastDocumentId;
			this.documentId = IndexId.GetDocumentId(fastDocumentId);
			this.mailboxNumber = IndexId.GetMailboxNumber(fastDocumentId);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000549E File Offset: 0x0000369E
		private FullTextIndexRow(long fastDocumentId, int conversationDocumentId) : this(fastDocumentId)
		{
			this.conversationDocumentId = new int?(conversationDocumentId);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000054B3 File Offset: 0x000036B3
		private FullTextIndexRow(Guid mailboxGuid, long fastDocumentId) : this(fastDocumentId)
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000054C3 File Offset: 0x000036C3
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000054CB File Offset: 0x000036CB
		[Queryable]
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000054D3 File Offset: 0x000036D3
		[Queryable]
		public int DocumentId
		{
			get
			{
				return this.documentId;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000054DB File Offset: 0x000036DB
		[Queryable]
		public long FastDocumentId
		{
			get
			{
				return this.fastDocumentId;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000054E3 File Offset: 0x000036E3
		public int? ConversationDocumentId
		{
			get
			{
				return this.conversationDocumentId;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000054EB File Offset: 0x000036EB
		public static FullTextIndexRow Parse(long fastDocumentId)
		{
			return new FullTextIndexRow(fastDocumentId);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000054F3 File Offset: 0x000036F3
		public static FullTextIndexRow Parse(long fastDocumentId, int conversationId)
		{
			return new FullTextIndexRow(fastDocumentId, conversationId);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000054FC File Offset: 0x000036FC
		public static FullTextIndexRow Parse(Guid mailboxGuid, long fastDocumentId)
		{
			return new FullTextIndexRow(mailboxGuid, fastDocumentId);
		}

		// Token: 0x0400005C RID: 92
		private readonly Guid mailboxGuid;

		// Token: 0x0400005D RID: 93
		private readonly int mailboxNumber;

		// Token: 0x0400005E RID: 94
		private readonly int documentId;

		// Token: 0x0400005F RID: 95
		private readonly int? conversationDocumentId;

		// Token: 0x04000060 RID: 96
		private readonly long fastDocumentId;
	}
}
