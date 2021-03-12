using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000083 RID: 131
	internal class TimeIntervalTooBigException : AvailabilityInvalidParameterException
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000E754 File Offset: 0x0000C954
		public TimeIntervalTooBigException(string propertyName, int allowedTimeSpanInDays, int actualTimeSpanInDays) : base(ErrorConstants.TimeIntervalTooBig, Strings.descTimeIntervalTooBig(propertyName, allowedTimeSpanInDays.ToString(), actualTimeSpanInDays.ToString()))
		{
		}
	}
}
