using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000089 RID: 137
	internal class ResultSetTooBigException : AvailabilityException
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000E809 File Offset: 0x0000CA09
		public ResultSetTooBigException(int allowedSize, int actualSize) : base(ErrorConstants.ResultSetTooBig, Strings.descResultSetTooBig(allowedSize.ToString(), actualSize.ToString()))
		{
		}
	}
}
