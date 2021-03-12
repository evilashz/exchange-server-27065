using System;
using System.Diagnostics;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x0200002E RID: 46
	public class InstanceContainer
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000032DC File Offset: 0x000014DC
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000032E4 File Offset: 0x000014E4
		public object InstanceLock { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000032ED File Offset: 0x000014ED
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000032F5 File Offset: 0x000014F5
		public InstanceGroupConfig Config { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000032FE File Offset: 0x000014FE
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00003306 File Offset: 0x00001506
		public InstanceState State { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000330F File Offset: 0x0000150F
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00003317 File Offset: 0x00001517
		public InstanceStatusInfo StatusInfo { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00003320 File Offset: 0x00001520
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00003328 File Offset: 0x00001528
		public Process HostingProcess { get; set; }
	}
}
