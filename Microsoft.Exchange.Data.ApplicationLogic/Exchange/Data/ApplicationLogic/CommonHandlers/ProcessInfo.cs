using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.CommonHandlers
{
	// Token: 0x020000C3 RID: 195
	public class ProcessInfo
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00021B98 File Offset: 0x0001FD98
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x00021BA0 File Offset: 0x0001FDA0
		public string ServerName { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00021BA9 File Offset: 0x0001FDA9
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x00021BB1 File Offset: 0x0001FDB1
		public int ProcessID { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00021BBA File Offset: 0x0001FDBA
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x00021BC2 File Offset: 0x0001FDC2
		public int ThreadCount { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00021BCB File Offset: 0x0001FDCB
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x00021BD3 File Offset: 0x0001FDD3
		public string Version { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00021BDC File Offset: 0x0001FDDC
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		public double ProcessUpTime { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00021BED File Offset: 0x0001FDED
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00021BF5 File Offset: 0x0001FDF5
		public long MemorySize { get; set; }
	}
}
