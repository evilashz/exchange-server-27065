using System;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions
{
	// Token: 0x0200000C RID: 12
	public abstract class HolidayCalendarException : Exception
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000287D File Offset: 0x00000A7D
		protected HolidayCalendarException(string message, params object[] args) : base(string.Format(message, args))
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000288C File Offset: 0x00000A8C
		protected HolidayCalendarException(Exception innerException, string message, params object[] args) : base(string.Format(message, args), innerException)
		{
		}
	}
}
