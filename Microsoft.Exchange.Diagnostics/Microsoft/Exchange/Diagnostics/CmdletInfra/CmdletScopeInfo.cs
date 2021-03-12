using System;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000F9 RID: 249
	internal class CmdletScopeInfo : ScopeInfo
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x0001C6DA File Offset: 0x0001A8DA
		public CmdletScopeInfo(Guid cmdletUniqueId, string groupName, string funcName) : base(groupName, funcName)
		{
			this.CmdletUniqueId = cmdletUniqueId;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001C6EB File Offset: 0x0001A8EB
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0001C6F3 File Offset: 0x0001A8F3
		public Guid CmdletUniqueId { get; private set; }
	}
}
