using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000010 RID: 16
	internal class IWTransientException : TransientException
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002C03 File Offset: 0x00000E03
		public IWTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C0C File Offset: 0x00000E0C
		public IWTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
