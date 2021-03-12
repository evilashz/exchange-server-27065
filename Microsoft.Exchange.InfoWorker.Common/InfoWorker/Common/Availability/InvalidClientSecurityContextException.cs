using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000085 RID: 133
	internal class InvalidClientSecurityContextException : AvailabilityInvalidParameterException
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0000E795 File Offset: 0x0000C995
		public InvalidClientSecurityContextException() : base(ErrorConstants.InvalidClientSecurityContext, Strings.descInvalidClientSecurityContext)
		{
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000E7A7 File Offset: 0x0000C9A7
		public InvalidClientSecurityContextException(Exception innerException) : base(ErrorConstants.InvalidClientSecurityContext, Strings.descInvalidClientSecurityContext, innerException)
		{
		}
	}
}
