using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000096 RID: 150
	internal class FreeBusyDLLimitReachedException : AvailabilityException
	{
		// Token: 0x0600035F RID: 863 RVA: 0x0000E955 File Offset: 0x0000CB55
		public FreeBusyDLLimitReachedException(int allowedSize) : base(ErrorConstants.FreeBusyDLLimitReached, Strings.descFreeBusyDLLimitReached(allowedSize))
		{
		}
	}
}
