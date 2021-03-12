using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004CB RID: 1227
	internal class DiagnosticsItemCallEstablishing : DiagnosticsItemWithServiceBase
	{
		// Token: 0x06001E97 RID: 7831 RVA: 0x000B84A6 File Offset: 0x000B66A6
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid == 15902;
		}
	}
}
