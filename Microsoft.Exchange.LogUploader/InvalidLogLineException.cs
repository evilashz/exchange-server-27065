using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200003F RID: 63
	internal class InvalidLogLineException : MessageTracingException
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000BC8F File Offset: 0x00009E8F
		public InvalidLogLineException(string message) : base(message)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000BC98 File Offset: 0x00009E98
		public InvalidLogLineException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
