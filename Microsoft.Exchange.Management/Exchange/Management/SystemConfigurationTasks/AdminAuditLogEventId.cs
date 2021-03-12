using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class AdminAuditLogEventId : ObjectId
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00007501 File Offset: 0x00005701
		internal AdminAuditLogEventId(ObjectId storeId)
		{
			if (storeId == null)
			{
				throw new ArgumentNullException("storeId");
			}
			this.storeId = storeId;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000751E File Offset: 0x0000571E
		public override byte[] GetBytes()
		{
			return this.storeId.GetBytes();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000752B File Offset: 0x0000572B
		public override string ToString()
		{
			return this.storeId.ToString();
		}

		// Token: 0x04000115 RID: 277
		private ObjectId storeId;
	}
}
