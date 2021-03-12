using System;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000F3 RID: 243
	internal class CmdletLatencyMonitor : IScopedPerformanceMonitor
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x0001BF0A File Offset: 0x0001A10A
		private CmdletLatencyMonitor()
		{
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001BF12 File Offset: 0x0001A112
		public bool Start(ScopeInfo scopeInfo)
		{
			return CmdletLatencyTracker.StartInternalTracking(CmdletLatencyMonitor.GetCmdletUniqueId(scopeInfo), scopeInfo.GroupName, scopeInfo.FuncName, false);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001BF2C File Offset: 0x0001A12C
		public void End(ScopeInfo scopeInfo)
		{
			CmdletLatencyTracker.EndInternalTracking(CmdletLatencyMonitor.GetCmdletUniqueId(scopeInfo), scopeInfo.GroupName, scopeInfo.FuncName);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001BF48 File Offset: 0x0001A148
		private static Guid GetCmdletUniqueId(ScopeInfo scopeInfo)
		{
			Guid result = Guid.Empty;
			if (scopeInfo is CmdletScopeInfo)
			{
				result = ((CmdletScopeInfo)scopeInfo).CmdletUniqueId;
			}
			return result;
		}

		// Token: 0x04000484 RID: 1156
		internal static readonly CmdletLatencyMonitor Instance = new CmdletLatencyMonitor();
	}
}
