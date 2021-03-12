using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000039 RID: 57
	internal class MessageTracingException : Exception
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000BC4F File Offset: 0x00009E4F
		public MessageTracingException(string message) : base(message)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000BC58 File Offset: 0x00009E58
		public MessageTracingException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
