using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200008F RID: 143
	internal class GetFolderRequestProcessingException : AvailabilityException
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public GetFolderRequestProcessingException(LocalizedString message) : base(ErrorConstants.GetFolderRequestProcessingFailed, message)
		{
		}
	}
}
