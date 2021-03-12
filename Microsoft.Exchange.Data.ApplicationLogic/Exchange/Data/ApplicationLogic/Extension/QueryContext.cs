using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage.Principal;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200012A RID: 298
	internal abstract class QueryContext
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x000324E3 File Offset: 0x000306E3
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x000324EB File Offset: 0x000306EB
		internal string Domain { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x000324F4 File Offset: 0x000306F4
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x000324FC File Offset: 0x000306FC
		internal string DeploymentId { get; set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00032505 File Offset: 0x00030705
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0003250D File Offset: 0x0003070D
		internal OrgEmptyMasterTableCache OrgEmptyMasterTableCache { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00032516 File Offset: 0x00030716
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0003251E File Offset: 0x0003071E
		internal bool IsUserScope { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00032527 File Offset: 0x00030727
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0003252F File Offset: 0x0003072F
		internal IExchangePrincipal ExchangePrincipal { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00032538 File Offset: 0x00030738
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x00032540 File Offset: 0x00030740
		internal CultureInfo CultureInfo { get; set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00032549 File Offset: 0x00030749
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x00032551 File Offset: 0x00030751
		internal string ClientInfoString { get; set; }
	}
}
