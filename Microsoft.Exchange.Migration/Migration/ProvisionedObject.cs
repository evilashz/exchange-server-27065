using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000030 RID: 48
	internal struct ProvisionedObject
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00009754 File Offset: 0x00007954
		public ProvisionedObject(ObjectId itemId, Guid jobId, ProvisioningType type)
		{
			this.ItemId = itemId;
			this.JobId = jobId;
			this.Type = type;
			this.Succeeded = false;
			this.Error = string.Empty;
			this.MailboxData = null;
			this.IsRetryable = false;
			this.TimeAttempted = ExDateTime.UtcNow;
			this.TimeFinished = null;
			this.GroupMemberProvisioned = 0;
			this.GroupMemberSkipped = 0;
		}

		// Token: 0x040000B2 RID: 178
		public readonly ObjectId ItemId;

		// Token: 0x040000B3 RID: 179
		public readonly Guid JobId;

		// Token: 0x040000B4 RID: 180
		public readonly ProvisioningType Type;

		// Token: 0x040000B5 RID: 181
		public bool Succeeded;

		// Token: 0x040000B6 RID: 182
		public string Error;

		// Token: 0x040000B7 RID: 183
		public bool IsRetryable;

		// Token: 0x040000B8 RID: 184
		public IMailboxData MailboxData;

		// Token: 0x040000B9 RID: 185
		public int GroupMemberProvisioned;

		// Token: 0x040000BA RID: 186
		public int GroupMemberSkipped;

		// Token: 0x040000BB RID: 187
		public ExDateTime TimeAttempted;

		// Token: 0x040000BC RID: 188
		public ExDateTime? TimeFinished;
	}
}
