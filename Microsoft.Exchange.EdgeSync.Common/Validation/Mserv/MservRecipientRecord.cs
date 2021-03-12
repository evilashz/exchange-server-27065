using System;

namespace Microsoft.Exchange.EdgeSync.Validation.Mserv
{
	// Token: 0x02000047 RID: 71
	public sealed class MservRecipientRecord
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000091BD File Offset: 0x000073BD
		public string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000091C5 File Offset: 0x000073C5
		public int PartnerId
		{
			get
			{
				return this.partnerId;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000091CD File Offset: 0x000073CD
		internal MservRecipientRecord(string address, int partnerId)
		{
			this.address = address;
			this.partnerId = partnerId;
		}

		// Token: 0x0400013D RID: 317
		private string address;

		// Token: 0x0400013E RID: 318
		private int partnerId;
	}
}
