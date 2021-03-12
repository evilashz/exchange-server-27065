using System;
using System.IO;
using System.Net;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x02000013 RID: 19
	internal class HolidayCalendarsService : IHolidayCalendarsService
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000295C File Offset: 0x00000B5C
		public void GetResource(WebRequest request, Action<Stream> responseProcessingDelegate)
		{
			using (WebResponse response = request.GetResponse())
			{
				responseProcessingDelegate(response.GetResponseStream());
			}
		}
	}
}
