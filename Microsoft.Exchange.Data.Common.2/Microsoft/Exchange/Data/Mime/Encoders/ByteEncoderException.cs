using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Mime.Encoders
{
	// Token: 0x02000094 RID: 148
	[Serializable]
	public class ByteEncoderException : ExchangeDataException
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x000229E1 File Offset: 0x00020BE1
		public ByteEncoderException(string message) : base(message)
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000229EA File Offset: 0x00020BEA
		public ByteEncoderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000229F4 File Offset: 0x00020BF4
		protected ByteEncoderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
