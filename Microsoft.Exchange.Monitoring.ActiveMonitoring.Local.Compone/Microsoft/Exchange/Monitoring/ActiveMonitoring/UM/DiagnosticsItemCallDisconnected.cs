using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004CE RID: 1230
	internal class DiagnosticsItemCallDisconnected : DiagnosticsItemWithServiceBase
	{
		// Token: 0x06001E9D RID: 7837 RVA: 0x000B84DC File Offset: 0x000B66DC
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid == 15905;
		}
	}
}
