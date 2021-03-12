using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007DE RID: 2014
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnseenItem : IUnseenItem
	{
		// Token: 0x06004B7D RID: 19325 RVA: 0x0013AAA0 File Offset: 0x00138CA0
		public UnseenItem(StoreId storeId, ExDateTime receivedTime)
		{
			ArgumentValidator.ThrowIfNull("storeId", storeId);
			this.Id = storeId;
			this.ReceivedTime = receivedTime;
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06004B7E RID: 19326 RVA: 0x0013AAC1 File Offset: 0x00138CC1
		// (set) Token: 0x06004B7F RID: 19327 RVA: 0x0013AAC9 File Offset: 0x00138CC9
		public StoreId Id { get; private set; }

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06004B80 RID: 19328 RVA: 0x0013AAD2 File Offset: 0x00138CD2
		// (set) Token: 0x06004B81 RID: 19329 RVA: 0x0013AADA File Offset: 0x00138CDA
		public ExDateTime ReceivedTime { get; private set; }
	}
}
