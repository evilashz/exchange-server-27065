using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200018E RID: 398
	[Serializable]
	public class InvalidSerializedFormatException : Exception
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x000528F9 File Offset: 0x00050AF9
		public InvalidSerializedFormatException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00052903 File Offset: 0x00050B03
		public InvalidSerializedFormatException(string message) : base(message)
		{
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0005290C File Offset: 0x00050B0C
		public InvalidSerializedFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
