using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000080 RID: 128
	internal class RequestStreamTooBigException : AvailabilityInvalidParameterException
	{
		// Token: 0x0600033F RID: 831 RVA: 0x0000E702 File Offset: 0x0000C902
		public RequestStreamTooBigException(long allowedSize, long actualSize) : base(ErrorConstants.RequestStreamTooBig, Strings.descRequestStreamTooBig(allowedSize.ToString(), actualSize.ToString()))
		{
		}
	}
}
