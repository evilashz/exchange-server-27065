using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004CA RID: 1226
	internal class DiagnosticsItemCallStart : DiagnosticsItemWithServiceBase
	{
		// Token: 0x06001E95 RID: 7829 RVA: 0x000B8494 File Offset: 0x000B6694
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid == 15901;
		}
	}
}
