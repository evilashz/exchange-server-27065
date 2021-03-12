using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync.Exceptions
{
	// Token: 0x02000501 RID: 1281
	[Serializable]
	public class DatabaseDiscoveryTimeOutException : TransportSyncException
	{
		// Token: 0x06001FAB RID: 8107 RVA: 0x000C0B57 File Offset: 0x000BED57
		public DatabaseDiscoveryTimeOutException(LocalizedString message) : base(message)
		{
		}
	}
}
