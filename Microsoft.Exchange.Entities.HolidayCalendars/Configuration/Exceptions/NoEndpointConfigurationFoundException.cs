using System;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions
{
	// Token: 0x02000011 RID: 17
	public class NoEndpointConfigurationFoundException : HolidayCalendarException
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000294F File Offset: 0x00000B4F
		public NoEndpointConfigurationFoundException(string message, params object[] args) : base(message, args)
		{
		}
	}
}
