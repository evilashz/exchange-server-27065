using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200003D RID: 61
	internal class TaskInvocationInfo
	{
		// Token: 0x0600015E RID: 350 RVA: 0x000077B5 File Offset: 0x000059B5
		public TaskInvocationInfo(string cmdletName, string snapinName, Dictionary<string, string> parameters)
		{
			this.CmdletName = cmdletName;
			this.SnapinName = snapinName;
			this.Parameters = parameters;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000077D2 File Offset: 0x000059D2
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000077DA File Offset: 0x000059DA
		public string CmdletName { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000077E3 File Offset: 0x000059E3
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000077EB File Offset: 0x000059EB
		public string SnapinName { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000077F4 File Offset: 0x000059F4
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000077FC File Offset: 0x000059FC
		public Dictionary<string, string> Parameters { get; private set; }
	}
}
