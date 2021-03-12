using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200007D RID: 125
	internal static class ErrorHandler
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0000E68C File Offset: 0x0000C88C
		public static void SetErrorCodeIfNecessary(LocalizedException e, ErrorConstants error)
		{
			if (!(e is AvailabilityException) && !(e is AvailabilityInvalidParameterException))
			{
				e.ErrorCode = (int)error;
			}
		}
	}
}
