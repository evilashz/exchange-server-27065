using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public class TnefException : ExchangeDataException
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x000318C2 File Offset: 0x0002FAC2
		public TnefException(string message) : base(message)
		{
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000318CB File Offset: 0x0002FACB
		public TnefException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000318D5 File Offset: 0x0002FAD5
		protected TnefException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
