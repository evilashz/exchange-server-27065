using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x0200025B RID: 603
	internal sealed class Changes
	{
		// Token: 0x06001163 RID: 4451 RVA: 0x00050010 File Offset: 0x0004E210
		public Changes(string syncState, bool moreChangesAvailable, ItemChange[] items)
		{
			this.SyncState = syncState;
			this.MoreChangesAvailable = moreChangesAvailable;
			this.Items = items;
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0005002D File Offset: 0x0004E22D
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x00050035 File Offset: 0x0004E235
		public string SyncState { get; private set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0005003E File Offset: 0x0004E23E
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x00050046 File Offset: 0x0004E246
		public bool MoreChangesAvailable { get; private set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0005004F File Offset: 0x0004E24F
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x00050057 File Offset: 0x0004E257
		public ItemChange[] Items { get; private set; }
	}
}
