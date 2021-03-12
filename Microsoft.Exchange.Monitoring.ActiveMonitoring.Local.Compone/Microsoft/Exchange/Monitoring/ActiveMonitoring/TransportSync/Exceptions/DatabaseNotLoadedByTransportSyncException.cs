using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync.Exceptions
{
	// Token: 0x02000503 RID: 1283
	[Serializable]
	public class DatabaseNotLoadedByTransportSyncException : TransportSyncException
	{
		// Token: 0x06001FAD RID: 8109 RVA: 0x000C0B69 File Offset: 0x000BED69
		public DatabaseNotLoadedByTransportSyncException(LocalizedString message) : base(message)
		{
		}
	}
}
