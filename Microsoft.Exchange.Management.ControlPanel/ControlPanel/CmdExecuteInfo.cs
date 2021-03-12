using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000047 RID: 71
	[DataContract]
	public class CmdExecuteInfo
	{
		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x00052680 File Offset: 0x00050880
		// (set) Token: 0x060019AA RID: 6570 RVA: 0x00052688 File Offset: 0x00050888
		[DataMember]
		public string LogId { get; set; }

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x00052691 File Offset: 0x00050891
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x00052699 File Offset: 0x00050899
		[DataMember]
		public CmdletStatus Status { get; set; }

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x000526A2 File Offset: 0x000508A2
		// (set) Token: 0x060019AE RID: 6574 RVA: 0x000526AA File Offset: 0x000508AA
		[DataMember]
		public string CommandText { get; set; }

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x000526B3 File Offset: 0x000508B3
		// (set) Token: 0x060019B0 RID: 6576 RVA: 0x000526BB File Offset: 0x000508BB
		[DataMember]
		public string Exception { get; set; }

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x000526C4 File Offset: 0x000508C4
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x000526CC File Offset: 0x000508CC
		[DataMember]
		public string StartTime { get; set; }
	}
}
