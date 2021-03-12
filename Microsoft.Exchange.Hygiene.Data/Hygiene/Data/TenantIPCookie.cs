using System;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000102 RID: 258
	internal class TenantIPCookie
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x0001EB8C File Offset: 0x0001CD8C
		public TenantIPCookie()
		{
			this.watermarkTime = SqlDateTime.MinValue.Value;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001EBB2 File Offset: 0x0001CDB2
		public TenantIPCookie(DateTime watermarkTime)
		{
			this.watermarkTime = watermarkTime;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		public DateTime UpdateWatermark()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.oldWatermarkTime = new DateTime?(this.watermarkTime);
			this.watermarkTime = utcNow;
			return this.oldWatermarkTime.Value;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001EBFA File Offset: 0x0001CDFA
		public void RevertToOldWatermark()
		{
			if (this.oldWatermarkTime != null)
			{
				this.watermarkTime = this.oldWatermarkTime.Value;
				this.oldWatermarkTime = null;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001EC26 File Offset: 0x0001CE26
		public void CommitNewWatermark()
		{
			this.oldWatermarkTime = null;
		}

		// Token: 0x0400053D RID: 1341
		private DateTime? oldWatermarkTime;

		// Token: 0x0400053E RID: 1342
		private DateTime watermarkTime;
	}
}
