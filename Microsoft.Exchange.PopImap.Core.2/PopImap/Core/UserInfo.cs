using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200002D RID: 45
	public class UserInfo
	{
		// Token: 0x060002AF RID: 687 RVA: 0x0000A550 File Offset: 0x00008750
		internal UserInfo()
		{
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000A558 File Offset: 0x00008758
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000A560 File Offset: 0x00008760
		public string Name { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000A569 File Offset: 0x00008769
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000A571 File Offset: 0x00008771
		public int SessionCount { get; set; }
	}
}
