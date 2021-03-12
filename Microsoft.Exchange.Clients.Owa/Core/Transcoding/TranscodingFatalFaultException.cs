using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002F4 RID: 756
	internal sealed class TranscodingFatalFaultException : TranscodingException
	{
		// Token: 0x06001C8D RID: 7309 RVA: 0x000A4BA5 File Offset: 0x000A2DA5
		public TranscodingFatalFaultException(string message, Exception innerException, object theObj) : base(message, innerException, theObj)
		{
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x000A4BB0 File Offset: 0x000A2DB0
		public TranscodingFatalFaultException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x000A4BBA File Offset: 0x000A2DBA
		public TranscodingFatalFaultException(string message) : base(message)
		{
		}
	}
}
