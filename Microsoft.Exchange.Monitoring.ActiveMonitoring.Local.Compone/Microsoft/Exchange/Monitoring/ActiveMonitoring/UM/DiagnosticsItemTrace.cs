using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004C9 RID: 1225
	internal class DiagnosticsItemTrace : DiagnosticsItemWithServiceBase
	{
		// Token: 0x06001E93 RID: 7827 RVA: 0x000B8482 File Offset: 0x000B6682
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid == 15900;
		}
	}
}
