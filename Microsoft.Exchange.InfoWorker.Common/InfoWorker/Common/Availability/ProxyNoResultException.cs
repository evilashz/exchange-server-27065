using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000A3 RID: 163
	internal class ProxyNoResultException : AvailabilityException
	{
		// Token: 0x0600036E RID: 878 RVA: 0x0000EA76 File Offset: 0x0000CC76
		public ProxyNoResultException(LocalizedString message, uint locationIdentifier) : base(ErrorConstants.ProxyRequestProcessingFailed, message, locationIdentifier)
		{
		}
	}
}
