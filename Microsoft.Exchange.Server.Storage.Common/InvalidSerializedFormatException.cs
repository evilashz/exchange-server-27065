using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000C9 RID: 201
	public class InvalidSerializedFormatException : Exception
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x0001D55A File Offset: 0x0001B75A
		public InvalidSerializedFormatException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001D564 File Offset: 0x0001B764
		public InvalidSerializedFormatException(string message) : base(message)
		{
		}
	}
}
