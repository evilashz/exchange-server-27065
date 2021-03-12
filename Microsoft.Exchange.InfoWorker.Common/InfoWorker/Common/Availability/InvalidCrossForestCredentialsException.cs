using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000094 RID: 148
	internal class InvalidCrossForestCredentialsException : AvailabilityException
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0000E925 File Offset: 0x0000CB25
		public InvalidCrossForestCredentialsException(LocalizedString message) : base(ErrorConstants.InvalidCrossForestCredentials, message)
		{
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000E933 File Offset: 0x0000CB33
		public InvalidCrossForestCredentialsException(LocalizedString message, Exception innerException) : base(ErrorConstants.InvalidCrossForestCredentials, message, innerException)
		{
		}
	}
}
