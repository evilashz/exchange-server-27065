using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001C2 RID: 450
	internal struct MRSSettingsData
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00027776 File Offset: 0x00025976
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x0002777E File Offset: 0x0002597E
		public string Context { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x00027787 File Offset: 0x00025987
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0002778F File Offset: 0x0002598F
		public string SettingName { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00027798 File Offset: 0x00025998
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x000277A0 File Offset: 0x000259A0
		public string SettingValue { get; set; }
	}
}
