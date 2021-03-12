using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002F3 RID: 755
	internal sealed class TranscodingUnconvertibleFileException : TranscodingException
	{
		// Token: 0x06001C8C RID: 7308 RVA: 0x000A4B9A File Offset: 0x000A2D9A
		public TranscodingUnconvertibleFileException(string message, Exception innerException, object theObj) : base(message, innerException, theObj)
		{
		}
	}
}
