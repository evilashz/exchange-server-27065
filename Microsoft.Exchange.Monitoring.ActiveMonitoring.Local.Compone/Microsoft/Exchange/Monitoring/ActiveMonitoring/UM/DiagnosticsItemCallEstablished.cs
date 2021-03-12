using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004CC RID: 1228
	internal class DiagnosticsItemCallEstablished : DiagnosticsItemWithServiceBase
	{
		// Token: 0x06001E99 RID: 7833 RVA: 0x000B84B8 File Offset: 0x000B66B8
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid == 15903;
		}
	}
}
