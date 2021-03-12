using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000006 RID: 6
	[XmlType("Task")]
	public class WLMTaskDetails
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002365 File Offset: 0x00000565
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000236D File Offset: 0x0000056D
		public string BudgetOwner { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002376 File Offset: 0x00000576
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000237E File Offset: 0x0000057E
		public string Description { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002387 File Offset: 0x00000587
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000238F File Offset: 0x0000058F
		public string TotalTime { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002398 File Offset: 0x00000598
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000023A0 File Offset: 0x000005A0
		public string ExecuteTime { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000023A9 File Offset: 0x000005A9
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000023B1 File Offset: 0x000005B1
		public string QueueTime { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000023BA File Offset: 0x000005BA
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000023C2 File Offset: 0x000005C2
		public string DelayTime { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000023CB File Offset: 0x000005CB
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000023D3 File Offset: 0x000005D3
		public int DelayCount { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000023DC File Offset: 0x000005DC
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000023E4 File Offset: 0x000005E4
		public int QueueCount { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000023ED File Offset: 0x000005ED
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000023F5 File Offset: 0x000005F5
		public int ExecuteCount { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000023FE File Offset: 0x000005FE
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002406 File Offset: 0x00000606
		public string Location { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000240F File Offset: 0x0000060F
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002417 File Offset: 0x00000617
		public DateTime StartTimeUTC { get; set; }
	}
}
