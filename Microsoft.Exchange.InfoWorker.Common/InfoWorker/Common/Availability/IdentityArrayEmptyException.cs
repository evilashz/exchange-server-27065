using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000081 RID: 129
	internal class IdentityArrayEmptyException : AvailabilityInvalidParameterException
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000E722 File Offset: 0x0000C922
		public IdentityArrayEmptyException() : base(ErrorConstants.IdentityArrayEmpty, Strings.descIdentityArrayEmpty)
		{
		}
	}
}
