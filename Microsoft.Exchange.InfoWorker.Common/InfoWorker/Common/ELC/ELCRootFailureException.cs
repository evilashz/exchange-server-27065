using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x02000192 RID: 402
	internal class ELCRootFailureException : IWTransientException
	{
		// Token: 0x06000ADA RID: 2778 RVA: 0x0002DF43 File Offset: 0x0002C143
		public ELCRootFailureException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002DF4D File Offset: 0x0002C14D
		public ELCRootFailureException(LocalizedString message) : base(message)
		{
		}
	}
}
