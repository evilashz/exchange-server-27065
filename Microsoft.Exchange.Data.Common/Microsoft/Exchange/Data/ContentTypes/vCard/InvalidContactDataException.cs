using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.ContentTypes.vCard
{
	// Token: 0x020000D7 RID: 215
	[Serializable]
	public class InvalidContactDataException : ExchangeDataException
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x0002E95E File Offset: 0x0002CB5E
		public InvalidContactDataException(string message) : base(message)
		{
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002E967 File Offset: 0x0002CB67
		public InvalidContactDataException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002E971 File Offset: 0x0002CB71
		protected InvalidContactDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
