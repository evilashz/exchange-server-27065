using System;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000F8 RID: 248
	internal class ScopeInfo
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001C6A2 File Offset: 0x0001A8A2
		public ScopeInfo(string groupName, string funcName)
		{
			this.GroupName = groupName;
			this.FuncName = funcName;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
		public string GroupName { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001C6C9 File Offset: 0x0001A8C9
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001C6D1 File Offset: 0x0001A8D1
		public string FuncName { get; private set; }
	}
}
