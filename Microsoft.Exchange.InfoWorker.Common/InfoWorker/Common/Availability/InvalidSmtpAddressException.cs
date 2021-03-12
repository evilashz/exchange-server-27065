using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200009A RID: 154
	internal class InvalidSmtpAddressException : AvailabilityException
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000E9B1 File Offset: 0x0000CBB1
		public InvalidSmtpAddressException(LocalizedString message) : base(ErrorConstants.InvalidSmtpAddress, message)
		{
		}
	}
}
