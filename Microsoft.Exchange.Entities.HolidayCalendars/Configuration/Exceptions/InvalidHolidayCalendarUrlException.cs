using System;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions
{
	// Token: 0x0200000F RID: 15
	public class InvalidHolidayCalendarUrlException : HolidayCalendarException
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000293B File Offset: 0x00000B3B
		public InvalidHolidayCalendarUrlException(string message, params object[] args) : base(message, args)
		{
		}
	}
}
