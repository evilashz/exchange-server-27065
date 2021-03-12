using System;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x0200026E RID: 622
	internal sealed class ItemChange
	{
		// Token: 0x060011AF RID: 4527 RVA: 0x00052619 File Offset: 0x00050819
		public ItemChange(ItemChangeType changeType, ItemIdType id)
		{
			this.ChangeType = changeType;
			this.Id = id;
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0005262F File Offset: 0x0005082F
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00052637 File Offset: 0x00050837
		public ItemChangeType ChangeType { get; private set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00052640 File Offset: 0x00050840
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x00052648 File Offset: 0x00050848
		public ItemIdType Id { get; private set; }
	}
}
