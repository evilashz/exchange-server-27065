using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000A0 RID: 160
	internal class ProxyServerWithMinimumRequiredVersionNotFound : AvailabilityException
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public ProxyServerWithMinimumRequiredVersionNotFound(EmailAddress requester, int serverVersion, int minimumServerVersion) : base(ErrorConstants.E14orHigherProxyServerNotFound, Strings.descMinimumRequiredVersionProxyServerNotFound(serverVersion, minimumServerVersion, requester.ToString()))
		{
		}
	}
}
