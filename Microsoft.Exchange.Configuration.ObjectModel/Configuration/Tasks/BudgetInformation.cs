using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000294 RID: 660
	internal class BudgetInformation
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0005595D File Offset: 0x00053B5D
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x00055965 File Offset: 0x00053B65
		public IPowerShellBudget Budget { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0005596E File Offset: 0x00053B6E
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x00055976 File Offset: 0x00053B76
		public CostHandle Handle { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x0005597F File Offset: 0x00053B7F
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x00055987 File Offset: 0x00053B87
		public ExEventLog.EventTuple ThrottledEventInfo { get; set; }
	}
}
