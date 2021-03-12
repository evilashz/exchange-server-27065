using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync.Exceptions
{
	// Token: 0x02000502 RID: 1282
	[Serializable]
	public class DatabaseOutOfSlaException : TransportSyncException
	{
		// Token: 0x06001FAC RID: 8108 RVA: 0x000C0B60 File Offset: 0x000BED60
		public DatabaseOutOfSlaException(LocalizedString message) : base(message)
		{
		}
	}
}
