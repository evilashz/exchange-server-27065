using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000043 RID: 67
	internal class PermanentDALException : Exception
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000BCBD File Offset: 0x00009EBD
		public PermanentDALException(string message) : base(message)
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000BCC6 File Offset: 0x00009EC6
		public PermanentDALException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
