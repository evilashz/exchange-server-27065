using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200008E RID: 142
	internal class ProxyWebRequestProcessingException : AvailabilityException
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000E897 File Offset: 0x0000CA97
		public ProxyWebRequestProcessingException(LocalizedString message) : base(ErrorConstants.ProxyRequestProcessingFailed, message)
		{
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000E8A5 File Offset: 0x0000CAA5
		public ProxyWebRequestProcessingException(LocalizedString message, Exception innerException) : base(ErrorConstants.ProxyRequestProcessingFailed, message, innerException)
		{
		}
	}
}
