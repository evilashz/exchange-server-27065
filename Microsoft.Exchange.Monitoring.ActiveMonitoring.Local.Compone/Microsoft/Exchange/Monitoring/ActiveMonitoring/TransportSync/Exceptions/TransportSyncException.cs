using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync.Exceptions
{
	// Token: 0x020004FF RID: 1279
	[Serializable]
	public abstract class TransportSyncException : LocalizedException
	{
		// Token: 0x06001FA9 RID: 8105 RVA: 0x000C0B45 File Offset: 0x000BED45
		public TransportSyncException(LocalizedString message) : base(message)
		{
		}
	}
}
