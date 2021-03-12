using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000693 RID: 1683
	public class AsyncGetListContext
	{
		// Token: 0x170027B8 RID: 10168
		// (get) Token: 0x06004865 RID: 18533 RVA: 0x000DCD19 File Offset: 0x000DAF19
		// (set) Token: 0x06004866 RID: 18534 RVA: 0x000DCD21 File Offset: 0x000DAF21
		public List<PSObject> PsObjectCollection { get; set; }

		// Token: 0x170027B9 RID: 10169
		// (get) Token: 0x06004867 RID: 18535 RVA: 0x000DCD2A File Offset: 0x000DAF2A
		// (set) Token: 0x06004868 RID: 18536 RVA: 0x000DCD32 File Offset: 0x000DAF32
		public int NextFetchStartAt { get; set; }

		// Token: 0x170027BA RID: 10170
		// (get) Token: 0x06004869 RID: 18537 RVA: 0x000DCD3B File Offset: 0x000DAF3B
		// (set) Token: 0x0600486A RID: 18538 RVA: 0x000DCD43 File Offset: 0x000DAF43
		public bool Completed { get; set; }

		// Token: 0x170027BB RID: 10171
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x000DCD4C File Offset: 0x000DAF4C
		// (set) Token: 0x0600486C RID: 18540 RVA: 0x000DCD54 File Offset: 0x000DAF54
		public string WorkflowOutput { get; set; }

		// Token: 0x170027BC RID: 10172
		// (get) Token: 0x0600486D RID: 18541 RVA: 0x000DCD5D File Offset: 0x000DAF5D
		// (set) Token: 0x0600486E RID: 18542 RVA: 0x000DCD65 File Offset: 0x000DAF65
		public DDIParameters Parameters { get; set; }

		// Token: 0x170027BD RID: 10173
		// (get) Token: 0x0600486F RID: 18543 RVA: 0x000DCD6E File Offset: 0x000DAF6E
		// (set) Token: 0x06004870 RID: 18544 RVA: 0x000DCD76 File Offset: 0x000DAF76
		public List<string> UnicodeOutputColumnNames { get; set; }

		// Token: 0x170027BE RID: 10174
		// (get) Token: 0x06004871 RID: 18545 RVA: 0x000DCD7F File Offset: 0x000DAF7F
		// (set) Token: 0x06004872 RID: 18546 RVA: 0x000DCD87 File Offset: 0x000DAF87
		public List<Tuple<int, string[], string>> UnicodeColumns { get; set; }
	}
}
