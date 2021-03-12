using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000084 RID: 132
	internal class InvalidMergedFreeBusyIntervalException : AvailabilityInvalidParameterException
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000E775 File Offset: 0x0000C975
		public InvalidMergedFreeBusyIntervalException(int minimumValue, int maximumValue) : base(ErrorConstants.InvalidMergedFreeBusyInterval, Strings.descInvalidMergedFreeBusyInterval(minimumValue.ToString(), maximumValue.ToString()))
		{
		}
	}
}
