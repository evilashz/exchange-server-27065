using System;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000029 RID: 41
	public class MessageEntry
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00006E8C File Offset: 0x0000508C
		public MessageEntry(int documentId, ExchangeId messageId)
		{
			this.documentId = documentId;
			this.messageId = messageId;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00006EA2 File Offset: 0x000050A2
		public int DocumentId
		{
			get
			{
				return this.documentId;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006EAA File Offset: 0x000050AA
		public ExchangeId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006EB2 File Offset: 0x000050B2
		public override int GetHashCode()
		{
			return this.DocumentId;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006EBC File Offset: 0x000050BC
		public override bool Equals(object other)
		{
			MessageEntry messageEntry = other as MessageEntry;
			return messageEntry != null && messageEntry.DocumentId == this.DocumentId;
		}

		// Token: 0x0400009F RID: 159
		private readonly int documentId;

		// Token: 0x040000A0 RID: 160
		private readonly ExchangeId messageId;
	}
}
