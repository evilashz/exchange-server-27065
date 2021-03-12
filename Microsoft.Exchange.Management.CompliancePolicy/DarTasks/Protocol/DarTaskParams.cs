using System;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol
{
	// Token: 0x02000011 RID: 17
	[DataContract]
	public class DarTaskParams : DarTaskParamsBase
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000356C File Offset: 0x0000176C
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003574 File Offset: 0x00001774
		[DataMember]
		public DarTaskState TaskState { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000357D File Offset: 0x0000177D
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003585 File Offset: 0x00001785
		[DataMember]
		public DateTime MinQueuedTime { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000358E File Offset: 0x0000178E
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003596 File Offset: 0x00001796
		[DataMember]
		public DateTime MaxQueuedTime { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000359F File Offset: 0x0000179F
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000035A7 File Offset: 0x000017A7
		[DataMember]
		public DateTime MinCompletionTime { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000035B0 File Offset: 0x000017B0
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000035B8 File Offset: 0x000017B8
		[DataMember]
		public DateTime MaxCompletionTime { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000035C1 File Offset: 0x000017C1
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000035C9 File Offset: 0x000017C9
		[DataMember]
		public bool ActiveInRuntime { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000035D2 File Offset: 0x000017D2
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000035DA File Offset: 0x000017DA
		[DataMember]
		public int Priority { get; set; }
	}
}
