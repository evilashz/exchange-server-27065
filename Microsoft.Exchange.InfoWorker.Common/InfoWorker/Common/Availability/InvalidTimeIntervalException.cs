using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200008C RID: 140
	internal class InvalidTimeIntervalException : AvailabilityInvalidParameterException
	{
		// Token: 0x06000350 RID: 848 RVA: 0x0000E871 File Offset: 0x0000CA71
		public InvalidTimeIntervalException(string propertyName) : base(ErrorConstants.InvalidTimeInterval, Strings.descInvalidTimeInterval(propertyName))
		{
		}
	}
}
