using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x02000077 RID: 119
	[DataContract]
	public class NoOpTaskData
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B265 File Offset: 0x00009465
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000B26D File Offset: 0x0000946D
		[DataMember]
		public List<DarTaskExecutionResult> States { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000B276 File Offset: 0x00009476
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000B27E File Offset: 0x0000947E
		[DataMember]
		public List<DarTaskState> StateHistory { get; set; }
	}
}
