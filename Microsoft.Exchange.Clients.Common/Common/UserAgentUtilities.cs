using System;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200002F RID: 47
	public static class UserAgentUtilities
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00009B50 File Offset: 0x00007D50
		public static bool IsMonitoringRequest(string userAgent)
		{
			return !string.IsNullOrEmpty(userAgent) && userAgent.IndexOf("MSEXCHMON", StringComparison.OrdinalIgnoreCase) != -1;
		}
	}
}
