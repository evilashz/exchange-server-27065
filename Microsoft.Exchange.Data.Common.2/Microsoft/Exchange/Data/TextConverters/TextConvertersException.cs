using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	public class TextConvertersException : ExchangeDataException
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x0006F689 File Offset: 0x0006D889
		internal TextConvertersException() : base("internal text conversion error (document too complex)")
		{
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0006F696 File Offset: 0x0006D896
		public TextConvertersException(string message) : base(message)
		{
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0006F69F File Offset: 0x0006D89F
		public TextConvertersException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0006F6A9 File Offset: 0x0006D8A9
		protected TextConvertersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
