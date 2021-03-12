using System;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions
{
	// Token: 0x02000010 RID: 16
	public class NoSupportedHolidayCalendarCultureException : HolidayCalendarException
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002945 File Offset: 0x00000B45
		public NoSupportedHolidayCalendarCultureException(string message, params object[] args) : base(message, args)
		{
		}
	}
}
