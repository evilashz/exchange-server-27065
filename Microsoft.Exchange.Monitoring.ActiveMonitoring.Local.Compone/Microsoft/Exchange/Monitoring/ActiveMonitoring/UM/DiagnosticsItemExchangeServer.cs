using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004C7 RID: 1223
	internal class DiagnosticsItemExchangeServer : DiagnosticsItemBase
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x000B8438 File Offset: 0x000B6638
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid >= 15500 && errorid <= 15899 && errorid != 15638 && errorid != 15644 && errorid != 15637 && errorid != 15643;
		}
	}
}
