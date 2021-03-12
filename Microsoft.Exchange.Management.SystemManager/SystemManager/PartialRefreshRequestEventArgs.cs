using System;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200000E RID: 14
	public class PartialRefreshRequestEventArgs : RefreshRequestEventArgs
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000050C4 File Offset: 0x000032C4
		public PartialRefreshRequestEventArgs(IProgress progress, object argument, object[] ids, RefreshRequestPriority priority) : base(false, progress, argument, priority)
		{
			this.Identities = ids;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000050D8 File Offset: 0x000032D8
		public PartialRefreshRequestEventArgs(IProgress progress, object argument, object[] ids) : this(progress, argument, ids, 0)
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000050E4 File Offset: 0x000032E4
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000050EC File Offset: 0x000032EC
		public object[] Identities { get; private set; }
	}
}
