using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000003 RID: 3
	internal class BasicItemId
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000021C4 File Offset: 0x000003C4
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000021CC File Offset: 0x000003CC
		public string Id { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021D5 File Offset: 0x000003D5
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000021DD File Offset: 0x000003DD
		public string SourceId { get; set; }
	}
}
