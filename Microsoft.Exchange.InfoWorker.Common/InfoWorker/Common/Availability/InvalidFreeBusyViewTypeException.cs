using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000086 RID: 134
	internal class InvalidFreeBusyViewTypeException : AvailabilityInvalidParameterException
	{
		// Token: 0x06000346 RID: 838 RVA: 0x0000E7BA File Offset: 0x0000C9BA
		public InvalidFreeBusyViewTypeException() : base(ErrorConstants.InvalidFreeBusyViewType, Strings.descInvalidFreeBusyViewType)
		{
		}
	}
}
