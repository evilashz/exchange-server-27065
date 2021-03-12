using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x02000004 RID: 4
	internal interface IDateTimeFactory
	{
		// Token: 0x06000011 RID: 17
		ExDateTime GetUtcNow();
	}
}
