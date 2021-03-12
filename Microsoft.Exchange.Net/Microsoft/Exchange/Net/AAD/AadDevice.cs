using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.AAD
{
	// Token: 0x02000588 RID: 1416
	internal sealed class AadDevice
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0002AA0C File Offset: 0x00028C0C
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x0002AA14 File Offset: 0x00028C14
		public bool? AccountEnabled { get; set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0002AA1D File Offset: 0x00028C1D
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x0002AA25 File Offset: 0x00028C25
		public bool? IsManaged { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0002AA2E File Offset: 0x00028C2E
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x0002AA36 File Offset: 0x00028C36
		public bool? IsCompliant { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0002AA3F File Offset: 0x00028C3F
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x0002AA47 File Offset: 0x00028C47
		public Guid? DeviceId { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0002AA50 File Offset: 0x00028C50
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x0002AA58 File Offset: 0x00028C58
		public string DisplayName { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0002AA61 File Offset: 0x00028C61
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x0002AA69 File Offset: 0x00028C69
		public List<string> ExchangeActiveSyncIds { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0002AA72 File Offset: 0x00028C72
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x0002AA7A File Offset: 0x00028C7A
		public DateTime LastUpdated { get; set; }
	}
}
