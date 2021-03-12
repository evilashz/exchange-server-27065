using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200009C RID: 156
	internal class NoFreeBusyAccessException : AvailabilityException
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000E9CE File Offset: 0x0000CBCE
		public NoFreeBusyAccessException(uint locationIdentifier) : base(ErrorConstants.NoFreeBusyAccess, Strings.descNoFreeBusyAccess, locationIdentifier)
		{
		}
	}
}
