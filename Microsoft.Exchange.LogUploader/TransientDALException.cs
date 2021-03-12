using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000044 RID: 68
	internal class TransientDALException : MessageTracingException
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public TransientDALException(string message) : base(message)
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000BCD9 File Offset: 0x00009ED9
		public TransientDALException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
