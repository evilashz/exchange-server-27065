using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002F7 RID: 759
	internal sealed class TranscodingTimeoutException : TranscodingException
	{
		// Token: 0x06001C96 RID: 7318 RVA: 0x000A4BFF File Offset: 0x000A2DFF
		public TranscodingTimeoutException(string message, Exception innerException, object theObj) : base(message, innerException, theObj)
		{
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x000A4C0A File Offset: 0x000A2E0A
		public TranscodingTimeoutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000A4C14 File Offset: 0x000A2E14
		public TranscodingTimeoutException(string message) : base(message)
		{
		}
	}
}
