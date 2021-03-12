using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync.Exceptions
{
	// Token: 0x02000500 RID: 1280
	[Serializable]
	public class InvalidDatabaseException : TransportSyncException
	{
		// Token: 0x06001FAA RID: 8106 RVA: 0x000C0B4E File Offset: 0x000BED4E
		public InvalidDatabaseException(LocalizedString message) : base(message)
		{
		}
	}
}
