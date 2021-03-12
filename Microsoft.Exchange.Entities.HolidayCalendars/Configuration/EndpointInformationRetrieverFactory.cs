using System;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x0200000A RID: 10
	internal class EndpointInformationRetrieverFactory : IEndpointInformationRetrieverFactory
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000286B File Offset: 0x00000A6B
		public IEndpointInformationRetriever Create(Uri baseUrl, int timeout = 30000)
		{
			return new EndpointInformationRetriever(baseUrl, timeout, null);
		}
	}
}
