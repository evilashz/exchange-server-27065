using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000095 RID: 149
	internal class TimeoutExpiredException : AvailabilityException
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000E942 File Offset: 0x0000CB42
		public TimeoutExpiredException(string requestState) : base(ErrorConstants.TimeoutExpired, Strings.descTimeoutExpired(requestState))
		{
		}
	}
}
