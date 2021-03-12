using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004CF RID: 1231
	internal class DiagnosticsItemCallAudioReceived : DiagnosticsItemWithServiceBase
	{
		// Token: 0x06001E9F RID: 7839 RVA: 0x000B84EE File Offset: 0x000B66EE
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid == 15906;
		}
	}
}
