using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004C5 RID: 1221
	internal class DiagnosticsItemCallReceived : DiagnosticsItemWithServiceBase
	{
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x000B83D0 File Offset: 0x000B65D0
		public DateTime ReceivedTime
		{
			get
			{
				DateTime minValue = DateTime.MinValue;
				DateTime.TryParse(base.GetValue("time"), out minValue);
				return minValue.ToUniversalTime();
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000B83FD File Offset: 0x000B65FD
		internal static bool IsExpectedErrorId(int errorid)
		{
			return errorid == 15638 || errorid == 15644;
		}
	}
}
