using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000028 RID: 40
	internal class DownLevelServerStatusEntry
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000073F9 File Offset: 0x000055F9
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00007401 File Offset: 0x00005601
		public BackEndServer BackEndServer { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000740A File Offset: 0x0000560A
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00007412 File Offset: 0x00005612
		public bool IsHealthy { get; set; }
	}
}
