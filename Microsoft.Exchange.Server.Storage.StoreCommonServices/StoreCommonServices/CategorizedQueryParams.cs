using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000107 RID: 263
	public class CategorizedQueryParams
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00036AD2 File Offset: 0x00034CD2
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00036ADA File Offset: 0x00034CDA
		public IReadOnlyDictionary<Column, Column> HeaderRenameDictionary { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00036AE3 File Offset: 0x00034CE3
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00036AEB File Offset: 0x00034CEB
		public IReadOnlyDictionary<Column, Column> LeafRenameDictionary { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00036AF4 File Offset: 0x00034CF4
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00036AFC File Offset: 0x00034CFC
		public CategorizedTableCollapseState CollapseState { get; set; }
	}
}
