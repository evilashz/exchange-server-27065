using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000045 RID: 69
	internal class TransientDataProviderUnavailableException : TransientDALException
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000BCE3 File Offset: 0x00009EE3
		public TransientDataProviderUnavailableException(string message) : base(message)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000BCEC File Offset: 0x00009EEC
		public TransientDataProviderUnavailableException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
