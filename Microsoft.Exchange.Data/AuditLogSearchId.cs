using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001ED RID: 493
	[Serializable]
	public class AuditLogSearchId : ObjectId
	{
		// Token: 0x0600110E RID: 4366 RVA: 0x00033D7F File Offset: 0x00031F7F
		public AuditLogSearchId(Guid requestId)
		{
			this.Guid = requestId;
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00033D8E File Offset: 0x00031F8E
		// (set) Token: 0x06001110 RID: 4368 RVA: 0x00033D96 File Offset: 0x00031F96
		public Guid Guid { get; private set; }

		// Token: 0x06001111 RID: 4369 RVA: 0x00033DA0 File Offset: 0x00031FA0
		public override byte[] GetBytes()
		{
			return this.Guid.ToByteArray();
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00033DBC File Offset: 0x00031FBC
		public override string ToString()
		{
			return this.Guid.ToString();
		}
	}
}
