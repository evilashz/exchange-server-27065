using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000099 RID: 153
	internal class InvalidAuthorizationContextException : AvailabilityException
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000E98C File Offset: 0x0000CB8C
		public InvalidAuthorizationContextException() : base(ErrorConstants.InvalidAuthorizationContext, Strings.descInvalidAuthorizationContext)
		{
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000E99E File Offset: 0x0000CB9E
		public InvalidAuthorizationContextException(Exception innerException) : base(ErrorConstants.InvalidAuthorizationContext, Strings.descInvalidAuthorizationContext, innerException)
		{
		}
	}
}
