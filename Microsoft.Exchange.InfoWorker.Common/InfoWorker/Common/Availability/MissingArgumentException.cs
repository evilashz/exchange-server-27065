using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000088 RID: 136
	internal class MissingArgumentException : AvailabilityInvalidParameterException
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0000E7FB File Offset: 0x0000C9FB
		public MissingArgumentException(LocalizedString message) : base(ErrorConstants.MissingArgument, message)
		{
		}
	}
}
