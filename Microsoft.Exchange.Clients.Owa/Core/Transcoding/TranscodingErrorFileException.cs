using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002F6 RID: 758
	internal sealed class TranscodingErrorFileException : TranscodingException
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x000A4BE1 File Offset: 0x000A2DE1
		public TranscodingErrorFileException(string message, Exception innerException, object theObj) : base(message, innerException, theObj)
		{
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x000A4BEC File Offset: 0x000A2DEC
		public TranscodingErrorFileException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000A4BF6 File Offset: 0x000A2DF6
		public TranscodingErrorFileException(string message) : base(message)
		{
		}
	}
}
