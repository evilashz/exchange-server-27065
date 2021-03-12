using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x02000066 RID: 102
	[DataContract]
	public class ComplianceTaskStatistics
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A4F2 File Offset: 0x000086F2
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000A4FA File Offset: 0x000086FA
		[DataMember]
		public int Completed { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000A503 File Offset: 0x00008703
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000A50B File Offset: 0x0000870B
		[DataMember]
		public int Yielded { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000A514 File Offset: 0x00008714
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000A51C File Offset: 0x0000871C
		[DataMember]
		public int TransientFailed { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000A525 File Offset: 0x00008725
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000A52D File Offset: 0x0000872D
		[DataMember]
		public int Failed { get; set; }
	}
}
