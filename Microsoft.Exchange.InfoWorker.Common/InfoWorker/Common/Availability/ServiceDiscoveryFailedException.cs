using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000090 RID: 144
	internal class ServiceDiscoveryFailedException : AvailabilityException
	{
		// Token: 0x06000355 RID: 853 RVA: 0x0000E8C2 File Offset: 0x0000CAC2
		public ServiceDiscoveryFailedException(LocalizedString message, Exception innerException) : base(ErrorConstants.ServiceDiscoveryFailed, message, innerException)
		{
		}
	}
}
