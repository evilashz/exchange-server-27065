using System;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x0200088B RID: 2187
	internal sealed class RecipientPendingOperation
	{
		// Token: 0x06002E78 RID: 11896 RVA: 0x0006673C File Offset: 0x0006493C
		public RecipientPendingOperation(RecipientSyncOperation recipientSyncOperation, OperationType type)
		{
			this.recipientSyncOperation = recipientSyncOperation;
			this.type = type;
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x00066752 File Offset: 0x00064952
		public RecipientSyncOperation RecipientSyncOperation
		{
			get
			{
				return this.recipientSyncOperation;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x0006675A File Offset: 0x0006495A
		public OperationType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x00066762 File Offset: 0x00064962
		public bool IsAdd
		{
			get
			{
				return this.type == OperationType.Add;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x0006676D File Offset: 0x0006496D
		public bool IsDelete
		{
			get
			{
				return this.type == OperationType.Delete;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x00066778 File Offset: 0x00064978
		public bool IsRead
		{
			get
			{
				return this.type == OperationType.Read;
			}
		}

		// Token: 0x04002885 RID: 10373
		private OperationType type;

		// Token: 0x04002886 RID: 10374
		private readonly RecipientSyncOperation recipientSyncOperation;
	}
}
