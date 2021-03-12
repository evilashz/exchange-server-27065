using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002F5 RID: 757
	internal sealed class TranscodingOverMaximumFileSizeException : TranscodingException
	{
		// Token: 0x06001C90 RID: 7312 RVA: 0x000A4BC3 File Offset: 0x000A2DC3
		public TranscodingOverMaximumFileSizeException(string message, Exception innerException, object theObj) : base(message, innerException, theObj)
		{
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x000A4BCE File Offset: 0x000A2DCE
		public TranscodingOverMaximumFileSizeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x000A4BD8 File Offset: 0x000A2DD8
		public TranscodingOverMaximumFileSizeException(string message) : base(message)
		{
		}
	}
}
