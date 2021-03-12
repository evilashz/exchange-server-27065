using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200009B RID: 155
	internal class IndividualMailboxLimitReachedException : AvailabilityException
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000E9BF File Offset: 0x0000CBBF
		public IndividualMailboxLimitReachedException(LocalizedString message, uint locationIdentifier) : base(ErrorConstants.IndividualMailboxLimitReached, message, locationIdentifier)
		{
		}
	}
}
