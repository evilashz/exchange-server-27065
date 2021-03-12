using System;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x02000009 RID: 9
	internal interface IEndpointInformationRetrieverFactory
	{
		// Token: 0x06000024 RID: 36
		IEndpointInformationRetriever Create(Uri baseUrl, int timeout = 30000);
	}
}
