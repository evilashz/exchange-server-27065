using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002F8 RID: 760
	internal sealed class TranscodingCrashException : TranscodingException
	{
		// Token: 0x06001C99 RID: 7321 RVA: 0x000A4C1D File Offset: 0x000A2E1D
		public TranscodingCrashException(string message, Exception innerException, object theObj) : base(message, innerException, theObj)
		{
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000A4C28 File Offset: 0x000A2E28
		public TranscodingCrashException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000A4C32 File Offset: 0x000A2E32
		public TranscodingCrashException(string message) : base(message)
		{
		}
	}
}
