using System;
using System.IO;
using System.Net;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x02000012 RID: 18
	internal interface IHolidayCalendarsService
	{
		// Token: 0x06000031 RID: 49
		void GetResource(WebRequest request, Action<Stream> responseProcessingDelegate);
	}
}
