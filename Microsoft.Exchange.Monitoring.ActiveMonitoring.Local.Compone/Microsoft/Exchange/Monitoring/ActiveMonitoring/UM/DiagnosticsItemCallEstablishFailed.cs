using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004CD RID: 1229
	internal class DiagnosticsItemCallEstablishFailed : DiagnosticsItemWithServiceBase
	{
		// Token: 0x06001E9B RID: 7835 RVA: 0x000B84CA File Offset: 0x000B66CA
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid == 15904;
		}
	}
}
