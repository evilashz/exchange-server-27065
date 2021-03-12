using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200009E RID: 158
	internal class ProxyNotAllowedForPersonalRelationship : AvailabilityException
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000E9F3 File Offset: 0x0000CBF3
		public ProxyNotAllowedForPersonalRelationship(EmailAddress recipient) : base(ErrorConstants.ProxyForPersonalNotAllowed, Strings.descProxyForPersonalNotAllowed(recipient.ToString()))
		{
		}
	}
}
