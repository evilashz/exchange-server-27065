using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x02000005 RID: 5
	internal class DateTimeFactory : IDateTimeFactory
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002370 File Offset: 0x00000570
		public ExDateTime GetUtcNow()
		{
			return ExDateTime.UtcNow;
		}
	}
}
