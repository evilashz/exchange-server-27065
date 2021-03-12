using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000087 RID: 135
	internal class ProxyRequestNotAllowedException : AvailabilityInvalidParameterException
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		public ProxyRequestNotAllowedException() : base(ErrorConstants.ProxyRequestNotAllowed, Strings.descProxyRequestNotAllowed)
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000E7DE File Offset: 0x0000C9DE
		public ProxyRequestNotAllowedException(LocalizedString message) : base(ErrorConstants.ProxyRequestNotAllowed, message)
		{
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		public ProxyRequestNotAllowedException(LocalizedString message, Exception innerException) : base(ErrorConstants.ProxyRequestNotAllowed, message, innerException)
		{
		}
	}
}
