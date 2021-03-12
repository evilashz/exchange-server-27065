using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200007F RID: 127
	internal abstract class AvailabilityInvalidParameterException : InvalidParameterException
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000E6E1 File Offset: 0x0000C8E1
		public AvailabilityInvalidParameterException(ErrorConstants errorCode, LocalizedString localizedString) : base(localizedString)
		{
			base.ErrorCode = (int)errorCode;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E6F1 File Offset: 0x0000C8F1
		public AvailabilityInvalidParameterException(ErrorConstants errorCode, LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
			base.ErrorCode = (int)errorCode;
		}
	}
}
