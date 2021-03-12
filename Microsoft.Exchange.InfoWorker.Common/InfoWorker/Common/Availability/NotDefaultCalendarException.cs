using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000098 RID: 152
	internal class NotDefaultCalendarException : AvailabilityException
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000E97A File Offset: 0x0000CB7A
		public NotDefaultCalendarException() : base(ErrorConstants.NotDefaultCalendar, Strings.descNotDefaultCalendar)
		{
		}
	}
}
