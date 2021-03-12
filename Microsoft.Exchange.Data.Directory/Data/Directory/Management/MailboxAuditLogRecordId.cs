using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000731 RID: 1841
	[Serializable]
	public class MailboxAuditLogRecordId : ObjectId
	{
		// Token: 0x0600582F RID: 22575 RVA: 0x0013A684 File Offset: 0x00138884
		internal MailboxAuditLogRecordId(ObjectId storeId)
		{
			if (storeId == null)
			{
				throw new ArgumentNullException("storeId");
			}
			this.storeId = storeId;
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x0013A6A1 File Offset: 0x001388A1
		public override byte[] GetBytes()
		{
			return this.storeId.GetBytes();
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x0013A6AE File Offset: 0x001388AE
		public override string ToString()
		{
			return this.storeId.ToString();
		}

		// Token: 0x04003B8A RID: 15242
		private ObjectId storeId;
	}
}
