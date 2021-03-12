using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002F1 RID: 753
	internal abstract class TranscodingException : OwaPermanentException
	{
		// Token: 0x06001C88 RID: 7304 RVA: 0x000A4B70 File Offset: 0x000A2D70
		public TranscodingException(string message, Exception innerException, object thisObj) : base(message, innerException, thisObj)
		{
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000A4B7B File Offset: 0x000A2D7B
		protected TranscodingException(string message, Exception innerException) : this(message, innerException, null)
		{
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000A4B86 File Offset: 0x000A2D86
		protected TranscodingException(string message) : base(message)
		{
		}
	}
}
