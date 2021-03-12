using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000082 RID: 130
	internal class IdentityArrayTooBigException : AvailabilityInvalidParameterException
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000E734 File Offset: 0x0000C934
		public IdentityArrayTooBigException(int allowedSize, int actualSize) : base(ErrorConstants.IdentityArrayTooBig, Strings.descIdentityArrayTooBig(allowedSize.ToString(), actualSize.ToString()))
		{
		}
	}
}
