using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200009F RID: 159
	internal class E14orHigherProxyServerNotFound : AvailabilityException
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000EA0B File Offset: 0x0000CC0B
		public E14orHigherProxyServerNotFound(EmailAddress requester, int minimumServerVersion) : base(ErrorConstants.E14orHigherProxyServerNotFound, Strings.descE14orHigherProxyServerNotFound(requester.ToString(), minimumServerVersion))
		{
		}
	}
}
