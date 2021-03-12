using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.AttachmentBlob
{
	// Token: 0x02000011 RID: 17
	public class InvalidAttachmentBlobException : InvalidSerializedFormatException
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x0000C870 File Offset: 0x0000AA70
		public InvalidAttachmentBlobException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000C87A File Offset: 0x0000AA7A
		public InvalidAttachmentBlobException(string message) : base(message)
		{
		}
	}
}
