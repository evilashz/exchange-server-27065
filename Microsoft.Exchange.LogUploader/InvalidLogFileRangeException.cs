using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200003E RID: 62
	internal class InvalidLogFileRangeException : MessageTracingException
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000BC86 File Offset: 0x00009E86
		public InvalidLogFileRangeException(string message) : base(message)
		{
		}
	}
}
