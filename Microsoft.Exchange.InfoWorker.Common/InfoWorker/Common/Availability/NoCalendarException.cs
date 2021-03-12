using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000097 RID: 151
	internal class NoCalendarException : AvailabilityException
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0000E968 File Offset: 0x0000CB68
		public NoCalendarException() : base(ErrorConstants.NoCalendar, Strings.descNoCalendar)
		{
		}
	}
}
