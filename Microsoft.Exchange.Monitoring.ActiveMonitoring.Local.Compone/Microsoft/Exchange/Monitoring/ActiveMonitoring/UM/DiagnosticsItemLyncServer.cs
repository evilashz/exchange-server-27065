using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004C6 RID: 1222
	internal class DiagnosticsItemLyncServer : DiagnosticsItemBase
	{
		// Token: 0x06001E8D RID: 7821 RVA: 0x000B8419 File Offset: 0x000B6619
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid >= 15000 && errorid <= 15499;
		}
	}
}
