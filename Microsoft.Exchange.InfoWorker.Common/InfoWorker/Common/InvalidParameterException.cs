using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class InvalidParameterException : LocalizedException
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public InvalidParameterException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002BF9 File Offset: 0x00000DF9
		public InvalidParameterException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
