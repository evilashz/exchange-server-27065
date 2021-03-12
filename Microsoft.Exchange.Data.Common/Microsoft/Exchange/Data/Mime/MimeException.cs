using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class MimeException : ExchangeDataException
	{
		// Token: 0x060002DA RID: 730 RVA: 0x000101CA File Offset: 0x0000E3CA
		public MimeException(string message) : base(Strings.InternalMimeError + " " + message)
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000101E2 File Offset: 0x0000E3E2
		public MimeException(string message, Exception innerException) : base(Strings.InternalMimeError + " " + message, innerException)
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000101FB File Offset: 0x0000E3FB
		protected MimeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
