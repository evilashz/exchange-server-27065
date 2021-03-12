using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PropertyBlob
{
	// Token: 0x02000019 RID: 25
	public class InvalidBlobException : InvalidSerializedFormatException
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000F39A File Offset: 0x0000D59A
		public InvalidBlobException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		public InvalidBlobException(string message) : base(message)
		{
		}
	}
}
