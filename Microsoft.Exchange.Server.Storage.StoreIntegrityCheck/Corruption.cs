using System;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200001E RID: 30
	public struct Corruption
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000056E1 File Offset: 0x000038E1
		public Corruption(CorruptionType corruptionType, ExchangeId? folderId, ExchangeId? messageId, bool isFixed)
		{
			this.corruptionType = corruptionType;
			this.folderId = folderId;
			this.messageId = messageId;
			this.isFixed = isFixed;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00005700 File Offset: 0x00003900
		public CorruptionType CorruptionType
		{
			get
			{
				return this.corruptionType;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00005708 File Offset: 0x00003908
		public ExchangeId? FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005710 File Offset: 0x00003910
		public ExchangeId? MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00005718 File Offset: 0x00003918
		public bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
		}

		// Token: 0x04000068 RID: 104
		private CorruptionType corruptionType;

		// Token: 0x04000069 RID: 105
		private ExchangeId? folderId;

		// Token: 0x0400006A RID: 106
		private ExchangeId? messageId;

		// Token: 0x0400006B RID: 107
		private bool isFixed;
	}
}
