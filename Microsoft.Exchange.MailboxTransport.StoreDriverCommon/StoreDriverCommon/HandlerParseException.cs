using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	internal class HandlerParseException : Exception
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00004D4C File Offset: 0x00002F4C
		public HandlerParseException(string message) : base(message)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004D55 File Offset: 0x00002F55
		public HandlerParseException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
