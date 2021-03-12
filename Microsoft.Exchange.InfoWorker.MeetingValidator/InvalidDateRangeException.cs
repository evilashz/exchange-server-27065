using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000037 RID: 55
	internal class InvalidDateRangeException : Exception
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000C4FA File Offset: 0x0000A6FA
		internal InvalidDateRangeException(ExDateTime rangeStart, ExDateTime rangeEnd) : base(string.Format("Invalid date range {0} to {1}", rangeStart, rangeEnd))
		{
		}
	}
}
