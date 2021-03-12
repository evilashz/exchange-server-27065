using System;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000F7 RID: 247
	internal class CmdletMonitoredScope : MonitoredScope
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001C690 File Offset: 0x0001A890
		public CmdletMonitoredScope(Guid cmdletUniqueId, string groupName, string funcName, params IScopedPerformanceMonitor[] monitors) : base(new CmdletScopeInfo(cmdletUniqueId, groupName, funcName), monitors)
		{
		}
	}
}
