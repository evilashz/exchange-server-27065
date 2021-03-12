using System;
using Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery
{
	// Token: 0x020001A0 RID: 416
	internal static class ConnectionTypeClassification
	{
		// Token: 0x06000FBA RID: 4026 RVA: 0x0004073A File Offset: 0x0003E93A
		public static bool IsExchangeServer(ConnectionSettingsType connectionSettingsType)
		{
			return connectionSettingsType == ConnectionSettingsType.ExchangeActiveSync;
		}
	}
}
