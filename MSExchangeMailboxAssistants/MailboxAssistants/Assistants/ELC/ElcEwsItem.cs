using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000094 RID: 148
	internal class ElcEwsItem
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0002B71E File Offset: 0x0002991E
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0002B726 File Offset: 0x00029926
		public string Id { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0002B72F File Offset: 0x0002992F
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0002B737 File Offset: 0x00029937
		public byte[] Data { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0002B740 File Offset: 0x00029940
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0002B748 File Offset: 0x00029948
		public ElcEwsException Error { get; set; }

		// Token: 0x04000439 RID: 1081
		public ItemData StorageItemData;
	}
}
